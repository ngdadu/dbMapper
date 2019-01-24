using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DBMapper
{
    public static class ProgUtils
    {
        public static string ReplaceEnvironmentMacros(this string strSource, string servername, string dbname)
        {
            if (string.IsNullOrEmpty(strSource)) return strSource;
            strSource = Environment.ExpandEnvironmentVariables(strSource.ReplaceEx("%SERVERNAME%", servername).ReplaceEx("%DBNAME%", dbname));
            return strSource;
        }
        public static string ReplaceEx(this string str, string search, string replacement)
        {
            return String.IsNullOrEmpty(str) || string.IsNullOrEmpty(search) ? str :
                Regex.Replace(str, Regex.Escape(search), replacement ?? "", RegexOptions.IgnoreCase);
        }
        public static int ToInt(this string value, int defValue = 0)
        {
            int result;
            return string.IsNullOrEmpty(value) || !Int32.TryParse(value, out result) ? defValue : result;
        }
        public static bool IsNumber(this string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            double result;
            if (Double.TryParse(value.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, "."), out result)) return true;
            value = value.ToLower();
            if (value.StartsWith("0x")) return !value.ToCharArray().Any(c => "0123456789abcdef".IndexOf(c) < 0);
            return false;
        }
        public static void DisableGridViewError(this DataGridView thisGrid)
        {
            thisGrid.ShowRowErrors = false;
            thisGrid.DataError += (s, e) => { e.ThrowException = false; };
        }

        public static string SimplifySQLName(string sqlName)
        {
            if (string.IsNullOrEmpty(sqlName)) return string.Empty;
            if (sqlName.StartsWith("\"") || sqlName.StartsWith("[")) sqlName = sqlName.Substring(1);
            if (sqlName.EndsWith("\"") || sqlName.EndsWith("]")) sqlName = sqlName.Substring(0, sqlName.Length - 1);
            return sqlName;
        }
    }
}
