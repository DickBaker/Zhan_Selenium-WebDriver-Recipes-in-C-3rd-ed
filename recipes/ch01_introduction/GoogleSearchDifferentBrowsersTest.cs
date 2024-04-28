using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace SeleniumRecipesCSharp.ch01_introduction;

[TestClass]
public class GoogleSearchDifferentBrowsersTest
{
    [TestMethod]
    public void TestInFirefox()
    {
        WebDriver driver = new FirefoxDriver();
        driver.Navigate().GoToUrl("https://agileway.com.au/demo");
        Thread.Sleep(1000);
        driver.Quit();
    }

    [TestMethod]
    public void TestInChrome()
    {
        WebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("http://agileway.com.au/demo");
        Thread.Sleep(1000);
        driver.Quit();
    }

    [TestMethod]
    public void TestInSafari()
    {
        WebDriver driver = new SafariDriver();
        driver.Navigate().GoToUrl("http://agileway.com.au/demo");
        Thread.Sleep(1000);
        driver.Quit();
    }

    [TestMethod]
    public void TestInEdge()
    {
        WebDriver driver = new EdgeDriver();
        System.Diagnostics.Debug.Write("Start... ");
        driver.Navigate().GoToUrl("http://travel.agileway.net");
        driver.FindElement(By.Name("username")).SendKeys("agileway");
        IWebElement passwordElem = driver.FindElement(By.Name("password"));
        passwordElem.SendKeys("testwise");
        passwordElem.Submit(); // not implemented 
        Thread.Sleep(1000);
        driver.Quit();
    }
}
