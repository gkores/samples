namespace TollCalculator;

public interface ITollFreeVehicleProvider
{
    bool IsTollFreeVehicle(IVehicle vehicle);
}