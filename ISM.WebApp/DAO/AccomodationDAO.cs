using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface AccomodationDAO
    {
        List<CurrentAccomodation> GetCurrentAccomodations(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, int page, int pageSize, string account, string fullname, int? student_id, int? student_group_id, string type, string location, string description, string note);
        int getTotalCurrentAccomodations(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, string account, string fullname, int? student_id, int? student_group_id, string type, string location, string description, string note);
        bool createCurrentAccomodation(int? student_id, int? student_group_id, string type, string location, string description, double? fee, string picture, string note);
        bool editCurrentAccomodation(int current_accomodation_id, string type, string location, string description, double? fee, string picture, string note);
        bool isCurrentAccomodationExist(int? student_id, int? student_group_id);
        List<RegisterAccomodation> getRegisterAccomodation(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, int page, int pageSize, string account, string fullname, string email, string home_univercity, string exchange_campus, string accomodation_option, string room_type, string other_request);
        int getTotalRegisterAccomodation(bool isAdmin, bool haveDegree, string degreeOrMobility, int current_staff_id, string account, string fullname, string email, string home_univercity, string exchange_campus, string accomodation_option, string room_type, string other_request);
        RegisterAccomodation GetRegisterAccomodation(int student_id);
        bool CreateOrEditRegisterAccomodation(int? register_accomodation_id, int student_id, string exchange_campus, string accomodation_option, double? cost_per_month, double? room_size, string room_type, double? distance, string other_request, DateTime register_date);
        bool SetupNotification(int days_before);
        CurrentAccomodation GetCurrentAccomodation(int? student_id, int? student_group_id);
    }
}
