using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Notica Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_notica">Apprise Wiki</see>.</para>
	/// </summary>
	public class Notica : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Notica Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="token">The Token that was generated for you after visiting their website. Alternatively this should be the token used by your self hosted solution.</param>
		/// <param name="hostname">The Web Server's hostname.</param>
		/// <param name="port">The port our Web server is listening on. By default the port is 80 or 443 if <paramref name="useHttps"/> is set to true.</param>
		/// <param name="user">If you're system is set up to use HTTP-AUTH, you can provide username for authentication to it.</param>
		/// <param name="password">If you're system is set up to use HTTP-AUTH, you can provide password for authentication to it.</param>
		/// <param name="useHttps">Indicates whether to use http or https.</param>
		public Notica(string appriseUrl, string token, string hostname = null, int? port = null, string user = null, string password = null, bool useHttps = false)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(token, hostname, port, user, password, useHttps);
		}

		private static string ServiceUrlBuilder(string token, string hostname, int? port, string user, string password, bool useHttps)
		{
			if (token.StartsWith("https://notica.us/?"))
				return token;

			if (string.IsNullOrWhiteSpace(hostname))
				return $"notica://{token}";

			var url = new StringBuilder();

			if (useHttps)
				url.Append("noticas://");
			else
				url.Append("notica://");

			if (!string.IsNullOrWhiteSpace(user))
			{
				url.Append(user);

				if (!string.IsNullOrWhiteSpace(password))
					url.Append($":{password}");

				url.Append('@');
			}

			url.Append(hostname);

			if (port.HasValue)
				url.Append($":{port}");

			url.Append($"/{token}");

			return url.ToString();
		}
	}
}
