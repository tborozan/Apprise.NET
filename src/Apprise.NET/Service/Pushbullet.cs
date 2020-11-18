using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Pushbullet Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_pushbullet">Apprise Wiki</see>.</para>
	/// </summary>
	public class Pushbullet : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Pushbullet Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="accessToken">The Access Token can be generated on the Settings page of your Pushbullet's account. You must have an access token for this Notification service to work.</param>
		/// <param name="deviceIds">Associated devices with your Pushbullet account can be found in your Settings</param>
		/// <param name="channels">Channels must be prefixed with a hash (#) or they will be interpreted as a device_id. Channels must be registered with your Pushbullet account to work.</param>
		/// <param name="emails">Emails only work if you've registered them with your Pushbullet account.</param>
		public Pushbullet(string appriseUrl, string accessToken, IEnumerable<string> deviceIds = null, IEnumerable<string> channels = null, IEnumerable<string> emails = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(accessToken, deviceIds, channels, emails);
		}

		private static string ServiceUrlBuilder(string accessToken, IEnumerable<string> deviceIds, IEnumerable<string> channels, IEnumerable<string> emails)
		{
			var url = new StringBuilder();

			url.Append($"pbul://{accessToken}");

			if (deviceIds.IsAny())
			{
				foreach (var deviceId in deviceIds)
					url.Append($"/{deviceId}");
			}

			if (channels.IsAny())
			{
				foreach (var channel in channels)
					url.Append($"/#{channel}");
			}

			if (emails.IsAny())
			{
				foreach (var email in emails)
					url.Append($"/{email}");
			}

			return url.ToString();
		}
	}
}
