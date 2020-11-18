namespace Apprise.Service
{
	/// <summary>
	/// <para>Webex Teams Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_wxteams">Apprise Wiki</see>.</para>
	/// </summary>
	public class WebexTeams : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Webex Teams Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="token">The tokens provided to you after creating a incoming-webhook</param>
		public WebexTeams(string appriseUrl, string token)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = $"wxteams://{token}/";
		}
	}
}
