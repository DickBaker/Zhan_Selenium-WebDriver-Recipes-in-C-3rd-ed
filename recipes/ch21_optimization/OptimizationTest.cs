using System.Diagnostics;
using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch21_optimization;

[TestClass]
public class Ch21OptimizationTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [TestInitialize]
    public void Before() =>
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/text_field.html");

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();

    [TestMethod]
    public void TestUsingTernaryOperator()
    {
        string refNo = driver.FindElement(By.Id("ref_no")).Text;
        if (refNo.Contains("VIP"))
        { // Special 
            Assert.AreEqual("Please go upstairs", driver.FindElement(By.Id("notes")).Text);
        }
        else
        {
            Assert.AreEqual("Please come in", driver.FindElement(By.Id("notes")).Text);
        }

        // using tenary operation
        Assert.AreEqual(refNo.Contains("VIP") ? "Please go upstairs" : "Please come in", driver.FindElement(By.Id("notes")).Text);
    }

    [TestMethod]
    public void TestPasteLargeTextInTextArea()
    {
        var watch = Stopwatch.StartNew();
        var longText = new string('*', 5000);
        IWebElement textArea = driver.FindElement(By.Id("comments"));
        textArea.SendKeys(longText);
        watch.Stop();
        var elapsedSec = watch.ElapsedMilliseconds / 1000.0;
        Console.WriteLine("time cost by SendKeys: " + elapsedSec + " seconds");

        textArea.Clear();
        watch = Stopwatch.StartNew();
        driver.ExecuteScript("document.getElementById('comments').value = arguments[0];", longText);
        watch.Stop();
        elapsedSec = watch.ElapsedMilliseconds / 1000.0;
        Console.WriteLine("time cost by JS set: " + elapsedSec + " seconds");
    }
}
