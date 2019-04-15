using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaret.Web.Models
{
    public class HeaderModel : ModelBase
    {
        public HeaderModel()
        {
            SocialMedia = new Dictionary<string, string>();
        }
        public string Logo { get; set; }
        public int LogoWidth { get; set; }
        public int LogoHeight { get; set; }
        public Dictionary<string, string> SocialMedia { get; set; }
        public string CouponText { get; set; }
        public string CouponLink { get; set; }
        public List<HeaderCategoriesModel> Categories { get; set; }
    }
    public class HeaderCategoriesModel
    {
        public string Baslik { get; set; }
        public string Link { get; set; }
    }
    public class FooterModel : ModelBase
    {
        public FooterModel()
        {

        }
    }
}