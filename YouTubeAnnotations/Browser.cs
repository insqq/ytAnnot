using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace YouTubeAnnotations
{
    public class Browser
    {
        public string DocumentText = "";
        CookieContainer cookie = new CookieContainer();
        public Browser()
        {

        }

        public string Navigate(string url)
        {
            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(url);
            rq.CookieContainer = cookie;
            using (HttpWebResponse response = (HttpWebResponse)rq.GetResponse())
            {
                DocumentText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //File.WriteAllBytes("1.html", Encoding.UTF8.GetBytes(respString.ToArray()));
            }
            return DocumentText;
        }

        public string Navigate(string url, string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            var data = Encoding.ASCII.GetBytes(postData);

            request.CookieContainer = cookie;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            DocumentText = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //File.WriteAllBytes("1.html", Encoding.UTF8.GetBytes(respString.ToArray()));
            return DocumentText;
        }
    }
}
