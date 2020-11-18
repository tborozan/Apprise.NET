using System;
using System.Collections.Generic;
using System.Text;

namespace Apprise.Service
{
	/// <summary>
	/// <para>Matrix Notification Service</para>
	/// <para>For more information, please visit <see href="https://github.com/caronc/apprise/wiki/Notify_matrix">Apprise Wiki</see>.</para>
	/// </summary>
	public class Matrix : AppriseNotificationService
	{
		/// <summary>
		/// Initializes a new instance of Matrix Notification Service
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="matrixHost">The matrix server you wish to connect to.</param>
		/// <param name="user">The user to authenticate (and/or register) with the matrix server</param>
		/// <param name="password">The password to authenticate (and/or register) with the matrix server.</param>
		/// <param name="roomAliases">The room aliases you wish to join (if not there already) and broadcast your notification.</param>
		/// <param name="roomIds">The room ids you wish to join (if not there already) and broadcast your notification.</param>
		/// <exception cref="ArgumentException">At least one room alias or room id must be specified.</exception>
		/// </summary>

		public Matrix(string appriseUrl, string matrixHost, string user, string password, IEnumerable<string> roomAliases = null, IEnumerable<string> roomIds = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(matrixHost, user, password, roomAliases, roomIds);
		}

		/// <summary>
		/// Initializes a new instance of Matrix Notification Service class in Device Mode
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="hostname">The matrix server you wish to connect to.</param>
		/// <param name="token">Token to use with request.</param>
		/// <param name="webhook">Type of webhook service set up.</param>
		/// <param name="user">The user to authenticate (and/or register) with the matrix server</param>
		/// <param name="port">The server port Matrix is listening on. By default is set to 80 or to 443 if <paramref name="useHttps"/> is set to true.</param>
		/// <param name="format">Format of the message.</param>
		/// <param name="useHttps">Indicates whether to use http or https.</param>
		public Matrix(string appriseUrl, string hostname, string token, string webhook, string user = null, int? port = null, string format = null, bool useHttps = false)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(hostname, token, webhook, user, port, format, useHttps);
		}

		/// <summary>
		/// Initializes a new instance of Matrix Notification Service class in Device Mode
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="t2botWebhookToken">This is effectively the hostname but acts as the t2bot webhook token if the mode is set to t2bot.</param>
		/// <param name="user">The user to authenticate (and/or register) with the matrix server</param>
		public Matrix(string appriseUrl, string t2botWebhookToken, string user = null)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = ServiceUrlBuilder(t2botWebhookToken, user);
		}

		/// <summary>
		/// Initializes a new instance of Matrix Notification Service class in Device Mode
		/// </summary>
		/// <param name="appriseUrl">The URL of Apprise API.</param>
		/// <param name="t2botUrl">T2bot URL as they share it with you from their website.</param>
		public Matrix(string appriseUrl, string t2botUrl)
		{
			AppriseUrl = appriseUrl;
			ServiceUrl = t2botUrl;
		}

		private static string ServiceUrlBuilder(string matrixHost, string user, string password, IEnumerable<string> roomAliases, IEnumerable<string> roomIds)
		{
			if (!roomAliases.IsAny() && !roomAliases.IsAny())
				throw new ArgumentException("At least one room alias or room id must be specified.");

			var url = new StringBuilder();

			url.Append($"matrix://{user}:{password}@{matrixHost}/");

			foreach (var roomAlias in roomAliases)
				url.Append($"#{roomAlias}/");

			foreach (var roomId in roomIds)
				url.Append($"!{roomId}/");

			return url.ToString();
		}

		private static string ServiceUrlBuilder(string hostname, string token, string webhook, string user, int? port, string format, bool useHttps)
		{
			var url = new StringBuilder();

			if (useHttps)
				url.Append("matrixs://");
			else
				url.Append("matrix://");

			if (!string.IsNullOrWhiteSpace(user))
				url.Append($"{user}:");

			url.Append($"{token}@{hostname}");

			if (port.HasValue)
				url.Append($":{port}");

			url.Append($"?webhook={webhook}");

			if (!string.IsNullOrWhiteSpace(format))
				url.AppendParam(nameof(format), format);

			return url.ToString();
		}

		private static string ServiceUrlBuilder(string t2botWebhookToken, string user)
		{
			var url = new StringBuilder();

			url.Append("matrix://");

			if (!string.IsNullOrWhiteSpace(user))
				url.Append($"{user}@");

			url.Append(t2botWebhookToken);

			return url.ToString();
		}
	}
}
