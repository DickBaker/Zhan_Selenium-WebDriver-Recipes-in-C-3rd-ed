
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
// Replace the version to match the Chrome version
using OpenQA.Selenium.DevTools.V115.Security;

namespace SeleniumRecipesCSharp.ch24_devtools;
[TestClass]
public class Ch24DevToolsSecurityTest
{
    static WebDriver driver = default!;

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();

    [TestMethod]
    public void TestIgnoreCertificateErrors()
    {
        driver = new ChromeDriver
        {
            Url = "https://expired.badssl.com"
        };
        Thread.Sleep(1000);
        Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("Your connection is not private"));

        DevToolsSession devToolsSession = ((IDevTools)driver).GetDevToolsSession();
        SetIgnoreCertificateErrorsCommandSettings securitySettings = new()
        {
            Ignore = true
        };
        OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains domains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains>();
        domains.Security.Enable();
        domains.Security.SetIgnoreCertificateErrors(securitySettings);
        driver.Navigate().GoToUrl("https://expired.badssl.com");
        Thread.Sleep(1000);
        Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains("Your connection is not private"));
    }
}