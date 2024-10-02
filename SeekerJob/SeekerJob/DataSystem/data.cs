using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeekerJob.DataSystem
{
    public class data
    {
        public static string GetRelativeTime(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalMinutes < 1)
                return "vừa đăng";
            if (timeSpan.TotalMinutes < 60)
                return $"{timeSpan.Minutes} phút trước";
            if (timeSpan.TotalHours < 24)
                return $"{timeSpan.Hours} giờ trước";
            if (timeSpan.TotalDays < 7)
                return $"{timeSpan.Days} ngày trước";
            if (timeSpan.TotalDays < 30)
                return $"{(int)(timeSpan.TotalDays / 7)} tuần trước";
            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} tháng trước";

            return $"{(int)(timeSpan.TotalDays / 365)} năm trước";
        }

    }
}