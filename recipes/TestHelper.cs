using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumRecipes
{
    public class TestHelper
    {
        // Replace the version to match the Chrome version
        // ensure matching Using.cs
#if LATESTVERSIONS // as of 27/3/2024
        public const string ChromeBrowserVersion = "123";   // used in Ch23SeleniumChromeForBrowserTest.TestChromeForTesting
#else       // use ones as per printed book (although well outdated!)
        public const string ChromeBrowserVersion = "116";   // used in Ch23SeleniumChromeForBrowserTest.TestChromeForTesting
#endif
 
        public static String SiteUrl() {
            // change to your installed location for the book site (http://zhimin.com/books/selenium-recipes-csharp)
            if (OperatingSystem.IsWindows()) {
                return "file:///C:/work/books/SeleniumRecipes-C%23/site";
            } else {
                return "file:///Users/zhimin/work/books/SeleniumRecipes-C%23/site";
            }
        }

        // change to yours
        public static String ScriptDir()
        {
            // return @"C:\agileway\books\SeleniumRecipes-C#\recipes"; // Windows
            // return "/Users/zhimin/work/books/selenium-webdriver-recipes-in-csharp-3ed/recipes"; // macOS/Linux
            return Environment.CurrentDirectory + "/../../..";
        }

        public static String TempDir()
        {     
           if (OperatingSystem.IsWindows()) {
             return @"C:\temp\";
           } else {
             return "/tmp/";
           }
        }
    }
}
