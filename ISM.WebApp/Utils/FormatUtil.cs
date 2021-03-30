using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISM.WebApp.Utils
{
    public static class FormatUtil
    {
        public static string semicolonStringList(List<string> stringList)
        {
            string result = "";
            for(int i = 0; i < stringList.Count; i++)
            {
                result += stringList[i] + "; ";
                if (i == stringList.Count - 1)
                {
                    result = result.Remove(result.Length - 2);
                }             
            }
            return result;
        }

        public static string ToInputDate(DateTime? input)
        {
            return input.HasValue ? input.Value.ToString("yyyy-MM-dd") : "";
        }

        public static string ToInputTime(TimeSpan? input)
        {
            return input.HasValue ? input.Value.ToString(@"hh\:mm\:ss") : "";
        }

        public static List<int> JsonStringToIntegerList(string input) {
            if (string.IsNullOrEmpty(input) || input.Equals("[]"))
            {
                List<int> emptyList = new List<int>();
                return emptyList;
            }
            input = input.Replace("[", "");
            input = input.Replace("]", "");
            string[] array = input.Split(",");
            int[] ints = Array.ConvertAll(array, s => int.Parse(s));
            List<int> output = ints.ToList<int>();
            return output;
        }


    }
}
