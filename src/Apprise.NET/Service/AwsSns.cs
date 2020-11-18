using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>AWS SNS Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_sns">Apprise Wiki</see>.</para>
	/// </summary>
	public class AwsSns : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of AWS SNS Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="accessKeyId">The generated Access Key ID from the AWS Management Console</param>
		/// <param name="accessKeySecret">The generated Access Key Secret from the AWS Management Console</param>
		/// <param name="region">The region code might look like us-east-1, us-west-2, cn-north-1, etc</param>
		/// <param name="phoneNumbers">The phone numbers MUST include the country codes dialing prefix as well when placed. You can optionally prefix the entire number with a plus symbol (+) to enforce that the value be interpreted as a phone number (in the event it can't be auto-detected otherwise). This field is also very friendly and supports brackets, spaces and hyphens in the event you want to format the number in an easy to read fashion.</param>
		/// <param name="topics">The topics you want to publish your message to.</param>
		/// <exception cref="ArgumentException">At least one phone number or topic must be specified.</exception>
		public AwsSns(string appriseUrl, string accessKeyId, string accessKeySecret, string region, IEnumerable<string> phoneNumbers = null, IEnumerable<string> topics = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(accessKeyId, accessKeySecret, region, phoneNumbers, topics);
		}

		private static string ServiceUrlBuilder(string accessKeyId, string accessKeySecret, string region, IEnumerable<string> phoneNumbers, IEnumerable<string> topics)
		{
			if (!phoneNumbers.IsAny() && !topics.IsAny())
				throw new ArgumentException("At least one phone number or topic must be specified.");

			var url = new StringBuilder();

			url.Append($"sns://{accessKeyId}/{accessKeySecret}/{region}");

			foreach (var phoneNumber in phoneNumbers ?? Enumerable.Empty<string>())
				url.Append($"/+{phoneNumber}");

			foreach (var topic in topics ?? Enumerable.Empty<string>())
				url.Append($"/#{topic}");

			return url.ToString();
		}
	}
}
