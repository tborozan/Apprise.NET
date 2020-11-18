using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>PopcornNotify Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_popcornnotify">Apprise Wiki</see>.</para>
	/// </summary>
	public class PopcornNotify : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of PopcornNotify Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="apiKey">The Personal API Token associated with your account.</param>
		/// <param name="phoneNumbers">Phone Numbers you wish to notify (via SMS).</param>
		/// <param name="emails">The Email addresses you wish to notify.</param>
		/// <exception cref="ArgumentException"/>
		public PopcornNotify(string appriseUrl, string apiKey, IEnumerable<string> phoneNumbers, IEnumerable<string> emails)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, phoneNumbers, emails);
		}

		private static string ServiceUrlBuilder(string apiKey, IEnumerable<string> phoneNumbers, IEnumerable<string> emails)
		{
			if (!phoneNumbers.Any() && !emails.Any())
				throw new ArgumentException("You must specify at least one phone number or email.");

			var url = new StringBuilder();

			url.Append($"popcorn://{apiKey}/");

			foreach (var phoneNumber in phoneNumbers ?? Enumerable.Empty<string>())
				url.Append($"{phoneNumber}/");

			foreach (var email in emails ?? Enumerable.Empty<string>())
				url.Append($"{email}/");

			return url.ToString();
		}
	}
}
