using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch27_case_studies;

[TestClass]
public class Ch27ChartGenerationTest
{
    static readonly WebDriver driver = new ChromeDriver();
    static string savedChartFilePath = "/tmp/chart.png";

    [ClassInitialize]
    public static void BeforeAll(TestContext context)
    {
        if (OperatingSystem.IsWindows())
        {
            savedChartFilePath = @"C:\temp\chart.png";
        }

        // a better way is to use relative path
        // 
        // savedChartFilePath = Path.Combine(Environment.CurrentDirectory, "chart.png"); 
        // Console.WriteLine("Outputing file to " + savedChartFilePath);

    }

    [TestMethod]
    public void TestSaveChart()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/dynamic-chart.html");
        try
        {
            driver.FindElement(By.TagName("svg"));
            Assert.Fail("The SVG should not exist yet");
        }
        catch (NoSuchElementException)
        {
            Console.WriteLine("As expected");
        }

        driver.FindElement(By.Id("gen-btn")).Click();
        Thread.Sleep(1000); // load JS

        WebElement svg_elem = (WebElement)driver.FindElement(By.TagName("svg"));
        Screenshot screenshot = svg_elem.GetScreenshot();

        screenshot.SaveAsFile(savedChartFilePath);
        Assert.IsTrue(File.Exists(savedChartFilePath));

        // Add nuget System.Drawing.Common
        // which only supported on Windows https://learn.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/system-drawing-common-windows-only
        if (OperatingSystem.IsWindows())
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(savedChartFilePath);
            Console.WriteLine("Width: " + img.Width + ", Height: " + img.Height);
            Assert.AreEqual(400, img.Height);
        }
    }

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();
}