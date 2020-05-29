using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Permisos.UITests.Pages {
    public abstract class WebPage {
        protected readonly IWebDriver _driver;
        protected WebPage(IWebDriver driver, By selector) {
            WaitForDriverToLoad(driver, selector);
            _driver = driver;
        }

        public virtual IWebElement HeaderLink =>
            _driver.FindElement(By.CssSelector("a.navbar-brand"));

        public virtual IWebElement SolicitarPermisoLink =>
            _driver.FindElement(By.Id("solicitarPermisoMenuLink"));

        public virtual IWebElement VerPermisosLink =>
            _driver.FindElement(By.Id("verPermisoMenuLink"));

        private static void WaitForDriverToLoad(IWebDriver driver, By selector) {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.MAX_WEBDRIVER_DELAY));
            wait.Until(condition => {
                try {
                    return driver.FindElement(selector).Displayed;
                } catch {
                    return false;
                }
            });
        }
    }
}
