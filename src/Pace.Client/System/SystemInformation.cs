using System;

namespace Pace.Client.System;

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
            OperatingSystem = GetPlatformName()
        };

        return systemInformation;
    }

    private static string GetPlatformName()
    {
#if WINDOWS
        var key = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        return Registry.LocalMachine.OpenSubKey(key).GetValue("ProductName").ToString();
#endif

        return string.Empty;
    }
}