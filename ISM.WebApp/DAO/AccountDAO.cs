﻿using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface AccountDAO
    {
        List<Account> GetAccounts ();
        Account GetAccount(string account, string password);
        bool haveDegree(int user_id);
        bool checkLogin(string username, string password);
    }
}
