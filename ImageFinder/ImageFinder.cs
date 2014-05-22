using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.IO;
using System.Drawing.Imaging;

namespace ImageFinder {
	public class ImageHelpers {
		public void SaveJpegToDisk(Image image, string fileName, string path) {
			image.Save(path + fileName, ImageFormat.Jpeg);
		}

		public List<ImageWithDimensions> FindImages(string HTML) {
			// Get img tags from HTML
			List<string> imgTags = GetImgTags(HTML);

			// Get img src from img tags
			List<string> imgURLS = GetImgURLS(imgTags);

			// Set up ImageWD Object list
			List<ImageWithDimensions> ImageWDs = new List<ImageWithDimensions>();
			foreach (var imgURL in imgURLS) {
				ImageWithDimensions image = new ImageWithDimensions();
				image = GetImageDimensions(imgURL);
				ImageWDs.Add(image);
			}

			return ImageWDs;
		}

		#region Private Methods
		/// <summary>
		/// Gets the img tags from a block of HTML
		/// </summary>
		/// <param name="HTML"></param>
		/// <returns></returns>
		private List<string> GetImgTags(string HTML) {
			List<string> imgs = new List<string>();

			while (HTML.IndexOf("<img") != -1) {
				int startIndex = HTML.IndexOf("<img");
				int endIndex = IndexAfter(HTML, startIndex, ">");
				string img = HTML.Substring(startIndex, endIndex - startIndex + 1);
				imgs.Add(img);
				HTML = HTML.Replace(img, "");
			}

			return imgs;
		}

		/// <summary>
		/// Gets the urls from a list of strings
		/// </summary>
		/// <param name="imgs"></param>
		/// <returns></returns>
		private List<string> GetImgURLS(List<string> imgs) {
			List<string> imgURLS = new List<string>();

			foreach (var img in imgs) {
				int startIndex = img.IndexOf("src=\"") + 5;
				int endIndex = IndexAfter(img, startIndex, "\"") - 1;
				string imgURL = img.Substring(startIndex, endIndex - startIndex + 1);
				// Check for HTTP
				if (imgURL.Contains("http")) {
					if (!imgURL.Contains("?")) {
						imgURLS.Add(imgURL);
					}
				}
			}

			return imgURLS;
		}

		/// <summary>
		/// Downloads the image and gets the dimensions
		/// </summary>
		/// <param name="imageURL"></param>
		/// <returns></returns>
		private ImageWithDimensions GetImageDimensions(string imageURL) {
			ImageWithDimensions imageWD = new ImageWithDimensions();
			imageWD.URL = imageURL;
			Image tempImg = DownloadImage(imageURL);
			var stream = new System.IO.MemoryStream();
			tempImg.Save(stream, ImageFormat.Jpeg);

			using (Image jpg = Image.FromStream(stream: stream, useEmbeddedColorManagement: false, validateImageData: false)) {
				imageWD.Width = Convert.ToInt32(jpg.PhysicalDimension.Width);
				imageWD.Height = Convert.ToInt32(jpg.PhysicalDimension.Height);
			}

			imageWD.Image = tempImg;

			return imageWD;
		}

		/// <summary>
		/// Downloads image from url and returns it
		/// </summary>
		/// <param name="imageURL"></param>
		/// <returns></returns>
		private Image DownloadImage(string imageURL) {
			HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(imageURL);
			HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
			Stream stream = httpWebReponse.GetResponseStream();
			return Image.FromStream(stream);
		}

		private int IndexAfter(string text, int startIndex, string match) {
			text = text.Substring(startIndex);
			return text.IndexOf(match) + startIndex;
		}
		#endregion
	}

	public class ImageWithDimensions {
		public string URL { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }
		public Image Image { get; set; }
	}
}
