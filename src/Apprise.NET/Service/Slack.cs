using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Slack Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_slack">Apprise Wiki</see>.</para>
	/// </summary>
	public class Slack : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Slack Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="webhook">The Webhook URL provided by Slack.</param>
		public Slack(string appriseUrl, string webhook)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = webhook;
		}

		/// <summary>
		/// Initializes a new instance of Slack Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="oAuthToken">The OAuth Token provided to you through the Slack App when using a a Bot instead of a Webhook. Token A, B and C are not used when using Bots.</param>
		/// <param name="botname">Identify the name of the bot that should issue the message. If one isn't specified then the default is to just use your account (associated with the incoming-webhook).</param>
		/// <param name="channels"></param>
		/// <param name="encodedIds">Slack allows you to represent channels and private channels by an encoded_id. If you know what they are, you can use this instead of the channel to send your notifications to. </param>
		/// <param name="userIds"></param>
		public Slack(string appriseUrl, string oAuthToken, string botname = null, IEnumerable<string> channels = null, IEnumerable<string> encodedIds = null, IEnumerable<string> userIds = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(oAuthToken, botname, channels, encodedIds, userIds);
		}

		/// <summary>
		/// Initializes a new instance of Slack Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="tokenA">The first part of 3 tokens provided to you after creating a incoming-webhook. The OAuthToken is not required if using the Slack Webhook.</param>
		/// <param name="tokenB">The second part of 3 tokens provided to you after creating a incoming-webhook. The OAuthToken is not required if using the Slack Webhook.</param>
		/// <param name="tokenC">The last part of 3 tokens provided to you after creating a incoming-webhook. The OAuthToken is not required if using the Slack Webhook.</param>
		/// <param name="botname">Identify the name of the bot that should issue the message. If one isn't specified then the default is to just use your account (associated with the incoming-webhook).</param>
		/// <param name="channels"></param>
		/// <param name="encodedIds">Slack allows you to represent channels and private channels by an encoded_id. If you know what they are, you can use this instead of the channel to send your notifications to. </param>
		/// <param name="userIds"></param>
		public Slack(string appriseUrl, string tokenA, string tokenB, string tokenC, string botname = null, IEnumerable<string> channels = null, IEnumerable<string> encodedIds = null, IEnumerable<string> userIds = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(tokenA, tokenB, tokenC, botname, channels, encodedIds, userIds);
		}

		private static string ServiceUrlBuilder(string oAuthToken, string botname, IEnumerable<string> channels, IEnumerable<string> encodedIds, IEnumerable<string> userIds)
		{
			var url = new StringBuilder();

			url.Append($"slack://");

			if (!string.IsNullOrWhiteSpace(botname))
				url.Append($"{botname}@");

			url.Append(oAuthToken);

			if (channels.IsAny())
			{
				foreach (var channel in channels)
					url.Append($"/#{channel}");
			}

			if (encodedIds.IsAny())
			{
				foreach (var encodedId in encodedIds)
					url.Append($"/+{encodedId}");
			}

			if (userIds.IsAny())
			{
				foreach (var userId in userIds)
					url.Append($"/@{userId}");
			}

			return url.ToString();
		}

		private static string ServiceUrlBuilder(string tokenA, string tokenB, string tokenC, string botname, IEnumerable<string> channels, IEnumerable<string> encodedIds, IEnumerable<string> userIds)
		{
			var url = new StringBuilder();

			url.Append($"slack://");

			if (!string.IsNullOrWhiteSpace(botname))
				url.Append($"{botname}@");

			url.Append($"{tokenA}/{tokenB}/{tokenC}");

			if (channels.IsAny())
			{
				foreach (var channel in channels)
					url.Append($"/#{channel}");
			}

			if (encodedIds.IsAny())
			{
				foreach (var encodedId in encodedIds)
					url.Append($"/+{encodedId}");
			}

			if (userIds.IsAny())
			{
				foreach (var userId in userIds)
					url.Append($"/@{userId}");
			}

			return url.ToString();
		}
	}
}
