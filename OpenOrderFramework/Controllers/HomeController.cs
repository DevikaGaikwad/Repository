using System.Web.Mvc;

namespace OpenOrderFramework.Controllers {
    public class HomeController : Controller {
        public ActionResult Index()
        {
            if (User.IsInRole("Vendor"))
            {
                // Vendor vendor = db.Vendors.Select(User.Identity.ToString());
                return RedirectToAction("Index", "Vendors");
            }
            return View();
        }

        
        public ActionResult About() {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
