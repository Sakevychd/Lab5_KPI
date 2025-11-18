using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumLab5.Pages;

public class FileDownloadPage
{
    private readonly IWebDriver _d; private readonly WebDriverWait _w; private readonly string _dir;
    public FileDownloadPage(IWebDriver d, WebDriverWait w, string downloadDir){_d=d; _w=w; _dir=downloadDir;}
    public void Open()=>_d.Navigate().GoToUrl("https://the-internet.herokuapp.com/download");
    public void DownloadFirst()
    {
        var link = _w.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#content a[href*='download/']")));
        var fileName = link.Text.Trim();
        link.Click();

        var target = Path.Combine(_dir, fileName);
        // чекаємо появу файлу
        for(int i=0;i<40;i++){
            if (File.Exists(target)) break;
            Thread.Sleep(250);
        }
    }
    public bool AnyFileDownloaded()=> Directory.EnumerateFiles(_dir).Any();
}
