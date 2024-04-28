// The below test script worked in Edition 2, and has not changed. It has not been verified in Edition 3 as IE was deprecated in 2022.
// Included as a reference only.
// To successfully run some test cases in this file, requires BuildWise Agent free Popup Handler running

namespace SeleniumRecipesCSharp.ch13_popup;

#if IWANTIE

[TestClass]
public class PopupTestIE
{

    static WebDriver driver = default!;

    [TestInitialize]
    public void Before()
    {
        driver = new InternetExplorerDriver();
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/popup.html");
    }

    [TestMethod]
    public void TestFileUpload()
    {
        string filePath = TestHelper.ScriptDir() + @"\testdata\users.csv";
        driver.FindElement(By.Name("document[file]")).SendKeys(filePath);
    }

    [TestMethod]
    public void TestIEModalDialog()
    {
        driver.FindElement(By.LinkText("Show Modal Dialog")).Click();

        ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
        string mainWin = windowHandles[0]; // first one is the main window
        string modalWin = windowHandles[^1];

        _ = driver.SwitchTo().Window(modalWin);
        driver.FindElement(By.Name("user")).SendKeys("in-modal");
        _ = driver.SwitchTo().Window(mainWin);
        driver.FindElement(By.Name("status")).SendKeys("Done");
    }

    [TestMethod]
    public void TestJavaScriptPopup()
    {
        driver.Navigate().GoToUrl("http://testwisely.com/demo/popups");
        notifyPopupHandlerJavaScript("Message from webpage");
        driver.FindElement(By.Id("buy_now_btn")).Click();
        Thread.Sleep(15000);
        driver.FindElement(By.LinkText("NetBank")).Click();

        // don't use Waits, it somehow causes Selenium to click the cancel butotn
        // WebDriverWait wait = new WebDriverWait(driver, 20);  // seconds
        //wait.until(ExpectedConditions.presenceOfElementLocated(By.LinkText("NetBank"))); // on next page
    }

    // The test should fail
    [TestMethod]
    [Timeout(5000)]
    public void TestTimeout()
    {
        driver.Navigate().GoToUrl("http://testwisely.com/demo/popups");
        notifyPopupHandlerJavaScript("Message from webpage");
        driver.FindElement(By.Id("buy_now_btn")).Click();
        Thread.Sleep(15000); // to fail the test
        driver.FindElement(By.LinkText("NetBank")).Click();
    }

    [TestMethod]
    public void TestBasicAuthenticationDialog()
    {
        string winTitle = "Windows Security";
        string username = "tony";
        string password = "password";
        notifyPopupHandlerBasicAuth(winTitle, username, password);
        driver.Navigate().GoToUrl("http://itest2.com/svn-demo/");
        Thread.Sleep(20000);
        driver.FindElement(By.LinkText("tony/")).Click();
    }

    public static string GetUrlText(string path)
    {
        // Popup Handler URL
        string handlerURL = "http://localhost:4208";
        try
        {
            Uri website = new(handlerURL + path);
            string? urlContent = null;
            using (WebClient client = new())
            {
                urlContent = client.DownloadString(website);
            }
            return urlContent;

        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex);
            return "Error";

        }
    }

    public static void notifyPopupHandlerBasicAuth(string winTitle, string username, string password)
    {

        string handlerPath = "/basic_authentication/win_title=" + HttpUtility.UrlEncode(winTitle) + "&user=" + username + "&password=" + password;
        _ = GetUrlText(handlerPath);
    }

    public static void notifyPopupHandlerJavaScript(string winTitle)
    {
        string handlerPath = "/popup/win_title=" + HttpUtility.UrlEncode(winTitle);
        _ = GetUrlText(handlerPath);
    }

    [TestCleanup]
    public void After()
    {
        if (driver != null)
        {
            driver.Quit();
            driver = default!;
        }
    }
}
#endif
