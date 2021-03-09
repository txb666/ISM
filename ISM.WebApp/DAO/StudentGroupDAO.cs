using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface StudentGroupDAO
    {
        List<StudentGroup> GetStudentGroups(int page, int pageSize, string program, string home_univercity, string campus, string coordinator);
        int getTotalStudentGroup(string program, string home_univercity, string campus, string coordinator);
        void createStudentGroup(int year, int program_id, int campus_id, DateTime? duration_start, DateTime? duration_end, string home_univercity, string note);
        void editStudentGroup();
        void assignCoordinator(int staff_id, int studentGroup_id);
    }
}
