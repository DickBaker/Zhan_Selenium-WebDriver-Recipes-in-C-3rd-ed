using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch23_selenium4;
[TestClass]
public class Ch23Selenium4ShadowRootTest
{
    [TestMethod]
    public void TestShadowRoot()
    {
        WebDriver driver = new ChromeDriver();
        driver.Navigate().GoToUrl("https://fiddle.luigi-project.io/#/home/wc1");
        // NOTE:
        //  add *1 to get its first child
        IWebElement elem = driver.FindElements(By.XPath("//div[contains(@class, 'wcContainer svelte-')]/*[1]"))[0];

        // NOTE: Selenium 4 has a new method to get Shadow Root easy, compared Selenium 3's using JavaScript way
        ISearchContext shadowRoot = elem.GetShadowRoot();
        IWebElement inputElem = shadowRoot.FindElement(By.CssSelector("input.add-new-list-item-input"));
        inputElem.SendKeys("Peach here I come!");
        shadowRoot.FindElement(By.CssSelector("button.editable-list-add-item.icon")).Click();
        Thread.Sleep(500);
        Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains("Peach here I come!"));

        // Clear up

        // NOTE: for shadown root, seem only can use "CssSelector", XPath/Tag returns "invalid locator" error 
        // var lastRemoveBtn = shadowRoot.FindElements(By.XPath("div/ul/li/button")).Last(); 

        IWebElement lastRemoveBtn = shadowRoot.FindElement(By.CssSelector("div > ul > li:last-child > button"));
        lastRemoveBtn.Click();
    }

    // OLD JS Way
    // IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
    //var element = js.ExecuteScript("return document.querySelector('selector_outside_shadow_root').shadowRoot.querySelector('selector_inside_shadow_root');");

}