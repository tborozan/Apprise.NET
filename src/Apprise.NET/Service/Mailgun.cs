using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Mailgun Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_mailgun">Apprise Wiki</see>.</para>
	/// </summary>
	public class Mailgun : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Mailgun Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="apiKey">The API Key associated with the domain you want to send your email from. This is available to you after signing into their website an accessing the dashboard.</param>
		/// <param name="domain">The Domain you wish to send your email from; this domain must be registered and set up with your mailgun account.</param>
		/// <param name="user">The user gets paired with the domain you specify on the URL to make up the From email address your recipients receive their email from.</param>
		/// <param name="emails">You can specify as many email addresses as you wish. Each address you identify here will represent the To.</param>
		/// <param name="region">Identifies which server region you intend to access. Supported options here are eu and us. By default this is set to us unless otherwise specified.</param>
		/// <param name="from"></param>
		public Mailgun(string appriseUrl, string apiKey, string domain, string user, IEnumerable<string> emails, string region = "us", string from = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, domain, user, emails, region, from);
		}

		private static string ServiceUrlBuilder(string apiKey, string domain, string user, IEnumerable<string> emails, string region, string from)
		{
			if (!emails.IsAny() && !emails.First().IsValidEmail())
				throw new ArgumentException("At least one valid E-Mail address must be specified.");

			var url = new StringBuilder();

			url.Append($"mailgun://{user}@{domain}/{apiKey}/");

			foreach (var email in emails ?? Enumerable.Empty<string>())
			{
				if (email.IsValidEmail())
					url.Append($"{email}/");
			}

			if (region is "eu")
				url.AppendParam(nameof(region), "eu");

			if (!string.IsNullOrWhiteSpace(from))
				url.AppendParam(nameof(from), from);

			return url.ToString();
		}
	}
}
