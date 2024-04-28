using System.Data.SQLite;
using OpenQA.Selenium.Chrome;

namespace SeleniumRecipesCSharp.ch15_testdata;

[TestClass]
public class Ch15QueryDatabaseTest
{
    static readonly WebDriver driver = new ChromeDriver();

    [TestMethod]
    public void TestDatabaseSqlite3()
    {
        driver.Navigate().GoToUrl(TestHelper.SiteUrl() + "/text_field.html");

        string? oldestUserLogin = null;
        SQLiteConnection? connection = null;

        try
        {
            // copy the test data from testdata/folder to output folder
            // String sourceFile =  Path.Combine(TestHelper.ScriptDir() + @"..\testdata\sample.db");

            string dbFile = Path.Combine(Environment.CurrentDirectory, "sample.db");
            Console.WriteLine("Using database: " + dbFile);
            connection = new SQLiteConnection("Data Source=" + dbFile + ";Version=3");
            connection.Open();

            const string sql = "select login from users order by age desc";
            SQLiteCommand command = new(sql, connection);

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {    // read the result set
                oldestUserLogin = (string)reader["login"];
                Console.WriteLine("Old Login: " + oldestUserLogin);
                break;
            }
        }
        catch (Exception e)
        {
            // probably means no database file is found
            Console.WriteLine(e.Message);
        }
        finally
        {
            try
            {
                connection?.Close();
            }
            catch (Exception e)
            {  // connection close failed.
                Console.WriteLine(e);
            }
        }

        Console.WriteLine(" => " + oldestUserLogin);
        driver.FindElement(By.Id("user")).SendKeys(oldestUserLogin);
    }
}
