using System;
using System.Globalization;

namespace AdvancedFunctions
{
    public static partial class Util
    {
        public static string DateToString(DateTime data)
        {
            string year = data.Year.ToString();
            string month = data.Month.ToString();
            string day = data.Day.ToString();
            string dt = data.ToString("" + year + "-" + month + "-" + day + "");
            return dt;
        }

        public static string DateTimeToString(DateTime dateTime)
        {
            string year = dateTime.Year.ToString();
            string month = dateTime.Month.ToString();
            string day = dateTime.Day.ToString();
            int hours = dateTime.Hour;
            int minutes = dateTime.Minute;
            string dt = $"{year}-{month}-{day} {hours}-{minutes}";
            return dt;
        }

        ///<summary>
        ///Для получения хорошего decimal по cultureInfo
        ///</summary>
        public static decimal ConvertToDecimal(object obj)
        {
            try
            {
                var cInfo = CultureInfo.CurrentCulture;
                string str = obj.ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    decimal dec = decimal.Parse(str.Replace(".", cInfo.NumberFormat.NumberDecimalSeparator), NumberStyles.Any);
                    return dec;
                }
                else return 0;
            }
            catch
            {
                //string str = obj.ToString();
                return 0;
            }
        }
        ///Для получения хорошего decimal в виде string для вставки в базу по cultureInfo
        public static string ConvertToString(object dec)
        {
            string str = dec.ToString().Replace(",", ".");
            return str;
        }

        public static string ReverseConvertToString(object dec)
        {
            string str = dec.ToString().Replace(".", ",");
            return str;
        }
    }
}
