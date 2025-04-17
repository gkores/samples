namespace TollCalculator.Tests;

public class TollFreeVehicleProviderTests
{
    [Fact]
    public void GivenACar_WhenEvaluated_ShouldReturnFalse()
    {
        // Arrange
        var vehicle = new Car();
        var sut = new TollFreeVehicleProvider();
        
        // Act
        var actual = sut.IsTollFreeVehicle(vehicle);
        
        // Assert
        Assert.False(actual);
    }
    
    [Fact]
    public void GivenAMotorbike_WhenEvaluated_ShouldReturnTrue()
    {
        // Arrange
        var vehicle = new Motorbike();
        var sut = new TollFreeVehicleProvider();
        
        // Act
        var actual = sut.IsTollFreeVehicle(vehicle);
        
        // Assert
        Assert.True(actual);
    }
}