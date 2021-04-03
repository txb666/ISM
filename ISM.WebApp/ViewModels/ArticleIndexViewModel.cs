using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.ViewModels
{
    public class ArticleIndexViewModel
    {
        public Article article { get; set; }
        public bool uniqueArticle { get; set; }
    }
}
