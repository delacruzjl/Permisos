using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Permisos.UITests.Pages;

namespace Permisos.UITests
{
  [TestClass]
  public class MenuTests
  {
    private IWebDriver _driver;

    private string _appURL = Constants.APP_URL;

    [TestInitialize()]
    public void BeforeEach()
    {
      _driver = _driver.SetupForBrowser("Chrome");
      _driver.Navigate().GoToUrl(_appURL);
    }

    [TestCleanup()]
    public void AfterEach()
    {
      _driver.Quit();
      _appURL = $"{Constants.APP_URL}/ver";
    }

    [TestMethod, TestCategory("Chrome")]
    public void PuedeNavegarAVerPermisos()
    {
      // arrange
      var homepage = new SolicitarPermisoWebPage(_driver);

      // act
      homepage.VerPermisosLink.Click();

      // assert
      for (var second = 0; ; second++)
      {
        if (second >= 4)
        {
          Assert
            .Fail("timeout, should not take more than 4 seconds to redirect");
        }

        try
        {
          if (
            _driver
              .FindElements(By
                .XPath("//h2[contains(text(),'Listado de permisos')]"))
              .Any()
          )
          {
            break;
          }
        }
        catch (Exception e)
        {
          throw e;
        }

        Thread.Sleep(1000);
      }
    }

    [TestMethod, TestCategory("Chrome")]
    public void PuedeNavegarASolicitudPermisos()
    {
      // arrange
      var homepage = new SolicitarPermisoWebPage(_driver);

      // act
      homepage.SolicitarPermisoLink.Click();

      // assert
      for (var second = 0; ; second++)
      {
        if (second >= Constants.MINIMUM_DELAY)
        {
          Assert
            .Fail($"timeout, should not take more than " +
            "{Constants.MINIMUM_DELAY} seconds to redirect");
        }

        try
        {
          if (
            _driver
              .FindElements(By
                .XPath("//h2[contains(text(),'Solicitud the Permiso')]"))
              .Any()
          )
          {
            break;
          }
        }
        catch (Exception e)
        {
          throw e;
        }

        Thread.Sleep(1000);
      }
    }

    //////
    private void SetupSeleniumDriver(string browser)
    {
      Debug.Write($"//TODO: {browser}");
      _driver = new ChromeDriver();
    }
  }
}
