
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

namespace SeleniumRecipesCSharp.ch24_devtools;
// Replace the version to match the Chrome version

[TestClass]
public class Ch24DevToolsBrowserTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [ClassCleanup]
    public static void AfterAll()
    {
    }

    [TestMethod]
    public void TestBrowserCrash()
    {
        driver.Url = "https://whenwise.agileway.net";
        Thread.Sleep(1000);

        DevToolsSession devToolsSession = ((IDevTools)driver).GetDevToolsSession();
        OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains domains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains>();
        domains.Browser.Crash();
        Console.WriteLine("Still can reach here, browser is gone");
    }

    /*Not working
        [TestMethod]
        public void TestBrowserCrashViaSendCommand() {
            driver.Url = "https://whenwise.agileway.net";
            Thread.Sleep(1000);

            var devToolsSession = ((IDevTools)driver).GetDevToolsSession();
            // https://chromedevtools.github.io/devtools-protocol/tot/
            devToolsSession.SendCommand("Browser.crash");
            Thread.Sleep(1000);
            System.Console.WriteLine("Still can reach here, browser is gone");
        }
    */
}