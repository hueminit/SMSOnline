namespace Models.ViewModel.Others
{
    public class MailSettingModel
    {
        public string Server { set; get; } = "smtp.sendgrid.net";
        public int Port { set; get; } = 465;
        public bool EnableSsl { set; get; } = true;
        public string UserName { set; get; } = "contact@zozo.vn";
        public string Password { set; get; } = "Zozo$%89#$1@1";
        public string FromEmail { set; get; } = "SMSOnline@gmail.com";
        public string FromName { set; get; } = "SMSOnline Admin";
    }
}