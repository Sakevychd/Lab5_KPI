using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace SeleniumLab5.Pages;

public class ContextMenuPage
{
    private readonly IWebDriver _d;
    public ContextMenuPage(IWebDriver d){_d=d;}
    public void Open()=>_d.Navigate().GoToUrl("https://the-internet.herokuapp.com/context_menu");
    public void RightClickHotspot()
    {
        var box=_d.FindElement(By.Id("hot-spot"));
        new Actions(_d).ContextClick(box).Perform();
    }
    public string AcceptAlert()
    {
        var alert = _d.SwitchTo().Alert();
        var text = alert.Text;
        alert.Accept();
        return text;
    }
}
