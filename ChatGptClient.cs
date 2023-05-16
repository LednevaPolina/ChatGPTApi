using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatGPTApi
{
    class ChatGptClient :IDisposable
    {
        private readonly HttpClient _httpClient;
        public ChatGptClient(string openAiApiKey)
        {           
            _httpClient = new HttpClient()
            {
                DefaultRequestHeaders =
                {
                    Authorization = AuthenticationHeaderValue.Parse($"Bearer {openAiApiKey}")
                }
            };
        }
        public async Task<string> GetChatGptResponse(string messageUser)
        {
           
            var chatGptRequest = new ChatGptRequest();
            chatGptRequest.Model = "gpt-3.5-turbo";
            chatGptRequest.Messages = new MessageRequest[] { new MessageRequest()
            { RoleRequest = "user", ContentRequest = messageUser } };
            
            var chatCompletionUri = "https://api.pawan.krd/v1/chat/completions";
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(chatCompletionUri, chatGptRequest);
            ChatGptResponse? chatGptResponse = await response.Content.ReadFromJsonAsync<ChatGptResponse>();
            return chatGptResponse.Choices[0].Message.Content.ToString();
        }
        public void Dispose()
        {
            _httpClient.Dispose();
        }        
    }
}
