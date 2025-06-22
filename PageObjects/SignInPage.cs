using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoePortal.PageObjects
{
    class SignInPage
    {
        BaseHelper _helper;
        public int txtuserlength => _helper.CountElements(By.Id("usr"));
        public int txtpwdlength => _helper.CountElements(By.Id("pwd"));
        public int btnLogin => _helper.CountElements(By.XPath("//input[@value='Login']"));
        public int btnRegistration => _helper.CountElements(By.Id("NewRegistration"));
        public string txtUsrPwdErrorMsg =>_helper.FindElement(By.XPath("//*[@id=\"second_form\"]/div[2]/span")).Text;

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public SignInPage(IWebDriver driver, int waitInSeconds = 10)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitInSeconds));
            _helper = BaseHelper.Instance;
        }

        public void clickNewRegistration()
        {
            var btnNewRegistration = _helper.FindElement(By.Id("NewRegistration"));
            btnNewRegistration.Click();
        }

        public void clickLogin()
        {
            var btnLgn = _helper.FindElement(By.XPath("//*[@id=\"second_form\"]/input"));
            btnLgn.Click();
        }

        public void enterUserName()
        {
            var txtUserName = _helper.FindElement(By.XPath("//*[@id=\"usr\"]"));
            txtUserName.SendKeys(ConfigurationManager.AppSettings["Username"]);
        }
        public void enterPassword()
        {
            var txtPassword = _helper.FindElement(By.XPath("//*[@id=\"pwd\"]"));
            txtPassword.SendKeys(ConfigurationManager.AppSettings["Password"]);
        }
    }
}
