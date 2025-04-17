namespace TollCalculator;

public interface ITollFreeDateProvider
{
    bool IsTollFreeDate(DateTime date);
}