
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
// Replace the version to match the Chrome version
using OpenQA.Selenium.DevTools.V115.Emulation;

namespace SeleniumRecipesCSharp.ch24_devtools;
[TestClass]
public class Ch24DevToolsEmulateTimezoneTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();

    [TestMethod]
    public void TestEmulateTimezone()
    {
        SetTimezoneOverrideCommandSettings timezoneSettings = new()
        {
            TimezoneId = "Asia/Tokyo"
        };
        DevToolsSession devToolsSession = ((IDevTools)driver).GetDevToolsSession();
        OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains domains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains>();
        domains.Emulation.SetTimezoneOverride(timezoneSettings);
        driver.Navigate().GoToUrl("https://whatismytimezone.com");
        Thread.Sleep(1000);
        Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("Japan Standard Time"));
    }
}