using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumUndetectedChromeDriver;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
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
            try
            {
                string undetectedChromeDriverPath = "";
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    undetectedChromeDriverPath = @"C:/Users/Timothy/source/repos/HLTVScrapperAPI/bin/ChromeDriver/chromedriver.exe";
                else
                    undetectedChromeDriverPath = @"bin/ChromeDriver/chromedriver";
                return UndetectedChromeDriver.Create(driverExecutablePath: undetectedChromeDriverPath);
            }
            catch (OpenQA.Selenium.WebDriverException e)
            {
                Debug.WriteLine($"Error occured while creating chromedriver {e.Message}");
                GetLatestChromeDriver();
                return null;
            }
        }
        public WebDriverWait CreateWaitDriver(int seconds)
        {
            if (Driver == null) { IWebDriver Driver = this.CreateDriver(); }
            WebDriverWait DriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(seconds));
            return DriverWait;
        }
        public void SearchEntity(string type, string name)
        {
            HashSet<string> types = new HashSet<string> { "player", "team", "article" };
            if (!types.Contains(type)) { throw new Exception("Invalid type"); }

            Driver.Navigate().GoToUrl($"https://www.hltv.org/");
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));

            IWebElement cybotCookieDialogDeclineElement = wait.Until(d =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                return d.FindElement(By.Id("CybotCookiebotDialogBodyButtonDecline"));
            });

            cybotCookieDialogDeclineElement.Click();
            Thread.Sleep(TimeSpan.FromSeconds(2));

            IWebElement searchInput = Driver.FindElement(By.CssSelector("input[class='navsearchinput tt-input']"));
            searchInput.Click();
            searchInput.SendKeys(name);
            searchInput.SendKeys(Keys.Return);
            Thread.Sleep(TimeSpan.FromSeconds(1));

            ReadOnlyCollection<IWebElement> searchTables = Driver.FindElements(By.CssSelector("table[class='table']"));
            IWebElement table = searchTables.First(table =>
            table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).Any(tr =>
                tr.FindElements(By.TagName("td")).Any(td =>
                    td.GetAttribute("class").Contains("table-header") && td.Text.ToLower().Contains(type))));

            IWebElement hyperlinkElement = null;

            var matchingRow = table.FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr"))
                .FirstOrDefault(tr => tr.FindElements(By.TagName("td"))
                    .Any(td => td.FindElements(By.TagName("a"))
                        .Any(a => a.Text.ToLower().Contains(name.ToLower()))));

            if (matchingRow != null)
            {
                hyperlinkElement = matchingRow.FindElements(By.TagName("td"))
                    .SelectMany(td => td.FindElements(By.TagName("a")))
                    .First(a => a.Text.ToLower().Contains(name.ToLower()));
                hyperlinkElement.Click();
            }
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

        private void GetLatestChromeDriver()
        {
            using (WebClient client = new WebClient())
            {
                string lastKnownStableVersion = client.DownloadString("https://googlechromelabs.github.io/chrome-for-testing/last-known-good-versions-with-downloads.json");
                dynamic json = JsonConvert.DeserializeObject<dynamic>(lastKnownStableVersion);
                string downloadurl = json.channels.Stable.downloads.chrome[4].url;
                client.DownloadFile(downloadurl, "C:/Users/Timothy/Downloads");
                
                using (ZipArchive archive = ZipFile.OpenRead("C:/Users/Timothy/Downloads/chromedriver-win64.zip"))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        entry.ExtractToFile(Path.Combine("C:/Users/Timothy/source/repos/HLTVScrapperAPI/bin/ChromeDriver/", entry.FullName));
                    }
                }
            }
        }
    }
}
