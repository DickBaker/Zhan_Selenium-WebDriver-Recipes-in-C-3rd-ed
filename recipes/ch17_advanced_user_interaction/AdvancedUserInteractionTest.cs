using System.Collections.ObjectModel;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace SeleniumRecipesCSharp.ch17_advanced_user_interaction;

[TestClass]
public class Ch17AdvancedUserInteractionTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [TestInitialize]
    public void Before() =>
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/html5.html");

    [TestMethod]
    public void TestDoubleClick()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/text_field.html");
        IWebElement elem = driver.FindElement(By.Id("quickfill"));
        Actions builder = new(driver);
        builder.DoubleClick(elem).Perform();

        Thread.Sleep(500);
        Assert.AreEqual("ABC", driver.FindElement(By.Id("pass")).GetAttribute("value"));
    }

    [TestMethod]
    public void TestMouseOver()
    {
        IWebElement elem = driver.FindElement(By.Id("email"));
        Actions builder = new(driver);
        builder.MoveToElement(elem).Perform();
    }

    [TestMethod]
    public void TestClickAndHold()
    {
        driver.Navigate().GoToUrl("http://jqueryui.com/selectable");
        driver.FindElement(By.LinkText("Display as grid")).Click();
        Thread.Sleep(500);
        driver.SwitchTo().Frame(0);
        ReadOnlyCollection<IWebElement> listItems = driver.FindElements(By.XPath("//ol[@id='selectable']/li"));
        Actions builder = new(driver);
        builder.ClickAndHold(listItems[1])
               .ClickAndHold(listItems[3])
               .Click()
               .Perform();
        driver.SwitchTo().DefaultContent();
    }

    public static void TestKeySequenceSelectAllAndDelete()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/text_field.html");
        driver.FindElement(By.Id("comments")).SendKeys("Multiple Line\r\n Text");
        IWebElement elem = driver.FindElement(By.Id("comments"));

        Actions builder = new(driver);
        builder.Click(elem)
               .KeyDown(Keys.Control)
               .SendKeys("a")
               .KeyUp(Keys.Control)
               .Perform();
        // this different from click element, the key is send to browser directly
        builder = new Actions(driver);
        builder.SendKeys(Keys.Backspace).Perform();
    }

    [TestMethod]
    public void TestDragAndDrop()
    {
        //this works OK on Chrome, error on Firefox, IE no effect
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/drag_n_drop.html");
        IWebElement dragFrom = driver.FindElement(By.Id("item_1"));
        IWebElement target = driver.FindElement(By.Id("trash"));

        Actions builder = new(driver);
        IAction dragAndDrop = builder.ClickAndHold(dragFrom)
                .MoveToElement(target)
                .Release(target).Build();

        dragAndDrop.Perform();
    }

    [TestMethod]
    public void TestSlider()
    {
        // this does not working on Firefox, yet
        Assert.AreEqual("15%", driver.FindElement(By.Id("pass_rate")).Text);
        IWebElement elem = driver.FindElement(By.Id("pass-rate-slider"));

        Actions move = new(driver);
        move.DragAndDropToOffset(elem, 30, 0).Perform();
        Assert.AreNotEqual("15%", driver.FindElement(By.Id("pass_rate")).Text);
    }

    [TestMethod]
    public void TestRightClick()
    {
        // the right-click context menu is no longer works on Firefox (Chrome much ealirer)
        // as it becomes native 
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/context-menu/index.html");
        Thread.Sleep(500);
        IWebElement elem = driver.FindElement(By.TagName("body"));

        // below shall fail, the context menu is not present yet
        // driver.FindElement(By.LinkText("WhenWise")).Click();

        Actions builder = new(driver);
        builder.ContextClick(elem).Perform();
        Thread.Sleep(500);

        driver.FindElement(By.LinkText("WhenWise")).Click();
        Thread.Sleep(500);
        Assert.AreEqual(driver.Url, "https://whenwise.com/");
    }
}
