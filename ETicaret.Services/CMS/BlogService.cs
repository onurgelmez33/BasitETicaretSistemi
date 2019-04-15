using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ETicaret.Data;
using ETicaret.Services.System;

namespace ETicaret.Services.CMS
{
    public class BlogService : IBlogService
    {
        private IUrlService _urlService;
        private AppDbContext _dbContext;
        private IPictureService _pictureService;
        public BlogService(IUrlService urlService, AppDbContext dbContext, IPictureService pictureService)
        {
            _urlService = urlService;
            _dbContext = dbContext;
            _pictureService = pictureService;
        }

        public void CreatePost(Blog blog, HttpPostedFileBase picture)
        {
            var post = new Blog();
            post.Aciklama = blog.Aciklama;
            post.Adi = blog.Adi;
            post.KisaAciklama = blog.KisaAciklama;
            post.Tags = blog.Tags;
            post.Slug = _urlService.GenerateSlug(post.Adi);
            post.OlusturulmaTarihi = DateTime.Now;
            if (picture != null && picture.ContentLength > 0)
            {
                post.ResimId = _pictureService.UploadPicture(picture, blog.Slug).Id;
            }
            _dbContext.Blog.Add(post);
            _dbContext.SaveChanges();
        }

        public void DeletePost(int id)
        {
            _dbContext.Blog.Remove(_dbContext.Blog.Find(id));
            _dbContext.SaveChanges();
        }

        public IQueryable<Blog> GetAllPosts()
        {
            return _dbContext.Blog;
        }

        public Blog GetPostById(int id)
        {
            return _dbContext.Blog.SingleOrDefault(f => f.Id == id);
        }

        public Blog GetPostBySlug(string id)
        {
            return _dbContext.Blog.SingleOrDefault(f => f.Slug == id);
        }

        public void UpdatePost(Blog blog, HttpPostedFileBase picture)
        {
            var post = GetPostById(blog.Id);
            post.Aciklama = blog.Aciklama;
            post.Adi = blog.Adi;
            post.KisaAciklama = blog.KisaAciklama;
            post.Tags = blog.Tags;
            post.Slug = _urlService.GenerateSlug(post.Adi);
            if (picture != null && picture.ContentLength > 0)
            {
                post.ResimId = _pictureService.UploadPicture(picture, blog.Slug).Id;
            }
            _dbContext.SaveChanges();
        }
    }
}
