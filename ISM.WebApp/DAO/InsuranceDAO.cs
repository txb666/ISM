using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface InsuranceDAO
    {
        List<Insurance> getInsurance(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, int page, int pageSize, string account, string fullname, DateTime? startDateFrom, DateTime? startDateTo, DateTime? expiryDateFrom, DateTime? expiryDateTo);
        int getTotalInsurance(bool isAdmin, string degreeOrMobility, bool haveDegree, int current_staff_id, string account, string fullname, DateTime? startDateFrom, DateTime? startDateTo, DateTime? expiryDateFrom, DateTime? expiryDateTo);
        bool editInsurance(int insurance_id, DateTime startDate, DateTime expiryDate);
    }
}
