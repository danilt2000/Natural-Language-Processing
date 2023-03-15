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

			for (item = 0; item < text.Count; item++)
			{
				if (months.Any(month => month == text[item]))
				{
					Console.WriteLine(item);

					if (item + 2 < text.Count)
					{
						Regex regex = new Regex(@"ColorSizeVerified(\w*)");

						MatchCollection matches = regex.Matches(text[item + 2]);

						//Console.WriteLine(matches.First());

						if (matches.First().ToString() == "Color"|| matches.First().ToString() == "Size" 
							|| matches.First().ToString() == "Verified")
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
				}
			}

			for (int p = flag; p < item; p++)
			{
				review += $" {text[p]}";
			}

			flag = item;

			parsedReviews.Add(review);

			return parsedReviews;
		}

		// открываем сайт
		//driver.Navigate().GoToUrl("https://www.example.com/");

		//// находим элемент на странице по значению атрибута "href"
		//IWebElement element = driver.FindElement(By.CssSelector("a[href='https://www.example.com/page']"));

		//// выводим текст элемента в консоль
		//Console.WriteLine(element.Text);

		// закрываем браузер
		//driver.Close();
	}
}
