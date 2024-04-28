using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch11_frame;

[TestClass]
public class Ch11FrameTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [TestInitialize]
    public void Before() =>
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/frames.html");

    [TestMethod]
    public void TestFrames()
    {
        driver.SwitchTo().Frame("topNav"); // name of the frame
        driver.FindElement(By.LinkText("Menu 2 in top frame")).Click();

        // need to switch to default before another switch
        driver.SwitchTo().DefaultContent();
        driver.SwitchTo().Frame("menu_frame");
        driver.FindElement(By.LinkText("Green Page")).Click();

        driver.SwitchTo().DefaultContent();
        driver.SwitchTo().Frame("content");
        driver.FindElement(By.LinkText("Back to original page")).Click();
    }

    [TestMethod]
    public void TestFindFrameWithFindElement()
    {
        IWebElement frameElem = driver.FindElement(By.Name("topNav"));
        driver.SwitchTo().Frame(frameElem);

        driver.FindElement(By.LinkText("Menu 2 in top frame")).Click(); // verify switched successfully
    }

    [TestMethod]
    public void TestIFrame()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/iframe.html");
        driver.FindElement(By.Name("user")).SendKeys("agileway");

        driver.SwitchTo().Frame("Frame1"); // name of the frame
        driver.FindElement(By.Name("username")).SendKeys("tester");
        driver.FindElement(By.Name("password")).SendKeys("TestWise");
        driver.FindElement(By.Id("loginBtn")).Click();
        Assert.IsTrue(driver.PageSource.Contains("Signed in"));
        driver.SwitchTo().DefaultContent();
        driver.FindElement(By.Id("accept_terms")).Click();
    }

    [TestMethod]
    public void TestIFrameByIndex()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/iframes.html");
        driver.SwitchTo().Frame(0);
        driver.FindElement(By.Name("username")).SendKeys("agileway");
        driver.SwitchTo().DefaultContent();
        driver.SwitchTo().Frame(1);
        driver.FindElement(By.Id("radio_male")).Click();
    }

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();
}
