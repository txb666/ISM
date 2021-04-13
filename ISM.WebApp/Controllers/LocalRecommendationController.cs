using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    public class LocalRecommendationController : Controller
    {
        public LocalRecommendationDAO localRecommendationDAO;
        private readonly IWebHostEnvironment hostingEnvironment;
        public LocalRecommendationController(LocalRecommendationDAO localRecommendationDAO, IWebHostEnvironment hostingEnvironment)
        {
            this.localRecommendationDAO = localRecommendationDAO;
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            LocalRecommendation localRecommendation = localRecommendationDAO.GetLocalRecommendationById(id);
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                return View("Views/Admin/LocalRecommendation/LocalRecommendation.cshtml", localRecommendation);
            }
            else if(sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility"))
            {
                return View("Views/Degree/LocalRecommendation/LocalRecommendation.cshtml", localRecommendation);
            }
            return View();
        }

        public IActionResult Edit(int local_recommendation_id, string title, IFormFile file)
        {
            string file_name = "local_recommendation_" + local_recommendation_id + ".pdf";
            if (file != null)
            {
                string article = Path.Combine(hostingEnvironment.WebRootPath, "Article");
                string subfolderPath = Path.Combine(article, "Local Recommendation");
                string filePath = Path.Combine(subfolderPath, file_name);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();
            }
            bool result = localRecommendationDAO.editLocalRecommendation(local_recommendation_id, title, file_name);
            if (result == false)
            {
                return Json(new { status = "error", message = "Edit Failed" });
            }
            return Json(new { status = "success", message = "Edit successfully" });
        }
    }
}
