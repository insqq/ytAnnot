using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Runtime.InteropServices;

namespace YouTubeAnnotations
{
    public class Browser
    {
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        public string DocumentText = "";
        Uri u;
        CookieContainer cookie = new CookieContainer();
        public Browser()
        {

        }

        public void setLastCookie()
        {
            foreach (Cookie item in cookie.GetCookies(u))
            {
                InternetSetCookie(u.AbsoluteUri, item.Name, item.Value);
            }
        }

        public string Navigate(string url)
        {
            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(url);
            rq.CookieContainer = cookie;
            using (HttpWebResponse response = (HttpWebResponse)rq.GetResponse())
            {
                DocumentText = new StreamReader(response.GetResponseStream()).ReadToEnd();
                //File.WriteAllBytes("1.html", Encoding.UTF8.GetBytes(respString.ToArray()));
                u = response.ResponseUri;
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
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                DocumentText = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            
            //File.WriteAllBytes("1.html", Encoding.UTF8.GetBytes(respString.ToArray()));
            return DocumentText;
        }

        public string NavigateJsonPost(string url, string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            var data = Encoding.UTF8.GetBytes(postData);

            request.CookieContainer = cookie;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                DocumentText = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            //File.WriteAllBytes("1.html", Encoding.UTF8.GetBytes(respString.ToArray()));
            return DocumentText;
        }

        public string NavigateMultipart(string url, string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            var data = Encoding.UTF8.GetBytes(postData);

            request.CookieContainer = cookie;
            request.Method = "POST";
            request.ContentType = "multipart/form-data; " + "boundary=---------------------------17101148691947789575814176401";
            request.ContentLength = data.Length;
            
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                DocumentText = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            //File.WriteAllBytes("1.html", Encoding.UTF8.GetBytes(respString.ToArray()));
            return DocumentText;
        }

        public string NavigateToFile(string url, string postData)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            var data = Encoding.ASCII.GetBytes(postData);

            request.CookieContainer = cookie;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.Accept = "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01";
            request.KeepAlive = true;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            Stream myStream = response.GetResponseStream();
            File.WriteAllBytes("D:/3.html", ReadFully(myStream));

            //File.WriteAllBytes("1.html", Encoding.UTF8.GetBytes(respString.ToArray()));
            return "D:/3.html";
        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public string NavigateJson(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);


            request.CookieContainer = cookie;
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";

            var response = (HttpWebResponse)request.GetResponse();
            DocumentText = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //File.WriteAllBytes("1.html", Encoding.UTF8.GetBytes(respString.ToArray()));
            return DocumentText;
        }
    }
}
