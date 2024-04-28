using System.Text;
using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch15_testdata;

[TestClass]
public class Ch15DataTest
{
    static readonly WebDriver driver = new ChromeDriver();
    static readonly Random Random = new();

    [TestInitialize]
    public void Before() =>
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/index.html");

    [ClassCleanup]
    public static void AfterAll() => driver.Quit();

    [TestMethod]
    public void TestDynamicDate()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/text_field.html");
        string todaysDate = DateTime.Now.ToString("MM/dd/yyyy");
        Console.WriteLine("todaysDate = " + todaysDate);
        driver.FindElement(By.Name("username")).SendKeys(todaysDate);

        driver.FindElement(By.Name("username")).SendKeys(Today("dd/MM/yyyy"));
        driver.FindElement(By.Name("username")).SendKeys(Tomorrow("AUS"));
        driver.FindElement(By.Name("username")).SendKeys(Yesterday("ISO"));
    }

    public static string GetDate(string format, int dateDiff)
    {
        format = format.Equals("AUS") || format.Equals("UK")
            ? "dd/MM/yyyy"
            : format.Equals("ISO")
                ? "yyyy-MM-dd"
                : "MM/dd/yyyy";

        return DateTime.Today.AddDays(dateDiff).ToString(format);
    }

    public static string Today(string format) => GetDate(format, 0);

    public static string Tomorrow(string format) => GetDate(format, 1);

    public static string Yesterday(string format) => GetDate(format, -1);

    [TestMethod]
    public void TestRandomBooleanForRadio()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/radio_button.html");
        string randomGender = GetRandomBoolean() ? "male" : "female";
        driver.FindElement(By.XPath("//input[@type='radio' and @name='gender' and @value='" + randomGender + "']")).Click();
    }

    [TestMethod]
    public void TestRandomBooleanWithTimestamp()
    {
        int timestamp = DateTime.Now.Second;
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/radio_button.html");
        string randomGender = timestamp % 2 == 0 ? "male" : "female";
        driver.FindElement(By.XPath("//input[@type='radio' and @name='gender' and @value='" + randomGender + "']")).Click();
    }

    [TestMethod]
    public void TestRandomStringForTextField()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/text_field.html");
        driver.FindElement(By.Name("password")).SendKeys(GetRandomString(8));
    }

    [TestMethod]
    public void TestRandomNumberForTextField()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/text_field.html");
        // a number between 10 and 99, will be different each run   
        driver.FindElement(By.Name("username")).SendKeys("tester" + GetRandomNumber(10, 99));
    }

    [TestMethod]
    public void TestRandomStringInCollection()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/text_field.html");
        string[] allowableStrings = new string[] { "Yes", "No", "Maybe" }; // one of these strings
        driver.FindElement(By.Name("username")).SendKeys(GetRandomStringIn(allowableStrings));
    }

    [TestMethod]
    public void TestGenerateFixedSizeFile()
    {
        string outputFilePath = TestHelper.TempDir() +
            (OperatingSystem.IsWindows()
                ? @"\2MB.txt"
                : "/2MB.txt");
        File.WriteAllBytes(outputFilePath, new byte[1024 * 1024 * 2]);
    }

    public static bool GetRandomBoolean()
    {
        int random_0_or_1 = Random.Next(0, 2);
        return random_0_or_1 > 0;
    }

    public static int GetRandomNumber(int min, int max) => Random.Next(min, max);

    public static char GetRandomChar()
    {
        int num = Random.Next(0, 26); // Zero to 25
        return (char)('A' + num);
    }

    public static string GetRandomString(int length)
    {
        StringBuilder sb = new();
        for (int i = 0; i < length; i++)
        {
            sb.Append(GetRandomChar());
        }
        return sb.ToString();
    }

    public static string GetRandomStringIn(string[] array) => array[GetRandomNumber(0, array.Length - 1)];
}