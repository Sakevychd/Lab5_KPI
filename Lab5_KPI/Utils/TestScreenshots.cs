using System.IO;
using System.Linq;
using OpenQA.Selenium;
using NUnit.Framework;

namespace Lab5_KPI.Utils
{
    public static class TestScreenshots
    {
        public static string Save(IWebDriver driver, string testName)
        {
            var dir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "screens");
            Directory.CreateDirectory(dir);

            var safeName = new string(testName.Select(ch =>
                Path.GetInvalidFileNameChars().Contains(ch) ? '_' : ch).ToArray());

            var path = Path.Combine(dir, $"{System.DateTime.Now:yyyyMMdd_HHmmss}_{safeName}.png");

            var shot = ((ITakesScreenshot)driver).GetScreenshot();
            File.WriteAllBytes(path, shot.AsByteArray); // cross-version safe

            TestContext.AddTestAttachment(path);
            return path;
        }
    }
}
