using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumLab5.Pages;

public class ForgotPasswordPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    public ForgotPasswordPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver; _wait = wait;
    }

    public void Open() => _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/forgot_password");
    public void TypeEmail(string email) => _driver.FindElement(By.Id("email")).SendKeys(email);
    public void Retrieve() => _driver.FindElement(By.Id("form_submit")).Click();
    public string Flash => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("content"))).Text;
}
