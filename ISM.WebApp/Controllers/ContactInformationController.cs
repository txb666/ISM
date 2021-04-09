using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.ViewModels;
using ISM.WebApp.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ISM.WebApp.Controllers
{
    public class ContactInformationController : Controller
    {
        public ContactInformationDAO _contactInformationDAO;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ContactInformationController(ContactInformationDAO contactInformationDAO, IWebHostEnvironment hostEnvironment)
        {
            _contactInformationDAO = contactInformationDAO;
            _hostingEnvironment = hostEnvironment;
        }
        public IActionResult Index(int page = 1, string staff_name = "", string staff_account = "", string staff_email = "", string telephone = "", string position = "", bool? status = null)
        {
            ContactInformationIndexViewModel model = new ContactInformationIndexViewModel();
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            bool isAdmin = sessionUser.role_name.Equals("Admin") ? true : false;
            int current_staff_id = sessionUser.user_id;
            if (isAdmin)
            {
                model.page = page;
                model.pageSize = pagingConst.PAGE_SIZE;
                model.totalPage = PagingUtils.calculateTotalPage(_contactInformationDAO.GetTotalContactInformation(staff_name, staff_account, staff_email, telephone, position, status), model.pageSize);
                model.contactList = _contactInformationDAO.GetContactInformation(model.page, model.pageSize, staff_name, staff_account, staff_email, telephone, position, status);
                model.contact = _contactInformationDAO.GetContact(current_staff_id);
                model.staff_name = staff_name;
                model.staff_email = staff_email;
                model.staff_account = staff_account;
                model.telephone = telephone;
                model.position = position;
                model.status = status;
            }
            else
            {
                model.contact = _contactInformationDAO.GetContact(current_staff_id);
            }
            return View("Views/Admin/ContactUs/ContactInformation.cshtml", model);
        }

        public IActionResult CreateOrEdit(int user_id, string telephone, string position, IFormFile picture)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            string pictureName = "";
            if (picture != null)
            {
                string extension = Path.GetExtension(picture.FileName);
                pictureName = "Contact_Information_" + user_id + "_" + sessionUser.username;
                pictureName += extension;
                string image = Path.Combine(_hostingEnvironment.WebRootPath, "image");
                string currentContact = Path.Combine(image, "ContactInformation");
                string filePath = Path.Combine(currentContact, pictureName);
                FileStream stream = new FileStream(filePath, FileMode.Create);
                picture.CopyTo(stream);
                stream.Close();
            }
            bool result = _contactInformationDAO.CreateOrEdit(user_id, telephone, position, pictureName);
            if (result == false)
            {
                return Json(new { status = "error", message = "Failed" });
            }
            return Json(new { status = "success", message = "Successfully" });
        }
    }
}
