using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumLab5.Drivers;

public static class WebDriverFactory
{
    public static IWebDriver CreateChrome(string? downloadDir = null)
    {
        var opts = new ChromeOptions();
        opts.AddArgument("--start-maximized");
        opts.AddArgument("--disable-infobars");
        opts.AddArgument("--disable-gpu");
        opts.AddArgument("--no-sandbox");

        // автозавантаження файлів для File Download
        if (!string.IsNullOrEmpty(downloadDir))
        {
            opts.AddUserProfilePreference("download.default_directory", downloadDir);
            opts.AddUserProfilePreference("download.prompt_for_download", false);
            opts.AddUserProfilePreference("safebrowsing.enabled", true);
        }
        return new ChromeDriver(opts);
    }
}
