using System;
using System.Collections.Generic;
using AceBackEnd.Controllers;
using AceBackEnd.Data_Transfer_Objects;
using AceBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace AceBackEnd.Tests
{
    public class FuelQuoteFormTests
    {
      
            [Fact]
            public void GetFuelQuoteForm_ReturnsCorrectData()
            {
            var mockDbContext = new AceDbContext();

            var controller = new FuelQuoteFormController(mockDbContext);
                var result = controller.FuelQuoteForm();
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnValue = Assert.IsType<FuelQuoteFormDTO>(okResult.Value);

                Assert.Equal(returnValue.amount, returnValue.gallonsRequested * 2.8);


            }

            [Fact]
            public void SubmitPurchaseFuelQuote_ReturnsCorrectData()
            {
                var mockDbContext = new AceDbContext();

                var controller = new FuelQuoteFormController(mockDbContext);

                var dtoObject = new FuelQuoteFormPurchaseDTO
                {
                    gallonsRequested = 8000,
                    dateYear = 2023,
                    dateMonth = 8,
                    dateDay = 6,
                    pricePerGallon = 1.71,
                    deliveryAddress = "123 aoeu st",
                    fuelQuoteTotal = 13680,
                    amount = 13680,
                    clientId = 54
                };

                var result = controller.GetFuelQuotePrice(dtoObject);

                // Assert
                Assert.IsType<OkObjectResult>(result);
            }
        
    }
}

