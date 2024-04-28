using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch27_case_studies;

[TestClass]
public class Ch27LazyLoadingTest
{
    static WebDriver driver = default!;

    [TestInitialize]
    public void Before() => driver = new ChromeDriver();

    [TestMethod]
    public void TestLazyLoading()
    {
        driver.Navigate().GoToUrl("https://agileway.substack.com/archive");
        // OK
        driver.FindElement(By.PartialLinkText("Micro-ISV 01"));

        // not shown yet
        for (int i = 1; i <= 100; i++)
        {
            Console.WriteLine("Scroll: " + i);
            driver.FindElement(By.TagName("body")).SendKeys(Keys.PageDown);
            Thread.Sleep(1000);
            try
            {
                driver.FindElement(By.PartialLinkText("Page Object Model is universally applicable"));
                // quit the loop if found
                break;
            }
            catch (NoSuchElementException)
            {
                // not found, continue
            }
        }

        driver.FindElement(By.PartialLinkText("Page Object Model is universally applicable")).Click();
        Thread.Sleep(1000); // loading JS
        Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("a well-known test design pattern in test automation"));
    }

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();
}