using OpenQA.Selenium;

namespace Lab5_KPI.Pages;

public class RedirectLinkPage
{
    private readonly IWebDriver _d;
    public RedirectLinkPage(IWebDriver d){_d=d;}
    public void Open()=>_d.Navigate().GoToUrl("https://the-internet.herokuapp.com/redirector");
    public void ClickHere()=>_d.FindElement(By.Id("redirect")).Click();
    public void OpenStatus(string code) => _d.FindElement(By.CssSelector($"a[href='status_codes/{code}']")).Click();
    public string BodyText => _d.FindElement(By.CssSelector("#content")).Text;
}
