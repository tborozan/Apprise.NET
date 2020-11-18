using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Telegram Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_clicksend">Apprise Wiki</see>.</para>
	/// </summary>
	public class ClickSend : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of ClickSend Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="username">The username associated with your ClickSend account.</param>
		/// <param name="password">The password associated with your ClickSend account.</param>
		/// <param name="phoneNumbers">At least one phone number MUST identified to use this plugin. This field is also very friendly and supports brackets, spaces and hyphens in the event you want to format the number in an easy to read fashion.</param>
		/// <exception cref="ArgumentException">You must specify at least one phone number.</exception>
		public ClickSend(string appriseUrl, string username, string password, IEnumerable<string> phoneNumbers)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(username, password, phoneNumbers);
		}

		private static string ServiceUrlBuilder(string username, string password, IEnumerable<string> phoneNumbers)
		{
			if (!phoneNumbers.IsAny())
				throw new ArgumentException("You must specify at least one phone number.");

			var url = new StringBuilder();

			url.Append($"clicksend://{username}:{password}@");

			foreach (var phoneNumber in phoneNumbers)
				url.Append($"/{phoneNumber}");

			return url.ToString();
		}
	}
}
