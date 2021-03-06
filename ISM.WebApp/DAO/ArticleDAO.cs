using ISM.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.DAO
{
    public interface ArticleDAO
    {
        int CreateArticle(string type, string title);
        bool EditArticle(int article_id, string title, string fileName);
        bool DeleteArticle(int article_id);
        List<Article> getArticleByType(string type);
        Article getArticleById(int article_id);
        bool isTitleExist(string title, string type);
    }
}
