using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageFinder {
	public class ImageSizer {
		public List<ImageWithDimensions> GetLargestImages(List<ImageWithDimensions> images) {
			int largestDimensions = 0;
			List<ImageWithDimensions> largestImages = new List<ImageWithDimensions>();
			List<ImageWithDimensions> finalLargestImages = new List<ImageWithDimensions>();

			foreach (ImageWithDimensions image in images) {
				int imageDimensions = image.Width * image.Height;
				if (imageDimensions >= largestDimensions) {
					largestDimensions = imageDimensions;
					largestImages.Add(image);
				}
			}

			foreach (ImageWithDimensions image in largestImages) {
				int imageDimensions = image.Width * image.Height;
				if (imageDimensions >= largestDimensions) {
					finalLargestImages.Add(image);
				}
			}

			return finalLargestImages;
		}

		public ImageWithDimensions GetFirstLargestImage(List<ImageWithDimensions> images) {
			int largestDimensions = 0;
			List<ImageWithDimensions> largestImages = new List<ImageWithDimensions>();
			ImageWithDimensions finalLargestImage = new ImageWithDimensions();

			foreach (ImageWithDimensions image in images) {
				int imageDimensions = image.Width * image.Height;
				if (imageDimensions >= largestDimensions) {
					largestDimensions = imageDimensions;
					largestImages.Add(image);
				}
			}

			foreach (ImageWithDimensions image in largestImages) {
				int imageDimensions = image.Width * image.Height;
				if (imageDimensions >= largestDimensions) {
					finalLargestImage = image;
					break;
				}
			}

			return finalLargestImage;
		}
	}
}
