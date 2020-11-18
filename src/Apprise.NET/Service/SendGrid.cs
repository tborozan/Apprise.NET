using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>SendGrid Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_sendgrid">Apprise Wiki</see>.</para>
	/// </summary>
	public class SendGrid : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of SendGrid Notification Service
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API</param>
		/// <param name="apiKey">The API Key you generated from within your SendGrid dashboard.</param>
		/// <param name="fromEmail">This is the email address will identify the email's origin (the From address). This address must contain a domain that was previously authenticated with your SendGrid account (See Domain Authentication).</param>
		/// <param name="toEmails">This is the email address will identify the email's destination (the To address). If one isn't specified then the from_email is used instead.</param>
		/// <param name="template">You may optionally specify the UUID of a previously generated SendGrid dynamic template to base the email on.</param>
		/// <param name="dynamicTemplateData">Templates allow you to define {{variables}} within them that can be substituted on the fly once the email is sent.</param>
		public SendGrid(string appriseUrl, string apiKey, string fromEmail, IEnumerable<string> toEmails = null, string template = null, IDictionary<string, string> dynamicTemplateData = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(apiKey, fromEmail, toEmails, template, dynamicTemplateData);
		}

		private static string ServiceUrlBuilder(string apiKey, string fromEmail, IEnumerable<string> toEmails, string template, IDictionary<string, string> dynamicTemplateData)
		{
			if (template is not null && dynamicTemplateData.IsAny())
				throw new ArgumentException("To use dynamic template data, a template UUID must be specified.");

			var url = new StringBuilder();

			url.Append($"sendgrid://{apiKey}:{fromEmail}");

			if (toEmails.IsAny())
			{
				foreach (var email in toEmails)
					url.Append($"/{email}");
			}

			if (template is not null)
				url.AppendParam(nameof(template), template);

			if (dynamicTemplateData.IsAny())
			{
				foreach (var data in dynamicTemplateData)
					url.Append($"&+{data.Key}={data.Value}");
			}

			return url.ToString();
		}
	}
}
