using ChatGPTApi;
using System.Net.Http.Headers;
using System.Net.Http.Json;

string messageUser = "";
Console.WriteLine("Спросите о чем-нибудь или нажмите 0 для выхода из программы: ");
var openAiApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
while (true)
{
    messageUser = Convert.ToString(Console.ReadLine())!;
    if (messageUser != "0")
    {
        ChatGptClient chatGptClient = new ChatGptClient(openAiApiKey) { };
        Console.WriteLine(await chatGptClient.GetChatGptResponse(messageUser));
        messageUser = "";
    }
    else
    {
        break;
    }
}
Environment.Exit(0);