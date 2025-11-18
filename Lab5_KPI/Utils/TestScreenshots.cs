using OpenQA.Selenium;

namespace SeleniumLab5.Utils;

public static class TestScreenshots
{
    public static string Save(IWebDriver driver, string testName)
    {
        var dir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "screens");
        Directory.CreateDirectory(dir);
        var path = Path.Combine(dir, $"{DateTime.Now:yyyyMMdd_HHmmss}_{testName}.png");
        var shot = ((ITakesScreenshot)driver).GetScreenshot();
        shot.SaveAsFile(path, ScreenshotImageFormat.Png);
        TestContext.AddTestAttachment(path);
        return path;
    }
}
