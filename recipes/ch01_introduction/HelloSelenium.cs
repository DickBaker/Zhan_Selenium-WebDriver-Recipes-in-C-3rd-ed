using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch01_introduction;

public static class HelloSelenium
{
    public static void main()
    {
        IWebDriver driver = new ChromeDriver();
        // Thread.Sleep(10000);
    }
}
