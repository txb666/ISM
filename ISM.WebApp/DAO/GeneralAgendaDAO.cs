using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface GeneralAgendaDAO
    {
        GeneralAgenda GetGeneralAgendaByStudentGroup(int student_group_id);
        bool EditGeneralAgenda(int general_agenda_id, string content, string note);
    }
}
