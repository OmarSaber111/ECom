using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Dtos.Email;
using Ecom.Core.IService;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Ecom.Infrastructure.Repositories.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService( IConfiguration configuration)
        {
          _configuration = configuration;
        }
        public async Task SendEmail(EmailDto emailDTO)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress("Omar", _configuration["EmailSetting:From"]));
            message.Subject = emailDTO.Subject;
            message.To.Add(new MailboxAddress( emailDTO.To, emailDTO.To));
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDTO.Content
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(
                    host: _configuration["EmailSetting:Host"],
                    port: int.Parse(_configuration["EmailSetting:Port"]),
                    useSsl: true);

                    await smtp.AuthenticateAsync(
                        userName: _configuration["EmailSetting:Username"],
                        password: _configuration["EmailSetting:Password"]);

                    await smtp.SendAsync(message);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await smtp.DisconnectAsync(true);
                    smtp.Dispose();
                }
            }
        }

    }
}
