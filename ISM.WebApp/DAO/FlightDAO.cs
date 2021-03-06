using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface FlightDAO
    {
        List<Flight> getFlight(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, int page, int pageSize, string account, string fullname, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d);
        int getTotalFlight(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, string account, string fullname, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d);
        bool editFlightDegree(int? flight_id, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a);
        bool editFlightMobility(int? flight_id, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d);
        bool SetupNotificationDegree(int days_before, DateTime deadline);
        bool SetupNotificationMobility(int days_before);
        List<Flight> GetVisaLettersAdminToExcel();
        List<Flight> GetVisaLettersStaffToExcel(int staff_id);
        bool CreateOrEditFlight(int student_id, int? flight_id, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string picture_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d, string picture_d);
        Flight GetFlight(int student_id);
        bool SkipNotification(int user_id);
    }
}
