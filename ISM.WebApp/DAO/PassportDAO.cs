using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface PassportDAO
    {
        List<Passport> GetPassports(int page, int pageSize, int id, string account, string picture, string student_name, string passport_number, DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expried_dateFrom, DateTime? expried_dateTo, string issuing_authority);
        int GetTotalPassports(int id, string account, string picture, string student_name, string passport_number, DateTime? start_dateFrom, DateTime? start_dateTo, DateTime? expried_dateFrom, DateTime? expried_dateTo, string issuing_authority);
        void editPassport(int passport_id, string passport_number, DateTime start_date, DateTime expried_date, string issuing_authoriry);
        bool isPassportAlreadyExist(string student_name, string passport_number);
        int createPassport(int student_id, string passport_number, DateTime start_date, DateTime expried_date, string issuing_authority);
        Passport GetPassportById(int id);
    }
}
