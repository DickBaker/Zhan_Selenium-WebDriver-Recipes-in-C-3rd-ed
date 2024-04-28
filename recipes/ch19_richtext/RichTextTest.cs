using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace SeleniumRecipesCSharp.ch19_richtext;

[TestClass]
public class Ch19RichTextEditorTest
{
    static WebDriver driver = default!;

    [TestInitialize]
    public void Before() => driver = new ChromeDriver();

    [TestMethod]
    public void TestTinyMCE()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/tinymce-4.1.9/tinyice_demo.html");

        IWebElement tinymceFrame = driver.FindElement(By.Id("mce_0_ifr"));
        driver.SwitchTo().Frame(tinymceFrame);
        IWebElement editorBody = driver.FindElement(By.CssSelector("body"));
        driver.ExecuteScript("arguments[0].innerHTML = '<h1>Heading</h1>AgileWay'", editorBody);
        Thread.Sleep(500);
        editorBody.SendKeys("New content");
        Thread.Sleep(500);
        editorBody.Clear();

        // click TinyMCE editor's 'Numbered List' button
        driver.ExecuteScript("arguments[0].innerHTML = '<p>one</p><p>two</p>'", editorBody);

        // switch out then can drive controls on the main page
        driver.SwitchTo().DefaultContent();
        IWebElement tinymceNumberListBtn = driver.FindElement(By.CssSelector(".mce-btn[aria-label='Numbered list'] button"));
        tinymceNumberListBtn.Click();

        // Insert using JavaScripts
        driver.ExecuteScript("tinyMCE.activeEditor.insertContent('<p>Brisbane</p>')");
    }

    [TestMethod]
    public void TestCKEditor()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/ckeditor-4.4.7/samples/uicolor.html");
        Thread.Sleep(500);
        IWebElement ckeditorFrame = driver.FindElement(By.ClassName("cke_wysiwyg_frame"));
        driver.SwitchTo().Frame(ckeditorFrame);
        IWebElement editorBody = driver.FindElement(By.TagName("body"));
        editorBody.SendKeys("Selenium Recipes\n by Zhimin Zhan");
        Thread.Sleep(500);
        editorBody.SendKeys("New content");
        Thread.Sleep(500);
        editorBody.Clear();

        // Clear content Another Method Using ActionBuilder to clear()        
        Actions builder = new(driver);
        builder.Click(editorBody)
                .KeyDown(Keys.Control)
                .SendKeys("a")
                .KeyUp(Keys.Control)
                .Perform();
        builder.SendKeys(Keys.Backspace)
                .Perform();

        // switch out then can drive controls on the main page
        driver.SwitchTo().DefaultContent();
        driver.FindElement(By.ClassName("cke_button__numberedlist")).Click(); // numbered list
    }

    [TestMethod]
    public void TestSummerNote()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/summernote-0.6.3/demo.html");
        Thread.Sleep(500);
        driver.FindElement(By.XPath("//div[@class='note-editor']/div[@class='note-editable']")).SendKeys("Text");
        // click a format button: unordered list
        driver.FindElement(By.XPath("//button[@data-event='insertUnorderedList']")).Click();
        // switch to code view
        driver.FindElement(By.XPath("//button[@data-event='codeview']")).Click();
        // insert code (unformatted)
        driver.FindElement(By.XPath("//textarea[@class='note-codable']")).SendKeys("\n<p>HTML</p>");
    }

    [TestMethod]
    public void TestCodeMirror()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/codemirror-5.1/demo/xmlcomplete.html");
        Thread.Sleep(500);
        IWebElement elem = driver.FindElement(By.ClassName("CodeMirror-scroll"));
        elem.Click();
        Thread.Sleep(500);
        // elem.sendKeys does not work
        Actions builder = new(driver);
        builder.SendKeys("<h3>Heading 3</h3><p>TestWise is Selenium IDE</p>")
                .Perform();
    }

    [ClassCleanup]
    public static void AfterAll() => driver?.Quit();
}
