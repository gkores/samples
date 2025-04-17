using TollCalculator.Providers;

namespace TollCalculator.Tests;

public class TollFreeDatesProviderTests
{
    [Fact]
    public void GivenASaturday_WhenEvaluated_ThenProviderShouldReturnTrue()
    {
        //Arrange
        var sut = new TollFreeDateProvider();
        var knownSaturday = new DateTime(2025, 4, 19);

        //Act
        var actual = sut.IsTollFreeDate(knownSaturday);

        //Assert
        Assert.True(actual);
    }
    
    [Fact]
    public void GivenASunday_WhenEvaluated_ThenProviderShouldReturnTrue()
    {
        //Arrange
        var sut = new TollFreeDateProvider();
        var knownSunday = new DateTime(2025, 4, 20);

        //Act
        var actual = sut.IsTollFreeDate(knownSunday);

        //Assert
        Assert.True(actual);
    }
    
    [Theory]
    [InlineData(14)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(17)]
    [InlineData(18)]
    public void GivenARegularWeekday_WhenEvaluated_ThenProviderShouldReturnFalse(int day)
    {
        //Arrange
        var sut = new TollFreeDateProvider();
        var knownWeekday = new DateTime(2025, 4, day);

        //Act
        var actual = sut.IsTollFreeDate(knownWeekday);

        //Assert
        Assert.False(actual);
    }
    
    [Theory]
    [InlineData(2013,1,1)]
    [InlineData(2013,3,28)]
    [InlineData(2013,3,29)]
    [InlineData(2013,4,1)]
    [InlineData(2013,4,30)]
    [InlineData(2013,5,1)]
    [InlineData(2013,5,8)]
    [InlineData(2013,5,9)]
    [InlineData(2013,6,5)]
    [InlineData(2013,6,6)]
    [InlineData(2013,6,21)]
    [InlineData(2013,7,5)]
    [InlineData(2013,7,20)]
    [InlineData(2013,11,1)]
    [InlineData(2013,12,24)]
    [InlineData(2013,12,25)]
    [InlineData(2013,12,26)]
    [InlineData(2013,12,31)]
    public void GivenATollFreeDate_WhenEvaluated_ThenProviderShouldReturnTrue(int year, int month, int day)
    {
        //Arrange
        var sut = new TollFreeDateProvider();
        var knownTollFreeDate = new DateTime(year, month, day);

        //Act
        var actual = sut.IsTollFreeDate(knownTollFreeDate);

        //Assert
        Assert.True(actual);
    }
}