
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OnlineShoePortal.PageObjects;

namespace OnlineShoePortal
{
    [TestClass]
    public class TS03_UserLogin
    {


        [TestMethod]
        public void TC03_UserLogin_ErrValidation()
        {
            try {

                using (var helper = BaseHelper.Instance)
                {
                    var registrationPage = helper.InitPage(driver => new RegistrationPage(driver));
                    var signInPage = helper.InitPage(driver => new SignInPage(driver));

                    var homePage = new HomePage(helper.Driver);
                    homePage.ClickSignInPortal();

                    signInPage.clickLogin();
                    Assert.AreEqual("Both Username and Password field are required", signInPage.txtUsrPwdErrorMsg);

                    signInPage.enterUserName();
                    signInPage.clickLogin();
                    Assert.AreEqual("Both Username and Password field are required", signInPage.txtUsrPwdErrorMsg);
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
