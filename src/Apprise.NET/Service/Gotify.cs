using Apprise.Enums;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Telegram Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_gotify">Apprise Wiki</see>.</para>
	/// </summary>
	public class Gotify : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Gotify Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="hostname">The Rocket.Chat server you're sending your notification to.</param>
		/// <param name="token">The Application Token you generated on your Gotify Server</param>
		/// <param name="port">The port the Gotify server is listening on.</param>
		/// <param name="path">For those that host their Gotify server on a hostname that requires you to specify an additional path prefix may just include this as part of their URL string (the default is '/').</param>
		/// <param name="priority">The priority level to pass the message along as. Possible values are low, moderate, normal, and high.</param>
		/// <param name="tls">Use secure connection.</param>
		public Gotify(string appriseUrl, string hostname, string token, int? port = null, string path = null, GotifyPriority priority = GotifyPriority.Normal, bool tls = false)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, token, port, path, priority, tls);
		}

		private static string ServiceUrlBuilder(string hostname, string token, int? port, string path, GotifyPriority priority, bool tls)
		{
			var url = new StringBuilder();

			if (tls)
				url.Append("gotifys://");
			else
				url.Append("gotify://");

			url.Append(hostname);

			if (port.HasValue)
				url.Append($":{port}");

			if (!string.IsNullOrWhiteSpace(path))
				url.Append($"/{path}");

			url.Append($"/{token}/?priority={priority.ToString().ToLower()}");

			return url.ToString();
		}
	}
}
