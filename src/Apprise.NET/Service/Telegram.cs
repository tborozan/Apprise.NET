using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Telegram Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_telegram">Apprise Wiki</see>.</para>
	/// </summary>
	public class Telegram : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Telegram Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="botToken">The token that identifies the bot you created through the <see href="https://botsfortelegram.com/project/the-bot-father/">BotFather</see>/></param>
		/// <param name="chatIds">Identify the users you want your bot to deliver your notifications to.</param>
		public Telegram(string appriseUrl, string botToken, IEnumerable<long> chatIds = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(botToken, chatIds);
		}

		private static string ServiceUrlBuilder(string botToken, IEnumerable<long> chatIds)
		{
			var url = new StringBuilder();

			url.Append($"tgram://{botToken}/");

			foreach (var chatId in chatIds ?? Enumerable.Empty<long>())
				url.Append($"{chatId}/");

			return url.ToString();
		}
	}
}
