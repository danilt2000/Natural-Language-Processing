using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;
using MLModelSentimentAnalysis;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Natural_Language_Processing
{
	internal class DataReceiverAmazon
	{
		public IWebDriver driver = new ChromeDriver();

		internal List<string> ParseTextToReviews(List<string> text)
		{
			List<string> parsedReviews = new List<string>();

			string[] months = DateTimeFormatInfo.CurrentInfo.MonthNames;

			string review = string.Empty;

			int flag = 0;

			int item = 0;

			item = TextProcessing(text, parsedReviews, months, ref review, ref flag);

			for (int p = flag; p < item; p++)
			{
				review += $" {text[p]}";
			}

			flag = item;

			parsedReviews.Add(review);

			return parsedReviews;
		}

		private static int TextProcessing(List<string> text, List<string> parsedReviews, string[] months, ref string review, ref int flag)
		{
			int item;

			for (item = 0; item < text.Count; item++)
			{
				if (months.Any(month => month == text[item]))
				{
					if (item + 2 < text.Count)
					{
						ProcessingReviews(text, parsedReviews, ref review, ref flag, item);
					}
				}
			}

			return item;
		}

		private static void ProcessingReviews(List<string> text, List<string> parsedReviews, ref string review, ref int flag, int item)
		{
			Regex regex = new Regex(@"\b[20]\w+");

			MatchCollection matches = regex.Matches(text[item + 2]);

			if (matches.Count > 0)
			{


				for (int p = flag; p < item; p++)
				{
					review += $" {text[p]}";
				}

				if (flag != 0)
				{
					parsedReviews.Add(review);
				}

				flag = item;

				review = string.Empty;
			}
		}

		internal List<T> RemoveDuplicates<T>(List<T> list)
		{
			return new HashSet<T>(list).ToList();
		}
		internal List<ReviewData> GetSentimentData(List<string> reviews)
		{
			//Много 0 = негативный

			//Много 1 = позитивный

			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();

			//var predictionResult2 = MLModel.Predict(new MLModel.ModelInput() { text = reviews[0] });

			List<ReviewData> reviewDatas = new List<ReviewData>();
			
			ReviewData.Sentiment predictDeсison;

			foreach (var reviewText in reviews)
			{
				float[] predictScore = MLModel.Predict(new MLModel.ModelInput() { text = reviewText }).Score;

				switch (predictScore[0])
				{
					case > 0.60f:
						predictDeсison = ReviewData.Sentiment.Negative;
						break;
					case < 0.40f:
						predictDeсison = ReviewData.Sentiment.Positive;
						break;
					default:
						predictDeсison = ReviewData.Sentiment.Neutral;
						break;
				}

				reviewDatas.Add(new ReviewData() { Review = reviewText, CurrentSentiment = predictDeсison });

			}

			stopwatch.Stop();

			return reviewDatas;
		}

		internal List<string> GetTop30FrequencyWords(List<string> words)
		{
			return words
						.Where(x => !string.IsNullOrEmpty(x))
						.GroupBy(x => x.ToLower())
						.OrderByDescending(x => x.Count())
						.Take(30)
						.Select(x => x.Key)
						.ToList();
		}
		internal List<string> GetTop30LengthWords(List<string> words)
		{
			words = RemoveDuplicates(words);

			List<string> topLengthWords = words
			   .Where(x => !string.IsNullOrEmpty(x))
			.OrderByDescending(x => x.Length)
			.Take(30)
			.ToList();

			return topLengthWords;
		}
	}
}
