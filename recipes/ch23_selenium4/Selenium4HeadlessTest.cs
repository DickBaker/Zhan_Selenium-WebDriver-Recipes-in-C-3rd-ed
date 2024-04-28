using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch23_selenium4;
[TestClass]
public class Ch23Selenium4HeadlessTest
{
    static WebDriver driver = default!;

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();

    [TestMethod]
    public void TestSavePdf()
    {
        PrintOptions printOptions = new()
        {
            Orientation = PrintOrientation.Portrait
        };

        ChromeOptions chromeOptions = new();
        // some doc says works in headless mode, but it seems works on both
        // chromeOptions.AddArguments("headless"); 

        driver = new ChromeDriver(chromeOptions);
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/image-map.html");

        //printing...
        PrintDocument printDocument = driver.Print(printOptions);

        //saving the file
        const string printFinalPath = "/tmp/sample.pdf";
        printDocument.SaveAsFile(printFinalPath);
    }
}