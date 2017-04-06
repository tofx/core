using System;

namespace tofx.Core.Utils.TypeExtensions
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

        public static bool IsInRange(this DateTime dateTime, DateTime rangeStart, DateTime rangeEnd)
        {
            return rangeStart <= dateTime && rangeEnd >= dateTime;
        }

        public static bool IsInRangeAtDayLevel(this DateTime dateTime, DateTime rangeStart, DateTime rangeEnd)
        {
            DateTime dateStart = new DateTime(rangeStart.Year, rangeStart.Month, rangeStart.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(rangeEnd.Year, rangeEnd.Month, rangeEnd.Day, 23, 59, 59);

            return (dateStart <= dateTime && dateEnd >= dateTime);
        }

        public static bool IsInRangeAtHourLevel(this DateTime dateTime, DateTime rangeStart, DateTime rangeEnd)
        {
            DateTime dateStart = new DateTime(
                rangeStart.Year, rangeStart.Month, rangeStart.Day, rangeStart.Hour, 0, 0);
            DateTime dateEnd = new DateTime(
                rangeEnd.Year, rangeEnd.Month, rangeEnd.Day, rangeEnd.Hour, 59, 59);

            return (dateStart <= dateTime && dateEnd >= dateTime);
        }

        public static bool IsInRangeAtMinuteLevel(this DateTime dateTime, DateTime rangeStart, DateTime rangeEnd)
        {
            DateTime dateStart = new DateTime(
                rangeStart.Year, rangeStart.Month, rangeStart.Day, rangeStart.Hour, rangeStart.Minute, 0);
            DateTime dateEnd = new DateTime(
                rangeEnd.Year, rangeEnd.Month, rangeEnd.Day, rangeEnd.Hour, rangeEnd.Minute, 59);

            return (dateStart <= dateTime && dateEnd >= dateTime);
        }
    }
}
