using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
