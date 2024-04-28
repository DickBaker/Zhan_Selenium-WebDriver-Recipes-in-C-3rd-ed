using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch14_debugging;

[TestClass]
public class Ch14DebuggingTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [TestInitialize]
    public void Before() =>
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/assert.html");

    [TestMethod]
    public void TestPrintOutText()
    {
        Console.WriteLine("Now on page: " + driver.Title);
        string app_no = driver.FindElement(By.Id("app_id")).Text;
        Console.WriteLine("Application number is " + app_no);
    }

    [TestMethod]
    public void TestWritePageOrElementHtmlToFile()
    {
        string imageLoginPage;
        string imageLoginParent;
        if (OperatingSystem.IsWindows())
        {
            imageLoginPage = TestHelper.TempDir() + @"\login_page.html";
            imageLoginParent = TestHelper.TempDir() + @"\login_parent.xhtml";
        }
        else
        {
            imageLoginPage = TestHelper.TempDir() + "/login_page.html";
            imageLoginParent = TestHelper.TempDir() + "/login_parent.xhtml";
        }
        using (var outfile = new StreamWriter(imageLoginPage))
        {
            outfile.Write(driver.PageSource);
        }

        IWebElement the_element = driver.FindElement(By.Id("div_parent"));
        string the_element_html = (string)driver.ExecuteScript("return arguments[0].outerHTML;", the_element);

        using (var outfile = new StreamWriter(imageLoginParent))
        {
            outfile.Write(the_element_html);
        }
    }

    [TestMethod]
    public void TestTakeScreenshot()
    {
        Screenshot ss = driver.GetScreenshot();
        string imageFilePath = OperatingSystem.IsWindows()
            ? TestHelper.TempDir() + @"\screenshot.png"
            : TestHelper.TempDir() + "/screenshot.png";
        ss.SaveAsFile(imageFilePath, ScreenshotImageFormat.Png);
    }

    [TestMethod]
    public void TestTakeScreenshotWithTimestamp()
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
        string imageFilePath = OperatingSystem.IsWindows()
            ? TestHelper.TempDir() + @"\Exception-" + timestamp + ".png"
            : TestHelper.TempDir() + "/Exception-" + timestamp + ".png";
        Screenshot screenshot = driver.GetScreenshot();
        screenshot.SaveAsFile(imageFilePath, ScreenshotImageFormat.Png);
    }

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();
}