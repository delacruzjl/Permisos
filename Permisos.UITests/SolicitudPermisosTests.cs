using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Permisos.UITests.Pages;
using System;
using System.Diagnostics;
using System.Threading;

namespace Permisos.UITests {
    [TestClass]
    public class SolicitudPermisosTests {
        private IWebDriver _driver;
        [TestInitialize()]
        public void BeforeEach() {
            _driver = _driver.SetupForBrowser("Chrome");
            _driver.Navigate().GoToUrl(Constants.APP_URL);
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
        public void IfISendEmptySpacesShouldNOTEnableSubmit() {
            // arrange          
            var webPage = new SolicitarPermisoWebPage(_driver);

            // act
            webPage.NombresInput.SendKeys(" ");
            webPage.ApellidosInput.SendKeys(LoremNET.Lorem.Words(2));
            webPage.FechaInput.Clear();
            webPage.FechaInput.SendKeys(LoremNET.Lorem.DateTime().ToString("MM/dd/yyyy"));
            webPage.TipoDePermisoSelect.SelectByIndex(LoremNET.Lorem.Random(new[] { 1, 2 }));

            // assert
            Assert.IsFalse(webPage.SolicitarPermisoButton.Enabled);
        }

        [TestMethod, TestCategory("Chrome")]
        public void AfterSavingSolicitudItShowsTheListOfPermisos() {
            // arrange          
            var webPage = new SolicitarPermisoWebPage(_driver);

            // act
            webPage.NombresInput.SendKeys(LoremNET.Lorem.Words(2));
            webPage.ApellidosInput.SendKeys(LoremNET.Lorem.Words(2));
            webPage.FechaInput.Clear();
            webPage.FechaInput.SendKeys(LoremNET.Lorem.DateTime().ToString("MM/dd/yyyy"));
            webPage.TipoDePermisoSelect.SelectByIndex(LoremNET.Lorem.Random(new[] {1, 2 }));

            if (!webPage.SolicitarPermisoButton.Enabled) {
                Assert.Fail("Invalid data was entered, the submit button was not enabled and nothing was created");
            }

            webPage.SolicitarPermisoButton.Submit();

            // assert
            for (var second = 0; ; second++) {
                if (second >= Constants.MINIMUM_DELAY) {
                    Assert.Fail($"timeout! took more than {Constants.MINIMUM_DELAY} seconds to load");
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
    }
}
