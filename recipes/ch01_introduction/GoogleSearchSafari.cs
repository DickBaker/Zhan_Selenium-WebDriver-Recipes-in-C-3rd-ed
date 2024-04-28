using OpenQA.Selenium.Safari;

namespace SeleniumRecipesCSharp.ch01_introduction;

public static class GoogleSearchSafari
{
    public static void main()
    {
        WebDriver driver = new SafariDriver();

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
