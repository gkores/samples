using TollCalculator.Providers;

namespace TollCalculator.Tests;

public class TollFreeProviderTests
{
    [Theory]
    [InlineData(5, 59, 0)]
    [InlineData(6, 0, 8)]
    [InlineData(6, 29, 8)]
    [InlineData(6, 30, 13)]
    [InlineData(6, 59, 13)]
    [InlineData(7, 0, 18)]
    [InlineData(7, 59, 18)]
    [InlineData(8, 0, 13)]
    [InlineData(8, 29, 13)]
    [InlineData(8, 30, 8)]
    [InlineData(12, 00, 8)]
    [InlineData(14, 59, 8)]
    [InlineData(15, 00, 13)]
    [InlineData(15, 29, 13)]
    [InlineData(15, 30, 18)]
    [InlineData(16, 59, 18)]
    [InlineData(17, 0, 13)]
    [InlineData(17, 59, 13)]
    [InlineData(18, 0, 8)]
    [InlineData(18, 29, 8)]
    [InlineData(18, 30, 0)]
    public void GivenATime_WhenCalculated_ShouldReturnCorrectFee(int hour, int minute, int expectedFee)
    {
        // Arrange
        var date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, hour, minute, 0);
        var sut = new TollFeeProvider();

        // Act
        var actualFee = sut.GetTollFee(date);

        // Assert
        Assert.Equal(expectedFee, actualFee);
    }
}