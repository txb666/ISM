using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface UserDAO
    {
        List<User> GetStaff(int page, int pageSize, string fullname, string email, string account, bool? status, DateTime? startDate, DateTime? endDate);
        int getTotalStaff(string fullname, string email, string account, bool? status, DateTime? startDate, DateTime? endDate);
        bool isStaffAlreadyExist(string account, string email);
        int createStaff(string fullname, string email, string account, DateTime? startDate, DateTime? endDate, bool status);
        bool editStaff(int id, string fullname, string email, DateTime? startDate, DateTime? endDate, bool status, string originalEmail);
        List<User> getAllStaff();
        bool isEmailExist(string email);
        List<User> getStudent(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, int page, int pageSize, string fullname, string account, string email, string nationality, DateTime? dob, bool? gender, int? campus_id, int? program_id, string emergency_contact, string home_univercity, string accomodation, bool? status, DateTime? program_duration_start, DateTime? program_duration_end);
        int getTotalStudent(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, string fullname, string account, string email, string nationality, DateTime? dob, bool? gender, int? campus_id, int? program_id, string emergency_contact, string home_univercity, string accomodation, bool? status, DateTime? program_duration_start, DateTime? program_duration_end);
        bool createStudent(string degreeOrMobility,string fullname, string account, string email, int student_group_id, bool status);
        bool editStudent(int id, string fullname, string email, bool status, string originalEmail);
    }
}
