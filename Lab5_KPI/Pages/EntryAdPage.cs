using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Lab5_KPI.Pages
{
    public class EntryAdPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public EntryAdPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _wait  = wait  ?? throw new ArgumentNullException(nameof(wait));
        }

        public void Open()
        {
            _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/entry_ad");
            // чекаємо, поки модалка хоч раз з’явиться (або буде видимою)
            _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("modal")));
        }

        private IWebElement Modal => _driver.FindElement(By.Id("modal"));

        public bool ModalVisible()
        {
            try
            {
                return Modal.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void CloseModal()
        {
            // кнопка "Close" в футері модального
            var close = _wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.XPath("//div[@id='modal']//p[text()='Close']"))
            );
            close.Click();

            // чекаємо, поки модалка стане невидимою
            _wait.Until(d =>
            {
                try
                {
                    return !d.FindElement(By.Id("modal")).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return true;
                }
            });
        }

        public void Reopen()
        {
            // посилання "click here" для рестарту реклами
            var restart = _wait.Until(
                ExpectedConditions.ElementToBeClickable(By.Id("restart-ad"))
            );
            restart.Click();

            // чекаємо, поки модалка знову стане видимою
            _wait.Until(d =>
            {
                try
                {
                    return d.FindElement(By.Id("modal")).Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }
    }
}
