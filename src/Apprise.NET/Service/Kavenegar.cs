using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Kavenegar Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_kavenegar">Apprise Wiki</see>.</para>
	/// </summary>
	public class Kavenegar : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Kavenegar Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="apiKey">The API Key associated with your Kavengar account. This is available to you via the account profile section of their website (after logging in).</param>
		/// <param name="toPhoneNumbers">Kavengar does not handle the + in front of the country codes. You need to substitute the correct amount of zero's in front of the outbound number in order for the call to be completed.</param>
		/// <param name="fromPhoneNumber">The number you wish to identify your call is coming from. This argument is optional.</param>
		/// <exception cref="ArgumentException">At least one \"To:\" phone number must be specified.</exception>
		public Kavenegar(string appriseUrl, string apiKey, IEnumerable<string> toPhoneNumbers, string fromPhoneNumber = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, toPhoneNumbers, fromPhoneNumber);
		}

		private static string ServiceUrlBuilder(string apiKey, IEnumerable<string> toPhoneNumbers, string fromPhoneNumber)
		{
			if (!toPhoneNumbers.IsAny())
				throw new ArgumentException("At least one \"To:\" phone number must be specified.");

			var url = new StringBuilder();

			url.Append("kavenegar://");

			if (!string.IsNullOrWhiteSpace(fromPhoneNumber))
				url.Append($"{fromPhoneNumber}@");

			url.Append($"{apiKey}/");

			foreach (var phoneNumber in toPhoneNumbers)
				url.Append($"{phoneNumber}/");

			return url.ToString();
		}
	}
}
