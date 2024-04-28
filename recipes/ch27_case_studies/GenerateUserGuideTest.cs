using System.Diagnostics;
using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch27_case_studies;

[TestClass]
public class Ch27GenerateUserGuideTest
{
    static WebDriver driver = default!;

    [TestInitialize]
    public void Before() => driver = new ChromeDriver();

    [TestMethod]
    public void TestGenerateUserGuide()
    {
        driver.Manage().Window.Size = new System.Drawing.Size(1280, 720);
        driver.Navigate().GoToUrl("https://whenwise.com/become-partner");

        Screenshot ss = driver.GetScreenshot();
        string imageFilePath = TestHelper.TempDir() + "step_1.png";
        ss.SaveAsFile(imageFilePath, ScreenshotImageFormat.Png);

        driver.FindElement(By.Id("biz_name")).SendKeys("Wise Business");
        const string dropdown_xpath = "//select[@name='biz[business_type]']/../..";
        driver.FindElement(By.XPath(dropdown_xpath)).Click();
        Thread.Sleep(250);
        WebElement elemBizTypeList = (WebElement)driver.FindElement(By.XPath(dropdown_xpath + "/ul"));
        ss = elemBizTypeList.GetScreenshot();
        ss.SaveAsFile(TestHelper.TempDir() + "step_2.png", ScreenshotImageFormat.Png);

        driver.FindElement(By.XPath(dropdown_xpath + "/ul/li/span[text()='Driving Instructors']")).Click();
        WebElement elemCreateBtn = (WebElement)driver.FindElement(By.Id("create-account"));
        ss = elemCreateBtn.GetScreenshot();
        ss.SaveAsFile(TestHelper.TempDir() + "step_3.png", ScreenshotImageFormat.Png);

        List<string> guideList = new()
        {
              "## Guide: Business Sign up",
              "1. **Open the sign up page**",
              "    <img src='step_1.png' height='240'/>",
              "2. **Enter your business name**",
              "3. **Select business type**",
              "   ![](step_2.png)",
              "4. **Click the 'SIGN UP' button**",
              "   ![](step_3.png)"
          };

        string guide_markdown = string.Join("\r\n\r\n", guideList);
        string guide_md_path = TestHelper.TempDir() + "guide.md";
        using (StreamWriter outputFile = new(guide_md_path))
        {
            outputFile.WriteLine(guide_markdown);
        }

        // execute "md2html" script (a part of md2html gem)
        string md2html_cmd = "/Users/zhimin/.rbenv/shims/md2html";
        if (!File.Exists(md2html_cmd))
        {
            md2html_cmd = "/opt/homebrew/opt/rbenv/shims/md2html";
        }
        ProcessStartInfo startInfo = new()
        {
            FileName = md2html_cmd,
            Arguments = guide_md_path,
            WorkingDirectory = TestHelper.TempDir()
        };
        Process proc = new()
        {
            StartInfo = startInfo,
        };
        proc.Start();

        // if (OperatingSystem.IsMacOS()) {
        // System.Diagnostics.Process.Start("md2html " +  md2html_cmd);
        // }

    }

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();
}