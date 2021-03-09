using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Constant
{
    public class Query
    {

    }

    public class Login
    {
        public const string GET_ALL_ACCOUNT = "select [account], [password], [role_id], [status], [isFirstLoggedIn] from Users";
    }
}
