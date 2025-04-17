namespace TollCalculator.Providers;

public class TollFeeProvider: ITollFeeProvider
{
    public int GetTollFee(DateTime date)
    {
        var hour = date.Hour;
        var minute = date.Minute;

        if (hour == 6 && minute is >= 0 and <= 29) return 8;
        if (hour == 6 && minute is >= 30 and <= 59) return 13;
        if (hour == 7 && minute is >= 0 and <= 59) return 18;
        if (hour == 8 && minute is >= 0 and <= 29) return 13;
        if (hour == 8 && minute is >= 30 and <= 59) return 8;
        if (hour is >= 9 and <= 14 && minute <= 59) return 8;
        if (hour == 15 && minute is >= 0 and <= 29) return 13;
        if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        if (hour == 17 && minute is >= 0 and <= 59) return 13;
        if (hour == 18 && minute is >= 0 and <= 29) return 8;
        return 0;
    }
}