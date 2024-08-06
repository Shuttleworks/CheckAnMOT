using System.Runtime.CompilerServices;
using CheckAnMOT.Core.Helpers;
using NuGet.Frameworks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;

namespace CheckAnMOT.Test
{
    public class SeleniumTests
    {
        private readonly string _localhostUrl = "https://localhost:7252/";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SubmitInvalidData()
        {
            string invalidData = "foo12345";
            string expectedResult = "Invalid numberplate entered!";
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = _localhostUrl;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);

            var textInput = driver.FindElement(By.Name("numberplate"));
            var submitButton = driver.FindElement(By.Id("btnSubmit"));

            textInput.SendKeys(invalidData);
            submitButton.Click();


            var result = driver.FindElement(By.Id("errormessage"));
            var message = result.Text;

            driver.Close();
            Assert.That(message, Is.EqualTo(expectedResult));
        }

        [Test]
        public void SubmitValidData()
        {
            string validData = "WP73 MKN";
            string expectedResult = "You are checking reg plate:";
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = _localhostUrl;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);

            var textInput = driver.FindElement(By.Name("numberplate"));
            var submitButton = driver.FindElement(By.Id("btnSubmit"));

            textInput.SendKeys(validData);
            submitButton.Click();


            var result = driver.FindElement(By.Id("check-confirm"));
            var message = result.Text;

            driver.Close();
            Assert.That(message, Is.EqualTo(expectedResult));
        }

        [Test]
        public void SubmitScrappedReg()
        {
            string validData = "M823 FTT";
            string expectedResult = "No current MOT";

            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = _localhostUrl;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);

            var textInput = driver.FindElement(By.Name("numberplate"));
            var submitButton = driver.FindElement(By.Id("btnSubmit"));

            textInput.SendKeys(validData);
            submitButton.Click();


            var result = driver.FindElement(By.Id("check-expiry"));
            var message = result.Text;

            driver.Close();
            Assert.That(message, Is.EqualTo(expectedResult));
        }

        [Test]
        public void SubmitInvalidDataEdge()
        {
            string invalidData = "12345678";
            string expectedResult = "Invalid numberplate entered!";
            IWebDriver driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = _localhostUrl;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);

            var textInput = driver.FindElement(By.Name("numberplate"));
            var submitButton = driver.FindElement(By.Id("btnSubmit"));

            textInput.SendKeys(invalidData);
            submitButton.Click();


            var result = driver.FindElement(By.Id("errormessage"));
            var message = result.Text;

            driver.Close();
            Assert.That(message, Is.EqualTo(expectedResult));
        }

        [Test]
        public void EnterInvalidData_SubmitShouldStayDisabled()
        {
            string invalidData = "foobar 123!";
            string expectedResult = "true";

            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = _localhostUrl;

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);

            var textInput = driver.FindElement(By.Name("numberplate"));
            var submitButton = driver.FindElement(By.Id("btnSubmit"));

            textInput.SendKeys(invalidData);
            var isDisabled = submitButton.GetAttribute("disabled");


            driver.Close();
            Assert.That(isDisabled, Is.EqualTo(expectedResult));
        }
    }
}