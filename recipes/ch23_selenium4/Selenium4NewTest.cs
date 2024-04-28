using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch23_selenium4;
[TestClass]
public class Ch23Selenium4NewTest
{
    static WebDriver? driver;

    [TestCleanup]
    public void After()
    {
        if (driver != null)
        {
            driver.Quit();
            driver = null;
        }
    }

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();

    [TestMethod]
    public void TestScreenshotElement()
    {
        WebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/coupon.html");
        driver.FindElement(By.Id("get_coupon_btn")).Click();
        Thread.Sleep(1000);
        WebElement elem = (WebElement)driver.FindElement(By.Id("details"));
        Screenshot screenshot = elem.GetScreenshot();
        screenshot.SaveAsFile("/tmp/coupon.png", ScreenshotImageFormat.Png);
    }

    [TestMethod]
    public void TestElementLocation()
    {
        WebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/image-map.html");
        IWebElement elem = driver.FindElement(By.Id("agileway_software"));
        Console.WriteLine(elem.Location.X);
        Console.WriteLine(elem.Location.Y);
        Console.WriteLine(elem.Size.Height);
        Console.WriteLine(elem.Size.Width);

        // puts elem.attribute("src")  # v3
        Assert.AreEqual("images/agileway_software.png", elem.GetDomAttribute("src"));
    }
}