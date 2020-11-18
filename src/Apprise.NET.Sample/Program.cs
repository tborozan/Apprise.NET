using Apprise.Service;

var appriseUrl = "http://localhost:8000/notify/";
var discordWebhook = "https://discordapp.com/api/webhooks/778615238530039839/nVBc-RHmXZzP_OUf7Kh7-qw1Q5v5TqGC7-wYXLa9f6y1VImKzG6KYFojqFE2n7-QmpGB";
var discordWebhookId = "778615238530039839";
var discordWebhookToken = "nVBc-RHmXZzP_OUf7Kh7-qw1Q5v5TqGC7-wYXLa9f6y1VImKzG6KYFojqFE2n7-QmpGB";

var discord = new Discord(appriseUrl, discordWebhook);

await discord.NotifyAsync(message: "Hello from Apprise.NET.");
await discord.NotifyAsync(messageTitle: "Apprise.NET", messageBody: "Hello.");

var discordNamed = new Discord(appriseUrl, discordWebhookId, discordWebhookToken, userId: "Apprise.NET Bot");

await discordNamed.NotifyAsync(message: "Hello from Apprise.NET.");
await discordNamed.NotifyAsync(messageTitle: "Apprise.NET", messageBody: "Hello.");
