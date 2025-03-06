using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using NUnit.Framework;

[TestFixture]
public class Tests
{
    private IWebDriver driver;
    private WebDriverWait wait;

    [SetUp]
    public void Setup()
    {
        string chromeDriverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Drivers");
        var service = ChromeDriverService.CreateDefaultService(chromeDriverPath);
        var options = new ChromeOptions();

        driver = new ChromeDriver(service, options);

        // **Maximizar ventana**
        driver.Manage().Window.Maximize();

        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    [Test]
    public void TestLogin()
    {
        // Obtener la URL desde el JSON
        string baseUrl = Config.GetBaseUrl();
        driver.Navigate().GoToUrl(baseUrl);

        // Hacer clic en el botón "Iniciar Sesión"
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"navbarSupportedContent\"]/div/a/strong"))).Click();

        // Ingresar usuario y contraseña
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("username"))).SendKeys("ADMNCS01");
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("password"))).SendKeys("12345678.");

        // Hacer clic en el botón de enviar login
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("btnAccederTienda"))).Click();

        // Validar que se haya iniciado sesión correctamente buscando un elemento de la página de usuario
        var elementoConfirmacion = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath("//*[@id=\"navbarSupportedContent\"]/div/div[1]/a[1]/strong")));
        Assert.That(elementoConfirmacion.Displayed, Is.True, "No se encontró el mensaje de bienvenida.");
    }

    [TearDown]
    public void TearDown()
    {
        if (driver != null)
        {
            driver.Quit();
            driver.Dispose();
            driver = null; // Evita que se vuelva a usar un driver eliminado
        }
    }
}
