using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

public class BaseHelper : IDisposable
{
    private static BaseHelper _instance;
    private static readonly object _lock = new object();
    private IWebDriver _driver;
    private bool _disposed;
    private WebDriverWait _wait => new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

    private BaseHelper() { }    

    public static BaseHelper Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new BaseHelper();
                }
            }
            return _instance;
        }
    }

    public IWebDriver Driver
    {
        get
        {
            if (_driver == null)
            {
                var options = new ChromeOptions();
                options.AddArgument("--no-sandbox");

                _driver = new ChromeDriver(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    options);

                _driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);
            }
            return _driver;
        }
    }

    public T InitPage<T>(Func<IWebDriver, T> factory) where T : class
    {
        return factory(Driver);
    }
    public IWebElement FindElement(By by)
    {
        var el = _wait.Until(driver => driver.FindElement(by));
        return el.Displayed ? el : null;
    }

    public int CountElements(By by)
    {
        var elements = _wait.Until(driver => driver.FindElements(by));
        return elements.Count;
    }

    public void QuitDriver()
    {
        if (_driver != null)
        {
            _driver.Quit();
            _driver.Dispose();
            _driver = null;
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            QuitDriver();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    ~BaseHelper()
    {
        Dispose();
    }
}
