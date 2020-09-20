using Models.ViewModel.Others;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }

    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                MailSettingModel mailSetting = new MailSettingModel();
                SmtpClient client = new SmtpClient(mailSetting.Server)
                {
                    UseDefaultCredentials = false,
                    Port = mailSetting.Port,
                    EnableSsl = mailSetting.EnableSsl,
                    Credentials = new NetworkCredential(mailSetting.UserName, mailSetting.Password),
                    Timeout = 5
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(mailSetting.FromEmail, mailSetting.FromName),
                };

                mailMessage.To.Add(email);
                mailMessage.Body = message;
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // todo
            }
            return Task.CompletedTask;
        }
    }
}