namespace Application;

public static class DateExtension
{   
    /// <summary>
    /// Set Date with requester time zone 
    /// </summary>
    /// <param name="utcDate"></param>
    /// <returns></returns>
    public static DateTime GetClientLocalDate(this DateTime utcDate)
    {
        string timezoneId = HttpContextHelper.HttpContextAccessor?.HttpContext.Request.Headers["TimezoneId"]!;
        TimeZoneInfo destinationTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezoneId);
        return TimeZoneInfo.ConvertTimeFromUtc(utcDate, destinationTimeZone);
    }
}
