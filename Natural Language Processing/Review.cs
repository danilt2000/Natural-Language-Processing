using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural_Language_Processing
{
	internal class Review
	{
		public enum Sentiment { Positive, Negative, Neutral };

		private Sentiment _sentiment;

		public Sentiment CurrentSentiment
		{
			get { return _sentiment; }
			set { _sentiment = value; }
		}


		//public enum Sentiment {
		//	Positive,
		//	Negative,
		//	Neutral
		//};
		//public _Sentiment Sentiment { get; set; }
		//enum Sentiment
		//{
		//	Positive,
		//	Negative,
		//	Neutral
		//}
	}
}
