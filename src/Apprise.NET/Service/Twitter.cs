using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Twitter Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_twitter">Apprise Wiki</see>.</para>
	/// </summary>
	public class Twitter : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Twitter Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="consumerKey"></param>
		/// <param name="consumerSecret"></param>
		/// <param name="accessToken">The Access Token; you would have had to generate this one from your Twitter App Configuration.</param>
		/// <param name="accessSecret">The Access Secret; you would have had to generate this one from your Twitter App Configuration.</param>
		/// <param name="screenNames">The UserID of your account such as l2gnux (if your id is @l2gnux)..</param>
		/// <param name="mode">This the the Twitter mode you want to operate in. Possible values are dm (for Private Direct Messages) and tweet to make a public post. By default this is set to dm</param>
		public Twitter(string appriseUrl, string consumerKey, string consumerSecret, string accessToken, string accessSecret, IEnumerable<string> screenNames, string mode = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(consumerKey, consumerSecret, accessToken, accessSecret, screenNames, mode);
		}

		private static string ServiceUrlBuilder(string consumerKey, string consumerSecret, string accessToken, string accessSecret, IEnumerable<string> screenNames, string mode)
		{
			if (!screenNames.IsAny())
				throw new ArgumentException("At least one screen name must be specified.");

			var url = new StringBuilder();

			url.Append($"twitter://{consumerKey}/{consumerSecret}/{accessToken}/{accessSecret}");

			foreach (var screenName in screenNames)
				url.Append($"/{screenName}");

			if (mode is "tweet")
				url.AppendParam(nameof(mode), "tweet");

			return url.ToString();
		}
	}
}
