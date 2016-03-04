using OpenOrderFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenOrderFramework.Controllers
{
    public class StoreController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();

        //
        // GET: /Store/

        public ActionResult Index()
        {
            var Categories = storeDB.Categories.ToList();

            return View(Categories);
        }

        //
        // GET: /Store/Browse?genre=Disco

        public ActionResult Browse(string category)
        {
            // Retrieve Genre and its Associated Items from database
            var catagorieModel = storeDB.Categories.Include("Items")
                .Single(g => g.Name == category);

            return View(catagorieModel);
        }

        //
        // GET: /Store/Details/5
        public ActionResult Details(int id)
        {
            var item = storeDB.Items.Find(id);

            return View(item);
        }

        //
        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var categories = storeDB.Categories.ToList();

            return PartialView(categories);
        }
    }
}