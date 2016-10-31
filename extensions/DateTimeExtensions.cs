using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class DateTimeExtensions
{
    public static DateTime ToEpochByMilliseconds(this long time)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(time);
    }

    public static DateTime ToEpochByMilliseconds(this int time)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(time);
    }

    public static int ToUnixTimestamp(this DateTime date)
    {
        var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var diff = date.ToUniversalTime() - origin;
        return (int)diff.TotalSeconds;
    }
}
