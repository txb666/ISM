using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface VisaDAO
    {
        List<Visa> GetVisa(int page, int pageSize, string account, string picture, string student_name,
            DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expired_dateFrom, DateTime? expired_dateTo,
            DateTime? entry_dateFrom, DateTime? entry_dateTo, string entry_port);
        int GetTotalVisa(string account, string picture, string student_name,
            DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expired_dateFrom, DateTime? expired_dateTo,
            DateTime? entry_dateFrom, DateTime? entry_dateTo, string entry_port);

    }
}
