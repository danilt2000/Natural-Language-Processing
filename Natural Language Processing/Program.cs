// See https://aka.ms/new-console-template for more information
using Azure.AI.TextAnalytics;
using Azure;
using System.Reflection.Metadata;
using System.Reflection;

string endpoint = "https://nlpservicehub.cognitiveservices.azure.com/";

string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ApiKey.txt");

List<string>? modifiedPath = path?.Split('\\').ToList();
foreach (var item in modifiedPath.ToList())
{
	if (item == "bin")
	{
		modifiedPath.Remove(item);
		continue;
	}
	if (item == "Debug")
	{
		modifiedPath.Remove(item);
		continue;
	}
	if (item == "net7.0")
	{
		modifiedPath.Remove(item);
		continue;
	}

}

//for (int i = 0; i < modifiedPath.Count; i++)
//{
//	if (modifiedPath[i] != "ApiKey.txt")
//	{
//		modifiedPath[i] = modifiedPath[i] + "\\";

//	}
//}
string apiKey = File.ReadAllText(string.Join("\\",modifiedPath));
var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

string document = @"I hate this movie..";

try
{
	Response<DocumentSentiment> response = client.AnalyzeSentiment(document);
	DocumentSentiment docSentiment = response.Value;

	Console.WriteLine($"Sentiment was {docSentiment.Sentiment}, with confidence scores: ");
	Console.WriteLine($"  Positive confidence score: {docSentiment.ConfidenceScores.Positive}.");
	Console.WriteLine($"  Neutral confidence score: {docSentiment.ConfidenceScores.Neutral}.");
	Console.WriteLine($"  Negative confidence score: {docSentiment.ConfidenceScores.Negative}.");
}
catch (RequestFailedException exception)
{
	Console.WriteLine($"Error Code: {exception.ErrorCode}");
	Console.WriteLine($"Message: {exception.Message}");
}

