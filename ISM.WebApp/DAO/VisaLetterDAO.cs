using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ISM.WebApp.Models;

namespace ISM.WebApp.DAO
{
    public interface VisaLetterDAO
    {
        List<VisaLetter> getAllVisaLetter();
        bool editVisaLetter(int id,string visa_type,string visa_period,string apply_receive);
        List<VisaLetter> GetVisaLetter(int page, int pagesize, string fullname, string apply_receive, string visa_period, string type_visa,  string nationality, string passport_number
           , DateTime? dob, DateTime? expired_dateFrom, DateTime? expired_dateTo);
        int GetTotalVisaLetter(string fullname, string apply_receive, string visa_period , string type_visa ,  string nationality , string passport_number
           , DateTime? dob , DateTime? expired_dateFrom , DateTime? expired_dateTo);

        
    }
}
