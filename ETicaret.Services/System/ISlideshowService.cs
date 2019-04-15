using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ETicaret.Services.System
{
    public interface ISlideshowService
    {
        IQueryable<Slideshow> GetHomePageSlides();
        IQueryable<Slideshow> GetAllSlides();
        Slideshow GetSlideById(int id);
        void Create(Slideshow slide, HttpPostedFileBase picture, string path = "~/");
        void Update(Slideshow slide, HttpPostedFileBase picture, string path = "~/");
        void Delete(int id);
    }
}
