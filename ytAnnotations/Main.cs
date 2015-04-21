using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using System.IO;
using System.Reflection;
using System.Threading.Tasks;


using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
//using Google.Apis.YouTube.v3.Data;
using System.Xml;
using Google.YouTube;
using Google.GData.Client;

namespace ytAnnotations
{
    public partial class Main : Form
    {
        
        string RedirectURIs = "urn:ietf:wg:oauth:2.0:oob";
        string developerKey = "AI39si7b0mdxvmauCnJDjsxsyiBYpWstXFC38n9qbr-7nGoEC5zQXaiDdVzuH0qmhYriNJ64FYsTQCOkXFUBYGKVF6AIAhZ9kw";
        UserCredential credential;

        public Main()
        {
            InitializeComponent();
            //Feed<Video> videoFeed = request.GetVideoFeed("beukerful");
            
            
        }
        private void pbAddChannel_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.ShowDialog();
            if(log.credential == null)
            {
                return;
            }
            credential = log.credential;
            MessageBox.Show(credential.Token.AccessToken);

            WebBrowser wb = new WebBrowser();
            wb.Navigate("", "_self", Encoding.ASCII.GetBytes(""), "Content-Type: application/x-www-form-urlencoded");


            YouTubeRequestSettings s = new YouTubeRequestSettings("ytAnnotations", developerKey);
            YouTubeRequest request = new YouTubeRequest(s);
            Feed<Video> videoFeed = request.GetVideoFeed("beukerful");

            foreach (var item in videoFeed.Entries)
            {
                MessageBox.Show(item.Title);
            }
            
        }

        private async Task Run()
        {
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                              new ClientSecrets { ClientId = "357960218741-1t1f7s2dhm99ps0dsk9datos50b1ia1o.apps.googleusercontent.com", ClientSecret = "8zJU_MJrNLu0ED-REQdHnUg8" },
                              new[] { YouTubeService.Scope.Youtube },
                              "user",
                              CancellationToken.None,
                              new FileDataStore(this.GetType().ToString())).Result;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = "dota"; // Replace with your search term.
            searchListRequest.MaxResults = 50;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }
            }

            foreach (var item in videos)
            {
                MessageBox.Show(item);
            }
        }
    }
}
