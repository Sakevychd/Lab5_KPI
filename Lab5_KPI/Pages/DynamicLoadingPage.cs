using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab5_KPI.Pages;

public class DynamicLoadingPage
{
    private readonly IWebDriver _d; private readonly WebDriverWait _w;
    public DynamicLoadingPage(IWebDriver d, WebDriverWait w){_d=d; _w=w;}
    public void OpenExample1()=>_d.Navigate().GoToUrl("https://the-internet.herokuapp.com/dynamic_loading/1");
    public void Start()=>_d.FindElement(By.CssSelector("#start button")).Click();
    public string HelloText()
    {
        _w.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("loading")));
        return _w.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#finish h4"))).Text;
    }
}
