using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface ContactInformationDAO
    {
        List<ContactInformation> GetContactInformation(int page, int pageSize, string staff_name, string staff_account, string staff_email, string telephone, string position, bool? status);
        int GetTotalContactInformation(string staff_name, string staff_account, string staff_email, string telephone, string position, bool? status);
        ContactInformation GetContact(int current_staff_id);
        bool CreateOrEdit(int user_id, string telephone, string position, string picture);
    }
}
