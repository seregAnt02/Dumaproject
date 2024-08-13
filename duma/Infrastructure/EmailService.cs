using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;


namespace duma.Infrastructure
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "email"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            //Attachments
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync("email", "password");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
        //-----------------------------------------------------------------------
        public async Task SendAsync(IdentityMessage message)
        {
            var email = "s_antonov02@rambler.ru";
            //var subject = "подтверждение электронной почты";
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "email"));
            emailMessage.To.Add(new MailboxAddress("", email));//электронная почта
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message.Body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.yandex.ru", 465, true);
                await client.AuthenticateAsync("email", "password");
                await client.SendAsync(emailMessage);// здесь походу не пропускает сервер smtp?

                await client.DisconnectAsync(true);
            }            
            // Подключите здесь службу электронной почты для отправки сообщения электронной почты.
            //return Task.FromResult(0);
        }
    }
}