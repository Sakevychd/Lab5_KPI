using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Lab5_KPI.Pages;

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
    // якщо алерта немає — кине NoAlertPresentException (це ок для контракту)
    var alert = _d.SwitchTo().Alert();
    var text = alert.Text ?? string.Empty;
    alert.Accept();
    return text;
}

}
