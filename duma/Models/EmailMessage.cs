using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;

namespace duma.Models {
    public class EmailMessage {

        internal async Task SendEmailAsync(string email, string subject, string message, string path) {

            //string path = "~/Content/Upload/" + Guid.NewGuid() + "/";

            //var path_file = Path.GetFileName(path);

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("email", "Администрация сайта");

            // кому отправляем
            MailAddress to = new MailAddress(email);

            // создаем объект сообщения
            using (MailMessage m = new MailMessage(from, to)) {

                //К письму мы можем прикрепить вложения с помощью свойства Attachments. Каждое вложение представляет объект System.Net.Mail.Attachment:
                if (path != null) m.Attachments.Add(new Attachment(path));

                m.Subject = subject;

                m.Body = message;

                using (var client = new SmtpClient("smtp.yandex.ru", 587)) {

                    client.EnableSsl = true;

                    client.Credentials = new NetworkCredential("email", "password");

                    await client.SendMailAsync(m);

                    Console.WriteLine("Письмо отправлено");

                }
                email = null;
                subject = null;
                message = null;
                path = null;
                from = null;
                to = null;
            }             
                           
        }

    }
}