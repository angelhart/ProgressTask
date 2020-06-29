using AsyncSubmit.Controllers;
using AsyncSubmit.Models;
using AsyncSubmit.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSubmit.Tests.HomeController_Tests
{
    [TestClass]
    public class Post_Should
    {
        [TestMethod]
        public async Task ReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockParser = new Mock<IDataParser>();
            var mockClientFactory = new Mock<IHttpClientFactory>();
            
            var controller = new HomeController(mockLogger.Object, mockParser.Object, mockClientFactory.Object);

            controller.ModelState.AddModelError("Email", "Required");

            // Act
            var result = await controller.Post(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task ReturnErrorStatusCode_WhenModelStateIsValid()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HomeController>>();

            var mockParser = new Mock<IDataParser>();
            mockParser.Setup(x => x.Serialize(It.IsAny<FormViewModel>()))
                      .Returns(new StringContent("[{'email':'email@domain.com'}]"));

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.BadRequest,
                   Content = new StringContent(""),
               })
               .Verifiable();

            var mockClientFactory = new Mock<IHttpClientFactory>();
            mockClientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                      .Returns(new HttpClient(handlerMock.Object));

            var controller = new HomeController(mockLogger.Object, mockParser.Object, mockClientFactory.Object);

            var input = new FormViewModel
            {
                Email = "email@domain.com"
            };

            // Act
            var result = await controller.Post(input);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(400, (result as ObjectResult).StatusCode);
        }

        [TestMethod]
        public async Task ReturnSuccessStatusCode_WhenModelStateIsValid()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HomeController>>();

            var mockParser = new Mock<IDataParser>();
            mockParser.Setup(x => x.Serialize(It.IsAny<FormViewModel>()))
                      .Returns(new StringContent("[{'email':'email@domain.com'}]"));

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(""),
               })
               .Verifiable();

            var mockClientFactory = new Mock<IHttpClientFactory>();
            mockClientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                      .Returns(new HttpClient(handlerMock.Object));

            var controller = new HomeController(mockLogger.Object, mockParser.Object, mockClientFactory.Object);

            var input = new FormViewModel
            {
                Email = "email@domain.com"
            };

            // Act
            var result = await controller.Post(input);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            Assert.AreEqual(200, (result as ObjectResult).StatusCode);
        }
    }
}
