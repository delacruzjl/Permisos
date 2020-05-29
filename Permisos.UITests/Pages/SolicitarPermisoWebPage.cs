using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Permisos.UITests.Pages
{
    public class SolicitarPermisoWebPage : WebPage {
        public SolicitarPermisoWebPage(IWebDriver driver) 
            : base(driver, By.XPath("//h2[contains(text(),'Solicitud the Permiso')]")) {
        }

        public IWebElement NombresInput =>
            _driver.FindElement(By.Name("nombreEmpleado"));

        public IWebElement ApellidosInput =>
            _driver.FindElement(By.Name("apellidosEmpleado"));

        public SelectElement TipoDePermisoSelect {
            get {
                var tipoPermisoElement = _driver.FindElement(By.Id("tipoPermisoId"));
                return new SelectElement(tipoPermisoElement);
            }
        }
        public IWebElement FechaInput =>
            _driver.FindElement(By.Name("fechaPermiso"));

        public IWebElement SolicitarPermisoButton =>
            _driver.FindElement(By.CssSelector("button[type='submit']"));

       

    }
}
