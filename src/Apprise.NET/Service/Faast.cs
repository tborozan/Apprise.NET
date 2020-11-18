namespace Apprise.Service
{
	/// <summary>
	/// <para>Faast Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_faast">Apprise Wiki</see>.</para>
	/// </summary>
	public class Faast : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Faast Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="authorizationtoken">The authorization token associated with your Faast account.</param>
		public Faast(string appriseUrl, string authorizationtoken)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(authorizationtoken);
		}

		private static string ServiceUrlBuilder(string authorizationtoken)
			=> $"faast://{authorizationtoken}";
	}
}
