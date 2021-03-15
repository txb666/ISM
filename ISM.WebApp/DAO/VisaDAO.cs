using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface VisaDAO
    {
        List<Visa> GetVisa(int page, int pageSize, int id, string account, string picture, string student_name,
            DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expried_dateFrom, DateTime? expried_dateTo,
            DateTime? entry_dateFrom, DateTime? entry_dateTo, string entry_port);
        int GetTotalVisa(int id, string account, string picture, string student_name,
            DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expried_dateFrom, DateTime? expried_dateTo,
            DateTime? entry_dateFrom, DateTime? entry_dateTo, string entry_port);

    }
}
