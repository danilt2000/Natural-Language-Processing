// See https://aka.ms/new-console-template for more information
using Azure.AI.TextAnalytics;
using Azure;
using System.Reflection.Metadata;
using System.Reflection;
using MLModelSentimentAnalysis;
internal class Program
{
	private static void Main(string[] args)
	{
		MLModel.ModelInput sampleData = new MLModel.ModelInput()
		{
			//_1467810369 = 1.467811E+09F,
			//Mon_Apr_06_22_19_45_PDT_2009 = @"Mon Apr 06 22:19:53 PDT 2009",
			//NO_QUERY = @"NO_QUERY",
			//__TheSpecialOne_ = @"mattycus",
			__switchfoot_http___twitpic_com_2y1zl___Awww__that_s_a_bummer___You_shoulda_got_David_Carr_of_Third_Day_to_do_it___D = @"I love you so much you",
		};

		// Make a single prediction on the sample data and print results
		var predictionResult = MLModel.Predict(sampleData);
	}
}
//string endpoint = "https://nlpservicehub.cognitiveservices.azure.com/";

//string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ApiKey.txt");

//List<string>? modifiedPath = path?.Split('\\').ToList();
//foreach (var item in modifiedPath.ToList())
//{
//	if (item == "bin")
//	{
//		modifiedPath.Remove(item);
//		continue;
//	}
//	if (item == "Debug")
//	{
//		modifiedPath.Remove(item);
//		continue;
//	}
//	if (item == "net7.0")
//	{
//		modifiedPath.Remove(item);
//		continue;
//	}

//}

////for (int i = 0; i < modifiedPath.Count; i++)
////{
////	if (modifiedPath[i] != "ApiKey.txt")
////	{
////		modifiedPath[i] = modifiedPath[i] + "\\";

////	}
////}
//string apiKey = File.ReadAllText(string.Join("\\",modifiedPath));
//var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

//string document = @"I hate this movie..";

//try
//{
//	Response<DocumentSentiment> response = client.AnalyzeSentiment(document);
//	DocumentSentiment docSentiment = response.Value;

//	Console.WriteLine($"Sentiment was {docSentiment.Sentiment}, with confidence scores: ");
//	Console.WriteLine($"  Positive confidence score: {docSentiment.ConfidenceScores.Positive}.");
//	Console.WriteLine($"  Neutral confidence score: {docSentiment.ConfidenceScores.Neutral}.");
//	Console.WriteLine($"  Negative confidence score: {docSentiment.ConfidenceScores.Negative}.");
//}
//catch (RequestFailedException exception)
//{
//	Console.WriteLine($"Error Code: {exception.ErrorCode}");
//	Console.WriteLine($"Message: {exception.Message}");
//}

