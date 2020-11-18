using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>KODI Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_kodi">Apprise Wiki</see>.</para>
	/// </summary>
	public class Kodi : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of KODI Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="hostname">The server Kodi is listening on.</param>
		/// <param name="port">The port Kodi is listening on.</param>
		/// <param name="userId">The account login to your KODI server.</param>
		/// <param name="password">The password associated with your KODI Server.</param>
		/// <param name="useHttps">Indicates whether to use HTTP or HTTPS.</param>
		public Kodi(string appriseUrl, string hostname, int? port = null, string userId = null, string password = null, bool useHttps = false)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, port, userId, password, useHttps);
		}

		private static string ServiceUrlBuilder(string hostname, int? port, string userId, string password, bool useHttps)
		{
			var url = new StringBuilder();

			if (useHttps)
				url.Append("kodis://");
			else
				url.Append("kodi://");

			if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(password))
				url.Append($"{userId}:{password}@");

			url.Append(hostname);

			if (port.HasValue)
				url.Append($":{port}");

			return url.ToString();
		}
	}
}
