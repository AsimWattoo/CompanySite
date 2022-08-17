using MailKit.Net.Smtp;

using MimeKit;
using MimeKit.Text;

namespace Company_Site.Helpers
{
	public class MailManager
	{
		#region Private Members

		private string _email;

		private string _keycode;

		#endregion

		#region Constructor

		/// <summary>
		/// Default Constructor
		/// </summary>
		public MailManager(string email, string keycode)
		{
			_email = email;
			_keycode = keycode;
		}

        #endregion

        #region Public Methods

        /// <summary>
        /// Sends email to the specified email
        /// </summary>
        /// <param name="email">The email to which mail is to be sent</param>
        /// <param name="code"> The code required for the email account </param>
        /// <param name="subject"> The subject of the Email </param>
        /// <param name="body"> The body of the email. </param>
        public async Task SendMail(string email, string subject, string body)
        {
            await Task.Run(() =>
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_email));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = body
                };

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    smtp.Authenticate(_email, _keycode);
                    smtp.Send(message);
                    smtp.Disconnect(true);
                }
            });
        }

        /// <summary>
		/// Sends email to the multiple mail addresses
		/// </summary>
		/// <param name="email">The email to which mail is to be sent</param>
		/// <param name="code"> The code required for the email account </param>
		/// <param name="subject"> The subject of the Email </param>
		/// <param name="body"> The body of the email. </param>
		public async Task SendMail(List<string> emails, string subject, string body)
        {
            await Task.Run(() =>
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(_email));
                emails.ForEach(e => message.To.Add(MailboxAddress.Parse(e)));
                message.Subject = subject;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = body
                };

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    smtp.Authenticate(_email, _keycode);
                    smtp.Send(message);
                    smtp.Disconnect(true);
                }
            });
        }

        #endregion

    }
}
