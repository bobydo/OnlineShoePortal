using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

class HomePage
{
    private readonly IWebDriver _driver;
    private readonly WebDriverWait _wait;
    BaseHelper _helper = BaseHelper.Instance;

    public HomePage(IWebDriver driver, int waitInSeconds = 10)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(waitInSeconds));
    }

    public void ClickSignInPortal()
    {
        var menuInput = _helper.FindElement(By.Id("menuToggle"));
        menuInput.FindElement(By.CssSelector("input[type=checkbox]")).Click();

        // Wait until the link becomes visible
        var signInLink = _wait.Until(driver =>
        {
            try
            {
                var el = driver.FindElement(By.XPath("//a[li[text()='Sign In Portal']]"));
                return el.Displayed && el.Enabled ? el : null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        });
        signInLink.Click();
    }
}
