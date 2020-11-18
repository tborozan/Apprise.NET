using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Emby Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_emby">Apprise Wiki</see>.</para>
	/// </summary>
	public class Emby : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Emby Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="hostname">The server Emby is listening on.</param>
		/// <param name="userId">The account login to your Emby server.</param>
		/// <param name="password">The password associated with your Emby Server.</param>
		/// <param name="port">The port Emby is listening on. By default the port is 8096 for both http and https.</param>
		/// <param name="modal">Defines if the notification should appear as a modal type box. By default this is set to No.</param>
		/// <param name="https">Indicates whether to use http or https.</param>
		public Emby(string appriseUrl, string hostname, string userId = null, string password = null, string port = null, bool modal = false, bool https = true)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, userId, password, port, modal, https);
		}

		private static string ServiceUrlBuilder(string hostname, string userId, string password, string port, bool modal, bool https)
		{
			var url = new StringBuilder();

			if (https)
				url.Append("embys://");
			else
				url.Append("emby://");

			if (!string.IsNullOrWhiteSpace(userId))
				url.Append($"{userId}:{password}@");

			url.Append(hostname);

			if (!string.IsNullOrWhiteSpace(port))
				url.Append($":{port}");

			if (modal)
				url.Append("?modal=yes");

			return url.ToString();
		}
	}
}
