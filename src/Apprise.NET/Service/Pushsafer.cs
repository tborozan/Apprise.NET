using Apprise.Enums;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Pushsafer Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_pushsafer">Apprise Wiki</see>.</para>
	/// </summary>
	public class Pushsafer : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Pushsafer Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="privateKey">The private key associated with your PushSafer account. This can be found on your dashboard after successfully logging in.</param>
		/// <param name="deviceIds">The device identifier to send your notification to. By default if one isn't specified then all of devices associated with your account are notified.</param>
		/// <param name="priority">Can be low, moderate, normal, high, or emergency; the default is to use whatever the default setting is for the device being notified.</param>
		/// <param name="sound">Can optionally identify one of the optional sound effects identified here. By default this variable isn't set at all.</param>
		/// <param name="vibration">Android and iOS devices can be set to vibrate upon the reception of a notification. By setting this, you're effectively setting the strength of the vibration. You can set this to 1, 2 or 3 where 3 is a maximum vibration setting and 1 causes a lighter vibration. By default this variable isn't set at all causing your device default settings to take effect.</param>
		public Pushsafer(string appriseUrl, string privateKey, IEnumerable<string> deviceIds = null, PushsaferPriority? priority = null, string sound = null, int? vibration = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(privateKey, deviceIds, priority, sound, vibration);
		}

		private static string ServiceUrlBuilder(string privateKey, IEnumerable<string> deviceIds, PushsaferPriority? priority, string sound, int? vibration)
		{
			var url = new StringBuilder();

			url.Append($"psafers://{privateKey}");

			if (deviceIds.IsAny())
			{
				foreach (var deviceId in deviceIds)
					url.Append($"/{deviceId}");
			}

			if (priority.HasValue)
				url.AppendParam(nameof(priority), priority.ToString());

			if (sound is not null)
				url.AppendParam(nameof(sound), sound);

			if (vibration.HasValue)
				url.AppendParam(nameof(vibration), vibration.ToString());

			return url.ToString();
		}
	}
}
