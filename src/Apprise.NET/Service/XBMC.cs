using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>XBMC Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_xbmc">Apprise Wiki</see>.</para>
	/// </summary>
	public class XBMC : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of XBMC Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="hostname">The server XBMC is listening on.</param>
		/// <param name="userId">The account login to your XBMC server.</param>
		/// <param name="password">The password associated with your XBMC Server.</param>
		/// <param name="port">The port XBMC is listening on. By default the port is 8080.</param>
		public XBMC(string appriseUrl, string hostname, string userId = null, string password = null, int? port = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, userId, password, port);
		}

		private static string ServiceUrlBuilder(string hostname, string userId, string password, int? port)
		{
			var url = new StringBuilder();

			url.Append($"xbmc://");

			if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(password))
				url.Append($"{userId}:{password}@");

			url.Append(hostname);

			if (port.HasValue)
				url.Append($":{port}");

			return url.ToString();
		}
	}
}
