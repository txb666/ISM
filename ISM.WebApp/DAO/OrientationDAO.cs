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
        bool editORTSchedule(int student_id, string content, DateTime date, TimeSpan time, string location, string require_document);
        bool deleteORTSchedule(int student_id);
        bool isORTAlreadyExist(int student_id, string content, DateTime date, TimeSpan time, string location);
        bool isSameTime(int student_id, DateTime date, TimeSpan time);
        bool SetupNotification(int days_before);
        List<ORTMaterials> GetORTMaterials(int student_group_id, int page, int pageSize, string content, string note);
        List<ORTMaterialSlide> GetORTMaterialSlides(int student_id, int page, int pageSize, string program, string content, string material);
        bool CreateORTMaterial(int student_group_id, string content, string note);
        bool EditORTMaterial(int ort_materials_id, string content, string note);
        bool DeleteORTMaterial(int ort_materials_id);
        bool CreateORTMaterialSlide(int student_id, string program, string content, string material);
        bool EditORTMaterialSlide(int ort_material_slide_id, string program, string content, string material);
        bool DeleteORTMaterialSlide(int ort_material_slide_id);
        int getTotalORTMaterials(int student_group_id, string content, string note);
        int getTotalORTMaterialSlides(int student_id, string program, string content, string material);
    }
}
