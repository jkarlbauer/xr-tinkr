using UnityEngine;

namespace Xrtinkr.System
{
    public static class SystemState
    {
        public static bool isHandheld => SystemInfo.deviceType == DeviceType.Handheld;
        public static bool isDesktop => SystemInfo.deviceType == DeviceType.Desktop;
        public static bool isOculusQuest => SystemInfo.deviceModel.Equals("Oculus Quest");

        public static string GetSystemInfo()
        {
            string systemInfo = $"{Application.productName} is running on:\n" +
                $"Device Name: {SystemInfo.deviceName}\n" +
                $"Device Type: {SystemInfo.deviceType}\n" +
                $"Device Model: {SystemInfo.deviceModel}\n" +
                $"Operating System: {SystemInfo.operatingSystem}\n";

            return systemInfo;
        }
    }
}

