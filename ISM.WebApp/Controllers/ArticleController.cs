using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISM.WebApp.Controllers
{
    public class ArticleController : Controller
    {
        public ArticleDAO articleDAO;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ArticleController(ArticleDAO articleDAO, IWebHostEnvironment hostingEnvironment)
        {
            this.articleDAO = articleDAO;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index(int? id=null, string type=null)
        {
            ArticleIndexViewModel view = new ArticleIndexViewModel();
            if (id != null)
            {
                view.article = articleDAO.getArticleById((int)id);
            }
            else
            {
                view.article = articleDAO.getArticleByType(type)[0];
            }
            if(view.article.type.Equals("Insurance Instruction") || view.article.type.Equals("Jobs 101") || view.article.type.Equals("Tips and samples for CV")
               || view.article.type.Equals("Tips to prepare for job interview"))
            {
                view.uniqueArticle = false;
            }
            else
            {
                view.uniqueArticle = true;
            }
            return View("Views/Admin/Article/Article.cshtml", view);
        }

        public IActionResult List(string type)
        {
            ArticleListViewModel view = new ArticleListViewModel();
            view.articles = articleDAO.getArticleByType(type);
            view.type = type;
            return View("Views/Admin/Article/ArticleList.cshtml", view);
        }

        public IActionResult Edit(int article_id, string title, string type, IFormFile file, string old_file_name)
        {
            string fileName = "";
            old_file_name = old_file_name == null ? " " : old_file_name;
            if (file != null)
            {
                fileName = file.FileName;                
                string article = Path.Combine(hostingEnvironment.WebRootPath, "Article");
                string subfolderPath = Path.Combine(article, type);
                string oldFilePath = Path.Combine(subfolderPath, old_file_name);
                string newfilePath= Path.Combine(subfolderPath, fileName);
                FileStream stream = new FileStream(newfilePath, FileMode.Create);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
                file.CopyTo(stream);
                stream.Close();
            }
            bool result = articleDAO.EditArticle(article_id, title, fileName);
            if (result == false)
            {
                return Json(new { status = "error", message = "Edit Failed" });
            }
            return Json(new { status = "success", message = "Edit successfully" });
        }
    }
}
