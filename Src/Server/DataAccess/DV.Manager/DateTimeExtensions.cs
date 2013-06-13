using System;

namespace DV.Manager
{
    //Need to move this to Core proj
    public static class DateTimeExtensions
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public static DateTime GetIndianTIme(this DateTime dt)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dt.ToUniversalTime(), INDIAN_ZONE);
        }    
    }
    
}
