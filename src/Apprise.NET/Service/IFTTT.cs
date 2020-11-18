using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>IFTTT Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_ifttt">Apprise Wiki</see>.</para>
	/// </summary>
	public class IFTTT : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of IFTTT Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="webhookUrl"></param>
		public IFTTT(string appriseUrl, string webhookUrl)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = webhookUrl;
		}

		/// <summary>
		/// Initializes a new instance of IFTTT Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="webhookId">Your webhooks API Key you got from <see href="https://ifttt.com/services/maker_webhooks">the settings area of the webhooks service itself</see>.</param>
		/// <param name="events">This is the Event Name you assigned to the Applet you created. You must at least pass in one of these. This is the event plan on triggering through the webhook.</param>
		/// <param name="useValue1">Set to false if you don't want to send the title message.</param>
		/// <param name="useValue2">Set to false if you don't want to send the body message.</param>
		/// <param name="useValue3">Set to false if you don't want to send the message type.</param>
		/// <param name="args">A dictionary of additional arguments.</param>
		/// <exception cref="ArgumentException">At least one event must be specified.</exception>
		public IFTTT(string appriseUrl, string webhookId, IEnumerable<string> events, bool useValue1 = true, bool useValue2 = true, bool useValue3 = true, IDictionary<string, string> args = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(webhookId, events, useValue1, useValue2, useValue3, args);
		}

		private static string ServiceUrlBuilder(string webhookId, IEnumerable<string> events, bool useValue1, bool useValue2, bool useValue3, IDictionary<string, string> args)
		{
			if (!events.IsAny())
				throw new ArgumentException("At least one event must be specified.");

			var url = new StringBuilder();

			url.Append($"ifttt://{webhookId}@");

			foreach (var eve in events)
				url.Append($"{eve}/");

			if (!useValue1)
				url.Append("?-value1");

			if (!useValue2)
				url.Append("?-value2");

			if (!useValue3)
				url.Append("?-value3");

			foreach (var arg in args)
				url.Append($"?+{arg.Key}={arg.Value}/");

			return url.ToString();
		}
	}
}
