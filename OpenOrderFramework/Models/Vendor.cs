using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenOrderFramework.Models
{
    public class Vendor
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public virtual FoodCourt FoodCourt { get; set; }
    }
}