using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface PassportDAO
    {
        List<Passport> GetPassports(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, int page, int pageSize, string fullname, string account, string passport_number, DateTime? start_date, DateTime? expired_date, string issuing_authority);
        int GetTotalPassports(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, string fullname, string account, string passport_number, DateTime? start_date, DateTime? expired_date, string issuing_authority);
        bool editPassport(int passport_id, string passport_number, DateTime start_date, DateTime expired_date, string issuing_authority);
        //bool isPassportAlreadyExist(string passport_number);
        int createPassport(int student_id, string passport_number, DateTime start_date, DateTime expired_date, string issuing_authority);
        bool CreateOrEdit(int days_before);
        Passport GetPassport(int student_id);
        bool CreateOrEditPassport(int student_id, int? passport_id, string picture, string passport_number, DateTime start_date, DateTime expired_date, string issuing_authority);
    }
}
