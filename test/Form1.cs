using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Google.GData.Client;
using Google.GData.Extensions.MediaRss;
using Google.GData.YouTube;
using Google.YouTube;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            settings = new YouTubeRequestSettings(
                "ytAnnotations",
                "AI39si7b0mdxvmauCnJDjsxsyiBYpWstXFC38n9qbr-7nGoEC5zQXaiDdVzuH0qmhYriNJ64FYsTQCOkXFUBYGKVF6AIAhZ9kw",
                "beuker93@gmail.com",
                "cdzncsy1");
            uploader = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            uploader.DoWork += UploaderDoWork;
            uploader.RunWorkerCompleted += delegate { MessageBox.Show("Upload completed!"); };
            uploader.RunWorkerAsync();
            MessageBox.Show("Initiated upload...");
        }

        private static BackgroundWorker uploader;

        private static YouTubeRequestSettings settings;

        static void UploaderDoWork(object sender, DoWorkEventArgs e)
        {
            var request = new YouTubeRequest(settings);
            Feed<Video> videoFeed = request.GetVideoFeed("beukerful");

            foreach (Video entry in videoFeed.Entries)
            {
                ListViewItem lvi = new ListViewItem(entry.Title);
                //listView1.Items.Add(lvi);
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
