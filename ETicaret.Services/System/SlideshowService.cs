using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ETicaret.Data;

namespace ETicaret.Services.System
{
    public class SlideshowService : ISlideshowService
    {
        private AppDbContext _dbContext;
        private IPictureService _pictureService;
        public SlideshowService(AppDbContext dbContext, IPictureService pictureService)
        {
            _dbContext = dbContext;
            _pictureService = pictureService;
        }

        public void Create(Slideshow slide, HttpPostedFileBase picture, string path = "~/")
        {
            var sli = new Slideshow();
            if (picture != null && picture.ContentLength > 0)
            {
                sli.ResimId = _pictureService.UploadPicture(picture, path).Id;
            }
            sli.Aciklama = slide.Aciklama;
            sli.Baslik = slide.Baslik;
            sli.ButonAdi = slide.ButonAdi;
            sli.ButonLink = slide.ButonLink;
            _dbContext.Slideshow.Add(sli);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var slide = _dbContext.Slideshow.Find(id);
            if (slide != null)
            {
                _dbContext.Slideshow.Remove(slide);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<Slideshow> GetAllSlides()
        {
            return _dbContext.Slideshow;
        }

        public IQueryable<Slideshow> GetHomePageSlides()
        {
            return _dbContext.Slideshow;
        }

        public Slideshow GetSlideById(int id)
        {
            return _dbContext.Slideshow.SingleOrDefault(f => f.Id == id);
        }

        public void Update(Slideshow slide, HttpPostedFileBase picture, string path = "~/")
        {
            var sli = _dbContext.Slideshow.Find(slide.Id);
            if (picture != null && picture.ContentLength > 0)
            {
                sli.ResimId = _pictureService.UploadPicture(picture, path).Id;
            }
            sli.Aciklama = slide.Aciklama;
            sli.Baslik = slide.Baslik;
            sli.ButonAdi = slide.ButonAdi;
            sli.ButonLink = slide.ButonLink;
            _dbContext.SaveChanges();
        }
    }
}
