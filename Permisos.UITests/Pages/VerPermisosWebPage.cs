using System.Collections.Generic;
using OpenQA.Selenium;

namespace Permisos.UITests.Pages
{
  public class VerPermisosWebPage : WebPage
  {
    public VerPermisosWebPage(IWebDriver driver) :
      base(driver, By.XPath("//h2[contains(text(),'Listado de permisos')]"))
    {
    }

    public IEnumerable<IWebElement> PermisoListDivs =>
      _driver.FindElements(By.CssSelector(".card"));
  }
}
