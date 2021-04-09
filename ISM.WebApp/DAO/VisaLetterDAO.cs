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
        List<VisaLetter> GetVisaLetter(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, int page, int pagesize, string fullname, string apply_receive, string visa_period, string visa_type, string nationality, string passport_number
           , DateTime? dob, DateTime? expired_date);
        int GetTotalVisaLetter(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, string fullname, string apply_receive, string visa_period, string visa_type, string nationality, string passport_number
           , DateTime? dob, DateTime? expired_date);
        VisaLetter GetVisaLetter(int student_id);
        
    }
}
