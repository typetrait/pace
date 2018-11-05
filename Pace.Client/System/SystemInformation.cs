using Microsoft.Win32;
using System;

namespace Pace.Client.System
{
    /// <summary>
    /// Represents the current system's information.
    /// </summary>
    public class SystemInformation
    {
        public string UserName { get; set; }
        public string ComputerName { get; set; }
        public string OperatingSystem { get; set; }

        public static SystemInformation Get()
        {
            var systemInformation = new SystemInformation
            {
                UserName = Environment.UserName,
                ComputerName = Environment.MachineName,
                OperatingSystem = GetProductName()
            };

            return systemInformation;
        }

        private static string GetProductName()
        {
            var key = @"SOFTWARE\Wow6432Node\Microsoft\Windows NT\CurrentVersion";
            return Registry.LocalMachine.OpenSubKey(key).GetValue("ProductName").ToString();
        }
    }
}