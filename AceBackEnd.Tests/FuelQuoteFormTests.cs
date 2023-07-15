using System;
using System.Collections.Generic;
using AceBackEnd.Controllers;
using AceBackEnd.Data_Transfer_Objects;
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
                var controller = new FuelQuoteFormController();
                var result = controller.FuelQuoteForm();
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnValue = Assert.IsType<FuelQuoteFormDTO>(okResult.Value);

                Assert.Equal(returnValue.amount, returnValue.gallonsRequested * 2.8);


            }
        
    }
}

