using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>RocketChat Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_rocketchat">Apprise Wiki</see>.</para>
	/// </summary>
	public class RocketChat : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Rocket Chat Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="hostname">The Rocket.Chat server you're sending your notification to.</param>
		/// <param name="username">The user identifier you've associated with your Rocket.Chat server. This is only required if you are not providing a webhook instead. This can be optionally combined with the webhook if you wish to over-ride the bot name.</param>
		/// <param name="password">The password identifier you've associated with your Rocket.Chat server. This is only required if you are not providing a webhook instead</param>
		/// <param name="port">The port the Rocket.Chat server is listening on. </param>
		/// <param name="roomIds">A room identifier. Available for both basic and webhook modes.</param>
		/// <param name="channels">Channels must be registered with your Rocket.Chat server to work.</param>
		/// <param name="useHttps"></param>
		public RocketChat(string appriseUrl, string hostname, string username, string password, int? port = null, IEnumerable<string> roomIds = null, IEnumerable<string> channels = null, bool useHttps = false)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, username, password, port, roomIds, channels, useHttps);
		}

		/// <summary>
		/// Initializes a new instance of Rocket Chat Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="hostname">The Rocket.Chat server you're sending your notification to.</param>
		/// <param name="webhook">The incoming webhook you created and associated with your Rocket.Chat server . This is only required if you are not providing a webhook instead</param>
		/// <param name="port">The port the Rocket.Chat server is listening on. </param>
		/// <param name="roomIds">A room identifier. Available for both basic and webhook modes.</param>
		/// <param name="channels">Channels must be registered with your Rocket.Chat server to work.</param>
		/// <param name="userIds">Another user you wish to notify.</param>
		/// <param name="useHttps"></param>
		public RocketChat(string appriseUrl, string hostname, string webhook, int? port = null, IEnumerable<string> roomIds = null, IEnumerable<string> channels = null, IEnumerable<string> userIds = null, bool useHttps = false)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, webhook, port, roomIds, channels, userIds, useHttps);
		}

		private static string ServiceUrlBuilder(string hostname, string username, string password, int? port, IEnumerable<string> roomIds, IEnumerable<string> channels, bool useHttps)
		{
			if (!roomIds.IsAny() && !channels.IsAny())
				throw new ArgumentException("At least one channel or roomId must be specified.");

			var url = new StringBuilder();

			if (useHttps)
				url.Append("rocket://");
			else
				url.Append("rocket://");

			url.Append($"{username}:{password}@{hostname}");

			if (port.HasValue)
				url.Append($":{port}");

			if (roomIds.IsAny())
			{
				foreach (var roomId in roomIds)
					url.Append($"{roomId}");
			}

			if (channels.IsAny())
			{
				foreach (var channel in channels)
					url.Append($"#{channel}");
			}

			return url.ToString();
		}

		private static string ServiceUrlBuilder(string hostname, string webhook, int? port, IEnumerable<string> roomIds, IEnumerable<string> channels, IEnumerable<string> userIds, bool useHttps)
		{
			if (!roomIds.IsAny() && !channels.IsAny() && !userIds.IsAny())
				throw new ArgumentException("At least one channel, roomId or userId must be specified.");

			var url = new StringBuilder();

			if (useHttps)
				url.Append("rocket://");
			else
				url.Append("rocket://");

			url.Append($"{webhook}@{hostname}");

			if (port.HasValue)
				url.Append($":{port}");

			if (roomIds.IsAny())
			{
				foreach (var roomId in roomIds)
					url.Append($"#{roomId}");
			}

			if (channels.IsAny())
			{
				foreach (var channel in channels)
					url.Append($"#{channel}");
			}

			if (userIds.IsAny())
			{
				foreach (var userId in userIds)
					url.Append($"@{userId}");
			}

			return url.ToString();
		}
	}
}
