using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OnlineShoePortal.PageObjects;

namespace OnlineShoePortal
{
    [TestClass]
    public class TS01_UserRegistration
    {
        [TestMethod]
        public void TC01_UserReg_ErrValidation()
        {
            try
            {
                using (var helper = BaseHelper.Instance)
                {
                    var registrationPage = helper.InitPage(driver => new RegistrationPage(driver));
                    var signInPage = helper.InitPage(driver => new SignInPage(driver));

                    var homePage = new HomePage(helper.Driver);
                    homePage.ClickSignInPortal();

                    Assert.AreEqual(1, signInPage.txtuserlength);
                    Assert.AreEqual(1, signInPage.txtpwdlength);
                    Assert.AreEqual(1, signInPage.btnLogin);
                    Assert.AreEqual(1, signInPage.btnRegistration);

                    signInPage.clickNewRegistration();

                    registrationPage.select_Salutation();
                    registrationPage.click_Submit();

                    Assert.AreEqual("This field is required", registrationPage.txtErrorMsg);

                    registrationPage.enter_FirstName();
                    registrationPage.click_Submit();

                    Assert.AreEqual("This field is required", registrationPage.txtErrorMsg);

                    registrationPage.enter_LastName();
                    registrationPage.click_Submit();

                    Assert.AreEqual("This field is required", registrationPage.txtErrorMsg);

                    registrationPage.enter_InvalidEmail();
                    registrationPage.click_Submit();

                    Assert.AreEqual("Enter a valid email", registrationPage.txtErrorMsg);

                    registrationPage.enter_ValidEmail();
                    registrationPage.click_Submit();

                    Assert.AreEqual("This field is required", registrationPage.txtErrorMsg);

                    registrationPage.enter_UsrName();
                    registrationPage.click_Submit();
                    Assert.AreEqual("This field is required", registrationPage.txtErrorMsg);

                    registrationPage.enter_Password();
                    registrationPage.click_Submit();
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
