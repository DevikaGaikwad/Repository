using OpenOrderFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenOrderFramework.ViewModels
{
    public class ItemCuisineViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Cuisine> Cuisines { get; set; }
    }
}