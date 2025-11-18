using OpenQA.Selenium;

namespace Lab5_KPI.Pages
{
    
    public class BasicAuthPage
    {
        private readonly IWebDriver _d;
        public BasicAuthPage(IWebDriver d) { _d = d; }

        public void OpenWithCredentials(string user, string pass)
        {
            var url = $"https://{user}:{pass}@the-internet.herokuapp.com/basic_auth";
            _d.Navigate().GoToUrl(url);
        }

        public string BodyText => _d.FindElement(By.CssSelector("#content")).Text;
    }
}
