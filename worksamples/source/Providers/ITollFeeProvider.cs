using TollCalculator.Vehicles;

namespace TollCalculator.Providers;

public interface ITollFeeProvider
{
    int GetTollFee(DateTime date);
}