using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface InformationDAO
    {
        User GetInformation(int user_id);
        bool ChangePassword(int user_id, string new_password);
        bool EditInformation(int user_id, string fullname, string nationality, DateTime dob, bool gender, string contact, string picture);
        bool isPasswordMatch(string current_password);
        bool updateAccount(int user_id);
    }
}
