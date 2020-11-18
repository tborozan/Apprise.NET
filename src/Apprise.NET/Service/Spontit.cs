using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Spontit Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_spontit">Apprise Wiki</see>.</para>
	/// </summary>
	public class Spontit : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Spontit Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="apiKey">This is the API key you generated for your Spontit account.</param>
		/// <param name="user">This is the User ID associated with your Spontit account.</param>
		/// <param name="channelIds">A Channel you wish to notify that you created.</param>
		public Spontit(string appriseUrl, string apiKey, string user, IEnumerable<string> channelIds = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, user, channelIds);
		}

		private static string ServiceUrlBuilder(string apiKey, string user, IEnumerable<string> channelIds)
		{
			var url = new StringBuilder();

			url.Append($"spontit://{user}@{apiKey}");

			if (channelIds.IsAny())
			{
				foreach (var channelId in channelIds)
					url.Append($"/{channelId}");
			}

			return url.ToString();
		}
	}
}
