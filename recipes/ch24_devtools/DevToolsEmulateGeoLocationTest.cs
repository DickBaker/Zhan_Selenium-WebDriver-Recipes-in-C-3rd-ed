using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

namespace SeleniumRecipes;

[TestClass]
public class Ch24DevToolsEmulateGeoTest
{
    static IWebDriver driver = new ChromeDriver();

    [ClassCleanup]
    public static void AfterAll() {
      if (driver != null)
        driver.Quit();
    }

    [TestMethod]
    public void TestEmulateGeoLocation() {
        IDevTools devTools = driver as IDevTools;

        DevToolsSession devToolsSession = devTools.GetDevToolsSession();
        var geoLocationOverrideCommandSettings = new Emulation.SetGeolocationOverrideCommandSettings
        {
            // set to Sydney Opera House
            Latitude = -33.856159,
            Longitude = 151.215256,
            Accuracy = 1
        };

        // note the version in class 
        var domains = devToolsSession.GetVersionSpecificDomains<Domains>();
        domains.Emulation.SetGeolocationOverride(geoLocationOverrideCommandSettings);
        driver.Navigate().GoToUrl("https://my-location.org/");
        System.Threading.Thread.Sleep(4500);
        Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("Sydney Opera House"));
    }
}