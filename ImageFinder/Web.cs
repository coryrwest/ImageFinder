using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImageFinder {
	public class WebHelpers {
		public string GetSourceOfURL(string Url) {

			HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url);
			myRequest.Method = "GET";
			WebResponse myResponse = myRequest.GetResponse();
			StreamReader sr = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);
			string result = sr.ReadToEnd();
			sr.Close();
			myResponse.Close();

			return result;
		}
	}
}
