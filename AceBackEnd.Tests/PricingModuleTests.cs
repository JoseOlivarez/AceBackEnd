using AceBackEnd.Controllers;
using AceBackEnd.Data_Transfer_Objects;
using AceBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

public class PricingModuleTests
{
    [Fact]
    public async void CalculatePrice_ReturnsTotalAmountDue()
    {
        // Arrange
        var mockDbContext = new AceDbContext();

        PricingService pricingService = new PricingService(mockDbContext);
        string clientId = "client1";
        string location = "Texas";
        int gallonsRequested = 500;
        int l = 1;
        // Act
        double[] result = await pricingService.CalculatePrice(l, location, gallonsRequested);

        // Assert
        Assert.Equal(862.5, result[1]); // Corrected expected value
    }

    [Fact]
    public async void CalculatePrice_ReturnsZeroForNoGallonsRequested()
    {
        var mockDbContext = new AceDbContext();

        PricingService pricingService = new PricingService(mockDbContext);
        int clientId = 1;
        string location = "Texas";
        int gallonsRequested = 0;

        // Act
        double[] result = await pricingService.CalculatePrice(clientId, location, gallonsRequested);

        //    // Assert
        Assert.Equal(0.0, result[1]);
    }

    [Fact]
    public async void CalculatePrice_ReturnsCorrectTotalAmountDueForTexasLocation()
    {
        // Arrange
        var mockDbContext = new AceDbContext();

        PricingService pricingService = new PricingService(mockDbContext);
        int clientId = 1;
        string location = "Texas";
        int gallonsRequested = 1000;

        //    // Act
        double[] result = await pricingService.CalculatePrice(clientId, location, gallonsRequested);

        // Assert
        Assert.Equal(1725, result[1]); // Corrected expected value
    }

      [Fact]
     public async void CalculatePrice_ReturnsCorrectTotalAmountDueForNonTexasLocation()
     {
       //    // Arrange
         var mockDbContext = new AceDbContext();

         PricingService pricingService = new PricingService(mockDbContext);
          int clientId = 1;
         string location = "New York";
        int gallonsRequested = 1000;

       // Act
        double[] result =   await pricingService.CalculatePrice(clientId, location, gallonsRequested);

       // Assert
       Assert.Equal(1755, result[1]); // Corrected expected value
    }

    [Fact]
    public async void CalculatePrice_ReturnsCorrectTotalAmountDueForClientWithRateHistory()
    {
        // Arrange
        var mockDbContext = new AceDbContext();

        PricingService pricingService = new PricingService(mockDbContext);
        int clientId = 1;
        string location = "Texas";
        int gallonsRequested = 1000;

        // Act
        double[] result = await pricingService.CalculatePrice(clientId, location, gallonsRequested);

        // Assert
        Assert.Equal(1725, result[1]); // Corrected expected value
    }

    [Fact]
    public async void CalculatePrice_ReturnsCorrectTotalAmountDueForGallonsRequestedGreaterThan1000()
    {
        var mockDbContext = new AceDbContext();

        // Arrange
        PricingService pricingService = new PricingService(mockDbContext);
        int clientId = 1;
        string location = "Texas";
        int gallonsRequested = 1500;

        // Act
        double[] result = await pricingService.CalculatePrice(clientId, location, gallonsRequested);

        // Assert
        Assert.Equal(2565, result[1]); // Corrected expected value
                                    //}
    }
}
