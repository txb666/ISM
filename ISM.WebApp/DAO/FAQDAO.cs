using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface FAQDAO
    {
        List<FAQ> getFAQ(int page, int pageSize, string question, string answer);
        int getTotalFAQ(string question, string answer);
        bool createFAQ(string question, string answer);
        bool editFAQ(int faq_id, string question, string answer);
        bool deleteFAQ(int faq_id);
    }
}
