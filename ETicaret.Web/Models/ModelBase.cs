using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Models
{
    public class ModelBase
    {
        public ModelBase()
        {
            CustomProperties = new Dictionary<string, dynamic>();
        }
        public Dictionary<string, dynamic> CustomProperties { get; set; }
    }
}