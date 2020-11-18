namespace Apprise.Service
{
	/// <summary>
	/// <para>Kumulos Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_kumulos">Apprise Wiki</see>.</para>
	/// </summary>
	public class Kumulos : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Kumulos Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="apiKey">The API Key associated with your Kumulos account.</param>
		/// <param name="serverKey">The Server Secret associated with your Kumulos account.</param>
		public Kumulos(string appriseUrl, string apiKey, string serverKey)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, serverKey);
		}

		private static string ServiceUrlBuilder(string apiKey, string serverKey)
			=> $"kumulos://{apiKey}/{serverKey}";
	}
}
