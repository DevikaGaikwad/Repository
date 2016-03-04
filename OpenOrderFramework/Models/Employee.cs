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
    public class Employee
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [Key]
        [DisplayName("Employee ID")]
        public int ID { get; set; }

        [DisplayName("First Name")]
        [StringLength(100)]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(100)]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Column(TypeName = "image")]
        [MaxLength]
        public byte[] ProfilePicture { get; set; }


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
                    ProfilePicture = target.ToArray();
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error(ex.StackTrace);
                }
            }
        }

        [DisplayName("Profile Picture URL")]
        [StringLength(1024)]
        public string ProfilePictureUrl { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}