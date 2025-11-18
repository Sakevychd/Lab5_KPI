using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumLab5.Drivers;
using SeleniumLab5.Pages;
using SeleniumLab5.Utils;

namespace SeleniumLab5.Tests;

[TestFixture]
public class Variant2Tests
{
    private IWebDriver _driver = null!;
    private WebDriverWait _wait = null!;
    private string _downloadDir = null!;

    [SetUp]
    public void SetUp()
    {
        
        _downloadDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, "downloads");
        Directory.CreateDirectory(_downloadDir);
        _driver = WebDriverFactory.CreateChrome(_downloadDir);
        _wait = new WebDriverWait(new SystemClock(), _driver, TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(250));
    }

    [TearDown]
    public void TearDown()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            TestScreenshots.Save(_driver, TestContext.CurrentContext.Test.Name);
        }
        _driver.Quit();
    }

    // 2.1 Forgot Password
    [Test]
    public void ForgotPassword_ShouldShowSuccessFlash()
    {
        var page = new ForgotPasswordPage(_driver, _wait);
        page.Open();
        page.TypeEmail("test@example.com");
        page.Retrieve();
        page.Flash.Should().Contain("Your e-mail's been sent!");
    }

    // 2.2 Horizontal Slider
    [Test]
    public void HorizontalSlider_ShouldReach_3_5()
    {
        var page = new HorizontalSliderPage(_driver, _wait);
        page.Open();
        page.SetValue(3.5);
        page.ValueText.Should().Be("3.5");
    }

    // 2.3 Dropdown
    [Test]
    public void Dropdown_Select_Option2()
    {
        var page = new DropdownPage(_driver);
        page.Open();
        page.SelectByText("Option 2");
        page.SelectedText.Should().Be("Option 2");
    }

    // 2.4 Typos (текст іноді з помилкою)
    [Test]
    public void Typos_ShouldEventuallyContain_ExpectedSentence()
    {
        var page = new TyposPage(_driver);
        page.Open();
        const string mustContain = "Sometimes you'll see a typo, other times you won't.";
        var found = false;
        for (int i = 0; i < 6; i++)
        {
            if (page.Paragraph.Contains("Sometimes") && page.Paragraph.EndsWith(".")) { found = true; break; }
            page.Refresh();
        }
        found.Should().BeTrue("ми очікуємо побачити варіант без орфографічної помилки за кілька перезавантажень");
    }

    // 2.5 Entry Ad (модал)
    [Test]
    public void EntryAd_Modal_OpenClose_Reopen()
    {
        var page = new EntryAdPage(_driver, _wait);
        page.Open();
        page.ModalVisible().Should().BeTrue();
        page.CloseModal();
        page.ModalVisible().Should().BeFalse();

        page.Reopen();
        page.ModalVisible().Should().BeTrue(); // модальне вікно знову показується
    }

    // 2.6 File Download
    [Test]
    public void FileDownload_FirstFile_ShouldAppearInFolder()
    {
        var page = new FileDownloadPage(_driver, _wait, _downloadDir);
        page.Open();
        page.DownloadFirst();
        page.AnyFileDownloaded().Should().BeTrue("файл має завантажитись до налаштованої директорії");
    }

    // 2.7 Basic Auth (через URL з кредами demo/demo або admin/admin)
    [Test]
    public void BasicAuth_ShouldShow_SuccessMessage()
    {
        _driver.Navigate().GoToUrl("https://admin:admin@the-internet.herokuapp.com/basic_auth");
        var content = _driver.FindElement(By.CssSelector("#content")).Text;
        content.Should().Contain("Congratulations! You must have the proper credentials.");
    }

    // 2.8 Dynamic Loading (Example 1)
    [Test]
    public void DynamicLoading_Start_Waits_HelloWorld()
    {
        var page = new DynamicLoadingPage(_driver, _wait);
        page.OpenExample1();
        page.Start();
        page.HelloText().Should().Be("Hello World!");
    }

    // 2.9 Context Menu (alert)
    [Test]
    public void ContextMenu_RightClick_ShowsAlert()
    {
        var page = new ContextMenuPage(_driver);
        page.Open();
        page.RightClickHotspot();
        var text = page.AcceptAlert();
        text.Should().Contain("You selected a context menu");
        // перевіримо, що алерта більше немає
        Action act = () => _driver.SwitchTo().Alert();
        act.Should().Throw<OpenQA.Selenium.NoAlertPresentException>();
    }

    // 2.10 Redirect Link → статус 200
    [Test]
    public void RedirectLink_GoesTo_StatusCodes_And_200()
    {
        var page = new RedirectLinkPage(_driver);
        page.Open();
        page.ClickHere();
        page.OpenStatus("200");
        page.BodyText.Should().Contain("This page returned a 200 status code");
    }
}
