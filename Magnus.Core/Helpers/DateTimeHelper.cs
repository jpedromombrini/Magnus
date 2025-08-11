namespace Magnus.Core.Helpers;

public static class DateTimeHelper
{
    private static readonly TimeZoneInfo BrasiliaTimeZone =
        TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

    public static DateTime NowInBrasilia(DateTime? date = null)
    {
        var utcDate = date ?? DateTime.UtcNow;

        if (utcDate.Kind == DateTimeKind.Unspecified)
            utcDate = DateTime.SpecifyKind(utcDate, DateTimeKind.Utc);
        else
            utcDate = utcDate.ToUniversalTime();

        return TimeZoneInfo.ConvertTimeFromUtc(utcDate, BrasiliaTimeZone);
    }
}