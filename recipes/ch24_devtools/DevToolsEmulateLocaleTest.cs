
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
// Replace the version to match the Chrome version
using OpenQA.Selenium.DevTools.V115.Emulation;

namespace SeleniumRecipesCSharp.ch24_devtools;
[TestClass]
public class Ch24DevToolsEmulateLocaleTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();

    [TestMethod]
    public void TestEmulateLocale()
    {
        driver.Url = "https://www.localeplanet.com/support/browser.html";
        Thread.Sleep(1000);
        Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains("オーストラリア東部標準時"));

        DevToolsSession devToolsSession = ((IDevTools)driver).GetDevToolsSession();
        SetLocaleOverrideCommandSettings localeSettings = new()
        {
            Locale = "ja_JP"
        };
        OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains domains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains>();
        domains.Emulation.SetLocaleOverride(localeSettings);
        driver.Navigate().GoToUrl("https://www.localeplanet.com/support/browser.html");
        Thread.Sleep(21000);
        Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("オーストラリア東部標準時"));
    }
}