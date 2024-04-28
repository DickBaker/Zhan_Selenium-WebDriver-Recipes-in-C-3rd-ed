namespace SeleniumRecipesCSharp;

public static class TestHelper
{
    public static string SiteUrl() =>
        // change to your installed location for the book site (http://zhimin.com/books/selenium-recipes-csharp)
        OperatingSystem.IsWindows()
              ? "file:///C:/work/books/SeleniumRecipes-C%23/site"
              : "file:///Users/zhimin/work/books/SeleniumRecipes-C%23/site";

    // change to yours
    public static string ScriptDir() =>
        // return @"C:\agileway\books\SeleniumRecipes-C#\recipes"; // Windows
        // return "/Users/zhimin/work/books/selenium-webdriver-recipes-in-csharp-3ed/recipes"; // macOS/Linux
        Environment.CurrentDirectory + "/../../..";

    public static string TempDir() => OperatingSystem.IsWindows()
        ? @"C:\temp\" : "/tmp/";
}
