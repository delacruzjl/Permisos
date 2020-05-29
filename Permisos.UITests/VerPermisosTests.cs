using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Permisos.UITests.Pages;
using System.Linq;

namespace Permisos.UITests {
    [TestClass]public class VerPermisosTests
    {
        private IWebDriver _driver;

        [TestInitialize()]
        public void BeforeEach() {
            _driver  = _driver.SetupForBrowser("Chrome");
            _driver.Navigate().GoToUrl($"{Constants.APP_URL}/ver");
        }

        [TestCleanup()]
        public void AfterEach() {
            _driver.Quit();
        }

        [TestMethod, TestCategory("Chrome")]
        public void CanLoadVerPermisos() {
            // arrange
            var webPage = new VerPermisosWebPage(_driver);

            // act
            var actual = webPage.PermisoListDivs.Any();

            // assert
            Assert.IsTrue(actual);
        }
    }
}
