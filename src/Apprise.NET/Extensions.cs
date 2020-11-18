using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Apprise
{
	internal static class Extensions
	{
		internal static bool IsValidEmail(this String email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			try
			{
				email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

				static string DomainMapper(Match match)
				{
					var idn = new IdnMapping();

					var domainName = idn.GetAscii(match.Groups[2].Value);

					return match.Groups[1].Value + domainName;
				}
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
			catch (ArgumentException)
			{
				return false;
			}

			try
			{
				return Regex.IsMatch(email,
					@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
					RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException)
			{
				return false;
			}
		}

		internal static bool IsAny<T>(this IEnumerable<T> data)
			=> data is not null && data.Any();

		internal static void AppendParam(this StringBuilder stringBuilder, string queryName, string query)
		{
			if (!string.IsNullOrWhiteSpace(query))
			{
				if (stringBuilder.ToString().Contains('?'))
					stringBuilder.Append($"&{queryName}={query}");
				else
					stringBuilder.Append($"?{queryName}={query}");
			}
		}

		internal static void AppendParam(this StringBuilder stringBuilder, string queryName, IEnumerable<string> query)
		{
			if (query.IsAny())
			{
				stringBuilder.Append($"{( stringBuilder.ToString().Contains('?') ? "&" : "?" )}{queryName}=");

				foreach (var item in query)
					stringBuilder.Append($"{item},");

				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
		}
	}
}
