using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>MSG91 Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_msg91">Apprise Wiki</see>.</para>
	/// </summary>
	public class MSG91 : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of MSG91 Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="authKey">The Authentication Key associated with your MSG91 account. This is available to you via the MSG91 Dashboard.</param>
		/// <param name="phoneNumbers">A phone number MUST include the country codes dialing prefix as well when placed. This field is also very friendly and supports brackets, spaces and hyphens in the event you want to format the number in an easy to read fashion</param>
		/// <param name="senderId">Id of the sender.</param>
		/// <param name="route">The SMS Route. This is an SMG91 configuration that defaults to 1 (Transactional) if not otherwise specified.</param>
		/// <param name="country">The SMS Country. This is an SMG91 optional configuration that can either be 91 if referencing India, 1 if the USA and 0 if International.</param>
		/// <exception cref="ArgumentException">At least one phone number must be specified.</exception>
		public MSG91(string appriseUrl, string authKey, IEnumerable<string> phoneNumbers, string senderId = null, string route = null, string country = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(authKey, phoneNumbers, senderId, route, country);
		}

		private static string ServiceUrlBuilder(string authKey, IEnumerable<string> phoneNumbers, string senderId, string route, string country)
		{
			if (!phoneNumbers.IsAny())
				throw new ArgumentException("At least one phone number must be specified.");
			var url = new StringBuilder();

			url.Append("msg91://");

			if (!string.IsNullOrWhiteSpace(senderId))
				url.Append($"{senderId}@");

			url.Append(authKey);

			foreach (var phoneNumber in phoneNumbers)
				url.Append($"/{phoneNumber}");

			if (!string.IsNullOrWhiteSpace(route))
				url.AppendParam(nameof(route), route);

			if (!string.IsNullOrWhiteSpace(country))
				url.AppendParam(nameof(country), country);

			return url.ToString();
		}
	}
}
