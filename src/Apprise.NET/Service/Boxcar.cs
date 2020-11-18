using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Boxcar Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_boxcar">Apprise Wiki</see>.</para>
	/// </summary>
	public class Boxcar : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Boxcar Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="accessKey">This is required for your account to work. You will be provided one from boxcar's website upon creating an account with them.</param>
		/// <param name="secureKey">This is required for your account to work. You will be provided one from boxcar's website upon creating an account with them.</param>
		/// <param name="deviceIds">Associated devices with your Boxcar setup. All device_ids are 64 characters in length.</param>
		/// <param name="tagIds">Tags must be prefixed with a @ symbol or they will be interpreted as a device_id and/or alias.</param>
		public Boxcar(string appriseUrl, string accessKey, string secureKey, IEnumerable<string> deviceIds = null, IEnumerable<string> tagIds = null)
		{
			AppriseUrl = appriseUrl;

			ServiceUrl = ServiceBuilder(accessKey, secureKey, deviceIds, tagIds);
		}

		private static string ServiceBuilder(string accessKey, string secureKey, IEnumerable<string> deviceIds, IEnumerable<string> tagIds)
		{
			var url = new StringBuilder();

			url.Append($"boxcar://{accessKey}/{secureKey}");

			foreach (var deviceId in deviceIds ?? Enumerable.Empty<string>())
				url.Append($"/{deviceId}");

			foreach (var tagId in tagIds ?? Enumerable.Empty<string>())
				url.Append($"/@{tagId}");

			return url.ToString();
		}
	}
}
