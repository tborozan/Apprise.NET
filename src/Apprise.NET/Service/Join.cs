using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Join Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_join">Apprise Wiki</see>.</para>
	/// </summary>
	public class Join : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Join Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="apiKey">The api key associated with your Join account.</param>
		/// <param name="deviceIds">The device identifier to send your notification to (a 32 bit alpha-numeric string).</param>
		/// <param name="groupIds">The group identifier to send your notification to.</param>
		/// <param name="deviceNames"></param>
		public Join(string appriseUrl, string apiKey, IEnumerable<string> deviceIds = null, IEnumerable<string> groupIds = null, IEnumerable<string> deviceNames = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, deviceIds, groupIds, deviceNames);
		}

		private static string ServiceUrlBuilder(string apiKey, IEnumerable<string> deviceIds, IEnumerable<string> groupIds, IEnumerable<string> deviceNames)
		{
			var url = new StringBuilder();

			url.Append($"join://{apiKey}/");

			foreach (var deviceId in deviceIds ?? Enumerable.Empty<string>())
				url.Append($"{deviceId}/");

			foreach (var groupId in groupIds ?? Enumerable.Empty<string>())
				url.Append($"group.{groupId}/");

			if (deviceIds == null && groupIds == null)
			{
				foreach (var deviceName in deviceNames ?? Enumerable.Empty<string>())
					url.Append($"{deviceName}/");
			}

			return url.ToString();
		}
	}
}
