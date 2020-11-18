using Apprise.Enums;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Prowl Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_prowl">Apprise Wiki</see>.</para>
	/// </summary>
	public class Prowl : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Prowl Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="apiKey">The API Key provided to you after you create yourself a Prowl account.</param>
		/// <param name="providerKey">The Provider Key is only required if you have been whitelisted.</param>
		/// <param name="priority">Can be low, moderate, normal, high, or emergency; the default is normal if a priority isn't specified.</param>
		public Prowl(string appriseUrl, string apiKey, string providerKey = null, ProwlPriority priority = ProwlPriority.Normal)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, providerKey, priority);
		}

		private static string ServiceUrlBuilder(string apiKey, string providerKey, ProwlPriority priority)
		{
			var url = new StringBuilder();

			url.Append($"prowl://{apiKey}");

			if (!string.IsNullOrWhiteSpace(providerKey))
				url.Append($"/{providerKey}");

			if (priority is not ProwlPriority.Normal)
				url.AppendParam(nameof(priority), priority.ToString());

			return url.ToString();
		}
	}
}
