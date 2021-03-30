using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface StudentHandbookDAO
    {
        List<StudentHandbook> getAllStudentHandbook();
        StudentHandbook GetStudentHandbookById(int student_handbook_id);
    }
}
