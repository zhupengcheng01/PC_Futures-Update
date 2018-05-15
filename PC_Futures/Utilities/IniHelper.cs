using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Utilities
{
    public static class IniHelper
    {
        public static string configpath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
        public static string parameterSetting = AppDomain.CurrentDomain.BaseDirectory + "ParameterSetting.ini";
        public static string defaultUserData = AppDomain.CurrentDomain.BaseDirectory + "DefaultUserData.ini";
        [DllImport("kernel32")] // 写入配置文件的接口
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")] // 读取配置文件的接口
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        // 向配置文件写入值
        public static void ProfileWriteValue(string section, string key, string value, string path)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        // 读取配置文件的值
        public static string ProfileReadValue(string section, string key, string path)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, string.Empty, sb, 255, path);
            return sb.ToString().Trim();
        }
    }
}
