namespace Magnus.Application.Helpers;

public static class DateTimeHelper
{
    private static readonly TimeZoneInfo BrasiliaTimeZone =
        TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

    public static DateTime NowInBrasilia()
    {
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, BrasiliaTimeZone);
    }
}