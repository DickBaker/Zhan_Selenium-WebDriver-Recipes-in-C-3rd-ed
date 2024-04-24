using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

namespace SeleniumRecipes;

[TestClass]
public class Ch24DevToolsEmulateLocaleTest
{
    static IWebDriver driver = new ChromeDriver();

    [ClassCleanup]
    public static void AfterAll() {
      if (driver != null)
        driver.Quit();
    }

   [TestMethod]
    public void TestEmulateLocale() {
        driver.Url = "https://www.localeplanet.com/support/browser.html";
        System.Threading.Thread.Sleep(1000);
        Assert.IsFalse(driver.FindElement(By.TagName("body")).Text.Contains("オーストラリア東部標準時"));

        IDevTools devTools = driver as IDevTools;
        DevToolsSession devToolsSession = devTools.GetDevToolsSession();
        var localeSettings = new Emulation.SetLocaleOverrideCommandSettings();
        localeSettings.Locale = "ja_JP";
        var domains = devToolsSession.GetVersionSpecificDomains<Domains>();
        domains.Emulation.SetLocaleOverride(localeSettings);
        driver.Navigate().GoToUrl("https://www.localeplanet.com/support/browser.html");
        System.Threading.Thread.Sleep(21000);
        Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("オーストラリア東部標準時"));
    }
}