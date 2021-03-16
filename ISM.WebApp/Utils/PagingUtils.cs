using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Utils
{
    public static class PagingUtils
    {
        public static String hyperlink(String number, String text)
        {
            return "<button class='paginglink' onclick='search("+number+")'>" + text + "</a>";
        }
        public static String currentlink(String text)
        {
            return "<span class='currentlink'>" + text + "</span>";
        }
        public static String pager(int totalpage, int pageindex, int gap)
        {
            String result = "";
            if (pageindex - gap > 1)
                result += hyperlink("1","First");
            for (int i = gap; i > 0; i--)
            {
                int page = pageindex - i;
                if (page > 0)
                    result += hyperlink(page.ToString(),page.ToString());
            }
            result += currentlink(pageindex.ToString());
            for (int i = 1; i <= gap; i++)
            {
                int page = pageindex + i;
                if (page <= totalpage)
                    result += hyperlink(page.ToString(),page.ToString());
            }
            if (pageindex + gap < totalpage)
                result += hyperlink(totalpage.ToString(),"Last");

            return result;
        }

        public static int calculateTotalPage(int totalRecord, int pageSize)
        {
            if (totalRecord % pageSize == 0)
            {
                return totalRecord / pageSize;
            }
            return totalRecord / pageSize + 1;
        }
    }
}
