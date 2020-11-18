using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Ryver Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_ryver">Apprise Wiki</see>.</para>
	/// </summary>
	public class Ryver : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Ryver Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="webhookUrl">The Webhook URL provided by Ryver</param>
		public Ryver(string appriseUrl, string webhookUrl)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = webhookUrl;
		}

		/// <summary>
		/// Initializes a new instance of Ryver Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="organisation">The organization you created your webhook under.</param>
		/// <param name="token">The token provided to you after creating a incoming-webhook</param>
		/// <param name="botname">Set the display name the message should appear from.</param>
		/// <param name="webhook">The type of webhook you created (Slack or Ryver). The only possible values are slack and ryver. The default value is ryver if the webhook value isn't specified.</param>
		public Ryver(string appriseUrl, string organisation, string token, string botname = null, string webhook = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(organisation, token, botname, webhook);
		}

		private static string ServiceUrlBuilder(string organisation, string token, string botname, string webhook)
		{
			var url = new StringBuilder();

			url.Append("ryver://");

			if (botname is not null)
				url.Append($"{botname}@");

			url.Append($"{organisation}/{token}");

			if (webhook is not null)
				url.AppendParam(nameof(webhook), webhook);

			return url.ToString();
		}
	}
}
