using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SeleniumRecipesCSharp.ch25_remote;
[TestClass]
public class Ch25RemoteTest
{
    [TestMethod]
    public void TestSeleniumGridRemoteChrome()
    {
        // server started with 
        // java -jar selenium-server-4.11.0.jar standalone --port 1234
        ChromeOptions opts = new();
        WebDriver driver = new RemoteWebDriver(new Uri("http://localhost:1234"), opts);
        driver.Navigate().GoToUrl("https://whenwise.agileway.net");
        Assert.AreEqual("WhenWise - Booking Made Easy", driver.Title);
        driver.Quit();
    }
}