using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Twilio Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_twilio">Apprise Wiki</see>.</para>
	/// </summary>
	public class Twilio : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Twilio Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="accountSid">The Account SID associated with your Twilio account. This is available to you via the Twilio Dashboard.</param>
		/// <param name="authToken">The Auth Token associated with your Twilio account. This is available to you via the Twilio Dashboard.</param>
		/// <param name="fromPhoneNo">The Active Phone Number associated with your Twilio account you wish the SMS message to come from. It must be a number registered with Twilio. As an alternative to the FromPhoneNo, you may provide a ShortCode instead. The phone number MUST include the country codes dialling prefix as well when placed. This field is also very friendly and supports brackets, spaces and hyphens in the event you want to format the number in an easy to read fashion.</param>
		/// <param name="phoneNumbers">A phone number MUST include the country codes dialling prefix as well when placed. This field is also very friendly and supports brackets, spaces and hyphens in the event you want to format the number in an easy to read fashion.</param>
		public Twilio(string appriseUrl, string accountSid, string authToken, string fromPhoneNo = null, IEnumerable<string> phoneNumbers = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(accountSid, authToken, fromPhoneNo, phoneNumbers);
		}

		private static string ServiceUrlBuilder(string accountSid, string authToken, string fromPhoneNo, IEnumerable<string> phoneNumbers)
		{
			var url = new StringBuilder();

			url.Append($"twilio://{accountSid}:{authToken}@{fromPhoneNo}");

			if (phoneNumbers.IsAny())
			{
				foreach (var number in phoneNumbers)
					url.Append($"/{number}");
			}

			return url.ToString();
		}
	}
}
