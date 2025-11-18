using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLab5.Pages;

public class DropdownPage
{
    private readonly IWebDriver _d;
    public DropdownPage(IWebDriver d){_d=d;}
    public void Open()=>_d.Navigate().GoToUrl("https://the-internet.herokuapp.com/dropdown");
    public void SelectByText(string text)
    {
        var sel = new SelectElement(_d.FindElement(By.Id("dropdown")));
        sel.SelectByText(text);
    }
    public string SelectedText => new SelectElement(_d.FindElement(By.Id("dropdown"))).SelectedOption.Text;
}
