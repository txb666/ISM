using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Controllers
{
    public class InformationController : Controller
    {
        public InformationDAO _informationDAO;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public InformationController(InformationDAO informationDAO, IWebHostEnvironment hostEnvironment)
        {
            _informationDAO = informationDAO;
            _hostingEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            InformationIndexViewModel model = new InformationIndexViewModel();
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            int user_id = sessionUser.user_id;
            model.user = _informationDAO.GetInformation(user_id);
            return View("Views/Admin/MyInformation/MyInformation.cshtml", model);
        }

        public IActionResult SaveProfile(int user_id, string fullname, string nationality, DateTime dob, bool gender, string contact, IFormFile picture)
        {
            string pictureName = "";
            if (picture != null)
            {
                string extension = Path.GetExtension(picture.FileName);
                pictureName = "user_profile_" + user_id;
                pictureName += extension;
                string image = Path.Combine(_hostingEnvironment.WebRootPath, "image");
                string currentAccomodation = Path.Combine(image, "Profile");
                string filePath = Path.Combine(currentAccomodation, pictureName);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                picture.CopyTo(stream);
                stream.Close();
            }
            bool result = _informationDAO.EditInformation(user_id, fullname, nationality, dob, gender, contact, pictureName);
            if (result == false)
            {
                return Json(new { status = "error", message = "Failed" });
            }
            return Json(new { status = "success", message = "Successfully" });
        }

        public bool CheckPasswordisMatch(string current_password)
        {
            bool result = _informationDAO.isPasswordMatch(current_password);
            return result;
        }

        public bool UpdatePassword(int user_id, string new_password)
        {
            bool result = _informationDAO.ChangePassword(user_id, new_password);
            return result;
        }
    }
}
