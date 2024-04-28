using OpenQA.Selenium.Edge;

namespace SeleniumRecipesCSharp.ch01_introduction;

public static class GoogleSearchEdge
{
    public static void main()
    {
        EdgeOptions options = new()
        {
            PageLoadStrategy = PageLoadStrategy.Eager
        };
        WebDriver driver = new EdgeDriver(options);

        // And now use this to visit Google
        driver.Navigate().GoToUrl("http://www.google.com");

        // Find the text input element by its name
        IWebElement element = driver.FindElement(By.Name("q"));

        // Enter something to search for
        element.SendKeys("Hello Selenium WebDriver!");

        // Now submit the form. WebDriver will find the form for us from the element
        element.Submit();

        // Check the title of the page
        Console.WriteLine("Page title is: " + driver.Title);

        driver.Quit();
    }
}