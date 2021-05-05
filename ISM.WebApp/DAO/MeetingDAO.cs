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
        List<User> GetStaff(int page, int pageSize, int student_id, string staff_name, string staff_email);
        int GetTotalStaff(int student_id, string staff_name, string staff_email);
        bool isExist(int staff_id, int student_id, DateTime date, TimeSpan start_time, TimeSpan end_time);
        bool BookAMeeting(int staff_id, int student_id, DateTime date, TimeSpan start_time, TimeSpan end_time, string note);
        List<MeetingSchedule> GetStudentMeetingSchedule(int student_id);
        User GetStaffById(int staff_id);
        bool CheckMeetingSchedule(int staff_id, DateTime date, TimeSpan start_time, TimeSpan end_time);
    }
}
