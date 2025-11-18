using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumLab5.Pages;

public class HorizontalSliderPage
{
    private readonly IWebDriver _d; private readonly WebDriverWait _w;
    public HorizontalSliderPage(IWebDriver d, WebDriverWait w){_d=d; _w=w;}
    public void Open()=>_d.Navigate().GoToUrl("https://the-internet.herokuapp.com/horizontal_slider");

    public void SetValue(double target)
    {
        var slider=_w.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[type='range']")));
        // крок 0.5; від 0 до 5
        var currentVal = double.Parse(_d.FindElement(By.Id("range")).Text, System.Globalization.CultureInfo.InvariantCulture);
        var steps = (int)((target - currentVal)/0.5);
        var actions = new Actions(_d);
        var key = steps > 0 ? Keys.ArrowRight : Keys.ArrowLeft;
        for(int i=0;i<Math.Abs(steps);i++) actions.SendKeys(slider, key).Perform();
    }
    public string ValueText => _d.FindElement(By.Id("range")).Text;
}
