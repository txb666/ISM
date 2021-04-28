using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface StudentGroupDAO
    {
        List<StudentGroup> GetStudentGroups(int page, int pageSize, int? year, string program, string home_univercity, string campus);
        int getTotalStudentGroup(int? year, string program, string home_univercity, string campus);
        bool createStudentGroup(int program_id, int campus_id, DateTime duration_start, DateTime duration_end, string home_univercity, string note, List<int> coordinators);
        bool editStudentGroup(int studentGroup_id, string note, List<int> coordinators, int campus_id, DateTime duration_start, DateTime duration_end, string home_univercity, DateTime original_duration_start, DateTime original_duration_end, int original_campus_id, string original_home_univercity, int program_id);
        int assignCoordinator(int staff_id, int studentGroup_id);
        bool isStudentGroupExist(int program_id, int campus_id, DateTime duration_start, DateTime duration_end, string home_univercity);
        bool resetCoordinator(int studentGroup_id);
        StudentGroup getStudentGroupById(int studentGroup_id);
        List<StudentGroup> GetStudentGroupByStaff(int staff_id, bool isAdmin, string degreeOrMobility);
        List<StudentGroup> GetStudentGroupByStaffWithPaging(int staff_id, bool isAdmin, string degreeOrMobility, int page, int pageSize, string program, string home_univercity, string campus, int? year);
        int getTotalStudentGroupByStaff(int staff_id, bool isAdmin, string degreeOrMobility, string program, string home_univercity, string campus, int? year);
    }
}
