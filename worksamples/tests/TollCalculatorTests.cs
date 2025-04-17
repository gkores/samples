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
        var car = new Car();
        var sut = new TollCalculator(new NoFreeDatesProvider());
        var passageTime = TodayAt(hour, minute);
        
        //Act
        var actual = sut.GetTollFee(car, [passageTime]);
        
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
        var car = new Car();
        var sut = new TollCalculator(new OnlyFreeDatesProvider());
        var passageTime = TodayAt(hour, minute);
        
        //Act
        var actual = sut.GetTollFee(car, [passageTime]);
        
        //Assert
        Assert.Equal(0, actual);
    }

    private static DateTime TodayAt(int hour, int minute) => new (DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hour, minute, 0);

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
