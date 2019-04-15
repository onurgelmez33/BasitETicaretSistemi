using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Models
{
    public class BlogDetailsModel
    {
        public string Name { get; set; }
        public string Short { get; set; }
        public string Full { get; set; }
        public string Image { get; set; }
        public string CreatedOn { get; set; }
        public string Tags { get; set; }
        public string Slug { get; set; }
    }
}