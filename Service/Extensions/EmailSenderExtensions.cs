using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Services.Interface;

namespace Services.Extensions
{
    public static class EmailSenderExtensions
    {
        // tạo ra link để xác nhận email
        public static Task SendEmailConfirmationAsync(this IEmailService emailService, string email, string link)
        {
            return emailService.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
