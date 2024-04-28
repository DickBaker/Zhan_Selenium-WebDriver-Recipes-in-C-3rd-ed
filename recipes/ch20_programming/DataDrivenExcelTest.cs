using OpenQA.Selenium.Firefox;

// requires Install Microsoft Office and make sure that the .NET Programmability Support feature is selected
// Add NuGet package Microsoft.Office.Interop.Excel
// ensure define IHAVEEXCEL conditional compilation (in Build General property tab) for below test code
using Excel = Microsoft.Office.Interop.Excel;

namespace SeleniumRecipesCSharp.ch20_programming;

[TestClass]
public class Ch20DataDrivenExcelTest
{
    readonly WebDriver driver = new FirefoxDriver();

#if IHAVEEXCEL
    [TestMethod]
    public void TestDataDrivenExcel()
    {
        string filePath = TestHelper.ScriptDir() + @"\testdata\users.xls";
        Console.WriteLine("Excel file: " + filePath);
        Excel.Application excelApp;
        Excel.Workbook excelWorkbook;
        Excel.Worksheet sheet;
        Excel.Range range;
        excelApp = new Excel.Application();
        //Opening Excel file
        excelWorkbook = excelApp.Workbooks.Open(filePath);
        sheet = (Excel.Worksheet)excelWorkbook.Worksheets.get_Item(1);

        range = sheet.UsedRange;
        // starting from 2, skip the header row
        for (int rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
        {
            Console.WriteLine("1.");

            Excel.Range myIDBinder = sheet.get_Range("A" + rCnt.ToString(), "A" + rCnt.ToString());
            description = myIDBinder.Value.ToString()!;

            myIDBinder = sheet.get_Range("B" + rCnt.ToString(), "B" + rCnt.ToString());
            string? login = myIDBinder.Value.ToString()!;

            myIDBinder = sheet.get_Range("C" + rCnt.ToString(), "C" + rCnt.ToString());
            string? password = myIDBinder.Value.ToString()!;

            myIDBinder = sheet.get_Range("D" + rCnt.ToString(), "D" + rCnt.ToString());
            string? expectedText = myIDBinder.Value.ToString()!;
            driver.Navigate().GoToUrl("http://travel.agileway.net");
            driver.FindElement(By.Name("username")).SendKeys(login);
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("username")).Submit();
            Assert.IsTrue(driver.FindElement(By.TagName("body")).Text.Contains(expectedText));

            try
            {
                // if logged in OK, try log out, so next one can continue
                driver.FindElement(By.LinkText("Sign off")).Click();
            }
            catch (Exception)
            {
                // ignore
            }
        }

        excelWorkbook.Close(true, null, null);
        excelApp.Quit();
    }
#endif
}
