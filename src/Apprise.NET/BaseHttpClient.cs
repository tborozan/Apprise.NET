using Apprise.Enums;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Apprise
{
	internal static class BaseHttpClient
	{
		internal static async Task<HttpResponseMessage> SendMessageAsync(string apiEndpoint, string serviceUrl, string messageBody, string messageTitle, NotificationType notificationType)
		{
			//serviceUrl = HttpUtility.UrlEncode(serviceUrl);

			var message = new Dictionary<string, string>
			{
				{ "urls", serviceUrl },
				{ "title", messageTitle },
				{ "body", messageBody },
				{ "type", notificationType.ToString().ToLower() }
			};

			return await SendData(apiEndpoint, message);
		}

		private static async Task<HttpResponseMessage> SendData<TData>(string apiEndpoint, TData data)
		{
			using var httpClient = new HttpClient();
			var json = JsonSerializer.Serialize(data);
			var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

			if (!apiEndpoint.EndsWith("/notify/"))
				apiEndpoint = apiEndpoint.TrimEnd('/') + "/notify/";

			var response = await httpClient.PostAsync(apiEndpoint, content);

			return response;
		}
	}
}
