using System.Configuration;

namespace Data.Helpers
{
    public class ConfigHelper
    {
        //lấy ra key ở app setting
        public static string GetByKey(string key)
        {   //references System.Configuration;
            return ConfigurationManager.AppSettings[key].ToString();
        }
    }
}