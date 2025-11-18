using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lab5_KPI.Pages;

public class ForgotPasswordPage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;

    public ForgotPasswordPage(IWebDriver driver, WebDriverWait wait)
    {
        _driver = driver;
        _wait = wait;
    }

    public void Open()
    {
        _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/forgot_password");
    }

    public void TypeEmail(string email)
    {
        var input = _wait.Until(d => d.FindElement(By.Id("email")));
        input.Clear();
        input.SendKeys(email);
    }

    public void Retrieve()
    {
        var btn = _wait.Until(d => d.FindElement(By.Id("form_submit")));
        btn.Click();
    }

    public string Flash
    {
        get
        {
            Thread.Sleep(2000); // даємо сторінці завантажитись, навіть якщо це 500

            try
            {
                return _driver.FindElement(By.Id("content")).Text.Trim();
            }
            catch
            {
                // Навіть при 500 на сторінці є <body> → читаємо весь текст
                return _driver.PageSource;
            }
        }
    }
}
