using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Mattermost Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_mattermost">Apprise Wiki</see>.</para>
	/// </summary>
	public class Mattermost : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Mattermost Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="hostname">The server Mattermost is listening on.</param>
		/// <param name="authToken">The Authentication Token (the WebHook ID) you would have gotten after setting up the Mattermost server.</param>
		/// <param name="port">The server port Mattermost is listening on. By default the port is 8065.</param>
		/// <param name="path">You can identify a sub-path if you wish.</param>
		/// <param name="botname">An optional botname you can associate with your post.</param>
		/// <param name="useHttps">Indicates whether to use http or https.</param>
		public Mattermost(string appriseUrl, string hostname, string authToken, int? port = null, string path = null, string botname = null, bool useHttps = false)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, authToken, port, path, botname, useHttps);
		}

		private static string ServiceUrlBuilder(string hostname, string authToken, int? port, string path, string botname, bool useHttps)
		{
			var url = new StringBuilder();

			if (useHttps)
				url.Append("mmosts://");
			else
				url.Append("mmost://");

			if (!string.IsNullOrWhiteSpace(botname))
				url.Append($"{botname}@");

			if (port.HasValue)
				url.Append($"{hostname}:{port}/");
			else
				url.Append($"{hostname}/");

			if (!string.IsNullOrWhiteSpace(path))
				url.Append($"{path}/");

			url.Append(authToken);

			return url.ToString();
		}
	}
}
