The Source For Selenium WebDriver Recipes in C# 3rd edition.

Inheritance hierarchy
IWebDriver : ISearchContext, IDisposable
  WebDriver : IHasCapabilities, IHasSessionId, IJavaScriptExecutor, ITakesScreenshot
    ChromiumDriver : IDevTools, ISupportsLogs
      ChromeDriver
      EdgeDriver
    FirefoxDriver : IDevTools
    SafariDriver
