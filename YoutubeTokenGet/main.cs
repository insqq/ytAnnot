using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace YoutubeTokenGet
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            string Url = "https://accounts.google.com/o/oauth2/auth?access_type=offline&response_type=code&client_id=357960218741-elgp2dlo51001lqk6fu3riuq7pib97d2.apps.googleusercontent.com&redirect_uri=http%3A%2F%2Flocalhost%3A57589%2Fauthorize%2F&scope=https%3A%2F%2Fwww.googleapis.com%2Fauth%2Fyoutube&pageId=none";

            HttpWebRequest request = HttpWebRequest.Create(Url) as HttpWebRequest;
            string result = null;
            request.Method = "POST";
            request.KeepAlive = true;
            request.ContentType = "application/x-www-form-urlencoded";

            using (WebResponse response = request.GetResponse())
            {
                var sr = new StreamReader(response.GetResponseStream());
                result = sr.ReadToEnd();
                sr.Close();
            }

            MessageBox.Show(result);
        }

        private static string _GoogleClientId = "357960218741-elgp2dlo51001lqk6fu3riuq7pib97d2.apps.googleusercontent.com";
        private static string _GoogleSecret = "61q7mVh5wPeSuss0dqN3J9D8";
    }
}
