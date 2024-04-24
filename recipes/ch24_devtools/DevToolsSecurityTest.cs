using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

namespace SeleniumRecipes;

[TestClass]
public class Ch24DevToolsSecurityTest
{
    static IWebDriver driver = new ChromeDriver();

    [ClassCleanup]
    public static void AfterAll() {
      if (driver != null)
        driver.Quit();
    }

   [TestMethod]
    public void TestIgnoreCertificateErrors() {
        driver.Url = "https://expired.badssl.com";
        System.Threading.Thread.Sleep(1000);
        Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("Your connection is not private"));

        IDevTools devTools = driver as IDevTools;
        DevToolsSession devToolsSession = devTools.GetDevToolsSession();
        var securitySettings = new Security.SetIgnoreCertificateErrorsCommandSettings();
        securitySettings.Ignore = true;
        var domains = devToolsSession.GetVersionSpecificDomains<Domains>();
        domains.Security.Enable();
        domains.Security.SetIgnoreCertificateErrors(securitySettings);
        driver.Navigate().GoToUrl("https://expired.badssl.com");
        System.Threading.Thread.Sleep(1000);
        Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains("Your connection is not private"));
    }
}