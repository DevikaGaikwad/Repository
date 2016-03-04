using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenOrderFramework.Models
{
    public class Cuisine
    {
        [Key]
        [DisplayName("Cuisine ID")]
        public int ID { get; set; }

        [DisplayName("Cuisine")]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}