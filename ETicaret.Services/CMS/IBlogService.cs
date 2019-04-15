using ETicaret.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ETicaret.Services.CMS
{
    public interface IBlogService
    {
        IQueryable<Blog> GetAllPosts();
        Blog GetPostById(int id);
        Blog GetPostBySlug(string id);
        void DeletePost(int id);
        void UpdatePost(Blog blog, HttpPostedFileBase picture);
        void CreatePost(Blog blog, HttpPostedFileBase picture);
    }
}
