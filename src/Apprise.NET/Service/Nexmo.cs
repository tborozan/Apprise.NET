using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Nexmo Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_nexmo">Apprise Wiki</see>.</para>
	/// </summary>
	public class Nexmo : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Nexmo Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="apiKey">The API Key associated with your Nexmo account. This is available to you via the Nexmo Dashboard.</param>
		/// <param name="apiSecret">The API Secret associated with your Nexmo account. This is available to you via the Nexmo Dashboard.</param>
		/// <param name="fromPhoneNumber">This must be a From Phone Number that has been provided to you from the Nexmo website.</param>
		/// <param name="toPhoneNumbers">A phone number MUST include the country codes dialling prefix as well when placed. This field is also very friendly and supports brackets, spaces and hyphens in the event you want to format the number in an easy to read fashion.</param>
		public Nexmo(string appriseUrl, string apiKey, string apiSecret, string fromPhoneNumber, IEnumerable<string> toPhoneNumbers = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, apiSecret, fromPhoneNumber, toPhoneNumbers);
		}

		private static string ServiceUrlBuilder(string apiKey, string apiSecret, string fromPhoneNumber, IEnumerable<string> toPhoneNumbers)
		{
			var url = new StringBuilder();

			url.Append($"nexmo://{apiKey}:{apiSecret}@{fromPhoneNumber}/");

			foreach (var toPhoneNumber in toPhoneNumbers ?? Enumerable.Empty<string>())
				url.Append($"{toPhoneNumber}/");

			return url.ToString();
		}
	}
}
