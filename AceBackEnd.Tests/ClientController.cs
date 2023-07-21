﻿using AceBackEnd.Controllers;
using AceBackEnd.Data_Transfer_Objects;
using AceBackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
namespace AceBackEnd.Tests.Controllers
{
    public class LoginControllerTests
    {
        [Fact]
        public void RegisterEndpoint_ValidCredentials_ReturnsOkResult()
        {
            // Arrange
            var mockDbContext = new AceDbContext();
            var controller = new LoginController(mockDbContext);
            var randomName = Guid.NewGuid().ToString("N");
            var dtoObject = new RegisterDTO
            {
                Username = randomName,
                Password = "123noe"
            };

            // Act
            var result = controller.RegisterEndpoint(dtoObject);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void RegisterEndpoint_InvalidCredentials_ReturnsBadRequestResult()
        {
            // Arrange
            var mockDbContext = new AceDbContext();
            var controller = new LoginController(mockDbContext);
            var dtoObject = new RegisterDTO
            {
                Username = "h",
                Password = "d"
            };

            // Act
            var result = controller.RegisterEndpoint(dtoObject);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void LoginEndpoint_ValidCredentials_ReturnsOkResult()
        {
            var mockDbContext = new AceDbContext();
            var controller = new LoginController(mockDbContext);
            var dtoObject = new LoginDTO
            {
                Username = "noel",
                Password = "123noe"
            };

            // Act
            var result = controller.LoginEndpoint(dtoObject);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public void LoginEndpoint_InvalidCredentials_ReturnsBadRequestResult()
        {
            var mockDbContext = new AceDbContext();
            var controller = new LoginController(mockDbContext);
            var dtoObject = new LoginDTO
            {
                Username = "asdfasdwe",
                Password = "asde"
            };

            // Act
            var result = controller.LoginEndpoint(dtoObject);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void FinishRegisterEndpoint_ValidCredentials_ReturnsOkResult()
        {
            var mockDbContext = new AceDbContext();
            var controller = new LoginController(mockDbContext);

            var dtoObject = new FinishProfileDTO
            {
                Fullname = "string",
                Addressone = "string",
                Addresstwo = "string",
                City = "New york",
                State="string",
                Zipcode ="string"
            };

            // Act
            var result = controller.FinishRegistration(dtoObject);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }


        [Fact]
        public void FinishRegisterEndpoint_InvalidCredentials_ReturnsBadRequestResult()
        {

            // Arrange
            var mockDbContext = new AceDbContext();
            var controller = new LoginController(mockDbContext);
            var dtoObject = new FinishProfileDTO
            {
                Fullname = "s",
                Addressone = "l",
                Addresstwo = "m",
                City = "k",
                State = "NY",
                Zipcode = "2"
            };

            // Act
            var result = controller.FinishRegistration(dtoObject);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }



        // Add more test methods for other endpoints...
    }

}
