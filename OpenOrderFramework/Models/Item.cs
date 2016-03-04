using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Web.Mvc.Html;

namespace OpenOrderFramework.Models
{
    [Bind(Exclude = "ID")]
    public class Item
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();


        [Key]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "An Item Name is required")]
        [StringLength(160)]
        public string Name { get; set; }

        [DisplayName("Description")]
        [StringLength(500)]
        public string Description { get; set; }

        [DisplayName("Veg")]
        [Required(ErrorMessage = "Veg/Non Veg is required")]
        public bool IsVeg { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999.99, ErrorMessage = "Price must be between 0.01 and 999.99")]
        public decimal Price { get; set; }

        [Column(TypeName = "image")]
        [MaxLength]
        public byte[] InternalImage { get; set; }

        [Display(Name = "Local file")]
        [NotMapped]
        public HttpPostedFileBase File
        {
            get
            {
                return null;
            }

            set
            {
                try
                {
                    MemoryStream target = new MemoryStream();

                    if (value.InputStream == null)
                        return;

                    value.InputStream.CopyTo(target);
                    InternalImage = target.ToArray();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
        }

        [DisplayName("Item Picture URL")]
        [StringLength(1024)]
        public string ItemPictureUrl { get; set; }

        [DisplayName("Quantity")]
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [DisplayName("Calories")]
        public int Calories { get; set; }

        [Display(Name = "Preperation Time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{hh:MM}", ApplyFormatInEditMode = true)]
        public DateTime PreperationTime { get; set; }

        public virtual Vendor Vendor { get; set; }

        public virtual Category Category { get; set; }

        [Required(ErrorMessage = "Cuisine is required")]
        public virtual Cuisine Cuisine { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}