using MailKit.Net.Smtp;
using MimeKit;

namespace Critoo.Services
{
    public class MailSenderService : IDisposable
    {

        private string _mailFrom;
        private string _smtp;
        private int _port;
        private string _username;
        private string _password;

        private MailKit.Net.Smtp.SmtpClient _client;
        public MailSenderService(string mailFrom, string smtp, int port, string username, string password)
        {
            _mailFrom = mailFrom;
            _smtp = smtp;
            _port = port;
            _username = username;
            _password = password;

            _client = new MailKit.Net.Smtp.SmtpClient();
        }

        public async Task SendEmailAsync (string to, string subject, string body)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_mailFrom));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                using var smtpClient = new SmtpClient();
                await _client.ConnectAsync(_smtp, _port, MailKit.Security.SecureSocketOptions.StartTls);
                await _client.AuthenticateAsync(_username, _password);

                var result = await _client.SendAsync(email);
            }
            catch
            {   }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
            {
                _client.DisconnectAsync(true).ConfigureAwait(false);
            }

        }
    }
}
