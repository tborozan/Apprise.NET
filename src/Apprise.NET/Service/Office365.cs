using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Office 365 Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_office365">Apprise Wiki</see>.</para>
	/// </summary>
	public class Office365 : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Office 365 Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="tenantId">The Tenant ID Associated with your Azure Application you created. This can also be referred to as your Directory ID.</param>
		/// <param name="accountEmail">The Email Associated with your Azure account.</param>
		/// <param name="clientId">The Client ID Associated with your Azure Application you created. This can also be referred to as your Application ID.</param>
		/// <param name="clientSecret">You will need to generate one of these; this can be done through the Azure portal (Also documented below).</param>
		/// <param name="from">If you want the email address ReplyTo address to be something other then your own email address, then you can specify it here.</param>
		/// <param name="to">This will enforce (or set the address) the email is sent To. By default the email is sent to the address identified by the <paramref name="accountEmail"/>.</param>
		public Office365(string appriseUrl, string tenantId, string accountEmail, string clientId, string clientSecret, string from = null, string to = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(tenantId, accountEmail, clientId, clientSecret, from, to);
		}

		private static string ServiceUrlBuilder(string tenantId, string accountEmail, string clientId, string clientSecret, string from, string to)
		{
			var url = new StringBuilder();

			clientSecret = clientSecret.Replace("?", "%3F").Replace("@", "%40");

			url.Append($"o365://{tenantId}:{accountEmail}/{clientId}/{clientSecret}/");

			if (from.IsValidEmail())
				url.AppendParam(nameof(from), from);

			if (to.IsValidEmail())
				url.AppendParam(nameof(to), to);

			return url.ToString();
		}
	}
}
