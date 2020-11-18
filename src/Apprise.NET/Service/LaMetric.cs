using Apprise.Enums;
using System;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>LaMetric Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_lametric">Apprise Wiki</see>.</para>
	/// </summary>
	public class LaMetric : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of LaMetric Notification Service in Device Mode
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="apiKey">Your Device API Key can be found on LaMetric's website.</param>
		/// <param name="hostname">This is the IP address or hostname of your Lametric device on your local network.</param>
		/// <param name="port">The port your LaMetric device is listening on. By default the port is 8080.</param>
		/// <param name="userId">The account login to your Lametric device on your local network. By default the user is set to dev.</param>
		/// <param name="cycles">The number of times message should be displayed. If cycles is set to 0, notification will stay on the screen until user dismisses it manually. By default it is set to 1.</param>
		/// <param name="sound">An audible alarm that can be sent with the notification.</param>
		/// <param name="priority">The priority of the message.</param>
		public LaMetric(string appriseUrl, string apiKey, string hostname, int? port = null, string userId = null, int? cycles = null, LaMetricSounds? sound = null, LaMetricPriority? priority = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, hostname, port, userId, cycles, sound, priority);
		}

		/// <summary>
		/// Initializes a new instance of LaMetric Notification Service in Cloud Mode
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="appId">Your Indicator App's Application ID can be found in your Indicator App Configuration*. You can access your application's configuration from the LaMetric's website.</param>
		/// <param name="appAccessToken">Your Indicator App's Access Token can be found in your Indicator App Configuration*. You can access your application's configuation from the LaMetric's website.</param>
		/// <param name="appVersion">The version associated with your Indicator App. If this isn't specified, then the default value of 1 (One) is used.</param>
		public LaMetric(string appriseUrl, string appId, string appAccessToken, Version appVersion = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(appId, appAccessToken, appVersion);
		}

		private static string ServiceUrlBuilder(string appId, string appAccessToken, Version appVersion)
		{
			var url = new StringBuilder();

			url.Append($"lametric://{appAccessToken}@{appId}");

			if (appVersion is not null)
				url.Append(appVersion.ToString());

			return url.ToString();
		}

		private static string ServiceUrlBuilder(string apiKey, string hostname, int? port, string userId, int? cycles, LaMetricSounds? sound, LaMetricPriority? priority)
		{
			var url = new StringBuilder();

			url.Append("lametric://");

			if (!string.IsNullOrWhiteSpace(userId))
				url.Append($"{userId}");

			url.Append($"{apiKey}@{hostname}");

			if (port.HasValue)
				url.Append($":{port}");

			if (cycles.HasValue)
				url.AppendParam(nameof(cycles), cycles.ToString());

			if (sound.HasValue)
			{
				switch (sound)
				{
					case LaMetricSounds.Bicycle:
						url.AppendParam(nameof(sound), "bicycle");
						break;

					case LaMetricSounds.Car:
						url.AppendParam(nameof(sound), "car");
						break;

					case LaMetricSounds.Cash:
						url.AppendParam(nameof(sound), "cash");
						break;

					case LaMetricSounds.Cat:
						url.AppendParam(nameof(sound), "cat");
						break;

					case LaMetricSounds.Dog:
						url.AppendParam(nameof(sound), "dog");
						break;

					case LaMetricSounds.Dog2:
						url.AppendParam(nameof(sound), "dog2");
						break;

					case LaMetricSounds.Energy:
						url.AppendParam(nameof(sound), "energy");
						break;

					case LaMetricSounds.KnockKnock:
						url.AppendParam(nameof(sound), "knock-knock");
						break;

					case LaMetricSounds.LetterEmail:
						url.AppendParam(nameof(sound), "letter_email");
						break;

					case LaMetricSounds.Lose1:
						url.AppendParam(nameof(sound), "lose1");
						break;

					case LaMetricSounds.Lose2:
						url.AppendParam(nameof(sound), "lose2");
						break;

					case LaMetricSounds.Negative1:
						url.AppendParam(nameof(sound), "negative1");
						break;

					case LaMetricSounds.Negative2:
						url.AppendParam(nameof(sound), "negative2");
						break;

					case LaMetricSounds.Negative3:
						url.AppendParam(nameof(sound), "negative3");
						break;

					case LaMetricSounds.Negative4:
						url.AppendParam(nameof(sound), "negative4");
						break;

					case LaMetricSounds.Negative5:
						url.AppendParam(nameof(sound), "negative5");
						break;

					case LaMetricSounds.Notification:
						url.AppendParam(nameof(sound), "notification");
						break;

					case LaMetricSounds.Notification2:
						url.AppendParam(nameof(sound), "notification2");
						break;

					case LaMetricSounds.Notification3:
						url.AppendParam(nameof(sound), "notification3");
						break;

					case LaMetricSounds.Notification4:
						url.AppendParam(nameof(sound), "notification4");
						break;

					case LaMetricSounds.OpenDoor:
						url.AppendParam(nameof(sound), "open_door");
						break;

					case LaMetricSounds.Positive1:
						url.AppendParam(nameof(sound), "positive1");
						break;

					case LaMetricSounds.Positive2:
						url.AppendParam(nameof(sound), "positive2");
						break;

					case LaMetricSounds.Positive3:
						url.AppendParam(nameof(sound), "positive3");
						break;

					case LaMetricSounds.Positive4:
						url.AppendParam(nameof(sound), "positive4");
						break;

					case LaMetricSounds.Positive5:
						url.AppendParam(nameof(sound), "positive5");
						break;

					case LaMetricSounds.Positive6:
						url.AppendParam(nameof(sound), "positive6");
						break;

					case LaMetricSounds.Statistic:
						url.AppendParam(nameof(sound), "statistic");
						break;

					case LaMetricSounds.Thunder:
						url.AppendParam(nameof(sound), "thunder");
						break;

					case LaMetricSounds.Water1:
						url.AppendParam(nameof(sound), "water1");
						break;

					case LaMetricSounds.Water2:
						url.AppendParam(nameof(sound), "water2");
						break;

					case LaMetricSounds.Win:
						url.AppendParam(nameof(sound), "win");
						break;

					case LaMetricSounds.Win2:
						url.AppendParam(nameof(sound), "win2");
						break;

					case LaMetricSounds.Wind:
						url.AppendParam(nameof(sound), "wind");
						break;

					case LaMetricSounds.WindShort:
						url.AppendParam(nameof(sound), "wind_short");
						break;

					case LaMetricSounds.Alarm1:
						url.AppendParam(nameof(sound), "alarm1");
						break;

					case LaMetricSounds.Alarm2:
						url.AppendParam(nameof(sound), "alarm2");
						break;

					case LaMetricSounds.Alarm3:
						url.AppendParam(nameof(sound), "alarm3");
						break;

					case LaMetricSounds.Alarm4:
						url.AppendParam(nameof(sound), "alarm4");
						break;

					case LaMetricSounds.Alarm5:
						url.AppendParam(nameof(sound), "alarm5");
						break;

					case LaMetricSounds.Alarm6:
						url.AppendParam(nameof(sound), "alarm6");
						break;

					case LaMetricSounds.Alarm7:
						url.AppendParam(nameof(sound), "alarm7");
						break;

					case LaMetricSounds.Alarm8:
						url.AppendParam(nameof(sound), "alarm8");
						break;

					case LaMetricSounds.Alarm9:
						url.AppendParam(nameof(sound), "alarm9");
						break;

					case LaMetricSounds.Alarm10:
						url.AppendParam(nameof(sound), "alarm10");
						break;

					case LaMetricSounds.Alarm11:
						url.AppendParam(nameof(sound), "alarm11");
						break;

					case LaMetricSounds.Alarm12:
						url.AppendParam(nameof(sound), "alarm12");
						break;

					case LaMetricSounds.Alarm13:
						url.AppendParam(nameof(sound), "alarm13");
						break;
				}
			}

			if (priority.HasValue)
				url.AppendParam(nameof(priority), priority.ToString().ToLower());

			return url.ToString();
		}
	}
}
