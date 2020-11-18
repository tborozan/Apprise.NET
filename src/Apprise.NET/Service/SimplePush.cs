namespace Apprise.Service
{
	/// <summary>
	/// <para>SimplePush Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_simplepush">Apprise Wiki</see>.</para>
	/// </summary>
	public class SimplePush : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of SimplePush Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="apiKey">This is required for your account to work. You will be provided one from your SimplePush account.</param>
		public SimplePush(string appriseUrl, string apiKey)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = $"spush://{apiKey}/";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="appriseUrl"></param>
		/// <param name="apiKey">This is required for your account to work. You will be provided one from your SimplePush account.</param>
		/// <param name="salt">The salt is provided to you by SimplePush and is the second part of the additional encryption you can use with this service. You must provide a <paramref name="password"/> with the <paramref name="salt"/> value in order to work.</param>
		/// <param name="password">SimplePush offers a method of further encrypting the message and title during transmission (on top of the secure channel it's already sent on). This is the Encryption password set. You must provide the <paramref name="salt"/> value with the <paramref name="password"/> in order to work.</param>
		public SimplePush(string appriseUrl, string apiKey, string salt, string password)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = $"spush://{salt}:{password}@{apiKey}/";
		}
	}
}
