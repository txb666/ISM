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

    public class NotificationConst
    {
        public const string GET_ALL_NOTIFICATION_DEGREE = "select a.[user_id],a.fullname,a.email,d.[type],d.days_before,e.expired_date " +
            "as Passport_Expired,f.expired_date as Visa_Expired,d.deadline,k.[date] as ORT_Date from Users a inner join Roles b on a.role_id " +
            "= b.role_id inner join Student_Group c on a.studentGroup_id = c.student_group_id inner join Notification_Configuration d on " +
            "c.student_group_id = d.studentGroup_id inner join Passports e on a.[user_id] = e.student_id inner join Visa f on a.[user_id] " +
            "= f.student_id inner join Insurances g on a.[user_id] = g.student_id inner join Programs h on c.program_id = h.program_id " +
            "inner join Flights i on a.[user_id] = i.student_id inner join ORT_Schedule k on a.[user_id] = k.student_id where b.role_name = 'Degree'";
        public const string GET_ALL_NOTIFICATION_MOBILITY = "select a.[user_id],a.fullname,a.email,d.[type],d.days_before,d.hours_before,e.expired_date " +
            "as Passport_Expired,f.expired_date as Visa_Expired,c.duration_start,h.[date] as Detail_Agenda_Date,i.[time] as Bus_Time,i.[date] as Bus_Date from Users a, " +
            "Roles b, Student_Group c, Notification_Configuration d, Passports e, Visa f, Insurances g, Detailed_Agenda h, Transportations i " +
            "where a.role_id = b.role_id and a.studentGroup_id = c.student_group_id and c.student_group_id = d.studentGroup_id and a.[user_id] = " +
            "e.student_id and a.[user_id] = f.student_id and a.[user_id] = g.student_id and c.student_group_id = d.studentGroup_id and " +
            "c.student_group_id = i.studentGroup_id and b.role_name = 'Mobility'";
    }
}
