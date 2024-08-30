using duma.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace duma.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public void AccountViewDataMessage()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            PartialViewResult result = controller.Login() as PartialViewResult;

            // Assert
            Assert.Equal("Hello world!", result?.ViewData["Message"]);
        }

        [Fact]
        void IndexViewDateNull()
        {
            //Arrange
            AccountController controller = new AccountController();

            //Act
            PartialViewResult result = controller.Login() as PartialViewResult;

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void AccountViewNameEqualIndex()
        {
            // Arrange
            AccountController controller = new AccountController();
            // Act
            PartialViewResult result = controller.Login() as PartialViewResult;
            // Assert
            Assert.Equal("Account", result?.ViewName);
        }
    }
}