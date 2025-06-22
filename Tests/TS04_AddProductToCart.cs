using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using OnlineShoePortal.PageObjects;

namespace OnlineShoePortal
{
    [TestClass]
    public class TS04_AddProductToCart
    {


        [TestMethod]
        public void TC04_AdddProductToCart()
        {
            try {
                using (var helper = BaseHelper.Instance)
                {
                    var signInPage = helper.InitPage(driver => new SignInPage(driver));

                    var homePage = new HomePage(helper.Driver);
                    homePage.ClickSignInPortal();

                    signInPage.enterUserName();
                    signInPage.enterPassword();
                    signInPage.clickLogin();
                    ShoeTypes.clickFormalShoeCollection();
                    FormalShoesTypes.Add_Product_to_Cart();
                    Assert.AreEqual("Added to Cart Successfully !!!", SuccessProductToCart.SuccessMsg);
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
