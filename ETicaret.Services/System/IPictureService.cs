using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ETicaret.Services.System
{
    public interface IPictureService
    {
        IQueryable<Resim> GetPicturesByIds(int[] ids);
        Resim GetPictureById(int id);
        Resim UploadPicture(HttpPostedFileBase picture, string path);
    }
}
