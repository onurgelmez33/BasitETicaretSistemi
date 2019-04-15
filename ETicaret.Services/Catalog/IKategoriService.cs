using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ETicaret.Services.Catalog
{
    public interface IKategoriService
    {
        IQueryable<Urun> GetProductsByCategoryIds(int[] ids);
        IQueryable<Urun> GetProductsByCategoryId(int id);
        IQueryable<Kategori> GetHomePageCategories();
        IQueryable<Kategori> GetHeaderCategories();
        Kategori GetCategoryBySlug(string id);
        Kategori GetCategoryById(int id);
        void CreateCategory(Kategori kategori, HttpPostedFileBase picture);
        void UpdateCategory(Kategori kategori, HttpPostedFileBase picture);
        void DeleteCategory(int id);
        IQueryable<Kategori> GetAll();
    }
}
