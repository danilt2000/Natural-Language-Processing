// See https://aka.ms/new-console-template for more information
using MLModelSentimentAnalysis;
using Natural_Language_Processing;
using OpenQA.Selenium;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using WordCloudSharp;

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
			text = @"I love you ",
		};


		var predictionResult = MLModel.Predict(sampleData);

		//dataReceiverAmazon.driver.Navigate().GoToUrl("https://www.amazon.com/Snpurdiri-Keyboard-Ultra-Compact-Waterproof-Black-White/dp/B097T276QL/ref=sr_1_1_sspa?keywords=gaming%2Bkeyboard&pd_rd_r=dc2c99a3-0204-4795-bbfb-a0c8f7dcfc71&pd_rd_w=HrIA5&pd_rd_wg=j7XhG&pf_rd_p=12129333-2117-4490-9c17-6d31baf0582a&pf_rd_r=682A85JRZ87088PCQSFX&qid=1678746765&sr=8-1-spons&spLa=ZW5jcnlwdGVkUXVhbGlmaWVyPUExOFo3R1VOWUJOR1owJmVuY3J5cHRlZElkPUEwNjYwMzUzN0RSSzdDOFpHUTE3JmVuY3J5cHRlZEFkSWQ9QTA3MDcyNTYxSE1FT1IzNFoySTFHJndpZGdldE5hbWU9c3BfYXRmJmFjdGlvbj1jbGlja1JlZGlyZWN0JmRvTm90TG9nQ2xpY2s9dHJ1ZQ&th=1");

		//dataReceiverAmazon.driver.Navigate().GoToUrl("https://www.amazon.com/nuphy-Halo65-Bluetooth%E3%80%812-4G-Connection%EF%BC%8CCompatible-Windows-Black/dp/B0BJTVK6TT/ref=cm_cr_arp_d_product_top?ie=UTF8");

		//dataReceiverAmazon.driver.Navigate().GoToUrl("https://www.amazon.com/Corsair-K100-Mechanical-Gaming-Keyboard/dp/B08HR68MQZ/ref=sr_1_36?keywords=gaming%2Bkeyboard&pd_rd_r=fdd7ea3a-5bb0-48d3-a8a2-5661d0661600&pd_rd_w=fGVGq&pd_rd_wg=oJylQ&pf_rd_p=12129333-2117-4490-9c17-6d31baf0582a&pf_rd_r=2BJHKTCD17E31HZNA119&qid=1679005483&sr=8-36&th=1");

		string url = "https://www.amazon.com/Manhattan-Wired-Membrane-Gaming-Keyboard/dp/B09XFG9BWF/ref=sr_1_59?keywords=gaming%2Bkeyboard&pd_rd_r=20ff9213-fddf-410b-b20a-4e430da8b701&pd_rd_w=Ok9Wr&pd_rd_wg=gtEiC&pf_rd_p=12129333-2117-4490-9c17-6d31baf0582a&pf_rd_r=KHN2F6AP2DWDKV72WZJP&qid=1679007831&sr=8-59&th=1";

		DataReceiverAmazon dataReceiverAmazon = new DataReceiverAmazon();

		dataReceiverAmazon.driver.Navigate().GoToUrl(url);

		Console.WriteLine($"URL:{url}");

		List<int> positionWordCloud = new List<int>();

		for (int i = 1; i <= 30; i++)
		{
			positionWordCloud.Add(i);
		}

		try
		{
			IWebElement buttonToStartReviews = dataReceiverAmazon.driver.FindElement(By.XPath("//a[@class='a-link-emphasis a-text-bold']"));

			buttonToStartReviews.Click();

			IWebElement elements = dataReceiverAmazon.driver.FindElement(By.XPath("//*[@id=\"cm_cr-review_list\"]"));

			List<string> reviews = new List<string>();

			reviews.AddRange(dataReceiverAmazon.ParseTextToReviews(elements.Text.Split(' ').ToList()));

			reviews.AddRange(GetAllReviews(dataReceiverAmazon));

			List<ReviewData> reviewDatas = dataReceiverAmazon.GetSentimentData(reviews);

			Console.WriteLine($"Reviews {reviews.Count}");

			CountReview positiveCount = new CountReview(CountReview.Sentiment.Positive);

			CountReview negativeCount = new CountReview(CountReview.Sentiment.Negative);

			CountReview neutralCount = new CountReview(CountReview.Sentiment.Neutral);

			foreach (var item in reviewDatas)
			{
				if (item.CurrentSentiment == ReviewData.Sentiment.Negative)
				{
					negativeCount.Count++;
				}
				if (item.CurrentSentiment == ReviewData.Sentiment.Positive)
				{
					positiveCount.Count++;
				}
				if (item.CurrentSentiment == ReviewData.Sentiment.Neutral)
				{
					neutralCount.Count++;
				}
			}

			Console.WriteLine($"Sentiment Neutral=>{neutralCount.Count}" +
				$" Positive=>{positiveCount.Count} Negative=>{negativeCount.Count}");

			List<CountReview> sentimentPosition = new List<CountReview>()

			{ positiveCount, negativeCount, neutralCount };

			sentimentPosition = sentimentPosition.OrderByDescending(x => x.Count).ToList();

			List<string> sentimentPositionForCloud = new List<string>();

			sentimentPosition.ForEach(x => sentimentPositionForCloud.Add(x.CurrentSentiment.ToString()));

			CreateWordCloud(sentimentPositionForCloud, "C:\\test\\CircleBitmap.png",
				$"G:\\temp\\WordCloud\\{Guid.NewGuid()}.png", new List<int>() { 1, 2, 3 });

			var wordsTemp = reviews.SelectMany(x => x.Split(' '));

			List<string> words = wordsTemp.ToList();

			for (int i = 0; i < words.Count(); i++)
			{
				words[i] = words[i].Replace("\r\n", string.Empty);
			}

			List<string> topFrequencyWords = dataReceiverAmazon.GetTop30FrequencyWords(words);

			Console.WriteLine("30 Most used words");

			int indexCount = 1;

			foreach (var item in topFrequencyWords)
			{
				Console.Write($"N{indexCount}=>{item},");

				indexCount++;
			}

			CreateWordCloud(topFrequencyWords, "C:\\test\\CircleBitmap.png", $"G:\\temp\\WordCloud\\{Guid.NewGuid()}.png", positionWordCloud);

			List<string> topLengthWords = dataReceiverAmazon.GetTop30LengthWords(words);

			Console.WriteLine();

			Console.WriteLine("30 Longest words");

			int indexCountLongest = 1;

			foreach (var item in topLengthWords)
			{
				Console.Write($"N{indexCountLongest}=>{item},");

				indexCountLongest++;
			}

			CreateWordCloud(topLengthWords, "C:\\test\\CircleBitmap.png", $"G:\\temp\\WordCloud\\{Guid.NewGuid()}.png", positionWordCloud);
		}
		catch (NoSuchElementException ex)
		{

		}
		finally
		{
			dataReceiverAmazon.driver.Quit();
		}
		// Make a single prediction on the sample data and print results
	}

	private static void CreateWordCloud(List<string> datas, string pathBitmap, string pathToSave, List<int> positionWordCloud)
	{
		Bitmap maskBitmap = new Bitmap(pathBitmap);

		var wordCloud = new WordCloud(500, 500, mask: maskBitmap, allowVerical: true, fontname: "YouYuan");
		//wordCloud.OnProgress += Wc_OnProgress;
		//var image = wordCloud.Draw(topLengthWords, positionWordCloud);
		var image = wordCloud.Draw(datas, positionWordCloud);

		image.Save(pathToSave, ImageFormat.Png);
	}

	private static List<string> GetAllReviews(DataReceiverAmazon dataReceiverAmazon)
	{
		List<string> reviewsParsed = new List<string>();

		try
		{
			while (true)
			{
				IWebElement buttonNext = dataReceiverAmazon.driver.FindElement(By.XPath("//a[contains(text(),'Next page')]"));

				buttonNext.Click();

				Thread.Sleep(1500);

				IWebElement elements = dataReceiverAmazon.driver.FindElement(By.XPath("//*[@id=\"cm_cr-review_list\"]"));

				reviewsParsed.AddRange(dataReceiverAmazon.ParseTextToReviews(elements.Text.Split(' ').ToList()));
			}
		}

		catch (Exception)
		{
			return reviewsParsed;

			throw;
		}
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

