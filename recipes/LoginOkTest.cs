using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp;
[TestClass]
public class SeleniumLoginOkTest
{
    WebDriver driver = default!;

    [TestMethod]
    public void TestLoginOKOnly()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://travel.agileway.net");
        driver.FindElement(By.Name("username")).SendKeys("agileway");
        driver.FindElement(By.Name("password")).SendKeys("testwise");
        driver.FindElement(By.Name("password")).Submit();
        Thread.Sleep(1000);
        Assert.IsTrue(driver.PageSource.Contains("Signed in!"));
        driver.Quit();
    }
}