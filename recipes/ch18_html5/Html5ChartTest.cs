using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch18_html5;

[TestClass]
public class Ch18Html5ChartTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [TestMethod]
    public void TestSaveSVGAsPng()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/charts.html");
        Thread.Sleep(500); // load JS
        WebElement svg_parent_elem = (WebElement)driver.FindElement(By.TagName("svg"));
        Screenshot screenshot = svg_parent_elem.GetScreenshot();
        screenshot.SaveAsFile("/tmp/svg.png");
        Assert.IsTrue(File.Exists("/tmp/svg.png"));
    }

    [TestMethod]
    public void TestSaveCanvasAsPng()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/canvas.html");
        Thread.Sleep(500); // load JS
        IWebElement canvas_elem = driver.FindElement(By.TagName("canvas"));
        //  extract canvas element's contents
        // ruby version, substring 21 works; safe to use 22, works for both
        const string js = "return arguments[0].toDataURL('image/png').substring(22);";
        string canvas_base64 = (string)driver.ExecuteScript(js, canvas_elem);
        // Console.WriteLine(canvas_base64);
        //  decode from the base64 format, get the image binary data
        using (StreamWriter writer = new("/tmp/c_sharp_base64.txt"))
        {
            writer.WriteLine(canvas_base64);
        }
        byte[] canvas_png = Convert.FromBase64String(canvas_base64);
        using BinaryWriter binWriter =
            new(File.Open("/tmp/c_sharp_chart.png", FileMode.Create));
        binWriter.Write(canvas_png);
    }

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();
}