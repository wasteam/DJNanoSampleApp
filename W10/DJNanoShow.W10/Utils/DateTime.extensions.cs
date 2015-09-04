using System;
using System.Text;

namespace Microsoft.AppStudio.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string MoreThan1YearString = "more than 1 year";
        public static string LessThan1SecondString = "less than 1 second";
        public static string MonthString = " month";
        public static string DayString = " day";
        public static string HourString = " hour";
        public static string MinuteString = " minute";
        public static string LessThan1Minute = "less than 1 minute";
        public static string BeforeExtensionString = "";
        public static string AfterExtensionString = " ago";

        public static string FriendlyDate(this DateTime dt)
        {
            InitializeStrings();
            StringBuilder sb = new StringBuilder();

            if (DateTime.UtcNow < dt)
            {
                return LessThan1SecondString;
            }

            var elapsed = DateTime.UtcNow.Subtract(dt);

            var years = TotalYears((int)elapsed.TotalDays);
            var months = TotalMonths((int)elapsed.TotalDays);
            var days = (int)elapsed.TotalDays;
            var hours = (int)elapsed.TotalHours;
            var minutes = (int)elapsed.TotalMinutes;

            sb.Append(BeforeExtensionString);

            if (years > 0)
            {
                sb.Append(MoreThan1YearString);
            }
            else if (months > 0)
            {
                sb.Append(months.ToString("0") + MonthString);
                if (days > 1)
                {
                    sb.Append("s");
                }
            }
            else if (days > 0)
            {
                sb.Append(days.ToString("0") + DayString);
                if (days > 1)
                {
                    sb.Append("s");
                }
            }
            else if (hours > 0)
            {
                sb.Append(hours.ToString("0") + HourString);
                if (hours > 1)
                {
                    sb.Append("s");
                }
            }
            else if (minutes > 0)
            {
                sb.Append(minutes.ToString("0") + MinuteString);
                if (minutes > 1)
                {
                    sb.Append("s");
                }
            }
            else
            {
                sb.Append(LessThan1Minute);
            }

            sb.Append(AfterExtensionString);

            return sb.ToString();
        }

        private static void InitializeStrings()
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            MoreThan1YearString = loader.GetString("MoreThan1YearString");
            LessThan1SecondString = loader.GetString("LessThan1SecondString");
            MonthString = loader.GetString("MonthString");
            DayString = loader.GetString("DayString");
            HourString = loader.GetString("HourString");
            MinuteString = loader.GetString("MinuteString");
            LessThan1Minute = loader.GetString("LessThan1Minute");
            BeforeExtensionString = loader.GetString("BeforeExtensionString");
            AfterExtensionString = loader.GetString("AfterExtensionString");
        }

        private static int TotalMonths(int elapsedDays)
        {
            return elapsedDays / 30;
        }

        private static int TotalYears(int elapsedDays)
        {
            return elapsedDays / 365;
        }
    }
}
