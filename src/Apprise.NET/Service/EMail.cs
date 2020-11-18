using Apprise.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>E-Mail Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_email">Apprise Wiki</see>.</para>
	/// </summary>
	public class EMail : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of E-Mail Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="userId">The account login to your SMTP server.</param>
		/// <param name="password">The password required to send an email via your SMTP Server.</param>
		/// <param name="mailProvider">E-Mail service provider to use</param>
		public EMail(string appriseUrl, string userId, string password, EMailProvider mailProvider)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(userId, password, mailProvider);
		}

		/// <summary>
		/// Initializes a new instance of E-Mail Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="userId">The account login to your SMTP server.</param>
		/// <param name="password">The password required to send an email via your SMTP Server.</param>
		/// <param name="domain">If your email address was test@example.com then example.com is your domain. You must provide this as part of the URL string!</param>
		/// <param name="port">The port your SMTP server is listening on. Leave empty to use defaults.</param>
		/// <param name="tls">Enable TLS prior to sending the user and password.</param>
		/// <param name="smtp">If the SMTP server differs from your specified domain, then you'll want to specify it as an argument in your URL.</param>
		/// <param name="from">If you want the email address ReplyTo address to be something other then your own email address, then you can specify it here.</param>
		/// <param name="to">This will enforce (or set the address the email is sent To). This is only required in special circumstances. The notification script is usually clever enough to figure this out for you.</param>
		/// <param name="name">With respect to <paramref name="from"/>, this allows you to provide a name with your ReplyTo address.</param>
		/// <param name="cc">Carbon Copy email address(es).</param>
		/// <param name="bcc">Blind Carbon Copy email address(es).</param>
		/// <param name="mode">Mode allows you to change the connection method. Some sites only support SSL (mode=ssl) while others only support STARTTLS (mode=starttls). The default value is starttls.</param>
		public EMail(string appriseUrl, string userId, string password, string domain, string port = null, bool tls = true, string smtp = null, string from = null, IEnumerable<string> to = null, string name = null, IEnumerable<string> cc = null, IEnumerable<string> bcc = null, string mode = "starttls")
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(userId, password, domain, port, tls, smtp, from, to, name, cc, bcc, mode);
		}

		private static string ServiceUrlBuilder(string userId, string password, EMailProvider mailProvider)
		{
			return mailProvider switch
			{
				EMailProvider.Yahoo => $"mailto://{userId}:{password}@yahoo.com",
				EMailProvider.Hotmail => $"mailto://{userId}:{password}@hotmail.com",
				EMailProvider.Live => $"mailto://{userId}:{password}@live.com",
				EMailProvider.Prontomail => $"mailto://{userId}:{password}@prontomail.com",
				EMailProvider.Gmail => $"mailto://{userId}:{password}@gmail.com",
				EMailProvider.Fastmail => $"mailto://{userId}:{password}@fastmail.com",
				EMailProvider.Zoho => $"mailto://{userId}:{password}@zoho.com",
				EMailProvider.Yandex => $"mailto://{userId}:{password}@yandex.com",
				_ => String.Empty,
			};
		}

		private static string ServiceUrlBuilder(string userId, string password, string domain, string port, bool tls, string smtp, string from, IEnumerable<string> to, string name, IEnumerable<string> cc, IEnumerable<string> bcc, string mode)
		{
			var url = new StringBuilder();

			if (tls)
				url.Append("mailtos://");
			else
				url.Append("mailto://");

			if (!userId.IsValidEmail())
				url.Append($"{userId}:");
			else
				throw new ArgumentException("Not a valid E-Mail.");

			url.Append($"{password}@{domain}");

			if (userId.IsValidEmail())
				url.AppendParam("user", userId);
			else
				throw new ArgumentException("Not a valid E-Mail.");

			url.AppendParam(nameof(port), port);
			url.AppendParam(nameof(smtp), smtp);
			url.AppendParam(nameof(from), from);
			url.AppendParam(nameof(to), to);
			url.AppendParam(nameof(name), name);
			url.AppendParam(nameof(cc), cc);
			url.AppendParam(nameof(bcc), bcc);
			url.AppendParam(nameof(mode), mode);

			return url.ToString();
		}
	}
}
