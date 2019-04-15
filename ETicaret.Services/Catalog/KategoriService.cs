using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ETicaret.Data;
using ETicaret.Services.System;

namespace ETicaret.Services.Catalog
{
    public class KategoriService : IKategoriService
    {
        private AppDbContext _dbContext;
        private IUrlService _urlService;
        private IPictureService _pictureService;
        public KategoriService(AppDbContext dbContext, IUrlService urlService, IPictureService pictureService)
        {
            _dbContext = dbContext;
            _urlService = urlService;
            _pictureService = pictureService;
        }
        public IQueryable<Urun> GetProductsByCategoryIds(int[] ids)
        {
            return _dbContext.Kategori_Urun_Mapping.Where(f => ids.Contains(f.KategoriId)).Select(f => f.Urun);
        }

        public IQueryable<Kategori> GetHomePageCategories()
        {
            return _dbContext.Kategori.OrderBy(f => f.Sira).Where(f => f.ShowOnHomePage);
        }

        public IQueryable<Kategori> GetHeaderCategories()
        {
            return _dbContext.Kategori.Where(f => f.UstKategoriId == null || f.UstKategoriId == 0);
        }

        public Kategori GetCategoryBySlug(string id)
        {
            return _dbContext.Kategori.SingleOrDefault(f => f.Slug == id);
        }

        public IQueryable<Urun> GetProductsByCategoryId(int id)
        {
            return _dbContext.Kategori_Urun_Mapping.Where(f => f.KategoriId == id).Select(f => f.Urun);
        }

        public void CreateCategory(Kategori kategori, HttpPostedFileBase picture)
        {
            var kat = new Kategori();
            kat.Aciklama = kategori.Aciklama;
            kat.Adi = kategori.Adi;
            kat.KisaAciklama = kategori.KisaAciklama;
            kat.KullaniciId = kategori.KullaniciId;
            kat.ShowOnHomePage = kategori.ShowOnHomePage;
            kat.Sira = kategori.Sira;
            kat.Slug = _urlService.GenerateSlug(kategori.Adi);
            if (picture != null && picture.ContentLength > 0)
            {
                kat.ResimId = _pictureService.UploadPicture(picture, kategori.Slug).Id;
            }
            kat.UstKategoriId = kategori.UstKategoriId;
            kat.OlusturulmaTarihi = kategori.OlusturulmaTarihi;
            _dbContext.Kategori.Add(kat);
            _dbContext.SaveChanges();
        }

        public void UpdateCategory(Kategori kategori, HttpPostedFileBase picture)
        {
            var kat = GetCategoryById(kategori.Id);
            kat.Aciklama = kategori.Aciklama;
            kat.Adi = kategori.Adi;
            kat.KisaAciklama = kategori.KisaAciklama;
            kat.KullaniciId = kategori.KullaniciId;
            kat.ShowOnHomePage = kategori.ShowOnHomePage;
            kat.Sira = kategori.Sira;
            kat.Slug = _urlService.GenerateSlug(kategori.Adi);
            if (picture != null && picture.ContentLength > 0)
            {
                kat.ResimId = _pictureService.UploadPicture(picture, kategori.Slug).Id;
            }
            kat.UstKategoriId = kategori.UstKategoriId;
            _dbContext.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var kategori = GetCategoryById(id);
            if (kategori != null)
            {
                _dbContext.Kategori.Remove(kategori);
                _dbContext.SaveChanges();
            }
        }

        public IQueryable<Kategori> GetAll()
        {
            return _dbContext.Kategori.OrderBy(f => f.Sira);
        }

        public Kategori GetCategoryById(int id)
        {
            return _dbContext.Kategori.SingleOrDefault(f => f.Id == id);
        }
    }
}
