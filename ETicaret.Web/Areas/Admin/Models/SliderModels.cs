using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Areas.Admin.Models
{
    public class SlideCreateEditModel
    {
        public int Id { get; set; }
        public string ResimId { get; set; }
        public HttpPostedFileBase Resim { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string ButonAdi { get; set; }
        public string ButonLink { get; set; }
    }

    public class SlideIndexModel
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string ResimYol { get; set; }
    }
}