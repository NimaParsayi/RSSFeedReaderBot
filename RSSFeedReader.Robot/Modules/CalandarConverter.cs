using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSFeedReader.Robot.Modules
{
    public static class CalandarConverter
    {
        public static string ToShamsi(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();

            return $"*{pc.GetHour(date).ToString("0,#")}:{pc.GetMinute(date).ToString("0,#")}:{pc.GetSecond(date).ToString("0,#")}* - {pc.GetYear(date)}/{pc.GetMonth(date).ToString("0,#")}/{pc.GetDayOfMonth(date).ToString("0,#")}";
        }
    }
}
