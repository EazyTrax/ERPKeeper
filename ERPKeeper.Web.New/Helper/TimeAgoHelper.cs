using System;

namespace ERPKeeperCore.Web.Helper
{
    public static class TimeAgoHelper
    {
        public static string ConvertToTimeAgo(DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan.TotalSeconds < 60)
                return $"{Math.Floor(timeSpan.TotalSeconds)} seconds ago";
            if (timeSpan.TotalMinutes < 60)
                return $"{Math.Floor(timeSpan.TotalMinutes)} minutes ago";
            if (timeSpan.TotalHours < 24)
            {
                int hours = timeSpan.Hours;
                int minutes = timeSpan.Minutes;
                return $"{hours} hour{(hours > 1 ? "s" : "")} {minutes} minute{(minutes > 1 ? "s" : "")} ago";
            }
            if (timeSpan.TotalDays < 7)
            {
                int days = timeSpan.Days;
                int hours = timeSpan.Hours;
                return $"{days} day{(days > 1 ? "s" : "")} {hours} hour{(hours > 1 ? "s" : "")} ago";
            }
            if (timeSpan.TotalDays < 30)
            {
                int weeks = (int)(timeSpan.TotalDays / 7);
                int days = (int)(timeSpan.TotalDays % 7);
                return $"{weeks} week{(weeks > 1 ? "s" : "")} {days} day{(days > 1 ? "s" : "")} ago";
            }
            if (timeSpan.TotalDays < 365)
            {
                int totalMonths = (int)(timeSpan.TotalDays / 30);
                int days = (int)(timeSpan.TotalDays % 30);
                return $"{totalMonths} month{(totalMonths > 1 ? "s" : "")} {days} day{(days > 1 ? "s" : "")} ago";
            }

            int years = (int)(timeSpan.TotalDays / 365);
            int remainingMonths = (int)((timeSpan.TotalDays % 365) / 30);
            return $"{years} year{(years > 1 ? "s" : "")} {remainingMonths} month{(remainingMonths > 1 ? "s" : "")} ago";
        }

    }
}