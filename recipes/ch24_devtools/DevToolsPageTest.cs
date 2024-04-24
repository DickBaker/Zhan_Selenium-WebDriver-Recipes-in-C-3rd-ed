using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

namespace SeleniumRecipes;

[TestClass]
public class Ch24DevToolsPageTest
{
    static IWebDriver driver = new ChromeDriver();

    [ClassCleanup]
    public static void AfterAll() {
      if (driver != null)
        driver.Quit();
    }

    [TestMethod]
    public void TestConsoleLog() {
        IDevTools devTools = driver as IDevTools;
        DevToolsSession devToolsSession = devTools.GetDevToolsSession();
        System.Threading.Thread.Sleep(1000);
        var domains  = devToolsSession.GetVersionSpecificDomains<Domains>();
//        domains.Page.Enable();
  //      domains.Page.Navigate("https://whenwise.agileway.net");
    //    domains.Console.ClearMessages();

      }

   [TestMethod]
    public void TestPrintPDF() {
        IDevTools devTools = driver as IDevTools;
        DevToolsSession devToolsSession = devTools.GetDevToolsSession();
        driver.Navigate().GoToUrl("https://whenwise.agileway.net");
        System.Threading.Thread.Sleep(1000);
        var domains  = devToolsSession.GetVersionSpecificDomains<Domains>();
        // TODO: how to get data out
        var printPdfSettings = new Page.PrintToPDFCommandSettings();
        domains.Page.PrintToPDF(printPdfSettings);
    }
}