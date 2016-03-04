using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenOrderFramework.Models
{
    public class ItemAvailability
    {
        [Key]
        [DisplayName("ID")]
        public int ID { get; set; }

        [DisplayName("Day")]
        [Required(ErrorMessage = "Day is required")]
        public string Day { get; set; }

        public virtual Item Item { get; set; }

    }
}