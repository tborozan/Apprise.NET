using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Nextcloud Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_nextcloud">Apprise Wiki</see>.</para>
	/// </summary>
	public class Nextcloud : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Nextcloud Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="hostname">The hostname of the server hosting your Nextcloud service.</param>
		/// <param name="notifyUsers">One or more users you wish to send your notification to.</param>
		/// <param name="adminUser">The administration user of the next cloud service you have set up.</param>
		/// <param name="password">The administrator password associated with the <paramref name="adminUser"/> for your Nextcloud account.</param>
		/// <param name="port">The server port Nextcloud is listening on.</param>
		/// <param name="useHttps">Indicates whether to use http or https.</param>
		/// <exception cref="ArgumentException">At least one user to notify must be specified.</exception>
		public Nextcloud(string appriseUrl, string hostname, IEnumerable<string> notifyUsers, string adminUser = null, string password = null, int? port = null, bool useHttps = false)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlbuilder(hostname, notifyUsers, adminUser, password, port, useHttps);
		}

		private static string ServiceUrlbuilder(string hostname, IEnumerable<string> notifyUsers, string adminUser, string password, int? port, bool useHttps)
		{
			if (!notifyUsers.IsAny())
				throw new ArgumentException("At least one user to notify must be specified.");

			var url = new StringBuilder();

			if (useHttps)
				url.Append("nclouds://");
			else
				url.Append("ncloud://");

			if (!string.IsNullOrWhiteSpace(adminUser) && !string.IsNullOrWhiteSpace(password))
				url.Append($"{adminUser}:{password}@");

			url.Append(hostname);

			if (port.HasValue)
				url.Append($":{port}");

			foreach (var user in notifyUsers ?? Enumerable.Empty<string>())
				url.Append($"/{user}");

			return url.ToString();
		}
	}
}
