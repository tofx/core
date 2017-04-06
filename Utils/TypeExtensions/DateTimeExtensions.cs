using System;

namespace TOF.Core.Utils.TypeExtensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ResetToDayBeginTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        public static DateTime ResetToDayEndTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }

        public static bool IsInRange(this DateTime dateTime, DateTime RangeStart, DateTime RangeEnd)
        {
            return RangeStart <= dateTime && RangeEnd >= dateTime;
        }

        public static bool IsInRangeAtDayLevel(this DateTime dateTime, DateTime RangeStart, DateTime RangeEnd)
        {
            DateTime dateStart = new DateTime(RangeStart.Year, RangeStart.Month, RangeStart.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(RangeEnd.Year, RangeEnd.Month, RangeEnd.Day, 23, 59, 59);

            return (dateStart <= dateTime && dateEnd >= dateTime);
        }

        public static bool IsInRangeAtHourLevel(this DateTime dateTime, DateTime RangeStart, DateTime RangeEnd)
        {
            DateTime dateStart = new DateTime(
                RangeStart.Year, RangeStart.Month, RangeStart.Day, RangeStart.Hour, 0, 0);
            DateTime dateEnd = new DateTime(
                RangeEnd.Year, RangeEnd.Month, RangeEnd.Day, RangeEnd.Hour, 59, 59);

            return (dateStart <= dateTime && dateEnd >= dateTime);
        }

        public static bool IsInRangeAtMinuteLevel(this DateTime dateTime, DateTime RangeStart, DateTime RangeEnd)
        {
            DateTime dateStart = new DateTime(
                RangeStart.Year, RangeStart.Month, RangeStart.Day, RangeStart.Hour, RangeStart.Minute, 0);
            DateTime dateEnd = new DateTime(
                RangeEnd.Year, RangeEnd.Month, RangeEnd.Day, RangeEnd.Hour, RangeEnd.Minute, 59);

            return (dateStart <= dateTime && dateEnd >= dateTime);
        }
    }
}
