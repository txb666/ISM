using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface AccountDAO
    {
        List<Account> GetAccounts ();
        bool haveDegree(int user_id);
    }
}
