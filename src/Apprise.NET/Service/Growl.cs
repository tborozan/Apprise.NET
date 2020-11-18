using Apprise.Enums;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Telegram Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_growl">Apprise Wiki</see>.</para>
	/// </summary>
	public class Growl : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Growl Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="hostname">The server Growl server is listening on.</param>
		/// <param name="port">The port Growl Server is listening on.</param>
		/// <param name="password">The password associated with the Growl server if you set one up.</param>
		/// <param name="useVersion1">If you would require the 1.4 version of the protocol.</param>
		/// <param name="priority">Can be low, moderate, normal, high, or emergency; the default is normal.</param>
		public Growl(string appriseUrl, string hostname, int? port = null, string password = null, bool useVersion1 = false, GrowlPriority priority = GrowlPriority.Normal)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, port, password, useVersion1, priority);
		}

		private static string ServiceUrlBuilder(string hostname, int? port, string password, bool useVersion1, GrowlPriority priority)
		{
			var url = new StringBuilder();

			url.Append("growl://");

			if (!string.IsNullOrWhiteSpace(password))
				url.Append($"{password}@");

			url.Append(hostname);

			if (port.HasValue)
				url.Append($":{port}");

			if (useVersion1)
				url.Append("?version=1");

			url.Append($"/?priority={priority.ToString().ToLower()}");

			return url.ToString();
		}
	}
}
