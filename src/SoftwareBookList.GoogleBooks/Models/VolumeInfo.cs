using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareBookList.GoogleBooks
{
	public class VolumeInfo
	{
		public  string Title { get; set; }
		public  string Subtitle { get; set; }
		public virtual List<string> Authors { get; set; }
		public  string Publisher { get; set; }
		public  string PublishedDate { get; set; }
		public  string Description { get; set; }
		public virtual List<IndustryIdentifier> IndustryIdentifiers { get; set; }
		public virtual ReadingModes ReadingModes { get; set; }
		public  int PageCount { get; set; }
		public  string PrintType { get; set; }
		public virtual List<string> Categories { get; set; }
		public  double AverageRating { get; set; }
		public  int RatingsCount { get; set; }
		public  string MaturityRating { get; set; }
		public  bool AllowAnonLogging { get; set; }
		public  string ContentVersion { get; set; }
		public virtual PanelizationSummary PanelizationSummary { get; set; }
		public virtual ImageLinks ImageLinks { get; set; }
		public  string Language { get; set; }
		public  string PreviewLink { get; set; }
		public  string InfoLink { get; set; }
		public  string CanonicalVolumeLink { get; set; }
	}
}
