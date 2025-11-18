using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lab5_KPI.Pages
{
    public class HorizontalSliderPage
    {
        private readonly IWebDriver _d;
        private readonly WebDriverWait _w;
        public HorizontalSliderPage(IWebDriver d, WebDriverWait w){_d=d; _w=w;}
        public void Open()=>_d.Navigate().GoToUrl("https://the-internet.herokuapp.com/horizontal_slider");

        private static double ParseVal(string s)
            => double.Parse(s.Trim(), System.Globalization.CultureInfo.InvariantCulture);

        public void SetValue(double target)
        {
            target = Math.Max(0, Math.Min(5, target));
            var slider  = _w.Until(d => d.FindElement(By.CssSelector("input[type='range']")));
            var display = _w.Until(d => d.FindElement(By.Id("range")));

            // фокус на повзунку
            slider.Click();

            // 1) Скидання у 0
            for (int i = 0; i < 20; i++) slider.SendKeys(Keys.ArrowLeft);
            Thread.Sleep(50);

            // 2) Рух праворуч до target кроком 0.5
            var steps = (int)Math.Round(target / 0.5);
            for (int i = 0; i < steps; i++)
            {
                slider.SendKeys(Keys.ArrowRight);
                Thread.Sleep(30);
            }

            // 3) Чекаємо чисельного збігу з допуском
            _w.Until(_ =>
            {
                try
                {
                    var v = ParseVal(display.Text);
                    return Math.Abs(v - target) < 0.001;
                }
                catch { return false; }
            });
        }

        public string ValueText => _d.FindElement(By.Id("range")).Text;
    }
}
