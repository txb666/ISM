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

namespace ISM.WebApp.Controllers
{
    public class ContactInformationController : Controller
    {
        public ContactInformationDAO _contactInformationDAO;
        public ContactInformationController(ContactInformationDAO contactInformationDAO)
        {
            _contactInformationDAO = contactInformationDAO;
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

        public bool CreateOrEdit(int user_id, string telephone, string position, string picture)
        {
            bool result = _contactInformationDAO.CreateOrEdit(user_id, telephone, position, picture);
            return result;
        }
    }
}
