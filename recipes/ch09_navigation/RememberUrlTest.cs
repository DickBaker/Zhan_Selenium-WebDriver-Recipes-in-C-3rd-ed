using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch09_navigation;

[TestClass]
public class Ch09RememberUrlTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [TestInitialize]
    public void Before() =>
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/index.html");

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();

    [TestMethod]
    public void TestRememberUrl()
    {
        string url = driver.Url;
        driver.FindElement(By.LinkText("Button")).Click();
        //...
        driver.Navigate().GoToUrl(url);
    }
}
