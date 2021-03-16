using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface ProgramDAO
    {
        List<ProgramStudy> getAllProgram(); 
    }
}
