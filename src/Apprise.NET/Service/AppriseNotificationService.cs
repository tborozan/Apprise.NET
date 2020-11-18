using Apprise.Enums;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Apprise.Service
{
#pragma warning disable CS1591
	/// <summary>
	/// Base class for Notification Services
	/// </summary>
	public class AppriseNotificationService
	{
		protected internal string AppriseUrl;
		protected internal string ServiceUrl;

		internal AppriseNotificationService()
		{
		}

		public AppriseNotificationService(string appriseUrl, string customServiceUrl)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = customServiceUrl;
		}

		/// <summary>
		/// Sends notification.
		/// </summary>
		/// <param name="message">Body of the message</param>
		/// <returns>Returns a value that indicates if the request was successful.</returns>
		public virtual async Task<HttpResponseMessage> NotifyAsync(string message)
			=> await BaseHttpClient.SendMessageAsync(AppriseUrl, ServiceUrl, message, null, NotificationType.Info);

		/// <summary>
		/// Sends notification.
		/// </summary>
		/// <param name="messageBody">Body of the message</param>
		/// <param name="messageTitle">Title of the message</param>
		/// <returns>Returns a value that indicates if the request was successful.</returns>
		public virtual async Task<HttpResponseMessage> NotifyAsync(string messageBody, string messageTitle)
			=> await BaseHttpClient.SendMessageAsync(AppriseUrl, ServiceUrl, messageBody, messageTitle, NotificationType.Info);

		/// <summary>
		/// Sends notification.
		/// </summary>
		/// <param name="messageBody">Body of the message</param>
		/// <param name="messageTitle">Title of the message</param>
		/// <param name="notificationType">Notification type</param>
		/// <returns>Returns a value that indicates if the request was successful.</returns>
		public virtual async Task<HttpResponseMessage> NotifyAsync(string messageBody, string messageTitle, NotificationType notificationType)
			=> await BaseHttpClient.SendMessageAsync(AppriseUrl, ServiceUrl, messageBody, messageTitle, notificationType);

		/// <summary>
		/// Appends custom paramters to service url.
		/// </summary>
		/// <param name="parameters">A dictionary of custom parameters to append.</param>
		public void AppendCustomParameters(IDictionary<string, string> parameters)
		{
			if (parameters.IsAny())
			{
				var url = new StringBuilder(ServiceUrl);

				foreach (var parameter in parameters)
					url.AppendParam(parameter.Key, parameter.Value);

				ServiceUrl = url.ToString();
			}
		}
	}
}
