namespace Models.ViewModel.Others
{
    public class MailSettingModel
    {
        public string Server { set; get; } = "smtp.gmail.com";
        public int Port { set; get; } = 587;
        public bool EnableSsl { set; get; } = true;
        public string UserName { set; get; } = "flatizeshop@gmail.com";
        public string Password { set; get; } = "flatize123";
        public string FromEmail { set; get; } = "SMSOnline@gmail.com";
        public string FromName { set; get; } = "SMSOnline Admin";
    }
}