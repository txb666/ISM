using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface FlightDAO
    {
        List<Flight> getFlight(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, int page, int pageSize, string account, string fullname, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string picture_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d, string picture_d);
        int getTotalFlight(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, string account, string fullname, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string picture_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d, string picture_d);
        bool editFlight(int flight_id, string flight_number_a, DateTime? arrival_date_a, TimeSpan? arrival_time_a, string airport_departure_a, string airport_arrival_a, string picture_a, string flight_number_d, DateTime? arrival_date_d, TimeSpan? arrival_time_d, string airport_departure_d, string airport_arrival_d, string picture_d);
    }
}
