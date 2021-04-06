using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface LocalRecommendationDAO
    {
        List<LocalRecommendation> getAllLocalRecommendation();
        LocalRecommendation GetLocalRecommendationById(int local_recommendation_id);
        bool editLocalRecommendation(int local_recommendation_id, string title, string file_name);
    }
}
