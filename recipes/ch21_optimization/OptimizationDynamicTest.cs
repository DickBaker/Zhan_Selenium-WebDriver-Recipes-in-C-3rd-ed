using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SeleniumRecipesCSharp.ch21_optimization;

[TestClass]
public class Ch21OptimizationDynamicTest
{
    static WebDriver driver = default!;
    static string siteRootUrl = "https://wisephysio.agileway.net"; // en
                                                                   // static String siteRootUrl = "https://books.agileway.net"; // cn

    static readonly Dictionary<string, string> natalieUserDict = new()
  {
          { "english", "natalie" },
          { "chinese", "tuo" },
          { "french", "dupont" }
      };

    static readonly Dictionary<string, string> markUserDict = new()
  {
          { "english", "mark" },
          { "chinese", "li" },
          { "french", "marc" }
      };

    [TestMethod]
    public void TestUseEnvironmentVariableToChangeTestBehaviourDynamically()
    {
        // Warning: Visual Studio Caching Environment Variables

        string? browserTypeSetInEnv = Environment.GetEnvironmentVariable("BROWSER");
        Console.WriteLine(browserTypeSetInEnv);
        driver = !string.IsNullOrEmpty(browserTypeSetInEnv) && browserTypeSetInEnv.Equals("Firefox")
          ? new FirefoxDriver() : new ChromeDriver();

        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("SITE_URL")))
        {
            siteRootUrl = Environment.GetEnvironmentVariable("SITE_URL")!;
        }
        driver.Navigate().GoToUrl(siteRootUrl);
    }

    [TestMethod]
    public void TestMultiLangWithDifferentTestUserAccounts()
    {
        if (!string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("BASE_URL")))
        {
            siteRootUrl = Environment.GetEnvironmentVariable("BASE_URL")!;
        }
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(siteRootUrl + "/admin");

        driver.FindElement(By.Id("sign_in_email")).SendKeys(IsChinese() ? "tuo" : "natalie");
        driver.FindElement(By.Id("sign_in_password")).SendKeys("test");
        driver.FindElement(By.Id("sign_in_btn")).Click();
        Assert.IsTrue(driver.PageSource.Contains(IsChinese() ? "不正确的用户名或密码" : "Invalid login or password"));
    }

    [TestMethod]
    public void TestTwoLanguagesUsingIfElse()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(siteRootUrl + "/admin"); // may be dynamically set

        if (siteRootUrl.Contains("wisephysio"))
        {
            driver.FindElement(By.Id("sign_in_email")).SendKeys("natalie");
            driver.FindElement(By.Id("sign_in_password")).SendKeys("test");
            driver.FindElement(By.Id("sign_in_btn")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Invalid login or password"));
        }
        else if (siteRootUrl.Contains("books"))
        {
            driver.FindElement(By.Id("sign_in_email")).SendKeys("tuo");
            driver.FindElement(By.Id("sign_in_password")).SendKeys("test");
            driver.FindElement(By.Id("sign_in_btn")).Click();
            Assert.IsTrue(driver.PageSource.Contains("不正确的用户名或密码"));
        }
    }

    public static bool IsChinese() => siteRootUrl.Contains("books");

    [TestMethod]
    public void TestTwoLanguagesUsingTernaryOperator()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(siteRootUrl + "/admin"); // may be dynamically set

        driver.FindElement(By.Id("sign_in_email")).SendKeys(IsChinese() ? "tuo" : "natalie");
        driver.FindElement(By.Id("sign_in_password")).SendKeys("test");
        driver.FindElement(By.Id("sign_in_btn")).Click();
        Thread.Sleep(1000);
        Assert.IsTrue(driver.PageSource.Contains(IsChinese() ? "不正确的用户名或密码" : "Invalid login or password"));
    }

    public static string SiteLang() => siteRootUrl.Contains("books")
        ? "chinese"
        : siteRootUrl.Contains("sandbox")
          ? "french"
          : "english";

    [TestMethod]
    public void TestMultipleLanguagesUsingIfElse()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(siteRootUrl + "/admin"); // may be dynamically set

        if (SiteLang() == "chinese")
        {
            driver.FindElement(By.Id("sign_in_email")).SendKeys("yake@biz.com");
        }
        else if (SiteLang() == "french")
        {
            driver.FindElement(By.Id("sign_in_email")).SendKeys("dupont");
        }
        else
        { // default
            driver.FindElement(By.Id("sign_in_email")).SendKeys("yoga@biz.com");
        }
        driver.FindElement(By.Id("sign_in_password")).SendKeys("test01");
        driver.FindElement(By.Id("sign_in_btn")).Click();
    }

    public static string UserLookup(string username) => SiteLang() switch
    {
        "chinese" => "tuo",
        "french" => "dupont",
        _ => username
    };

    [TestMethod]
    public void TestMultipleLanguagesUsingLookup()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(siteRootUrl + "/admin"); // may be dynamically set

        driver.FindElement(By.Id("sign_in_email")).SendKeys(UserLookup("natalie"));
        driver.FindElement(By.Id("sign_in_password")).SendKeys("test");
        driver.FindElement(By.Id("sign_in_btn")).Click();
    }

    public static string UserLookupDict(string username) => username switch
    {
        "natalie" => natalieUserDict[SiteLang()],
        "mark" => markUserDict[SiteLang()],
        _ => username
    };

    [TestMethod]
    public void TestMultipleLanguagesUsingHashLookup()
    {
        driver = new ChromeDriver();
        driver.Navigate().GoToUrl(siteRootUrl + "/admin"); // may be dynamically set
        driver.FindElement(By.Id("sign_in_email")).SendKeys(UserLookupDict("natalie"));
        driver.FindElement(By.Id("sign_in_password")).SendKeys("test");
        driver.FindElement(By.Id("sign_in_btn")).Click();
    }
}