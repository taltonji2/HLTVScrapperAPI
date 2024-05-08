using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;

namespace HLTVScrapperAPI.Utility
{
    public class WebAutomation
    {
        public static void BypassCloudflare(IWebDriver driver, string downloadLink)
        {
            // Execute JavaScript to open a new tab with the download link
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.open('{downloadLink}', '_blank')");

            Thread.Sleep(TimeSpan.FromSeconds(3)); // Wait for the page to load

            // Switch to the first window
            driver.SwitchTo().Window(driver.WindowHandles[0]);

            // Close the first window
            driver.Close();

            // Switch back to the original window
            driver.SwitchTo().Window(driver.WindowHandles[0]);

            // Switch to the frame (if any)
            driver.SwitchTo().Frame(0);

            // Find the button and click it
            IWebElement button = driver.FindElement(By.CssSelector("#challenge-stage"));
            button.Click();
        }

        public static void NavigateToHLTV(IWebDriver driver, string route)
        {
            string urlString = $"https://www.hltv.org/{route}";
            string url = Uri.EscapeDataString(urlString);

            driver.Navigate().GoToUrl(url);

            Thread.Sleep(TimeSpan.FromSeconds(1));

            driver.FindElement(By.Id("CybotCookiebotDialogBodyButtonDecline"));
        }

        public static void CleanupProcesses()
        {
            try
            {
                Process[] chromeProcesses = Process.GetProcesses()
                    .Where(p => p.ProcessName.Equals("chrome") && p.MainModule?.FileName.EndsWith("chrome.exe") == true)
                    .ToArray();

                foreach (Process chromeProcess in chromeProcesses)
                {
                    chromeProcess.Kill();
                    Console.WriteLine($"Chrome process with PID {chromeProcess.Id} killed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error killing Chrome processes: {ex.Message}");
            }
        }

        public static Func<IWebDriver, IWebElement> IsElementClickableDelegate(By locator)
        {
            return driver =>
            {
                var element = driver.FindElement(locator);
                return (element != null && element.Displayed && element.Enabled) ? element : null;
            };
        }
    }
}
