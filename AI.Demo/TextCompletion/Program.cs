using Microsoft.Extensions.AI;
using TextCompletion;

ILLMModel model = new Mini4oGpt();

#region Synchronous Call
//ChatResponse response = await model.GetResponse("What is LLM, write answer in 20 words.");

//Console.WriteLine("Total token for input: ", response.Usage.InputTokenCount);
//Console.WriteLine("Total token for output: ", response.Usage.OutputTokenCount);

//Console.WriteLine("---- Reponse -----");

//Console.WriteLine(response);
#endregion

#region Streaming Call
//var responseStream = model.GetStreamingResponse("What is LLM, write answer in 20 words.");

//Console.WriteLine("---- Reponse -----");

//await foreach (var response in responseStream)
//{
//    Console.Write(response.Text);
//}

#endregion

#region Classification Call

//var classificationPrompt = """
//    Classify the following text into one of the categories: Technology, Health, Finance, Education, Entertainment.
//    Text: "The stock market saw a significant increase in tech stocks today as investors showed renewed interest in AI and cloud computing."
//    Category:
//    """;

//ChatResponse classificationResponse = await model.GetResponse(classificationPrompt);

//Console.WriteLine("---- Classification Reponse -----");

//Console.WriteLine(classificationResponse);

#endregion

#region Summarization Call

//var summarizationPrompt = """
//    Summarize the following text in one sentence.
//    Text: "Artificial Intelligence (AI) is transforming industries by enabling machines to learn from data, identify patterns, and make decisions with minimal human intervention. From healthcare to finance, AI applications are enhancing efficiency, accuracy, and innovation."
//    Summary:
//    """;

//ChatResponse summarizationResponse = await model.GetResponse(summarizationPrompt);

//Console.WriteLine("---- Summarization Reponse -----");

//Console.WriteLine(summarizationResponse);

#endregion

#region Sentiment Analysis Call

//var sentimentPrompt = """
//    Analyze the sentiment of the following text and classify it as Positive, Negative, or Neutral.
//    Text: "I absolutely love the new features in the latest smartphone update! The camera quality has improved significantly, and the user interface is much more intuitive."
//    Sentiment:
//    """;

//ChatResponse sentimentResponse = await model.GetResponse(sentimentPrompt);

//Console.WriteLine("---- Sentiment Analysis Reponse -----");

//Console.WriteLine(sentimentResponse);

#endregion

#region Chat App

List<ChatMessage> chatHistory = new List<ChatMessage>
{
    new ChatMessage(ChatRole.System, "You are a helpful assistant.")
};

while (true)
{
    Console.Write("User: ");
    var userInput = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(userInput))
    {
        break;
    }
    var response = string.Empty;
    chatHistory.Add(new ChatMessage(ChatRole.User, userInput));
    var responseStream = model.GetStreamingResponse(chatHistory);

    Console.Write("Assistant: ");
    await foreach (var message in responseStream)
    {
        Console.Write(message.Text);
        response += message.Text;
    }
    chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
    Console.WriteLine();
}

#endregion