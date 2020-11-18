using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Discord Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_discord">Apprise Wiki</see>.</para>
	/// </summary>
	public class Discord : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Discord Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="webhookUrl">The Webhook URL provided by Discord</param>
		public Discord(string appriseUrl, string webhookUrl)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = webhookUrl;
		}

		/// <summary>
		/// Initializes a new instance of Discord Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="webhookId">The first part of 2 tokens provided to you after creating a incoming-webhook</param>
		/// <param name="webhookToken">The second part of 2 tokens provided to you after creating a incoming-webhook</param>
		public Discord(string appriseUrl, string webhookId, string webhookToken, string userId = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(webhookId, webhookToken, userId);
		}

		private static string ServiceUrlBuilder(string webhookId, string webhookToken, string userId)
		{
			var url = new StringBuilder();

			url.Append("discord://");

			if (!string.IsNullOrWhiteSpace(userId))
				url.Append($"{userId}@");

			url.Append($"{webhookId}/{webhookToken}/");

			return url.ToString();
		}
	}
}
