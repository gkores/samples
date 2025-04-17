using TollCalculator.Vehicles;

namespace TollCalculator.Providers;

public interface ITollFreeVehicleProvider
{
    bool IsTollFreeVehicle(IVehicle vehicle);
}