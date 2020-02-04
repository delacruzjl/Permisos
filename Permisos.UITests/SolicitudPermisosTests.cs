using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Permisos.UITests.Pages;
using System;
using System.Threading;

namespace Permisos.UITests {
    [TestClass]
    public class SolicitudPermisosTests {
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

        [TestMethod, TestCategory("Chrome")]
        public void DefaultSolicitudTipoPermisoListExistsOnLoad() {
            // arrange
            var webPage = new SolicitarPermisoWebPage(_driver);

            // act
            var actual = webPage.TipoDePermisoSelect.Options.Count;

            // assert
            Assert.AreEqual(3, actual);
        }

        [TestMethod, TestCategory("Chrome")]
        public void AfterSavingSolicitudItShowsTheListOfPermisos() {
            // arrange          
            var webPage = new SolicitarPermisoWebPage(_driver);

            // act
            webPage.NombresInput.SendKeys(nameof(SolicitudPermisosTests));
            webPage.ApellidosInput.SendKeys(nameof(SolicitudPermisosTests));
            webPage.FechaInput.Clear();
            webPage.FechaInput.SendKeys("1/25/2020");
            webPage.TipoDePermisoSelect.SelectByIndex(1);
            webPage.SolicitarPermisoButton.Submit();

            // assert
            for (var second = 0; ; second++) {
                if (second >= 2) {
                    Assert.Fail("timeout");
                }

                try {
                    if (!_driver.Url.EndsWith("/")) {
                        break;
                    }
                } catch (Exception e) {
                    throw e;
                }

                Thread.Sleep(1000);
            }

            Assert.IsTrue(_driver.Url.EndsWith("/ver"));
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
