using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>AWS SNS Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_gitter">Apprise Wiki</see>.</para>
	/// </summary>
	public class Gitter : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Gitter Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="token">The Personal Access Token associated with your account. This is available to you after signing into their <see href="https://developer.gitter.im/apps">development website</see>.</param>
		/// <param name="rooms">Rooms you want to notify. You can specify as many as you want.</param>
		/// <exception cref="ArgumentException">At least one room must be specified.</exception>
		public Gitter(string appriseUrl, string token, IEnumerable<string> rooms)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(token, rooms);
		}

		private static string ServiceUrlBuilder(string token, IEnumerable<string> rooms)
		{
			if (!rooms.IsAny())
				throw new ArgumentException("At least one room must be specified.");

			var url = new StringBuilder();

			url.Append($"gitter://{token}/");

			foreach (var room in rooms)
				url.Append($"{room}/");

			return url.ToString();
		}
	}
}
