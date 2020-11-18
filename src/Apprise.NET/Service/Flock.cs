using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Flock Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_flock">Apprise Wiki</see>.</para>
	/// </summary>
	public class Flock : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Flock Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="token">The first part of 3 tokens provided to you after creating a incoming-webhook and or an application/bot</param>
		/// <param name="channels">Channels must represent the encoded id of the channel name (not the human readable reference).</param>
		/// <param name="users">Users must represent the encoded id of the user name (not the human readable reference).</param>
		/// <param name="botname">Identify the name of the bot that should issue the message. If one isn't specified then the default is to just use your account (associated with the incoming-webhook).</param>
		/// <exception cref="ArgumentException">At least one channel or user must be specified.</exception>
		public Flock(string appriseUrl, string token, IEnumerable<string> channels = null, IEnumerable<string> users = null, string botname = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(token, channels, users, botname);
		}

		private static string ServiceUrlBuilder(string token, IEnumerable<string> channels, IEnumerable<string> users, string botname)
		{
			if (!channels.IsAny() && !users.IsAny())
				throw new ArgumentException("At least one channel or user must be specified.");

			var url = new StringBuilder();

			url.Append("flock://");

			if (!string.IsNullOrWhiteSpace(botname))
				url.Append($"botname@");

			url.Append(token);

			foreach (var channel in channels ?? Enumerable.Empty<string>())
				url.Append($"/g:{channel}");

			foreach (var user in users ?? Enumerable.Empty<string>())
				url.Append($"/u:{user}");

			return url.ToString();
		}
	}
}
