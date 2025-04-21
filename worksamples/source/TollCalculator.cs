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
    
    /// <summary>
    /// Calculate the total toll fee for one day
    /// </summary>
    /// <param name="vehicle">the vehicle</param>
    /// <param name="dates">date and time of all passes on one day</param>
    /// <returns>the total toll fee for that day</returns>
    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        // Assuming that the all entries are within the same day, and that the entries are in order
        var intervalStart = dates[0];

        if (_tollFreeVehicleProvider.IsTollFreeVehicle(vehicle)) return 0;
        if (_tollFreeDateProvider.IsTollFreeDate(intervalStart)) return 0;
        
        var totalFee = 0;
        var workingIntervalFees = new List<int>();

        foreach (var date in dates)
        {
            if (intervalStart == date)
            {
                workingIntervalFees.Add(_tollFeeProvider.GetTollFee(date));
                continue;
            }
            
            var diffInMinutes = (date - intervalStart).TotalMinutes;
            if (diffInMinutes > 60.0)
            {
                // Add the max fee of the previous interval and start a new one
                totalFee += workingIntervalFees.Max();
                workingIntervalFees.Clear();
                intervalStart = date;
            }
 
            workingIntervalFees.Add(_tollFeeProvider.GetTollFee(date));
        }
        
        // Add the max fee of the last interval
        totalFee += workingIntervalFees.Max();
        
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }
}