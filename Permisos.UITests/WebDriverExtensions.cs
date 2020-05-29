using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace Permisos.UITests
{
    public static class WebDriverExtensions
    {
        public static IWebDriver SetupForBrowser(this IWebDriver obj, string browser) {
            Debug.WriteLine($"{browser}: browser requested");
            obj = new ChromeDriver();
            return obj;
        }
    }
}
