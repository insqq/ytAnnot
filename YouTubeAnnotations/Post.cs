using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml.Linq;

namespace YouTubeAnnotations
{
    public partial class Post : UserControl
    {
        public string Text { get; set; }
        public string link { get; set; }
        public DateTime date { get; set; }

        public Post(string _Text, string _link, DateTime _date, XElement attach)
        {
            InitializeComponent();
            Text = _Text.Replace("<br>", Environment.NewLine);
            link = _link;
            date = _date;
            bool pic = false;
            if (attach != null)
            {
                foreach (XElement item in attach.Elements())
                {
                    string type = item.Element("type").Value.ToString();
                    if (type == "photo" && !pic)
                    {
                        PictureBox pb = new PictureBox();
                        this.Controls.Add(pb);
                        pb.Dock = DockStyle.Top;
                        pb.SizeMode = PictureBoxSizeMode.AutoSize;
                        pb.BringToFront();
                        pb.Load(item.Element("photo").Element("src_big").Value.ToString());
                        pic = true;
                    }
                    if (type == "link")
                    {
                        LinkLabel url = new LinkLabel();
                        this.Controls.Add(url);
                        url.Dock = DockStyle.Top;
                        url.BringToFront();
                        url.Text = item.Element("link").Element("url").Value.ToString();
                        url.AutoSize = true;
                        url.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5BB1E6");
                        url.Click += (s, ev) => { Process.Start(url.Text); };
                    }
                }
            }

            lblContent.Text = Text;
            lblTime.Text = date.ToString();
            lblTime.BringToFront();
            label1.BringToFront();
        }

        private void lblTime_Click(object sender, EventArgs e)
        {
            Process.Start(link);
        }

        private void lblTime_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        private void lblTime_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Hand;
        }
    }
}
