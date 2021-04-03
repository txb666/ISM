using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class ArticleListViewModel
    {
        public List<Article> articles { get; set; }
        public string type { get; set; }
    }
}
