namespace TollCalculator.Providers;

public interface ITollFreeDateProvider
{
    bool IsTollFreeDate(DateTime date);
}