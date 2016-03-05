using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OpenOrderFramework.Configuration;
using OpenOrderFramework.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenOrderFramework.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        AppConfigurations appConfig = new AppConfigurations();

        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            var previousOrder = storeDB.Orders.FirstOrDefault(x => x.Username == User.Identity.Name);

            if (previousOrder != null)
                return View(previousOrder);
            else
                return View();
        }

        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public async Task<ActionResult> AddressAndPayment(FormCollection values)
        {
            string result =  values[2];
            
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                    order.Username = User.Identity.Name;
                    order.Email = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    var currentUserId = User.Identity.GetUserId();

                    if (order.SaveInfo && !order.Username.Equals("guest@guest.com"))
                    {
                        
                        var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                        var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
                        var ctx = store.Context;
                        var currentUser = manager.FindById(User.Identity.GetUserId());                       
                        currentUser.FirstName = order.FirstName;

                        await ctx.SaveChangesAsync();

                        await storeDB.SaveChangesAsync();
                    }
                    

                    //Save Order
                    storeDB.Orders.Add(order);
                    await storeDB.SaveChangesAsync();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    order = cart.CreateOrder(order);



                    CheckoutController.SendOrderMessage(order.FirstName, "New Order: " + order.OrderId,order.ToString(order), appConfig.OrderEmail);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                return View(order);
            }
            //{
            //    //Invalid - redisplay with errors
            //    return View(order);
            //}
        }

        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }

        private static RestResponse SendOrderMessage(String toName, String subject, String body, String destination)
        {
            RestClient client = new RestClient();
            //fix this we have this up top too
            AppConfigurations appConfig = new AppConfigurations();
            client.BaseUrl = "https://api.mailgun.net/v2";
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              appConfig.EmailApiKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                appConfig.DomainForApiKey, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", appConfig.FromName + " <" + appConfig.FromEmail + ">");
            request.AddParameter("to", toName + " <" + destination + ">");
            request.AddParameter("subject", subject);
            request.AddParameter("html", body);
            request.Method = Method.POST;
            IRestResponse executor = client.Execute(request);
            return executor as RestResponse;
        }
    }
}