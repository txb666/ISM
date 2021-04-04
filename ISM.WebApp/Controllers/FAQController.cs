using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Constant;
using ISM.WebApp.DAO;
using ISM.WebApp.Utils;
using ISM.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            FAQIndexViewModel view = new FAQIndexViewModel();
            view.page = page;
            view.pageSize = pagingConst.PAGE_SIZE;
            view.totalPage = PagingUtils.calculateTotalPage(fAQDAO.getTotalFAQ(question, answer), view.pageSize);
            view.faqs = fAQDAO.getFAQ(view.page, view.pageSize, question, answer);
            view.question = question;
            view.answer = answer;
            return View("Views/Admin/FAQs/FAQs.cshtml", view);
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
