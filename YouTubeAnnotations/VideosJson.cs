using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouTubeAnnotations
{
    public class VideosJson
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public string prevPageToken { get; set; }
        public pInfo pageInfo { get; set; }
        public struct pInfo
        {
            public int totalResults { get; set; }
            public int resultsPerPage { get; set; }
        }
        public List<itms> items { get; set; }

        public class itms
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public snpt snippet { get; set; }
            public struct snpt
            {
                public string publishedAt { get; set; }
                public string channelId { get; set; }
                public string title { get; set; }
                public string description { get; set; }
                public thmb thumbnails { get; set; }
                public struct thmb
                {
                    public dflt Default { get; set; }
                    public struct dflt
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                    public mdm medium { get; set; }
                    public struct mdm
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                    public hght hight { get; set; }
                    public struct hght
                    {
                        public string url { get; set; }
                        public int width { get; set; }
                        public int height { get; set; }
                    }
                }
                public string channelTitle { get; set; }
                public string playlistId { get; set; }
                public int position { get; set; }
                public resId resourceId { get; set; }
                public struct resId
                {
                    public string kind { get; set; }
                    public string videoId { get; set; }
                }

            }

        }
    }
}
