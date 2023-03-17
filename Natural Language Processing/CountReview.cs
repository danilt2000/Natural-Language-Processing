using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natural_Language_Processing
{
	internal class CountReview
	{
		public enum Sentiment { Positive, Negative, Neutral };

		private Sentiment _sentiment;

		public CountReview(Sentiment currentSentiment)
		{
			CurrentSentiment = currentSentiment;
		}

		public Sentiment CurrentSentiment
		{
			get { return _sentiment; }
			set { _sentiment = value; }
		}

		public int Count { get; set; }
	}
}
