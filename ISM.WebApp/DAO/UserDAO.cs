using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface UserDAO
    {
        List<User> GetStaff(int page, int pageSize, string fullname, string email, string account, bool? status, DateTime? startDateFrom, DateTime? startDateTo, DateTime? endDateFrom, DateTime? endDateTo);
        int getTotalStaff(string fullname, string email, string account, bool? status, DateTime? startDateFrom, DateTime? startDateTo, DateTime? endDateFrom, DateTime? endDateTo);
        bool isStaffAlreadyExist(string account, string email);
        int createStaff(string fullname, string email, string account, DateTime? startDate, DateTime? endDate, bool status);
        void editStaff(int id, string fullname, string email, string account, DateTime? startDate, DateTime? endDate, bool status);
    }
}
