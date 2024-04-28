using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch23_selenium4;
[TestClass]
public class Ch23SeleniumChromeForBrowserTest
{
    static readonly WebDriver driver = default!;

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();

    [TestMethod]
    public void TestChromeForTesting()
    {
        ChromeOptions options = new()
        {
            BrowserVersion = "116"
        };
        WebDriver driver = new ChromeDriver(options);

        driver.Navigate().GoToUrl("http://www.google.com");
        driver.FindElement(By.Name("q")).SendKeys("Hello Selenium WebDriver!");
        Thread.Sleep(30000);
    }
}