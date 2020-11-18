using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Twist Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_twist">Apprise Wiki</see>.</para>
	/// </summary>
	public class Twist : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Twist Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="email">This is the email address you associated with your Twist account when you signed up.</param>
		/// <param name="password">This is the password you set when you signed up with Twist</param>
		/// <param name="channels">This is the channel you wish to notify. If you don't specify one then the #General channel will be used by default from within your default team.</param>
		public Twist(string appriseUrl, string email, string password, IEnumerable<string> channels)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(email, password, channels);
		}

		/// <summary>
		/// Initializes a new instance of Twist Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="email">This is the email address you associated with your Twist account when you signed up.</param>
		/// <param name="password">This is the password you set when you signed up with Twist</param>
		/// <param name="teamChannelPairs"></param>
		public Twist(string appriseUrl, string email, string password, IDictionary<string, string> teamChannelPairs)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(email, password, teamChannelPairs);
		}

		private static string ServiceUrlBuilder(string email, string password, IEnumerable<string> channels)
		{
			if (!channels.IsAny())
				throw new ArgumentException("At least one channel must be specified.");

			var url = new StringBuilder();

			url.Append($"twist://{email}/{password}");

			foreach (var channel in channels)
				url.Append($"/#{channel}");

			return url.ToString();
		}

		private static string ServiceUrlBuilder(string email, string password, IDictionary<string, string> teamChannelPairs)
		{
			if (!teamChannelPairs.IsAny())
				throw new ArgumentException("At least one channel must be specified.");

			var url = new StringBuilder();

			url.Append($"twist://{email}/{password}");

			foreach (var teaamChannelPair in teamChannelPairs)
				url.Append($"/#{teaamChannelPair.Key}:{teaamChannelPair.Value}");

			return url.ToString();
		}
	}
}
