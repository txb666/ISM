using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface PassportDAO
    {
        List<Passport> GetPassports(int page, int pageSize, string account, string picture, string student_name, string passport_number, DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expired_dateFrom, DateTime? expired_dateTo, string issuing_authority);
        int GetTotalPassports(string account, string picture, string student_name, string passport_number, DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expired_dateFrom, DateTime? expired_dateTo, string issuing_authority);
        bool editPassport(int passport_id, string passport_number, DateTime start_date, DateTime expired_date, string issuing_authority);
        //bool isPassportAlreadyExist(string passport_number);
        int createPassport(int student_id, string passport_number, DateTime start_date, DateTime expired_date, string issuing_authority);
    }
}
