using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface OrientationDAO
    {
        List<User> GetDegreeStudent(bool isAdmin, int staff_id, string account, string fullname, int page, int pageSize);
        int GetTotalDegreeStudent(bool isAdmin, int staff_id, string account, string fullname);
        List<OrientationSchedule> GetSearchOrientationSchedules(int student_id, int page, int pageSize, string content, DateTime? date, TimeSpan? time, string location, string require_document);
        int GetSearchTotalORT(int student_id, string content, DateTime? date, TimeSpan? time, string location, string require_document);
        bool createORTSchedule(int student_id, string content, DateTime date, TimeSpan time, string location, string require_document);
        bool editORTSchedule(int ort_schedule_id, string content, DateTime date, TimeSpan time, string location, string require_document);
        bool deleteORTSchedule(int ort_schedule_id);
        bool isORTAlreadyExist(int student_id, string content, DateTime date, TimeSpan time, string location);
        bool isSameTime(int student_id, DateTime date, TimeSpan time);
        bool SetupNotification(int days_before);
    }
}
