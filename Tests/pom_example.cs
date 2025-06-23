// File: BasePage.cs
using AutomationFramework.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationFramework.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected void ClickElement(IWebElement element)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
        }

        protected void ClickElement(By locator)
        {
            IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            element.Click();
        }

        protected void EnterText(IWebElement element, string text)
        {
            wait.Until(ExpectedConditions.ElementIsVisible((By)element));
            element.Clear();
            element.SendKeys(text);
        }

        protected void EnterText(By locator, string text)
        {
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetElementText(By locator)
        {
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            return element.Text;
        }

        protected bool IsElementDisplayed(By locator)
        {
            try
            {
                return driver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}

namespace AutomationFramework.Pages
{
    public class LoginPage : BasePage
    {
        // Element locators
        private readonly By usernameField = By.Id("username");
        private readonly By passwordField = By.Id("password");
        private readonly By loginButton = By.Id("login-btn");
        private readonly By errorMessage = By.ClassName("error-message");
        private readonly By forgotPasswordLink = By.LinkText("Forgot Password?");

        public LoginPage(IWebDriver driver) : base(driver) { }

        // Page Methods
        public void EnterUsername(string username)
        {
            EnterText(usernameField, username);
        }

        public void EnterPassword(string password)
        {
            EnterText(passwordField, password);
        }

        public void ClickLoginButton()
        {
            ClickElement(loginButton);
        }

        public DashboardPage Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
            return new DashboardPage(driver);
        }

        public string GetErrorMessage()
        {
            return GetElementText(errorMessage);
        }

        public bool IsLoginButtonDisplayed()
        {
            return IsElementDisplayed(loginButton);
        }

        public bool IsErrorMessageDisplayed()
        {
            return IsElementDisplayed(errorMessage);
        }

        public void ClickForgotPassword()
        {
            ClickElement(forgotPasswordLink);
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }
    }
}

namespace AutomationFramework.Pages
{
    public class DashboardPage : BasePage
    {
        // Element locators
        private readonly By welcomeMessage = By.Id("welcome-message");
        private readonly By logoutButton = By.Id("logout-btn");
        private readonly By userProfile = By.ClassName("user-profile");
        private readonly By navigationMenu = By.Id("nav-menu");
        private readonly By userAvatar = By.ClassName("user-avatar");

        public DashboardPage(IWebDriver driver) : base(driver) { }

        public string GetWelcomeMessage()
        {
            return GetElementText(welcomeMessage);
        }

        public LoginPage Logout()
        {
            ClickElement(logoutButton);
            return new LoginPage(driver);
        }

        public bool IsUserProfileVisible()
        {
            return IsElementDisplayed(userProfile);
        }

        public bool IsWelcomeMessageDisplayed()
        {
            return IsElementDisplayed(welcomeMessage);
        }

        public bool IsNavigationMenuVisible()
        {
            return IsElementDisplayed(navigationMenu);
        }

        public void ClickUserAvatar()
        {
            ClickElement(userAvatar);
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }
    }
}


namespace AutomationFramework.Tests
{
    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://example.com/login");
            loginPage = new LoginPage(driver);
        }

        [TestMethod]
        public void ValidLogin_ShouldRedirectToDashboard()
        {
            // Arrange
            string username = "validuser@example.com";
            string password = "validpassword";

            // Act
            DashboardPage dashboardPage = loginPage.Login(username, password);

            // Assert
            Assert.IsTrue(dashboardPage.IsUserProfileVisible(), "User profile should be visible after login");
            Assert.IsTrue(dashboardPage.GetWelcomeMessage().Contains("Welcome"), "Welcome message should contain 'Welcome'");
            Assert.IsTrue(dashboardPage.IsNavigationMenuVisible(), "Navigation menu should be visible");
        }

        [TestMethod]
        public void InvalidLogin_ShouldShowErrorMessage()
        {
            // Arrange
            string username = "invalid@example.com";
            string password = "wrongpassword";

            // Act
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.ClickLoginButton();

            // Assert
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed(), "Error message should be displayed");
            Assert.IsTrue(loginPage.GetErrorMessage().Contains("Invalid"), "Error message should contain 'Invalid'");
            Assert.IsTrue(loginPage.IsLoginButtonDisplayed(), "Login button should still be displayed");
        }

        [TestMethod]
        public void LoginAndLogout_ShouldReturnToLoginPage()
        {
            // Arrange
            string username = "validuser@example.com";
            string password = "validpassword";

            // Act
            DashboardPage dashboardPage = loginPage.Login(username, password);
            LoginPage returnedLoginPage = dashboardPage.Logout();

            // Assert
            Assert.IsTrue(returnedLoginPage.IsLoginButtonDisplayed(), "Should return to login page");
            Assert.IsTrue(returnedLoginPage.GetPageTitle().Contains("Login"), "Page title should contain 'Login'");
        }

        [TestMethod]
        public void EmptyCredentials_ShouldShowValidationError()
        {
            // Act
            loginPage.ClickLoginButton();

            // Assert
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed(), "Validation error should be displayed");
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver?.Quit();
        }
    }
}

namespace AutomationFramework.Tests
{
    [TestClass]
    public class DashboardTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;
        private DashboardPage dashboardPage;

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://example.com/login");
            loginPage = new LoginPage(driver);
            
            // Login to get to dashboard
            dashboardPage = loginPage.Login("validuser@example.com", "validpassword");
        }

        [TestMethod]
        public void Dashboard_ShouldDisplayWelcomeMessage()
        {
            // Assert
            Assert.IsTrue(dashboardPage.IsWelcomeMessageDisplayed(), "Welcome message should be displayed");
            Assert.IsTrue(dashboardPage.GetWelcomeMessage().Length > 0, "Welcome message should not be empty");
        }

        [TestMethod]
        public void UserProfile_ShouldBeVisible()
        {
            // Assert
            Assert.IsTrue(dashboardPage.IsUserProfileVisible(), "User profile should be visible");
        }

        [TestMethod]
        public void NavigationMenu_ShouldBeAccessible()
        {
            // Assert
            Assert.IsTrue(dashboardPage.IsNavigationMenuVisible(), "Navigation menu should be visible");
        }

        [TestMethod]
        public void UserAvatar_ShouldBeClickable()
        {
            // Act & Assert - should not throw exception
            dashboardPage.ClickUserAvatar();
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver?.Quit();
        }
    }
}