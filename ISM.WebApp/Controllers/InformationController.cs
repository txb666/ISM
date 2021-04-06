using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Controllers
{
    public class InformationController : Controller
    {
        public InformationDAO _informationDAO;
        public InformationController(InformationDAO informationDAO)
        {
            _informationDAO = informationDAO;
        }
        public IActionResult Index()
        {
            InformationIndexViewModel model = new InformationIndexViewModel();
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            int user_id = sessionUser.user_id;
            model.user = _informationDAO.GetInformation(user_id);
            return View("Views/Admin/MyInformation/MyInformation.cshtml", model);
        }

        public bool SaveProfile(int user_id, string fullname, string nationality, DateTime dob, bool gender, string contact)
        {
            bool result = _informationDAO.EditInformation(user_id,fullname,nationality,dob,gender,contact);
            return result;
        }

        public bool CheckPasswordisMatch(string current_password)
        {
            bool result = _informationDAO.isPasswordMatch(current_password);
            return result;
        }

        public bool UpdatePassword(int user_id, string new_password)
        {
            bool result = _informationDAO.ChangePassword(user_id,new_password);
            return result;
        }
    }
}
