using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ISM.WebApp.Controllers
{
    public class LocalRecommendationController : Controller
    {
        public LocalRecommendationDAO localRecommendationDAO;
        public LocalRecommendationController(LocalRecommendationDAO localRecommendationDAO)
        {
            this.localRecommendationDAO = localRecommendationDAO;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int id)
        {
            LocalRecommendation localRecommendation = localRecommendationDAO.GetLocalRecommendationById(id);
            return View("Views/Admin/LocalRecommendation/LocalRecommendation.cshtml", localRecommendation);
        }
    }
}
