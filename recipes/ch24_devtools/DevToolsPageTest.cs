
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
// Replace the version to match the Chrome version
using OpenQA.Selenium.DevTools.V115.Page;

namespace SeleniumRecipesCSharp.ch24_devtools;
[TestClass]
public class Ch24DevToolsPageTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();

    [TestMethod]
    public void TestConsoleLog()
    {
        Thread.Sleep(1000);
        DevToolsSession devToolsSession = ((IDevTools)driver).GetDevToolsSession();
        var domains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains>();
        //        domains.Page.Enable();
        //      domains.Page.Navigate("https://whenwise.agileway.net");
        //    domains.Console.ClearMessages();

    }

    [TestMethod]
    public void TestPrintPDF()
    {
        driver.Navigate().GoToUrl("https://whenwise.agileway.net");
        Thread.Sleep(1000);
        DevToolsSession devToolsSession = ((IDevTools)driver).GetDevToolsSession();
        OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains domains = devToolsSession.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V115.DevToolsSessionDomains>();
        PrintToPDFCommandSettings printPdfSettings = new();
        // TODO: how to get data out
        domains.Page.PrintToPDF(printPdfSettings);
    }
}