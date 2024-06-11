using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumUndetectedChromeDriver;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HLTVScrapperAPI.Services
{
    public class Scraper
    {
        protected IWebDriver Driver { get; set; }

        Hashtable ChromeProcesses { get; set; } = new Hashtable();

        public Scraper()
        {
            Driver = this.CreateDriver();
        }

        public IWebDriver CreateDriver()
        {
            string undetectedChromeDriverPath = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                undetectedChromeDriverPath = @"C:/Users/Timothy/source/repos/HLTVScrapperAPI/bin/ChromeDriver/chromedriver.exe";
            else
                undetectedChromeDriverPath = @"bin/ChromeDriver/chromedriver";
            return UndetectedChromeDriver.Create(driverExecutablePath: undetectedChromeDriverPath);
        }

        public WebDriverWait CreateWaitDriver(int seconds)
        {
            if (Driver == null) { IWebDriver Driver = this.CreateDriver(); }
            WebDriverWait DriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            return DriverWait;
        }

        public void DisposeDriver() 
        {
            try
            {
                Driver.Close();
                Driver.Quit();
                Driver.Dispose();

                //TODO: close all chrome processes except the main application
                IEnumerable<int> chromePids = Process.GetProcessesByName("chrome").Select(p => p.Id);
                
                foreach (int pid in chromePids)
                {
                    Process chromeProcess = Process.GetProcessById(pid);
                    if (chromeProcess != null) Process.GetProcessById(pid).Kill();
                    Debug.WriteLine($"Killed process {pid}");
                }
                Debug.WriteLine("Successfully disposed chromedriver");
            } 
            catch (Exception e)
            {
                Debug.WriteLine($"Error occured while disposing chromedriver {e.Message}");
            }
           
        }
    }
}
