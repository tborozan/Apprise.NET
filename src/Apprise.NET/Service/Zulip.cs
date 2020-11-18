using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Zulip Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_zulip">Apprise Wiki</see>.</para>
	/// </summary>
	public class Zulip : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Zulip Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="botname">The botname associated with the API Key. The -bot portion of the bot name is not required, however this is gracefully handled if specified.</param>
		/// <param name="organization">The organization you created your webhook under. The trailing part of the organization reading .zulipchat.com is not required here, however this is gracefully handled if specified.</param>
		/// <param name="token">The API token provided to you after creating a bot</param>
		/// <param name="emails">An email belonging to one of the users that have been added to your organization the private message.</param>
		/// <param name="channels">A channel to notify.</param>
		public Zulip(string appriseUrl, string botname, string organization, string token, IEnumerable<string> emails = null, IEnumerable<string> channels = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(botname, organization, token, emails, channels);
		}

		private static string ServiceUrlBuilder(string botname, string organization, string token, IEnumerable<string> emails, IEnumerable<string> channels)
		{
			if (!emails.IsAny() && !channels.IsAny())
				throw new ArgumentException("At least one email or channel must be specified.");

			var url = new StringBuilder();

			url.Append($"zulip://{botname}@{organization}/{token}");

			if (emails.IsAny())
			{
				foreach (var email in emails)
					url.Append($"/{email}");
			}

			if (channels.IsAny())
			{
				foreach (var channel in channels)
					url.Append($"/{channel}");
			}

			return url.ToString();
		}
	}
}
