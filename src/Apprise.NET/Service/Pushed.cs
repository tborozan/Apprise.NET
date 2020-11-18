using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Pushed Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_pushed">Apprise Wiki</see>.</para>
	/// </summary>
	public class Pushed : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Pushed Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="appKey">The Application Key can be generated on the Settings page of your Pushed's account. You must have an application key for this Notification service to work.</param>
		/// <param name="appSecret">The Application Secret can be generated on the Settings page of your Pushed's account. You must have an application secret for this Notification service to work.</param>
		/// <param name="userPushedIds">Users must be prefixed with an at (@) character or they will be ignored. You can identify users here by their Pushed ID.</param>
		/// <param name="channelAliases">Channels must be prefixed with a hash tag (#) or they will be ignored. Channels must be registered with your Pushed account to work. This must be the channel alias itself; not the channel. The alias can be retrieved from the channel settings from within your pushed.io account.</param>
		public Pushed(string appriseUrl, string appKey, string appSecret, IEnumerable<string> userPushedIds = null, IEnumerable<string> channelAliases = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(appKey, appSecret, userPushedIds, channelAliases);
		}

		private static string ServiceUrlBuilder(string appKey, string appSecret, IEnumerable<string> userPushedIds, IEnumerable<string> channelAliases)
		{
			var url = new StringBuilder();

			url.Append($"pushed://{appKey}/{appSecret}");

			if (userPushedIds.IsAny())
			{
				foreach (var userIds in userPushedIds)
					url.Append($"/@{userIds}");
			}

			if (channelAliases.IsAny())
			{
				foreach (var channel in channelAliases)
					url.Append($"/#{channel}");
			}

			return url.ToString();
		}
	}
}
