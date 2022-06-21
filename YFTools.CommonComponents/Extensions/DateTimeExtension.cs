using System;
using System.Globalization;

namespace YFTools.CommonComponents.Extensions;

public static class DateTimeExtension
{
    public static int WeekOfYear(this DateTime dt)
    {
        var gc = new GregorianCalendar();
        return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
    }

    public static int WeekOfYear(this DateTime? dt)
    {
        return dt?.WeekOfYear() ?? 0;
    }

    public static string DayOfWeekInChinese(this DateTime dt)
    {
        var weekdays = new[] { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        return weekdays[(int)dt.DayOfWeek];
    }

    public static string DayOfWeekInChinese(this DateTime? dt)
    {
        return dt?.DayOfWeekInChinese() ?? string.Empty;
    }

    public static string NewsTime(this DateTime dt)
    {
        var ts = DateTime.Now - dt;
        if (ts.TotalDays >= 365) return $"{Math.Floor(ts.TotalDays / 365)}年前";
        if (ts.TotalDays >= 30) return $"{Math.Floor(ts.TotalDays / 30)}月前";
        if (ts.TotalDays >= 7) return $"{Math.Floor(ts.TotalDays / 7)}周前";
        if (ts.TotalDays >= 2) return $"{Math.Floor(ts.TotalDays)}天前";
        if (ts.TotalDays >= 1) return "昨天";
        if (ts.TotalHours >= 1) return $"{Math.Floor(ts.TotalHours)}小时前";
        if (ts.TotalMinutes >= 1) return $"{Math.Floor(ts.TotalMinutes)}分钟前";
        return "刚刚";
    }

    public static string NewsTime(this DateTime? dt)
    {
        return dt?.NewsTime() ?? string.Empty;
    }
}