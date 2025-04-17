using TollCalculator.Providers;
using TollCalculator.Vehicles;

namespace TollCalculator;

public class TollCalculator
{
    private readonly ITollFreeDateProvider _tollFreeDateProvider;
    private readonly ITollFreeVehicleProvider _tollFreeVehicleProvider;
    private readonly ITollFeeProvider _tollFeeProvider;

    public TollCalculator(ITollFreeDateProvider tollFreeDateProvider, ITollFreeVehicleProvider tollFreeVehicleProvider, ITollFeeProvider tollFeeProvider)
    {
        _tollFreeDateProvider = tollFreeDateProvider;
        _tollFreeVehicleProvider = tollFreeVehicleProvider;
        _tollFeeProvider = tollFeeProvider;
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        // Assuming that the all entries are within the same day, and that the entries are in order
        DateTime intervalStart = dates[0];

        if (_tollFreeVehicleProvider.IsTollFreeVehicle(vehicle)) return 0;
        if (_tollFreeDateProvider.IsTollFreeDate(intervalStart)) return 0;
        
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = _tollFeeProvider.GetTollFee(date);
            int tempFee = _tollFeeProvider.GetTollFee(intervalStart);

            var diffInMinutes = (date - intervalStart).TotalMinutes;
            if (diffInMinutes <= 60.0)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }
}