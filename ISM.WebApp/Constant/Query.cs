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
        public const string GET_ALL_NOTIFICATION_DEGREE_PASSPORT = "select a.[user_id],a.isUpdatePassport,a.fullname,a.email,d.[type],d.days_before,e.expired_date as Passport_Expired from Users a inner join Roles b on a.role_id = b.role_id inner join Student_Group c on a.studentGroup_id = c.student_group_id inner join Notification_Configuration d on c.student_group_id = d.studentGroup_id inner join Passports e on a.[user_id] = e.student_id inner join Programs h on c.program_id = h.program_id where b.role_name = 'Degree' and d.[type] = 'passport'";
        public const string GET_ALL_NOTIFICATION_DEGREE_VISA = "select a.[user_id],a.isUpdateVisa,a.fullname,a.email,d.[type],d.days_before,e.expired_date as Visa_Expired from Users a inner join Roles b on a.role_id = b.role_id inner join Student_Group c on a.studentGroup_id = c.student_group_id inner join Notification_Configuration d on c.student_group_id = d.studentGroup_id inner join Visa e on a.[user_id] = e.student_id inner join Programs h on c.program_id = h.program_id where b.role_name = 'Degree' and d.[type] = 'visa'";
        public const string GET_ALL_NOTIFICATION_DEGREE_ORIENTATION = "select a.[user_id],a.fullname,a.email,d.[type],d.days_before,e.[date] as ORT_Date from Users a inner join Roles b on a.role_id = b.role_id inner join Student_Group c on a.studentGroup_id = c.student_group_id inner join Notification_Configuration d on c.student_group_id = d.studentGroup_id inner join ORT_Schedule e on a.[user_id] = e.student_id inner join Programs h on c.program_id = h.program_id where b.role_name = 'Degree' and d.[type] = 'ort_schedule'";
        public const string GET_ALL_NOTIFICATION_MOBILITY_PASSPORT = "select a.[user_id],a.isUpdatePassport,a.fullname,a.email,d.[type],d.days_before,e.expired_date as Passport_Expired from Users a, Roles b, Student_Group c, Notification_Configuration d, Passports e where a.role_id = b.role_id and a.studentGroup_id = c.student_group_id and c.student_group_id = d.studentGroup_id and a.[user_id] = e.student_id and b.role_name = 'Mobility' and d.[type] != 'Transportation' and d.[type] = 'passport'";
        public const string GET_ALL_NOTIFICATION_MOBILITY_VISA = "select a.[user_id],a.isUpdateVisa,a.fullname,a.email,d.[type],d.days_before,e.expired_date as Visa_Expired from Users a, Roles b, Student_Group c, Notification_Configuration d, Visa e where a.role_id = b.role_id and a.studentGroup_id = c.student_group_id and c.student_group_id = d.studentGroup_id and a.[user_id] = e.student_id  and b.role_name = 'Mobility' and d.[type] != 'Transportation' and d.[type] = 'visa'";
        public const string GET_ALL_NOTIFICATION_MOBILITY_DETAIL_AGENDA = "select a.[user_id],a.fullname,a.email,d.[type],d.days_before,e.[date] as Detail_Agenda_Date from Users a, Roles b, Student_Group c, Notification_Configuration d, Detailed_Agenda e where a.role_id = b.role_id and a.studentGroup_id = c.student_group_id  and c.student_group_id = d.studentGroup_id and b.role_name = 'Mobility' and d.[type] != 'Transportation' and d.[type] = 'Detail_Agenda'";
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
        public const string SUBJECT_ACCOMMODATION = "Accommodation Notification";
    }
}
