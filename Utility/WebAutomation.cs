using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;

namespace HLTVScrapperAPI.Utility
{
    public class WebAutomation
    {
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
    }
}
