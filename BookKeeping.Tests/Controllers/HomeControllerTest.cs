using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookKeeping;
using BookKeeping.Controllers;

namespace BookKeeping.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual("Главная", result.ViewBag.Title);
        }
    }
}
