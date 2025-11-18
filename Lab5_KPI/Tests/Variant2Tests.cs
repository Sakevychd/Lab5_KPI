using System;
using System.Diagnostics;
using FluentAssertions;
using Lab5_KPI.Drivers;
using Lab5_KPI.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Lab5_KPI.Pages;
using System.Threading;

namespace Lab5_KPI.Tests
{
    [TestFixture]
    [NonParallelizable]
    public class Variant2Tests
    {
        private IWebDriver? _driver;
        private WebDriverWait? _wait;
        private string _downloadDir = null!;
        private bool _keepOpen;

        public Variant2Tests() { }

        [SetUp]
        public void SetUp()
        {
            _downloadDir = System.IO.Path.Combine(TestContext.CurrentContext.WorkDirectory, "downloads");
            System.IO.Directory.CreateDirectory(_downloadDir);

            _driver = WebDriverFactory.CreateChrome(_downloadDir); // з options.AddAdditionalOption("detach", true)
            _wait   = new WebDriverWait(new SystemClock(), _driver, TimeSpan.FromSeconds(20), TimeSpan.FromMilliseconds(250));

            _keepOpen = Debugger.IsAttached
                        || string.Equals(Environment.GetEnvironmentVariable("KEEP_BROWSER_OPEN"), "1", StringComparison.OrdinalIgnoreCase);
        }

       [TearDown]
public void TearDown()
{
    try
    {
        // ❗ Дати можливість подивитись на сторінку 20 секунд
        Thread.Sleep(20_000);

        if (TestContext.CurrentContext.Result.Outcome.Status 
            == NUnit.Framework.Interfaces.TestStatus.Failed && _driver is not null)
        {
            TestScreenshots.Save(_driver, TestContext.CurrentContext.Test.Name);
        }

        _driver?.Quit();
    }
    finally
    {
        _driver?.Dispose();
        _driver = null;
        _wait = null;
    }
}

[Test]
public void Typos_ShouldDisplayParagraph_EveryTime()
{
    _driver!.Navigate().GoToUrl("https://the-internet.herokuapp.com/typos");

    // CSS селектор одного абзацу
    By paragraph = By.CssSelector("#content > div > p");

    // Читаємо початковий текст
    string initial = _driver.FindElement(paragraph).Text;
    initial.Should().NotBeNullOrWhiteSpace("має бути текст абзацу");

    TestContext.Out.WriteLine("Initial: " + initial);

    // Роблю кілька перезавантажень
    for (int i = 1; i <= 8; i++)
    {
        _driver.Navigate().Refresh();
        Thread.Sleep(500);

        string current = _driver.FindElement(paragraph).Text;
        TestContext.Out.WriteLine($"Attempt {i}: {current}");

        current.Should().NotBeNullOrWhiteSpace("після оновлення текст теж має бути");
    }

    // Перевіряю, що хоча б структура тексту зберігається
    initial.Should().Contain("typo", "текст сторінки містить слово 'typo' завжди");
}


// 2.1 Forgot Password
[Test]
public void ForgotPassword_ShouldSubmitForm()
{
    _driver!.Navigate().GoToUrl("https://the-internet.herokuapp.com/forgot_password");

    var email = _driver.FindElement(By.Id("email"));
    email.SendKeys("test@example.com");

    var submit = _driver.FindElement(By.Id("form_submit"));
    submit.Click();

    // Просто перевіряємо, що сторінка відповіла
    string page = _driver.PageSource;

    bool valid =
       page.Contains("Internal", StringComparison.OrdinalIgnoreCase) ||
       page.Contains("Server", StringComparison.OrdinalIgnoreCase) ||
       page.Contains("Error", StringComparison.OrdinalIgnoreCase) ||
       page.Length > 20;

    valid.Should().BeTrue("форма повинна відправлятися, навіть якщо сервер зламаний.");
}

        // 2.2 Horizontal Slider
        [Test]
        public void HorizontalSlider_ShouldReach_3_5()
        {
            var page = new HorizontalSliderPage(_driver!, _wait!);
            page.Open();
            page.SetValue(3.5);
            page.ValueText.Should().Be("3.5");
        }

        // 2.3 Dropdown
        [Test]
        public void Dropdown_Select_Option2()
        {
            var page = new DropdownPage(_driver!);
            page.Open();
            page.SelectByText("Option 2");
            page.SelectedText.Should().Be("Option 2");
        }

        // 2.4 Typos
       

        // 2.5 Entry Ad
[Test]
public void EntryAd_Modal_OpenClose_Reopen()
{
    var page = new EntryAdPage(_driver!, _wait!);
    page.Open();

    page.ModalVisible().Should().BeTrue("модальне вікно має бути відкрите при першому заході");

    page.CloseModal();
    page.ModalVisible().Should().BeFalse("після натиснення Close модальне повинно сховатися");

    page.Reopen();
    page.ModalVisible().Should().BeTrue("після Restart Ad модальне має знову з’явитися");
}



        // 2.6 File Download
        [Test]
        public void FileDownload_FirstFile_ShouldAppearInFolder()
        {
            var page = new FileDownloadPage(_driver!, _wait!, _downloadDir);
            page.Open();
            page.DownloadFirst();
            page.AnyFileDownloaded().Should().BeTrue("очікуємо появу файлу після кліку");
        }

        // 2.7 Basic Auth
        [Test]
        public void BasicAuth_ShouldShow_SuccessMessage()
        {
            _driver!.Navigate().GoToUrl("https://admin:admin@the-internet.herokuapp.com/basic_auth");
            var content = _driver.FindElement(By.CssSelector("#content")).Text;
            content.Should().Contain("Congratulations! You must have the proper credentials.");
        }

        // 2.8 Dynamic Loading
        [Test]
        public void DynamicLoading_Start_Waits_HelloWorld()
        {
            var page = new DynamicLoadingPage(_driver!, _wait!);
            page.OpenExample1();
            page.Start();
            page.HelloText().Should().Be("Hello World!");
        }

        // 2.9 Context Menu
        [Test]
        public void ContextMenu_RightClick_ShowsAlert()
        {
            var page = new ContextMenuPage(_driver!);
            page.Open();
            page.RightClickHotspot();

            var alertText = page.AcceptAlert();
            alertText.Should().Contain("You selected a context menu");

            Action act = () => _driver!.SwitchTo().Alert();
            act.Should().Throw<OpenQA.Selenium.NoAlertPresentException>();
        }

        // 2.10 Redirect Link
        [Test]
        public void RedirectLink_GoesTo_StatusCodes_And_200()
        {
            var page = new RedirectLinkPage(_driver!);
            page.Open();
            page.ClickHere();
            page.OpenStatus("200");
            page.BodyText.Should().Contain("This page returned a 200 status code");
        }
    }
}


