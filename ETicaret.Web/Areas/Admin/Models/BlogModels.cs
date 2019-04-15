using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Areas.Admin.Models
{
    public class BlogIndexModel
    {
        public int Id { get; set; }
        [Display(Name = "İsim")]
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreatedOn { get; set; }
    }
    public class BlogEditCreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Short { get; set; }
        public string Full { get; set; }
        public string Tags { get; set; }
        public string PictureUrl { get; set; }
        public DateTime? CreatedOn { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }
}