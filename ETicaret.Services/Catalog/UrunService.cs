using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ETicaret.Data;
using ETicaret.Services.System;
using System.Data.Entity;

namespace ETicaret.Services.Catalog
{
    public class UrunService : IUrunService
    {
        private AppDbContext _dbContext;
        private IKategoriService _kategoriService;
        private IPictureService _pictureService;
        private IUrlService _urlService;
        public UrunService(AppDbContext dbContext,
            IKategoriService kategoriService, 
            IPictureService pictureService,
            IUrlService urlService)
        {
            _dbContext = dbContext;
            _kategoriService = kategoriService;
            _pictureService = pictureService;
            _urlService = urlService;
        }
        public void DeleteProduct(int id)
        {
            var urun = _dbContext.Urun.FirstOrDefault(f => f.Id == id);
            urun.Deleted = true;
            _dbContext.SaveChanges();
        }

        public IQueryable<Urun> GetAllProducts()
        {
            return _dbContext.Urun.Where(f => f.Deleted != true);
        }

        public IQueryable<Urun> GetFeaturedProducts()
        {
            return _dbContext.Urun.Where(f => f.ShowOnHomePage && f.Visibility && f.Deleted != true);
        }

        public Urun GetProductById(int id)
        {
            return _dbContext.Urun.SingleOrDefault(f => f.Id == id && f.Visibility && f.Deleted != true);
        }

        public Urun GetProductBySlug(string slug)
        {
            return _dbContext.Urun.SingleOrDefault(f => f.Slug == slug && f.Visibility && f.Deleted != true);
        }

        public IQueryable<Urun> GetProductsByCategoryIds(int[] ids)
        {
            return _kategoriService.GetProductsByCategoryIds(ids);
        }

        public IQueryable<Urun> GetProductsByIds(int[] ids)
        {
            return _dbContext.Urun.Where(f => ids.Contains(f.Id) && f.Visibility && f.Deleted != true);
        }

        public IQueryable<Urun> GetRelatedProductsById(int id)
        {
            var catId = _dbContext.Urun.SingleOrDefault(f => f.Id == id).Kategori_Urun_Mapping.Select(f => f.KategoriId);
            return _dbContext.Kategori_Urun_Mapping.Where(f => catId.Contains(f.KategoriId) && f.OzelUrun && f.UrunId != id).OrderBy(f => f.Sira).Select(f => f.Urun).Where(f => f.Deleted != true && f.Visibility);
        }

        public IQueryable<Urun> GetSpecialProducts()
        {
            return _dbContext.Urun.Where(f => f.OzelFiyatBaslangicTarihi < DateTime.Now && f.OzelFiyatBitisTarihi > DateTime.Now && f.Visibility && f.Deleted != true);
        }

        public void InsertProduct(Urun urun, HttpPostedFileBase anaResim, IEnumerable<HttpPostedFileBase> galeri, string path, int[] categories)
        {
            try
            {
                foreach (var item in categories)
                {
                    urun.Kategori_Urun_Mapping.Add(new Kategori_Urun_Mapping { KategoriId = item, OzelUrun = false, Sira = urun.Sira });
                }
                if (anaResim != null && anaResim.ContentLength > 0)
                {
                    urun.UrunResim.Add(new UrunResim { ResimId = _pictureService.UploadPicture(anaResim, path).Id, Sira = 0 });
                }
                int i = 1;
                foreach (var item in galeri)
                {
                    urun.UrunResim.Add(new UrunResim { ResimId = _pictureService.UploadPicture(anaResim, path).Id, Sira = i });
                    i++;
                }
                urun.Slug = _urlService.GenerateSlug(urun.UrunAdi);
                _dbContext.Urun.Add(urun);
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string error = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    error = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                       error += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(error);
            }
        }

        public void UpdateProduct(Urun urun, HttpPostedFileBase anaResim, IEnumerable<HttpPostedFileBase> galeri, string path, int[] categories)
        {
            try
            {
                _dbContext.Kategori_Urun_Mapping.RemoveRange(_dbContext.Kategori_Urun_Mapping.Where(f => f.UrunId == urun.Id));
                _dbContext.UrunResim.RemoveRange(_dbContext.UrunResim.Where(f => f.UrunId == urun.Id));
                _dbContext.SaveChanges();
                foreach (var item in categories)
                {
                    _dbContext.Kategori_Urun_Mapping.Add(new Kategori_Urun_Mapping { KategoriId = item, OzelUrun = false, Sira = urun.Sira, UrunId = urun.Id });
                }
                if (anaResim != null && anaResim.ContentLength > 0)
                {
                    _dbContext.UrunResim.Add(new UrunResim { ResimId = _pictureService.UploadPicture(anaResim, path).Id, Sira = 0, UrunId = urun.Id });
                }
                int i = 1;
                foreach (var item in galeri)
                {
                    _dbContext.UrunResim.Add(new UrunResim { ResimId = _pictureService.UploadPicture(item, path).Id, Sira = i, UrunId = urun.Id });
                    i++;
                }
                urun.Slug = _urlService.GenerateSlug(urun.UrunAdi);
                _dbContext.Entry(urun).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string error = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    error = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        error += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                             ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new Exception(error);
            }
        }
    }
}
