using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Enigma2 Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_enigma2">Apprise Wiki</see>.</para>
	/// </summary>
	public class Enigma2 : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Enigma2 Notification Service
		/// </summary>
		/// <param name="appriseUrl"></param>
		/// <param name="hostname">The Enigma2 devices IP/hostname</param>
		/// <param name="port">The port our Web server is listening on. By default the port is 80 for enigma2s:// and 443 for all enigma2:// references.</param>
		/// <param name="user">If your system is set up to use HTTP-AUTH, you can provide username for authentication to it.</param>
		/// <param name="password">If your system is set up to use HTTP-AUTH, you can provide password for authentication to it.</param>
		/// <param name="tls">Enable TLS prior to sending the user and password.</param>
		public Enigma2(string appriseUrl, string hostname, int? port = null, string user = null, string password = null, bool tls = false)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, port, user, password, tls);
		}

		private static string ServiceUrlBuilder(string hostname, int? port, string user, string password, bool tls)
		{
			var url = new StringBuilder();

			if (tls)
				url.Append("enigma2://");
			else
				url.Append("enigma2s://");

			if (!string.IsNullOrWhiteSpace(user))
			{
				if (!string.IsNullOrWhiteSpace(password))
					url.Append($"{user}:{password}");
				else
					url.Append($"{user}@");
			}

			url.Append(hostname);

			if (port.HasValue)
				url.Append($":{port}");

			return url.ToString();
		}
	}
}
