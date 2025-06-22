using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using OnlineShoePortal.PageObjects;

namespace OnlineShoePortal
{
    [TestClass]
    public class TS02_UserRegistration
    {
        [TestMethod]
        public void TC02_UserReg_SuccessfulValidation()
        {
            try
            {
                using (var helper = BaseHelper.Instance)
                {
                    var registrationPage = helper.InitPage(driver => new RegistrationPage(driver));
                    var signInPage = helper.InitPage(driver => new SignInPage(driver));

                    var homePage = new HomePage(helper.Driver);
                    homePage.ClickSignInPortal();

                    signInPage.clickNewRegistration();
                    registrationPage.select_Salutation();
                    registrationPage.enter_FirstName();
                    registrationPage.enter_LastName();
                    registrationPage.enter_UsrName();
                    registrationPage.enter_Password();
                    registrationPage.click_Submit();
                    Assert.AreEqual("User Registered Successfully !!!", SuccessRegistrationPage.SuccessMsg);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Test failed with exception: " + ex.Message);
            }
            finally
            {
                if (PropertiesCollections.driver != null)
                {
                    PropertiesCollections.driver.Close();
                    PropertiesCollections.driver.Quit();
                }
            }
        }
    }
}
