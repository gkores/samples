using TollCalculator.Providers;
using TollCalculator.Vehicles;

namespace TollCalculator.Tests;

public class TollCalculatorTests
{
    [Theory]
    [InlineData(5,59,0)]
    [InlineData(6,0,8)]
    [InlineData(6,29,8)]
    [InlineData(6,30,13)]
    [InlineData(6,59,13)]
    [InlineData(7,0,18)]
    [InlineData(7,59,18)]
    [InlineData(8,0,13)]
    [InlineData(8,29,13)]
    [InlineData(8,30,8)]
    [InlineData(12,00,8)]
    [InlineData(14,59,8)]
    [InlineData(15,00,13)]
    [InlineData(15,29,13)]
    [InlineData(15,30,18)]
    [InlineData(16,59,18)]
    [InlineData(17,0,13)]
    [InlineData(17,59,13)]
    [InlineData(18,0,8)]
    [InlineData(18,29,8)]
    [InlineData(18,30,0)]
    public void GivenSingleCarPass_WhenCalculated_ShouldReturnCorrectRate(int hour, int minute, int expected)
    {
        //Arrange
        var vehicle = new Car();
        var sut = new TollCalculator(new NoFreeDatesProvider(), new NoFreeVehiclesProvider(), new TollFeeProvider());
        var passageTime = TodayAt(hour, minute);
        
        //Act
        var actual = sut.GetTollFee(vehicle, [passageTime]);
        
        //Assert
        Assert.Equal(expected, actual);
    }
    
    
    [Theory]
    [InlineData(5,59)]
    [InlineData(6,0)]
    [InlineData(8,30)]
    [InlineData(15,29)]
    [InlineData(18,0)]
    [InlineData(18,30)]
    public void GivenOnlyTollFreeDates_WhenCalculated_ShouldReturnCorrectRate(int hour, int minute)
    {
        //Arrange
        var vehicle = new Car();
        var sut = new TollCalculator(new OnlyFreeDatesProvider(), new NoFreeVehiclesProvider(), new TollFeeProvider());
        var passageTime = TodayAt(hour, minute);
        
        //Act
        var actual = sut.GetTollFee(vehicle, [passageTime]);
        
        //Assert
        Assert.Equal(0, actual);
    }

    [Fact]
    public void GivenMultipleCarPasses_WhenCalculated_ShouldMaxOutRateAt60()
    {
        //Arrange
        var vehicle = new Car();
        var sut = new TollCalculator(new NoFreeDatesProvider(), new NoFreeVehiclesProvider(), new TollFeeProvider());
        DateTime[] passageTimes = [TodayAt(6, 30), TodayAt(7, 50), TodayAt(15, 0), TodayAt(16, 10), TodayAt(17, 20), TodayAt(18, 25)];
        
        //Act
        var actual = sut.GetTollFee(vehicle, passageTimes);
        
        //Assert
        Assert.Equal(60, actual);
    }
    
    [Fact]
    public void GivenMultipleCarPasses_WhenWithin60Minutes_ShouldOnlyPayHighestSingleFee()
    {
        //Arrange
        var vehicle = new Car();
        var sut = new TollCalculator(new NoFreeDatesProvider(), new NoFreeVehiclesProvider(), new TollFeeProvider());
        DateTime[] passageTimes = [TodayAt(6, 29), TodayAt(6, 50), TodayAt(7, 19)];
        
        //Act
        var actual = sut.GetTollFee(vehicle, passageTimes);
        
        //Assert
        Assert.Equal(18, actual);
    }

    private static DateTime TodayAt(int hour, int minute) => new (DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hour, minute, 0);

    #region Custom ITollFreeVehicleProvider implementaions
    
    private class NoFreeVehiclesProvider : ITollFreeVehicleProvider
    {
        public bool IsTollFreeVehicle(IVehicle vehicle) => false;
    }
    
    #endregion    
    
    #region Custom ITollFreeDateProvider implementaions
    
    private class NoFreeDatesProvider : ITollFreeDateProvider
    {
        public bool IsTollFreeDate(DateTime date) => false;
    }
    
    private class OnlyFreeDatesProvider : ITollFreeDateProvider
    {
        public bool IsTollFreeDate(DateTime date) => true;
    }
    
    #endregion
}
