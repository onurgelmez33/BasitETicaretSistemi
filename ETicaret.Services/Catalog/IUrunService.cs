using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ETicaret.Services.Catalog
{
    public interface IUrunService
    {
        IQueryable<Urun> GetFeaturedProducts();
        IQueryable<Urun> GetSpecialProducts();
        IQueryable<Urun> GetProductsByIds(int[] ids);
        IQueryable<Urun> GetProductsByCategoryIds(int[] ids);
        Urun GetProductById(int id);
        Urun GetProductBySlug(string slug);
        IQueryable<Urun> GetRelatedProductsById(int id);
        void InsertProduct(Urun urun, HttpPostedFileBase anaResim, IEnumerable<HttpPostedFileBase> galeri, string path, int[] categories);
        void UpdateProduct(Urun urun, HttpPostedFileBase anaResim, IEnumerable<HttpPostedFileBase> galeri, string path, int[] categories);
        void DeleteProduct(int id);
        IQueryable<Urun> GetAllProducts();
    }
}
