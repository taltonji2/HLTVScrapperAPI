using OpenQA.Selenium;
using SeleniumUndetectedChromeDriver;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HLTVScrapperAPI.Services
{
    public class Scraper
    {
        protected UndetectedChromeDriver Driver { get; set; }

        List<string> ChromeProcessId { get; set; }

        public Scraper()
        {
            string undetectedChromeDriverPath = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                undetectedChromeDriverPath = @"C:/Users/Timothy/source/repos/HLTVScrapperAPI/bin/ChromeDriver/chromedriver.exe";
            else 
                undetectedChromeDriverPath = @"bin/ChromeDriver/chromedriver";
            Driver = UndetectedChromeDriver.Create(driverExecutablePath: undetectedChromeDriverPath);
            
        }
        protected void Dispose() 
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
