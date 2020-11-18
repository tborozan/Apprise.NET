using Apprise.Enums;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Pushover Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_pushover">Apprise Wiki</see>.</para>
	/// </summary>
	public class Pushover : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Pushover Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="userKey">The user key identifier associated with your Pushover account. This is NOT your email address. The key can be acquired from your Pushover dashboard.</param>
		/// <param name="token">The token associated with your Pushover account.</param>
		/// <param name="deviceIds">The device identifier to send your notification to. By default if one isn't specified then all of devices associated with your account are notified.</param>
		/// <param name="priority">Can be low, moderate, normal, high, or emergency; the default is normal if a priority isn't specified. To send an emergency-priority notification, the retry and expire parameters should be supplied.</param>
		/// <param name="expire">The expire parameter specifies how many seconds your notification will continue to be retried for (every retry seconds). If the notification has not been acknowledged in expire seconds, it will be marked as expired and will stop being sent to the user. Note that the notification is still shown to the user after it is expired, but it will not prompt the user for acknowledgement. This parameter has a maximum value of at most 10800 seconds (3 hours). The default is 3600 seconds (1 hr) if nothing is otherwise specified.</param>
		/// <param name="retry">The retry parameter specifies how often (in seconds) the Pushover servers will send the same notification to the user. In a situation where your user might be in a noisy environment or sleeping, retrying the notification (with sound and vibration) will help get his or her attention. This parameter must have a value of at least 30 seconds between retries. The default is 900 seconds (15 minutes) if nothing is otherwise specified.</param>
		/// <param name="sound">Can optionally identify one of the optional sound effects identified here. The default sound is pushover.</param>
		public Pushover(string appriseUrl, string userKey, string token, IEnumerable<string> deviceIds = null, PushoverPriority priority = PushoverPriority.Normal, int? expire = null, int? retry = null, string sound = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(userKey, token, deviceIds, priority, expire, retry, sound);
		}

		private static string ServiceUrlBuilder(string userKey, string token, IEnumerable<string> deviceIds, PushoverPriority priority, int? expire, int? retry, string sound)
		{
			var url = new StringBuilder();

			url.Append($"pover://{userKey}@{token}");

			if (deviceIds.IsAny())
			{
				foreach (var deviceId in deviceIds)
					url.Append($"/{deviceId}");
			}

			if (priority is not PushoverPriority.Normal)
				url.AppendParam(nameof(priority), priority.ToString());

			if (expire.HasValue)
				url.AppendParam(nameof(expire), expire.ToString());

			if (retry.HasValue)
				url.AppendParam(nameof(retry), retry.ToString());

			if (sound is not null)
				url.AppendParam(nameof(sound), sound);

			return url.ToString();
		}
	}
}
