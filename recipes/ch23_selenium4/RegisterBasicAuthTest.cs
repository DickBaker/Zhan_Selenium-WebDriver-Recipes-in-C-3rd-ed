using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch23_selenium4;
[TestClass]
public class Ch23RegisterBasicAuthTest
{
    static WebDriver driver = default!;

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();

    // Doc: https://www.selenium.dev/documentation/webdriver/bidirectional/bidi_api/#register-basic-auth
    [TestMethod]
    public async Task TestRegisterBasicAuth()
    {
        driver = new ChromeDriver();

        NetworkAuthenticationHandler handler = new()
        {
            UriMatcher = (d) => d.Host.Contains("zhimin.com"),
            Credentials = new PasswordCredentials("agileway", "SUPPORTWISE15")
        };

        INetwork networkInterceptor = driver.Manage().Network;
        networkInterceptor.AddAuthenticationHandler(handler);
        await networkInterceptor.StartMonitoring();

        driver.Navigate().GoToUrl("http://zhimin.com/books/bought-learn-ruby-programming-by-examples");
        driver.FindElement(By.LinkText("Download"));
    }
}