using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouTubeAnnotations
{
    public class AnnotationInfo
    {
        public AnnotationInfo()
        {

        }
        public string Type { get; set; }
        public bool Targetable { get; set; }
        public int Transparency { get; set; }
        public int FontSize { get; set; }
        public int FontColor { get; set; }
        public int BackGroundColor { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public string Text { get; set; }
    }
}
