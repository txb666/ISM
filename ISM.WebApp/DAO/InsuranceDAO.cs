using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface InsuranceDAO
    {
        List<Insurance> getInsurance(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, int page, int pageSize, string account, string fullname, DateTime? startDate, DateTime? expiryDate);
        int getTotalInsurance(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, string account, string fullname, DateTime? startDate, DateTime? expiryDate);
        bool editInsurance(int insurance_id, DateTime startDate, DateTime expiryDate);
        bool SetupNotificationDegree(int days_before, DateTime deadline);
        bool SetupNotificationMobility(int days_before);
        Insurance GetInsurance(int student_id);
    }
}
