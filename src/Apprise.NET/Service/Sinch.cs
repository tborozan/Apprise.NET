using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Sinch Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_sinch">Apprise Wiki</see>.</para>
	/// </summary>
	public class Sinch : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Sinch Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="servicePlanId">The Account SID associated with your Sinch account. This is available to you via the Sinch Dashboard.</param>
		/// <param name="apiToken">The Auth Token associated with your Sinch account. This is available to you via the Sinch Dashboard.</param>
		/// <param name="fromPhoneNo">The Active Phone Number associated with your Sinch account you wish the SMS message to come from. It must be a number registered with Sinch. As an alternative to the FromPhoneNo, you may also provide a ShortCode here instead. The phone number MUST include the country codes dialling prefix as well when placed. This field is also very friendly and supports brackets, spaces and hyphens in the event you want to format the number in an easy to read fashion.</param>
		/// <param name="phoneNumbers">The Active Phone Number associated with your Sinch account you wish the SMS message to come from. It must be a number registered with Sinch. As an alternative to the FromPhoneNo, you may also provide a ShortCode here instead. The phone number MUST include the country codes dialling prefix as well when placed. This field is also very friendly and supports brackets, spaces and hyphens in the event you want to format the number in an easy to read fashion.</param>
		/// <param name="region">Can be either us or eu. By default the region is set to us.</param>
		public Sinch(string appriseUrl, string servicePlanId, string apiToken, string fromPhoneNo = null, IEnumerable<string> phoneNumbers = null, string region = "us")
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(servicePlanId, apiToken, fromPhoneNo, phoneNumbers, region);
		}

		private static string ServiceUrlBuilder(string servicePlanId, string apiToken, string fromPhoneNo, IEnumerable<string> phoneNumbers, string region)
		{
			var url = new StringBuilder();

			url.Append($"sinch://{servicePlanId}:{apiToken}@{fromPhoneNo}");

			if (phoneNumbers.IsAny())
			{
				foreach (var number in phoneNumbers)
					url.Append($"/{number}");
			}

			if (region is "eu")
				url.AppendParam(nameof(region), "eu");

			return url.ToString();
		}
	}
}
