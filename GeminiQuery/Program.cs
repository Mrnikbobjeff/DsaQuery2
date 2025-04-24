using GenerativeAI;
using GenerativeAI.Types;

namespace GeminiQuery;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // 1) Initialize your AI instance (GoogleAI) with credentials or environment variables
        var googleAI = new GoogleAi("AIzaSyAUyN2AxU6VUgermEvIz1wzUdXVgQML1Og");
        var models = await googleAI.ListModelsAsync();
        var actuals = models.Models.Where(m => m.Name.Contains("gemini")).ToList();

        // 2) Create a GenerativeModel using the model name "gemini-1.5-flash"
        var generativeModel = googleAI.CreateGenerativeModel("models/gemini-2.0-flash");
        // Generate content from a local file (e.g., an image)

        // 3) Start a chat session from the GenerativeModel
        var chatSession = generativeModel.StartChat();

        // 4) Send and receive messages
        var request = new GenerateContentRequest();
        request.AddText(
            "In future queries, answer detailed questions about the provided document which represents a chat history in json format: " +
            File.ReadAllText(args[0]));
        var response = await chatSession.GenerateContentAsync(request);
        // Attach a local file
        Console.WriteLine("First response: " + response.Text());

        // Continue the conversation
        while (true)
            try
            {
                var secondResponse = await chatSession.GenerateContentAsync(Console.ReadLine());
                Console.WriteLine("Second response: " + secondResponse.Text());
            }
            catch (Exception ex)

            {
                Console.WriteLine("Error: " + ex.Message);
            }
    }
}