using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ETicaret.Data;

namespace ETicaret.Services.System
{
    public class PictureService : IPictureService
    {
        AppDbContext db = new AppDbContext();

        public Resim GetPictureById(int id)
        {
            if (id == 0)
            {
                return new Resim { DosyaYol = "/Content/noimage.png" };
            }
            return db.Resim.FirstOrDefault(f => f.Id == id);
        }

        public IQueryable<Resim> GetPicturesByIds(int[] ids)
        {
            return db.Resim.Where(f => ids.Contains(f.Id));
        }

        public Resim UploadPicture(HttpPostedFileBase picture, string path)
        {
            if (picture != null && picture.ContentLength > 0)
            {
                var fileName = Path.GetFileName(picture.FileName);
                var filePath = Path.Combine(path, fileName);
                picture.SaveAs(filePath);
                Resim resim = new Resim();
                resim.DosyaYol = "/uploads/" + fileName;
                resim.KullaniciId = 4;
                resim.OlusturulmaTarihi = DateTime.Now;
                db.Resim.Add(resim);
                db.SaveChanges();
                return resim;
            }
            else
            {
                throw new Exception("Resim Bos!");
            }
        }
    }
}
