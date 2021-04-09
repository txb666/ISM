using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface MeetingDAO
    {
        List<AvailableTime> GetAvailableTimes(int page, int pageSize, int staff_id, DateTime? date, TimeSpan? start_time, TimeSpan? end_time);
        int GetTotalAvailableTime(int staff_id, DateTime? date, TimeSpan? start_time, TimeSpan? end_time);
        bool CreateMAT(int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time);
        bool EditMAT(int mat_id, int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time);
        bool DeleteMAT(int mat_id, int staff_id);
        bool isSameTime(int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time);
        List<MeetingSchedule> GetMeetingRegister(int staff_id);
        List<MeetingSchedule> GetMeetingSchedule(int staff_id);
        bool AcceptMeetingRegister(int ms_id);
        bool SetupNotification(int days_before);
    }
}
