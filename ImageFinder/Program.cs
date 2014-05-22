using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFinder {
	class Program {
		static void Main(string[] args) {
			WebHelpers web = new WebHelpers();
			string source = web.GetSourceOfURL("http://imgur.com/a/AD9E0");

			ImageHelpers imageHelper = new ImageHelpers();
			List<ImageWithDimensions> images = imageHelper.FindImages(source);

			ImageSizer sizer = new ImageSizer();
			List<ImageWithDimensions> largestImages = sizer.GetLargestImages(images);
			ImageWithDimensions largestImage = sizer.GetFirstLargestImage(images);

			imageHelper.SaveJpegToDisk(largestImage.Image, "test.jpg", AppDomain.CurrentDomain.BaseDirectory);
		}
	}
}
