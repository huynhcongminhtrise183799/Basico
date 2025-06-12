using AccountService.Application.IService;
using AccountService.Application.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Write.Email
{
	public class EmailService : IEmailService
	{
		private readonly EmailSettings _settings;

		public EmailService(IOptions<EmailSettings> options)
		{
			_settings = options.Value;
		}

		public async Task SendAsync(string toEmail, string subject, string body)
		{
			using var client = new SmtpClient(_settings.SmtpServer, _settings.Port)
			{
				Credentials = new NetworkCredential(_settings.FromEmail, _settings.Password),
				EnableSsl = true
			};

			var mail = new MailMessage(_settings.FromEmail, toEmail, subject, body);
			await client.SendMailAsync(mail);
		}
	}
}
