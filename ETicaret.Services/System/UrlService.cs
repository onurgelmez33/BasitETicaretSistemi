using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ETicaret.Services.System
{
    public class UrlService : IUrlService
    {
        public string GenerateSlug(string name)
        {
            if (name != null)
            {
                name = name.Replace("ş", "s");
                name = name.Replace("Ş", "s");
                name = name.Replace("İ", "i");
                name = name.Replace("I", "i");
                name = name.Replace("ı", "i");
                name = name.Replace("ö", "o");
                name = name.Replace("Ö", "o");
                name = name.Replace("ü", "u");
                name = name.Replace("Ü", "u");
                name = name.Replace("Ç", "c");
                name = name.Replace("ç", "c");
                name = name.Replace("ğ", "g");
                name = name.Replace("Ğ", "g");
                name = name.Replace(" ", "-");
                name = name.Replace("---", "-");
                name = name.Replace("?", "");
                name = name.Replace("/", "");
                name = name.Replace(".", "");
                name = name.Replace("'", "");
                name = name.Replace("#", "");
                name = name.Replace("%", "");
                name = name.Replace("&", "");
                name = name.Replace("*", "");
                name = name.Replace("!", "");
                name = name.Replace("@", "");
                name = name.Replace("+", "");
                name = name.ToLower();
                name = name.Trim();
                // tüm harfleri küçült
                string encodedUrl = (name ?? "").ToLower();
                // & ile " " yer değiştirme
                encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");
                // " " karakterlerini silme
                encodedUrl = encodedUrl.Replace("'", "");
                // geçersiz karakterleri sil
                encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");
                // tekrar edenleri sil
                encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");
                // karakterlerin arasına tire koy
                encodedUrl = encodedUrl.Trim('-');
                return encodedUrl;
            }
            else
            {
                return "";
            }
        }
    }
}
