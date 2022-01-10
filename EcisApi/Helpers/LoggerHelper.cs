using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EcisApi.Helpers
{
    public interface ILoggerHelper
    {
        Task LogInformation(string message);
        Task LogError(string message);
    }

    public class LoggerHelper : ILoggerHelper
    {
        private readonly AppSettings appSettings;
        private readonly IHttpClientFactory httpClientFactory;

        public LoggerHelper(
            IOptions<AppSettings> appSettings,
            IHttpClientFactory httpClientFactory)
        {
            this.appSettings = appSettings.Value;
            this.httpClientFactory = httpClientFactory;
        }

        private string SendMessageRoute => $"{appSettings.TelegramBotPath}{appSettings.TelegramBotToken}/sendMessage";

        public async Task LogInformation(string message)
        {
            var sendMessageClient = httpClientFactory.CreateClient();
            var sendMessageRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(SendMessageRoute),
                Content = JsonContent.Create(new
                {
                    chat_id = appSettings.TelegramBotChatId,
                    text = $"*[Information]*: {message}",
                    disable_notification = true
                }),
            };
            Console.WriteLine($"[Information]: {message}");
            await sendMessageClient.SendAsync(sendMessageRequest);
        }

        public async Task LogError(string message)
        {
            var sendMessageClient = httpClientFactory.CreateClient();
            var sendMessageRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(SendMessageRoute),
                Content = JsonContent.Create(new
                {
                    chat_id = appSettings.TelegramBotChatId,
                    text = $"*[Error]*: {message}",
                    disable_notification = false
                }),
            };
            Console.WriteLine($"[Error]: {message}");
            await sendMessageClient.SendAsync(sendMessageRequest);
        }
    }
}
