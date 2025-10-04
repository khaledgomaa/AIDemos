using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using OpenAI;
using System.ClientModel;

namespace TextCompletion
{
    internal interface ILLMModel
    {
        Task<ChatResponse> GetResponse(string prompt);

        IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponse(string prompt);

        IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponse(List<ChatMessage> prompt);
    }

    internal class Mini4oGpt : ILLMModel
    {
        private readonly IChatClient _client;

        public Mini4oGpt()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var credentials = new ApiKeyCredential(config["GithubModels:Token"]) ?? throw new ArgumentNullException("ApiKeyCredential is null");

            var endpoint = new Uri("https://models.github.ai/inference");

            var options = new OpenAIClientOptions()
            {
                Endpoint = endpoint
            };

            var model = "openai/gpt-4o-mini";

            _client = new OpenAIClient(credentials, options).GetChatClient(model).AsIChatClient();

        }
        public async Task<ChatResponse> GetResponse(string prompt)
        {
            return await _client.GetResponseAsync(prompt);
        }

        public IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponse(string prompt)
        {
            return _client.GetStreamingResponseAsync(prompt);
        }

        public IAsyncEnumerable<ChatResponseUpdate> GetStreamingResponse(List<ChatMessage> prompt)
        {
            return _client.GetStreamingResponseAsync(prompt);
        }
    }
}
