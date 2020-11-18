namespace Apprise.Service
{
	/// <summary>
	/// <para>Microsoft Teams Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_msteams">Apprise Wiki</see>.</para>
	/// </summary>
	public class MicrosoftTeams : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Microsoft Teams Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="webhook">Incoming webhook you generated on MS Teams.</param>
		public MicrosoftTeams(string appriseUrl, string webhook)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = webhook;
		}

		/// <summary>
		/// Initializes a new instance of Microsoft Teams Notification Service class
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="tokenA">The first part of 3 tokens provided to you after creating a incoming-webhook.</param>
		/// <param name="tokenB">The second part of 3 tokens provided to you after creating a incoming-webhook.</param>
		/// <param name="tokenC">The last part of 3 tokens provided to you after creating a incoming-webhook.</param>
		public MicrosoftTeams(string appriseUrl, string tokenA, string tokenB, string tokenC)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(tokenA, tokenB, tokenC);
		}

		private static string ServiceUrlBuilder(string tokenA, string tokenB, string tokenC)
			=> $"msteams://{tokenA}/{tokenB}/{tokenC}/";
	}
}
