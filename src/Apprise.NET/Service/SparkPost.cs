using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>SparkPost Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_sparkpost">Apprise Wiki</see>.</para>
	/// </summary>
	public class SparkPost : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of SparkPost Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="apiKey">The API Key associated with the domain you want to send your email from. This is available to you after signing into their website an accessing the dashboard.</param>
		/// <param name="user">The user gets paired with the domain you specify on the URL to make up the From email address your recipients receive their email from.</param>
		/// <param name="domain">The Domain you wish to send your email from; this domain must be registered and set up with your sparkpost account.</param>
		/// <param name="emails">You can specify as many email addresses as you wish. Each address you identify here will represent the To.</param>
		/// <param name="region">Identifies which server region you intend to access. Supported options here are eu and us. By default this is set to us unless otherwise specified. This specifically affects which API server you will access to send your emails from.</param>
		public SparkPost(string appriseUrl, string apiKey, string user, string domain, IEnumerable<string> emails = null, string region = "us")
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, user, domain, emails, region);
		}

		private static string ServiceUrlBuilder(string apiKey, string user, string domain, IEnumerable<string> emails, string region)
		{
			var url = new StringBuilder();

			url.Append($"sparkpost://{user}@{domain}/{apiKey}/");

			if (emails is not null)
			{
				foreach (var email in emails)
					url.Append($"/{email}");
			}

			if (region is "eu")
				url.AppendParam(nameof(region), region);

			return url.ToString();
		}
	}
}
