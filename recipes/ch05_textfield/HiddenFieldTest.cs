using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch05_textfield;

[TestClass]
public class Ch05HiddenFieldTest
{
    static WebDriver driver = default!;

    [ClassInitialize]
    public static void BeforeAll(TestContext context)
    {
        ChromeOptions options = new();
        options.AddArgument("--headless");
        driver = new ChromeDriver(options);
    }

    [TestInitialize]
    public void Before() =>
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/text_field.html");

    [TestMethod]
    public void TestSetAndAssertHiddenField()
    {
        IWebElement theHiddenElem = driver.FindElement(By.Name("currency"));
        Assert.AreEqual("USD", theHiddenElem.GetAttribute("value"));
        driver.ExecuteScript("arguments[0].value = 'AUD';", theHiddenElem);
        Assert.AreEqual("AUD", theHiddenElem.GetAttribute("value"));
    }

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();
}