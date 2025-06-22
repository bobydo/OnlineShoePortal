using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;

namespace OnlineShoePortal.PageObjects
{
    class RegistrationPage
    {
        BaseHelper _helper;
        public string txtErrorMsg => _helper.FindElement(By.XPath("//*[@id=\"first_form\"]/div/span")).Text;
        public string txtErrorMsg2 => _helper.FindElement(By.XPath("//*[@id=\"first_form\"]/div/span")).Text;

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public RegistrationPage(IWebDriver driver, int waitInSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitInSeconds));
            _helper = BaseHelper.Instance;

        }

        public void select_Salutation()
        {
            var dropdownElement = _helper.FindElement(By.Id("Salutation"));
            var drpSalutation = new SelectElement(dropdownElement);
            drpSalutation.SelectByText(ConfigurationManager.AppSettings["Salutation"]);
        }

        public void click_Submit()
        {
            var btnSubmit = _helper.FindElement(By.XPath("//input[@value='Submit']"));
            btnSubmit.Click();       
        }
        public void enter_FirstName()
        {
            var txtFirstName = _helper.FindElement(By.Id("firstname"));
            txtFirstName.SendKeys(ConfigurationManager.AppSettings["FirstName"]);
        }
        public void enter_LastName()
        {
            var txtLastName = _helper.FindElement(By.Id("lastname"));
            txtLastName.SendKeys(ConfigurationManager.AppSettings["LastName"]);
        }
        public void enter_InvalidEmail()
        {
            var txtEmailid = _helper.FindElement(By.Id("emailId"));
            txtEmailid.SendKeys(ConfigurationManager.AppSettings["InvalidEmailAddress"]);
        }
        public void enter_ValidEmail()
        {
            var txtEmailid = _helper.FindElement(By.Id("emailId"));
            txtEmailid.SendKeys(ConfigurationManager.AppSettings["ValidEmailAddress"]);            
        }
        public void enter_UsrName()
        {
            var txtUsername = _helper.FindElement(By.Id("usr"));
            txtUsername.SendKeys(ConfigurationManager.AppSettings["Username"]);
        }
        public void enter_Password()
        {
            var txtPassword = _helper.FindElement(By.Id("pwd"));
            txtPassword.SendKeys(ConfigurationManager.AppSettings["Password"]);
        }

    }
}
