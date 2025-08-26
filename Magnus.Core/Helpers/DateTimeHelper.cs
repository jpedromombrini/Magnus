namespace Magnus.Core.Helpers;

public static class DateTimeHelper
{
    private static readonly TimeZoneInfo BrasiliaTimeZone;

    static DateTimeHelper()
    {
        var timeZoneId = OperatingSystem.IsWindows()
            ? "E. South America Standard Time"
            : "America/Sao_Paulo";

        BrasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }

    public static DateTime NowInBrasilia(DateTime? date = null)
    {
        var utcDate = date ?? DateTime.UtcNow;

        utcDate = utcDate.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(utcDate, DateTimeKind.Utc)
            : utcDate.ToUniversalTime();

        return TimeZoneInfo.ConvertTimeFromUtc(utcDate, BrasiliaTimeZone);
    }
}