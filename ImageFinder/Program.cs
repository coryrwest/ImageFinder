using System;
using System.Collections.Generic;

namespace ImageFinder {
	class Program {
		static void Main(string[] args) {
			WebHelpers web = new WebHelpers();
			string source = web.GetSourceOfURL("http://imgur.com/a/AD9E0");

            ImageFinder imageFinder = new ImageFinder();
            List<ImageWithDimensions> images = imageFinder.FindImages(source);

			ImageSizer sizer = new ImageSizer();
			List<ImageWithDimensions> largestImages = sizer.GetLargestImages(images);
			ImageWithDimensions largestImage = sizer.GetFirstLargestImage(images);

            ImageHelpers imageHelper = new ImageHelpers();
			imageHelper.SaveJpegToDisk(largestImage.Image, "test.jpg", AppDomain.CurrentDomain.BaseDirectory);
		}
	}
}
