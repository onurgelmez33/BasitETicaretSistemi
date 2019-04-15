using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.System
{
    public class RouteService : IRouteService
    {
        AppDbContext db = new AppDbContext();
        public string UrlKontrolEt(string id)
        {
            if (db.Urun.Any(f => f.Slug == id))
            {
                return "Products";
            }
            else if(db.Kategori.Any(f => f.Slug == id))
            {
                return "Categories";
            }
            else if (db.Blog.Any(f => f.Slug == id))
            {
                return "Blog";
            }
            else
            {
                return "Error";
            }
        }
        
    }
}
