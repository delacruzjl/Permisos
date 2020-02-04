using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Permisos.UITests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permisos.UITests
{
    [TestClass]public class VerPermisosTests
    {
        private IWebDriver _driver;
        private string _appURL;

        [TestInitialize()]
        public void BeforeEach() {
            _appURL = "http://localhost:5000/ver";
            SetupSeleniumDriver("Chrome");
            _driver.Navigate().GoToUrl(_appURL);

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

        //////

        private void SetupSeleniumDriver(string browser) {
            _driver = browser switch
            {
                "Chrome" => new ChromeDriver(),
                _ => new ChromeDriver(),
            };
        }
    }
}
