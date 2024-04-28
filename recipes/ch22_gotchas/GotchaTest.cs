using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch22_gotchas;

[TestClass]
public class Ch22GotchaTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [TestInitialize]
    public void Before() =>
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/emberjs-crud-rest/index.html");

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();

    [TestMethod]
    public void TestChangeLogicBasedBrowser()
    {
        ICapabilities caps = driver.Capabilities;
        var driverName = caps.GetType().ToString();
        var browserName = (string)caps.GetCapability("browserName");
        if (browserName == "chrome")
        {
            Console.WriteLine("Browser is Chrome");
            // chrome specific test statement
        }
        else if (browserName == "firefox")
        {
            // firefox specific test statement
            Console.WriteLine("Browser is FireFox");
        }
        else
        {
            throw new Exception("Unsupported browser: " + browserName);
        }
    }
}
