using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Lab5_KPI.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateChrome(string downloadDir)
        {
            var options = new ChromeOptions();

            // налаштування завантажень
            options.AddUserProfilePreference("download.default_directory", downloadDir);
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("safebrowsing.enabled", true);

            // можна лишити, щоб вікно було великим
            options.AddArgument("--start-maximized");
            return new ChromeDriver(options);
        }
    }
}
