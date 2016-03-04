using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace OpenOrderFramework.Models
{
    public class FoodCourt
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

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

        [DisplayName("Food Court Picture URL")]
        [StringLength(1024)]
        public string FoodCourtPictureUrl { get; set; }

    }
}