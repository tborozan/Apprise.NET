using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>XMPP Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_xmpp">Apprise Wiki</see>.</para>
	/// </summary>
	public class XMPP : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of XMPP Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="hostname">The server XMPP is listening on.</param>
		/// <param name="password">The password associated with the XMPP Server.</param>
		/// <param name="userId">The account login used to build the JID with if one isn't specified.</param>
		/// <param name="port">The port the XMPP server is listening on.</param>
		/// <param name="jids">The JID account to associate/authenticate with the XMPP Server.</param>
		/// <param name="useHttps"></param>
		public XMPP(string appriseUrl, string hostname, string password, string userId = null, int? port = null, IEnumerable<string> jids = null, bool useHttps = false)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, password, userId, port, jids, useHttps);
		}

		private static string ServiceUrlBuilder(string hostname, string password, string userId, int? port, IEnumerable<string> jids, bool useHttps)
		{
			var url = new StringBuilder();

			if (useHttps)
				url.Append("xmpps://");
			else
				url.Append("xmpp://");

			if (!string.IsNullOrWhiteSpace(userId))
				url.Append($"{userId}:");

			url.Append($"{password}@{hostname}");

			if (port.HasValue)
				url.Append($":{port}");

			if (jids.IsAny())
			{
				foreach (var jid in jids)
					url.Append($"/{jid}");
			}

			return url.ToString();
		}
	}
}
