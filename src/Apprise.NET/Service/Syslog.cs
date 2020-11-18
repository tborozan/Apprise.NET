using Apprise.Enums;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Syslog Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_syslog">Apprise Wiki</see>.</para>
	/// </summary>
	public class Syslog : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Syslog Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="facility">The facility to use.</param>
		public Syslog(string appriseUrl, SyslogFacility facility = SyslogFacility.User)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = $"syslog://{facility.ToString().ToLower()}";
		}
	}
}
