using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace SeleniumRecipesCSharp.ch27_case_studies;

[TestClass]
public class Ch27DrawCanvasTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [TestMethod]
    public void TestDrawCanvas()
    {
        driver.Manage().Window.Size = new System.Drawing.Size(1156, 764);
        driver.Navigate().GoToUrl("https://whenwise.agileway.net/reset");
        driver.FindElement(By.Id("email")).SendKeys("physio@biz.com");
        driver.FindElement(By.Id("password")).SendKeys("test01");
        driver.FindElement(By.Id("login-btn")).Click();
        Thread.Sleep(500);

        driver.Navigate().GoToUrl("https://whenwise.agileway.net/work_orders/1");
        driver.FindElement(By.Id("work-charts")).Click();
        Thread.Sleep(1000);

        IWebElement elem = driver.FindElement(By.TagName("canvas"));

        // add the a circle shape
        driver.FindElement(By.Id("drawing-circle")).Click();
        Thread.Sleep(1000);

        // move it to the back neck position
        Actions builder = new(driver);
        builder.MoveToLocation(elem.Location.X + 40, elem.Location.Y + 40);
        builder.ClickAndHold();
        builder.MoveByOffset(450, 60);
        builder.Release();
        builder.Perform();

        // draw X
        driver.FindElement(By.Id("drawing-mode")).Click();
        Thread.Sleep(500);

        builder = new Actions(driver);
        builder.MoveToLocation(elem.Location.X + 50, elem.Location.Y + 50);
        builder.ClickAndHold();
        builder.MoveByOffset(50, 50);
        builder.Release();
        builder.MoveToLocation(elem.Location.X + 100, elem.Location.Y + 50);
        builder.ClickAndHold();
        builder.MoveByOffset(-50, +50);
        builder.Release();
        builder.Perform();

        // add Text
        driver.FindElement(By.Id("drawing-text")).Click();
        Thread.Sleep(500);

        IWebElement elemTextBox = driver.FindElement(By.XPath("//div[@class='modal open']//input[@type='text' and contains(@id, 'modal-input-')]"));
        elemTextBox.SendKeys("Selenium WebDriver is the best!");
        driver.FindElement(By.XPath("//div[@class='modal open']/div[@class='modal-footer']/a[@data-name='confirm']")).Click();
    }

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();
}