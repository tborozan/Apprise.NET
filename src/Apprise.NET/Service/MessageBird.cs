using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>MessageBird Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_messagebird">Apprise Wiki</see>.</para>
	/// </summary>
	public class MessageBird : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of MessageBird Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="apiKey">The API Key associated with your MessageBird account. This is available to you via the MessageBird Dashboard.</param>
		/// <param name="fromPhoneNumber">A from phone number MUST include the country codes dialling prefix as well when placed. This field is also very friendly and supports brackets, spaces and hyphens in the event you want to format the number in an easy to read fashion. This MUST be the the number you registered with your MessageBird account.</param>
		/// <param name="toPhoneNumbers">Phone numbers MUST include the country codes dialling prefix as well when placed. This field is also very friendly and supports brackets, spaces and hyphens in the event you want to format the number in an easy to read fashion. If no <paramref name="toPhoneNumbers"/> is specified, then the <paramref name="fromPhoneNumber"/> is notified instead.</param>
		public MessageBird(string appriseUrl, string apiKey, string fromPhoneNumber, IEnumerable<string> toPhoneNumbers = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, fromPhoneNumber, toPhoneNumbers);
		}

		private static string ServiceUrlBuilder(string apiKey, string fromPhoneNumber, IEnumerable<string> toPhoneNumbers)
		{
			var url = new StringBuilder();

			url.Append($"msgbird://{apiKey}/{fromPhoneNumber}");

			foreach (var toPhoneNumber in toPhoneNumbers ?? Enumerable.Empty<string>())
				url.Append($"/{toPhoneNumber}");

			return url.ToString();
		}
	}
}
