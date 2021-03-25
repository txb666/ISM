using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface TransportationDAO
    {
        List<Transportation> GetTransportations(int studentGroup_id, int page, int pageSize, DateTime? date, string bus, string driver, string itinerary, string supporter);
        int getTotalTransportation(int studentGroup_id, DateTime? date, string bus, string driver, string itinerary, string supporter);
        bool createTransportation(int studentGroup_id, DateTime date, TimeSpan time, string bus, string driver, string itinerary, string supporter, string note);
        bool editTransportation(int transportations_id, DateTime date, TimeSpan time, string bus, string driver, string itinerary, string supporter, string note);
    }
}
