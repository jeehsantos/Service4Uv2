using ServiceApp.Data.Data.Repository;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServiceApp.Data.Data.Repository
{
    internal class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Article article)
        {
            var objArt = _context.Articles.FirstOrDefault(s => s.ArticleID == article.ArticleID);
            objArt.Name = article.Name;
            objArt.Description = article.Description;
            objArt.UrlImage = article.UrlImage;
            objArt.CategoryID = article.CategoryID;

            _context.SaveChanges();
        }
    }
}
