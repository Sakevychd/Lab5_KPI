using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumLab5.Pages;

public class EntryAdPage
{
    private readonly IWebDriver _d; private readonly WebDriverWait _w;
    public EntryAdPage(IWebDriver d, WebDriverWait w){_d=d; _w=w;}
    public void Open()=>_d.Navigate().GoToUrl("https://the-internet.herokuapp.com/entry_ad");

    public bool ModalVisible()
    {
        try
        {
            var modal=_w.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".modal")));
            return modal.Displayed;
        } catch { return false; }
    }

    public void CloseModal()
    {
        var close = _w.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(".modal .modal-footer p")));
        close.Click();
        _w.Until(ExpectedConditions.InvisibilityOfElementLocated(By.CssSelector(".modal")));
    }

    public void Reopen()
    {
        _d.FindElement(By.LinkText("click here")).Click();
    }
}
