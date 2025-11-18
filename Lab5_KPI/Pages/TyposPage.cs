using OpenQA.Selenium;

namespace SeleniumLab5.Pages;

public class TyposPage
{
    private readonly IWebDriver _d;
    public TyposPage(IWebDriver d){_d=d;}
    public void Open()=>_d.Navigate().GoToUrl("https://the-internet.herokuapp.com/typos");
    public string Paragraph => _d.FindElement(By.CssSelector("#content p")).Text;
    public void Refresh()=>_d.Navigate().Refresh();
}
