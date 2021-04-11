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
        public const string GET_ALL_NOTIFICATION_DEGREE = "select a.[user_id],a.isUpdatePassport,a.isUpdateVisa,a.fullname,a.email,d.[type],d.days_before,e.expired_date as Passport_Expired,f.expired_date as Visa_Expired,k.[date] as ORT_Date from Users a inner join Roles b on a.role_id = b.role_id inner join Student_Group c on a.studentGroup_id = c.student_group_id inner join Notification_Configuration d on c.student_group_id = d.studentGroup_id inner join Passports e on a.[user_id] = e.student_id inner join Visa f on a.[user_id] = f.student_id inner join Programs h on c.program_id = h.program_id inner join ORT_Schedule k on a.[user_id] = k.student_id where b.role_name = 'Degree'";
        public const string GET_ALL_NOTIFICATION_MOBILITY = "select a.[user_id],a.isUpdatePassport,a.isUpdateVisa,a.fullname,a.email,d.[type],d.days_before,e.expired_date as Passport_Expired,f.expired_date as Visa_Expired,h.[date] as Detail_Agenda_Date from Users a, Roles b, Student_Group c, Notification_Configuration d, Passports e, Visa f, Detailed_Agenda h where a.role_id = b.role_id and a.studentGroup_id = c.student_group_id and c.student_group_id = d.studentGroup_id and a.[user_id] = e.student_id and a.[user_id] = f.student_id and c.student_group_id = d.studentGroup_id and b.role_name = 'Mobility' and d.[type] != 'Transportation'";
    }

    public class pagingConst
    {
        public const int PAGE_SIZE = 5;
        public const int GAP = 3;
    }

    public class notificationConst
    {
        public const string TYPE_PASSPORT = "passport";
        public const string TYPE_VISA = "visa";
        public const string TYPE_ORIENTATION = "ort_schedule";
        public const string TYPE_INSURANCE = "insurance";
        public const string TYPE_FLIGHT = "flight";
        public const string TYPE_DETAIL_AGENDA = "Detail_Agenda";
        public const string TYPE_MEETING_SCHEDULE = "meeting_schedule";
        public const string KIND_DEGREE = "degree";
        public const string KIND_MOBILITY = "mobility";
        public const string SUBJECT_PASSPORT = "Passport Notification";
        public const string SUBJECT_VISA = "Visa Notification";
        public const string SUBJECT_ORIENTATION = "Orientation Notification";
        public const string SUBJECT_INSURANCE = "Insurance Notification";
        public const string SUBJECT_FLIGHT = "Flight Notification";
        public const string SUBJECT_DETAIL_AGENDA = "Detail Agenda Notification";
        public const string SUBJECT_MEETING_SCHEDULE = "Meeting Schedule Notification";
        public const string SUBJECT_TRANSPORTATION = "Meeting Schedule Notification";
        public const string SUBJECT_ISM_NOTIFICATION = "ISM Notification";
    }
}
