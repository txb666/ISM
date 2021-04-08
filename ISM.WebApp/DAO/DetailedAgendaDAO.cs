using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface DetailedAgendaDAO
    {
        List<DetailedAgenda> GetDetailedAgenda(int student_group_id, DateTime? date, string time_zone, string venue, string PIC, string content, int page, int pageSize);
        int GetTotalDetailedAgenda(int student_group_id, DateTime? date, string time_zone, string venue, string PIC, string content);
        bool CreateDetailedAgenda(int student_group_id, DateTime date, TimeSpan time_start, TimeSpan time_end, string time_zone, string venue, string PIC, string content);
        bool EditDetailedAgenda(int detailed_agenda_id, DateTime date, TimeSpan time_start, TimeSpan time_end, string time_zone, string venue, string PIC, string content);
        bool isDetailedAgendaExist(DateTime date, TimeSpan time_start, TimeSpan time_end, string time_zone);
        bool DeleteDetailedAgenda(int detailed_agenda_id);
    }
}
