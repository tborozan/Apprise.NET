using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Notifico Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_notifico">Apprise Wiki</see>.</para>
	/// </summary>
	public class Notifico : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Notifico Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="url">Native URL as retrieved from the website.</param>
		public Notifico(string appriseUrl, string url)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = url;
		}

		/// <summary>
		/// Initializes a new instance of Notifico Notification Service class in Device Mode
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="projectId">The project ID is an integer and makes up the first part of the provided Notifico Message Hook URL.</param>
		/// <param name="messageHook">The message hook can be found at the end of the provided Notifico Message Hook URL.</param>
		/// <param name="color">Uses IRC Coloring to provide a richer experience. It also allows the parsing of IRC colors found in the notification passed in. You must ensure the Color Checkbox is selected when setting up your Message Hook for this to work. By default this is set to Yes.</param>
		/// <param name="prefix">All messages sent to IRC by default have a Prefix that help identify the type of message (info, error, warning, or success) as well as the system performing the notification. By default this is set to Yes.</param>
		public Notifico(string appriseUrl, string projectId, string messageHook, bool color = true, bool prefix = true)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(projectId, messageHook, color, prefix);
		}

		private static string ServiceUrlBuilder(string projectId, string messageHook, bool color, bool prefix)
		{
			var url = new StringBuilder();

			url.Append($"notifico://{projectId}/{messageHook}");

			if (!color)
				url.AppendParam(nameof(color), "off");

			if (!prefix)
				url.AppendParam(nameof(prefix), "off");

			return url.ToString();
		}
	}
}
