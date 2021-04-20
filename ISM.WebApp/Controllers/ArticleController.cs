using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    [Authorize(Roles = "Admin,Staff,Degree,Mobility")]
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
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
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
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                return View("Views/Admin/Article/Article.cshtml", view);
            }
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                return View("Views/Degree/Article/Article.cshtml", view);
            }
            return View();
        }

        public IActionResult List(string type)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            ArticleListViewModel view = new ArticleListViewModel();
            view.articles = articleDAO.getArticleByType(type);
            view.type = type;
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                return View("Views/Admin/Article/ArticleList.cshtml", view);
            }
            else if (sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                return View("Views/Degree/Article/ArticleList.cshtml", view);
            }
            return View();
        }

        [Authorize(Roles = "Admin,Staff")]
        public IActionResult Edit(int article_id, string title, string type, IFormFile file)
        {
            string fileName = "article_"+article_id + ".pdf";
            if (file != null)
            {                
                string article = Path.Combine(hostingEnvironment.WebRootPath, "Article");
                string subfolderPath = Path.Combine(article, type);
                string filePath= Path.Combine(subfolderPath, fileName);
                FileStream stream = new FileStream(filePath, FileMode.Create);              
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

        [Authorize(Roles = "Admin,Staff")]
        public IActionResult Create(string title, string type, IFormFile file)
        {
            int inserted_id = articleDAO.CreateArticle(type, title);
            string fileName = "article_" + inserted_id + ".pdf";
            if (file != null && inserted_id!=0)
            {
                string article = Path.Combine(hostingEnvironment.WebRootPath, "Article");
                string subfolderPath = Path.Combine(article, type);
                string filePath = Path.Combine(subfolderPath, fileName);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();
                return Json(new { status = "success", message = "Edit successfully" });
            }           
                return Json(new { status = "error", message = "Edit Failed" });                   
        }

        [Authorize(Roles = "Admin,Staff")]
        public bool Delete(int article_id, string type, string file_name)
        {
            bool result = articleDAO.DeleteArticle(article_id);
            string article = Path.Combine(hostingEnvironment.WebRootPath, "Article");
            string subfolderPath = Path.Combine(article, type);
            string filePath = Path.Combine(subfolderPath, file_name);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return result;
        }
    }
}
