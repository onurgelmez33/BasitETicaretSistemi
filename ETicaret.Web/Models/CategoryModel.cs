using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public string Short { get; set; }
        public string Image { get; set; }
        public IEnumerable<ProductModel> Products { get; set; }
    }
}