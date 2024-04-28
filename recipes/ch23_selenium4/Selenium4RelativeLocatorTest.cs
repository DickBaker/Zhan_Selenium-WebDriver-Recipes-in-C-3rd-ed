
using System.Collections.ObjectModel;
using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch23_selenium4;
[TestClass]
public class Ch23Selenium4RelativeLocatorTest
{
    static WebDriver driver = default!;
    static readonly string[] Expected = new string[] { "BuildWise", "ClinicWise", "Test automation products only" };

    [ClassInitialize]
    public static void BeforeAll(TestContext context)
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/relative.html");
    }

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();

    [TestMethod]
    public void TestRelativeRight()
    {
        IWebElement start_cell = driver.FindElement(By.Id("test_products_only_flag"));
        IWebElement elem_label = driver.FindElement(RelativeBy.WithLocator(By.TagName("span")).RightOf(start_cell));
        Assert.AreEqual(elem_label.Text, "Test automation products only");

        // XPath alternative
        elem_label = driver.FindElement(By.XPath("//input[@id='test_products_only_flag']/../span"));
        Assert.AreEqual(elem_label.Text, "Test automation products only");

        // a better way
        elem_label = driver.FindElement(By.XPath("//input[@id='test_products_only_flag']/following-sibling::*"));
        Assert.AreEqual(elem_label.Text, "Test automation products only");
    }

    [TestMethod]
    public void TestRelativeLeftWithList()
    {
        IWebElement start_cell = driver.FindElement(By.Id("accept_terms"));
        IWebElement elem_label = driver.FindElement(RelativeBy.WithLocator(By.TagName("span")).LeftOf(start_cell));
        Assert.AreEqual(elem_label.Text, "I accept Terms and Conditions");

        ReadOnlyCollection<IWebElement> leftSpans = driver.FindElements(RelativeBy.WithLocator(By.TagName("span")).LeftOf(start_cell));
        Assert.AreEqual(6, leftSpans.Count);
        Assert.AreEqual("I accept Terms and Conditions", leftSpans[0].Text);
        Assert.AreEqual("ClinicWise", leftSpans[^1].Text);

        // XPath alternative
        elem_label = driver.FindElement(By.XPath("//input[@id='accept_terms']/preceding-sibling::*"));
        Assert.AreEqual("I accept Terms and Conditions", elem_label.Text);
    }

    [TestMethod]
    public void TestRelativeAboveInTable()
    {
        IWebElement start_cell = driver.FindElement(By.Id("sitewise"));
        IWebElement the_above = driver.FindElement(RelativeBy.WithLocator(By.TagName("span")).Above(start_cell));
        Assert.AreEqual(the_above.Text, "BuildWise");

        ReadOnlyCollection<IWebElement> above_cells = driver.FindElements(RelativeBy.WithLocator(By.TagName("span")).Above(start_cell));
        Assert.AreEqual(above_cells.Count, 3);
        string[] cell_texts = above_cells.Select(s => s.Text).ToArray();
        // NOTE, using CollectionAssert, normal Assert does not work
        CollectionAssert.AreEqual(Expected, cell_texts);
    }

    [TestMethod]
    public void TestRelativeBelowInTable()
    {
        IWebElement start_cell = driver.FindElement(By.XPath("//tr[3]/td[2]"));
        Assert.AreEqual(start_cell.Text, "2014");
        IWebElement the_below = driver.FindElement(RelativeBy.WithLocator(By.TagName("td")).Below(start_cell));
        Console.WriteLine("The below 2014 is " + the_below.Text);
        // Assert.AreEqual(the_below.Text, "2007"); //  => 2018, expected 2007

        IWebElement span_cell = driver.FindElement(By.XPath("//tr[3]/td[2]/span"));
        Assert.AreEqual(start_cell.Text, "2014");
        the_below = driver.FindElement(RelativeBy.WithLocator(By.TagName("td")).Below(span_cell));
        Assert.AreEqual(the_below.Text, "2007");
    }

    [TestMethod]
    public void TestRelativeMultipleDirectionsInTable()
    {
        IWebElement start_cell = driver.FindElement(By.XPath("//tr[4]/td[2]/span"));
        Assert.AreEqual("2007", start_cell.Text);
        IWebElement the_above = driver.FindElement(RelativeBy.WithLocator(By.TagName("td")).Above(start_cell));
        IWebElement the_below = driver.FindElement(RelativeBy.WithLocator(By.TagName("td")).Below(start_cell));
        IWebElement the_left = driver.FindElement(RelativeBy.WithLocator(By.TagName("td")).LeftOf(start_cell));
        IWebElement the_right = driver.FindElement(RelativeBy.WithLocator(By.TagName("td")).RightOf(start_cell));
        Assert.AreEqual("2014", the_above.Text);
        Assert.AreEqual("2018", the_below.Text);
        Assert.AreEqual("TestWise", the_left.Text);
        Assert.AreEqual("https://testwisely.com/testwise", the_right.Text);

        string the_cell_text = driver.FindElement(By.XPath("//table[@id='grid']//tr[4]/td[2]")).Text;
        Assert.AreEqual("2007", the_cell_text);
        string the_above_text = driver.FindElement(By.XPath("//table[@id='grid']//tr[3]/td[2]")).Text;
        string the_below_text = driver.FindElement(By.XPath("//table[@id='grid']//tr[5]/td[2]")).Text;
        string the_left_text = driver.FindElement(By.XPath("//table[@id='grid']//tr[4]/td[1]")).Text;
        string the_right_text = driver.FindElement(By.XPath("//table[@id='grid']//tr[4]/td[3]")).Text;
        Assert.AreEqual("2014", the_above_text);
        Assert.AreEqual("2018", the_below_text);
        Assert.AreEqual("TestWise", the_left_text);
        Assert.AreEqual("https://testwisely.com/testwise", the_right_text);
    }
}