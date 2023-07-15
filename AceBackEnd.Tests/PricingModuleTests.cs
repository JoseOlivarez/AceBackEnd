using Xunit;

public class PricingModuleTests
{
    [Fact]
    public void CalculatePrice_ReturnsTotalAmountDue()
    {
        // Arrange
        PricingService pricingService = new PricingService();
        string clientId = "client1";
        string location = "Texas";
        int gallonsRequested = 500;

        // Act
        double result = pricingService.CalculatePrice(clientId, location, gallonsRequested);

        // Assert
        Assert.Equal(862.5, result); // Corrected expected value
    }

    [Fact]
    public void CalculatePrice_ReturnsZeroForNoGallonsRequested()
    {
        // Arrange
        PricingService pricingService = new PricingService();
        string clientId = "client1";
        string location = "Texas";
        int gallonsRequested = 0;

        // Act
        double result = pricingService.CalculatePrice(clientId, location, gallonsRequested);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculatePrice_ReturnsCorrectTotalAmountDueForTexasLocation()
    {
        // Arrange
        PricingService pricingService = new PricingService();
        string clientId = "client1";
        string location = "Texas";
        int gallonsRequested = 1000;

        // Act
        double result = pricingService.CalculatePrice(clientId, location, gallonsRequested);

        // Assert
        Assert.Equal(1725, result); // Corrected expected value
    }

    [Fact]
    public void CalculatePrice_ReturnsCorrectTotalAmountDueForNonTexasLocation()
    {
        // Arrange
        PricingService pricingService = new PricingService();
        string clientId = "client1";
        string location = "New York";
        int gallonsRequested = 1000;

        // Act
        double result = pricingService.CalculatePrice(clientId, location, gallonsRequested);

        // Assert
        Assert.Equal(1755, result); // Corrected expected value
    }

    [Fact]
    public void CalculatePrice_ReturnsCorrectTotalAmountDueForClientWithRateHistory()
    {
        // Arrange
        PricingService pricingService = new PricingService();
        string clientId = "client2";
        string location = "Texas";
        int gallonsRequested = 1000;

        // Act
        double result = pricingService.CalculatePrice(clientId, location, gallonsRequested);

        // Assert
        Assert.Equal(1725, result); // Corrected expected value
    }

    [Fact]
    public void CalculatePrice_ReturnsCorrectTotalAmountDueForGallonsRequestedGreaterThan1000()
    {
        // Arrange
        PricingService pricingService = new PricingService();
        string clientId = "client1";
        string location = "Texas";
        int gallonsRequested = 1500;

        // Act
        double result = pricingService.CalculatePrice(clientId, location, gallonsRequested);

        // Assert
        Assert.Equal(2565, result); // Corrected expected value
    }
}
