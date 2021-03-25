using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface VisaDAO
    {
        List<Visa> GetVisa(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, int page, int pageSize, string account, string fullname, DateTime? start_date, DateTime? expired_date, DateTime? date_entry, string entry_port);
        int GetTotalVisa(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, string account, string fullname, DateTime? start_date, DateTime? expired_date, DateTime? date_entry, string entry_port);
        bool editVisa(int visa_id, DateTime start_date, DateTime expired_date, DateTime entry_date, string entry_port);
        bool CreateOrEdit(int days_before);
    }
}
