
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
// Replace the version to match the Chrome version
using OpenQA.Selenium.DevTools.V115.Emulation;

namespace SeleniumRecipesCSharp.ch24_devtools;
[TestClass]
public class Ch24DevToolsEmulateGeoTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();

    [TestMethod]
    public void TestEmulateGeoLocation()
    {
        DevToolsSession devToolsSession = ((IDevTools)driver).GetDevToolsSession();
        SetGeolocationOverrideCommandSettings geoLocationOverrideCommandSettings = new()
        {
            // set to Sydney Opera House
            Latitude = -33.856159,
            Longitude = 151.215256,
            Accuracy = 1
        };

        // note the version in class 
        OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains domains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains>();
        domains.Emulation.SetGeolocationOverride(geoLocationOverrideCommandSettings);
        driver.Navigate().GoToUrl("https://my-location.org/");
        Thread.Sleep(4500);
        Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("Sydney Opera House"));
    }
}