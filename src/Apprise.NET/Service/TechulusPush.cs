namespace Apprise.Service
{
	/// <summary>
	/// <para>Techulus Push Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_techulus">Apprise Wiki</see>.</para>
	/// </summary>
	public class TechulusPush : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Techulus Push Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="apiKey">The apikey associated with your Techulus Push account.</param>
		public TechulusPush(string appriseUrl, string apiKey)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = $"push://{apiKey}/";
		}
	}
}
