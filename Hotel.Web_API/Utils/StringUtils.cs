using System;

namespace Hotel.Web_API.Utils
{
    public class StringUtils
    {
        public static bool IsBlank(string s)
        {
            return s == null || s.Trim().Equals("");
        }
    }
}
