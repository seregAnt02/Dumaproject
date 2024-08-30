using duma.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace duma.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexViewDataMessage()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            PartialViewResult result = controller.Index() as PartialViewResult;

            // Assert
            Assert.Equal("Hello world!", result?.ViewData["Message"]);
        }

        [Fact]
        void IndexViewDateNull()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            PartialViewResult result = controller.Index() as PartialViewResult;

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IndexViewNameEqualIndex()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            PartialViewResult result = controller.Index() as PartialViewResult;
            // Assert
            Assert.Equal("Index", result?.ViewName);
        }

    }
}