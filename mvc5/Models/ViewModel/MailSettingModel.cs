using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class MailSettingModel
    {
        public string Server { set; get; } = "smtp.gmail.com";
        public int Port { set; get; } = 587;
        public bool EnableSsl { set; get; } = true;
        public string UserName { set; get; } = "hung2k.com@gmail.com";
        public string Password { set; get; } = "Manhhung@18082018";
        public string FromEmail { set; get; } = "hung2k.com@gmail.com";
        public string FromName { set; get; } = "SMSOnline Admin";
    }
}
