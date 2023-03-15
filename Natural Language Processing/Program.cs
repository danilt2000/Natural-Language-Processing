﻿// See https://aka.ms/new-console-template for more information
using Azure.AI.TextAnalytics;
using Azure;
using System.Reflection.Metadata;
using System.Reflection;
using MLModelSentimentAnalysis;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Diagnostics;
using OpenQA.Selenium.DevTools.V108.Network;
using Natural_Language_Processing;

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

		DataReceiverAmazon dataReceiverAmazon = new DataReceiverAmazon();

		var predictionResult = MLModel.Predict(sampleData);
		//dataReceiverAmazon.driver.Navigate().GoToUrl("https://www.amazon.com/Snpurdiri-Keyboard-Ultra-Compact-Waterproof-Black-White/dp/B097T276QL/ref=sr_1_1_sspa?keywords=gaming%2Bkeyboard&pd_rd_r=dc2c99a3-0204-4795-bbfb-a0c8f7dcfc71&pd_rd_w=HrIA5&pd_rd_wg=j7XhG&pf_rd_p=12129333-2117-4490-9c17-6d31baf0582a&pf_rd_r=682A85JRZ87088PCQSFX&qid=1678746765&sr=8-1-spons&spLa=ZW5jcnlwdGVkUXVhbGlmaWVyPUExOFo3R1VOWUJOR1owJmVuY3J5cHRlZElkPUEwNjYwMzUzN0RSSzdDOFpHUTE3JmVuY3J5cHRlZEFkSWQ9QTA3MDcyNTYxSE1FT1IzNFoySTFHJndpZGdldE5hbWU9c3BfYXRmJmFjdGlvbj1jbGlja1JlZGlyZWN0JmRvTm90TG9nQ2xpY2s9dHJ1ZQ&th=1");

		dataReceiverAmazon.driver.Navigate().GoToUrl("https://www.amazon.com/nuphy-Halo65-Bluetooth%E3%80%812-4G-Connection%EF%BC%8CCompatible-Windows-Black/dp/B0BJTVK6TT/ref=cm_cr_arp_d_product_top?ie=UTF8");

		try
		{
			IWebElement buttonToStartReviews = dataReceiverAmazon.driver.FindElement(By.XPath("//a[@class='a-link-emphasis a-text-bold']"));

			buttonToStartReviews.Click();

			IWebElement elements = dataReceiverAmazon.driver.FindElement(By.XPath("//*[@id=\"cm_cr-review_list\"]"));

			List<string> reviews = new List<string>();

			reviews.AddRange(dataReceiverAmazon.ParseTextToReviews(elements.Text.Split(' ').ToList()));

			reviews.AddRange(GetAllReviews(dataReceiverAmazon));


			//Stopwatch stopwatch = new Stopwatch();

			//stopwatch.Start();

			//var predictionResult2 = MLModel.Predict(new MLModel.ModelInput() { text = reviews[0] });

			//stopwatch.Stop();
		}
		catch (NoSuchElementException ex)
		{
			// Если элемент не найден, выводим сообщение об ошибке
			Console.WriteLine("Элемент не найден на странице");
		}
		finally
		{
			// Закрываем браузер
			dataReceiverAmazon.driver.Quit();
		}
		// Make a single prediction on the sample data and print results
	}

	private static List<string> GetAllReviews(DataReceiverAmazon dataReceiverAmazon)
	{
		List<string> reviewsParsed = new List<string>();

		IWebElement elements;
		
		try
		{
			while (true)
			{
				IWebElement buttonNext = dataReceiverAmazon.driver.FindElement(By.XPath("//a[contains(text(),'Next page')]"));

				buttonNext.Click();

				elements = dataReceiverAmazon.driver.FindElement(By.XPath("//*[@id=\"cm_cr-review_list\"]"));

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

