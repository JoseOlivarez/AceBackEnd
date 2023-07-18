using System;
using System.Collections.Generic;
using AceBackEnd.Controllers;
using AceBackEnd.Data_Transfer_Objects;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AceBackEnd.Tests
{
    public class FuelQuoteHistoryControllerTests
    {
        [Fact]
        public void GetFuelQuoteHistory_ReturnsCorrectData()
        {
            // Arrange
            var controller = new FuelQuoteHistoryController();

            // Act
            var result = controller.GetFuelQuoteHistory();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<FuelQuoteHistoryDTO>>(okResult.Value);
            
            // Assert the count
            Assert.Equal(15, returnValue.Count);
            
            // Assert data integrity and correctness
            for (int i = 0; i < returnValue.Count; i++)
            {
                var item = returnValue[i];

                Assert.Equal(i + 1, item.Id);
                Assert.Equal(150 + i + 1, item.GallonsRequested);
                Assert.Equal($"123 Main St, Anywhere, USA {i + 1}", item.DeliveryAddress);
                Assert.Equal(new DateTime(2023, 7, 1).AddDays(i + 1), item.DeliveryDate);
                Assert.Equal(2.50m, item.SuggestedPrice);
                Assert.Equal((150 + i + 1) * 2.50m, item.TotalAmountDue);
            }
        }
    }
}
