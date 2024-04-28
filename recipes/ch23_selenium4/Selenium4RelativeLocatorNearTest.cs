
using System.Collections.ObjectModel;
using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch23_selenium4;
[TestClass]
public class Ch23Selenium4RelativeLocatorNearTest
{
    static WebDriver driver = default!;
    static readonly string[] ExpectedPrevNext = new string[] { "Prev", "Next" };
    static readonly string[] ExpectedPrevNextFirst = new string[] { "Prev", "Next", "First" };
    static readonly string[] ExpectedPrevNextFirst3urls = new string[] { "Prev", "Next", "First", "https://whenwise.com", "http://sitewisecms.com", "https://testwisely.com/testwise" };

    [ClassInitialize]
    public static void BeforeAll(TestContext context)
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/relative.html");
    }

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();

    [TestMethod]
    public void TestRelativeNear()
    {
        IWebElement start_cell = driver.FindElement(By.Id("current-page"));
        ReadOnlyCollection<IWebElement> allLinks = driver.FindElements(RelativeBy.WithLocator(By.TagName("a")));
        Assert.AreEqual(9, allLinks.Count);

        // TODO: System.ArgumentNullException: 
        // not returning C# on macOS, not  C# on Windows, 
        // but works fine on Ruby
        ReadOnlyCollection<IWebElement> neighbours = driver.FindElements(RelativeBy.WithLocator(By.TagName("a")).Near(start_cell));
        Assert.AreEqual(2, neighbours.Count);
        string[] cell_texts = neighbours.Select(s => s.Text).ToArray();
        CollectionAssert.AreEqual(ExpectedPrevNext, cell_texts);

        neighbours = driver.FindElements(RelativeBy.WithLocator(By.TagName("a")).Near(start_cell, 20));
        cell_texts = neighbours.Select(s => s.Text).ToArray();
        CollectionAssert.AreEqual(ExpectedPrevNext, cell_texts);

        neighbours = driver.FindElements(RelativeBy.WithLocator(By.TagName("a")).Near(start_cell, 145));
        cell_texts = neighbours.Select(s => s.Text).ToArray();
        CollectionAssert.AreEqual(ExpectedPrevNextFirst, cell_texts);

        neighbours = driver.FindElements(RelativeBy.WithLocator(By.TagName("a")).Near(start_cell, 208));
        cell_texts = neighbours.Select(s => s.Text).ToArray();
        CollectionAssert.AreEqual(ExpectedPrevNextFirst3urls, cell_texts);
        Assert.IsFalse(cell_texts.Contains("Last"));

        neighbours = driver.FindElements(RelativeBy.WithLocator(By.TagName("a")).Near(start_cell, 238));
        cell_texts = neighbours.Select(s => s.Text).ToArray();
        Assert.IsTrue(cell_texts.Contains("Last"));
    }
}