using OpenQA.Selenium;

namespace Lab5_KPI.Pages
{
    public class TyposPage
    {
        private readonly IWebDriver _driver;

        public TyposPage(IWebDriver driver)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        public void Open()
        {
            _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/typos");
        }

        public string Paragraph
        {
            get
            {
                // Безпечний пошук елементів
                var elements = _driver.FindElements(By.CssSelector("#content p"));

                if (elements.Count == 0)
                    return string.Empty; // ніколи не повертає null

                return elements[0].Text ?? string.Empty;
            }
        }

        public void Refresh()
        {
            _driver.Navigate().Refresh();
        }
    }
}
