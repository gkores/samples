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
}