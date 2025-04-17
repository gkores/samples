namespace TollCalculator;

public class TollFreeDateProvider: ITollFreeDateProvider
{
    public bool IsTollFreeDate(DateTime date)
    {
        var year = date.Year;
        var month = date.Month;
        var day = date.Day;

        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) return true;

        // TODO: This data should probably be input to the program or fetched from a suitable service
        if (year != 2013) return false;
        
        return month == 1 && day == 1 ||
               month == 3 && day is 28 or 29 ||
               month == 4 && day is 1 or 30 ||
               month == 5 && day is 1 or 8 or 9 ||
               month == 6 && day is 5 or 6 or 21 ||
               month == 7 ||
               month == 11 && day == 1 ||
               month == 12 && day is 24 or 25 or 26 or 31;
    }
}