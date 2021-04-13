using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Models;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISM.WebApp.Controllers
{
    public class FAQController : Controller
    {
        public FAQDAO fAQDAO;
        public FAQController(FAQDAO fAQDAO)
        {
            this.fAQDAO = fAQDAO;
        }
        public IActionResult Index(string question=null,string answer=null,int page=1)
        {
            Account sessionUser = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString(LoginConst.SessionKeyName));
            FAQIndexViewModel view = new FAQIndexViewModel();
            view.page = page;
            view.pageSize = pagingConst.PAGE_SIZE;
            view.totalPage = PagingUtils.calculateTotalPage(fAQDAO.getTotalFAQ(question, answer), view.pageSize);
            view.faqs = fAQDAO.getFAQ(view.page, view.pageSize, question, answer);
            view.question = question;
            view.answer = answer;
            if (sessionUser.role_name.Equals("Admin") || sessionUser.role_name.Equals("Staff"))
            {
                return View("Views/Admin/FAQs/FAQs.cshtml", view);
            }
            else if (sessionUser.role_name.Equals("Degree") || sessionUser.role_name.Equals("Mobility")) {
                return View("Views/Degree/FAQs/FAQs.cshtml", view);
            }
            return View();
        }

        public bool Create(string question, string answer)
        {
            bool result = fAQDAO.createFAQ(question, answer);
            return result;
        }

        public bool Edit(int faq_id, string question, string answer)
        {
            bool result = fAQDAO.editFAQ(faq_id, question, answer);
            return result;
        }
        public bool Delete(int faq_id)
        {
            bool result = fAQDAO.deleteFAQ(faq_id);
            return result;
        }
    }
}
