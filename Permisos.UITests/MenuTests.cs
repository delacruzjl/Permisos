using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Permisos.UITests.Pages;
using System;
using System.Linq;
using System.Threading;

namespace Permisos.UITests {
    [TestClass]public class MenuTests
    {
        private IWebDriver _driver;
        private string _appURL;

        [TestInitialize()]
        public void BeforeEach() {
            _appURL = "http://localhost:5000/";
            SetupSeleniumDriver("Chrome");
            _driver.Navigate().GoToUrl(_appURL);
        }

        [TestCleanup()]
        public void AfterEach() {
            _driver.Quit();
        }

        [TestMethod]
        public void PuedeNavegarAVerPermisos() {
            // arrange
            var homepage = new SolicitarPermisoWebPage(_driver);
            // act
            homepage.VerPermisosLink.Click();

            // assert
            for (var second = 0; ; second++) {
                if (second >= 2) {
                    Assert.Fail("timeout, should not take more than 2 seconds to redirect");
                }

                try {
                    if (_driver.FindElements(By.CssSelector(".card")).Any()) {
                        break;
                    }
                } catch (Exception e) {
                    throw e;
                }

                Thread.Sleep(1000);
            }
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
