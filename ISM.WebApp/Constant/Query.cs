using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Constant
{
    public class Query
    {

    }

    public class LoginConst
    {
        public const string GET_ALL_ACCOUNT = "select a.[user_id],a.account,a.[password],a.[status],a.isFirstLoggedIn,b.role_name from Users a inner join Roles b on a.role_id = b.role_id";
        public const string SessionKeyName = "_Name";
    }
}
