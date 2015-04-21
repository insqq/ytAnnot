using Google.GData.Client;
using Google.YouTube;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Threading;

namespace YouTubeAnnotations
{
    public partial class Main : Form
    {
        XDocument doc;
        string developerKey = "AI39si7b0mdxvmauCnJDjsxsyiBYpWstXFC38n9qbr-7nGoEC5zQXaiDdVzuH0qmhYriNJ64FYsTQCOkXFUBYGKVF6AIAhZ9kw";
        string youtubeLoginUrl = "https://accounts.google.com/ServiceLogin?continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252F%26action_handle_signin%3Dtrue%26feature%3Dsign_in_button%26hl%3Dru%26app%3Ddesktop&passive=true&uilel=3&hl=ru&service=youtube";
        WebBrowser wb = new WebBrowser();


        public Main()
        {
            InitializeComponent();
            if (TemplateFileExist())
            {
                doc = XDocument.Load("Templates.xml");
            }
            else
            {
                doc = new XDocument();
                doc.Add(new XElement("Templates"));
                doc.Save("Templates.xml");
            }
            foreach (XElement item in doc.Element("Templates").Elements())
            {
                cbAnnotationName.Items.Add(item.Name);
            }
            // check user is logined, configure webbrowser
            wb.ScriptErrorsSuppressed = true;
            btnLogout_Click((object)wb, new EventArgs());
            wb.Dock = DockStyle.Fill;
            tabPage3.Controls.Add(wb);
        }

        #region Page One

        private void btnClearCache_Click(object sender, EventArgs e)
        {
            Process.Start("cmd.exe", "/C RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 255");
            setLogouted();
        }

        private void dgvMain_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if ((dgvMain.Rows[0].Cells[0] as DataGridViewCheckBoxCell).Value.ToString() == "False")
                {
                    foreach (DataGridViewRow row in dgvMain.Rows)
                    {
                        (row.Cells[0] as DataGridViewCheckBoxCell).Value = true;
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dgvMain.Rows)
                    {
                        (row.Cells[0] as DataGridViewCheckBoxCell).Value = false;
                    }
                }
            }
        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                Process.Start(dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            setWaitState();
            wb.Navigate("https://youtube.com/logout");
            while (wb.ReadyState != WebBrowserReadyState.Complete)
                System.Windows.Forms.Application.DoEvents();
            lblStatus.Text = "";
            setLogouted();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblLoginingStatus.Text = "loading";
            // go to login page
            wb.Navigate(youtubeLoginUrl);
            while (wb.ReadyState != WebBrowserReadyState.Complete)
                System.Windows.Forms.Application.DoEvents();
            string page = wb.DocumentText;

            // create post data
            page = Regex.Replace(page, "" + (char)10, "");
            string data = "";
            foreach (Match input in Regex.Matches(page, "<input(.*?)>"))
            {
                string tmp = input.Value;
                string name = Regex.Match(tmp, "name=\"(.*?)\"").Groups[1].Value;
                data += name + "=";
                if (name == "Email")
                {
                    data += tbLogin.Text + "&";
                    continue;
                }
                if (name == "Passwd")
                {
                    data += tbPw.Text + "&";
                    continue;
                }
                data += Regex.Match(tmp, "value=\"(.*?)\"").Groups[1].Value + "&";
            }
            data = data.Remove(data.Length - 1, 1);
            data = data.Replace("amp;", "");

            // login
            wb.Navigate(youtubeLoginUrl, "_self", Encoding.ASCII.GetBytes(data), "Content-Type: application/x-www-form-urlencoded");
            while (wb.ReadyState != WebBrowserReadyState.Complete)
                System.Windows.Forms.Application.DoEvents();
            page = wb.DocumentText;

            // get channel name
            string channelName = Regex.Match(page, "<div class=\"yt-masthead-picker-name\" dir=\"ltr\">(.*?)</div>").Groups[1].Value;
            if (channelName == "")
            {
                setLogouted();
                lblStatus.Text = "Incorrect email or password\n or try to clear your cache";
                lblLoginingStatus.Text = "";
                return;
            }
            lblStatus.Text = "channel: " + channelName;
            setLogined();

            Thread t = new Thread(setVideoInformation);
            t.Start();
        }

        #region func helpers

        void setVideoInformation()
        {
            YouTubeRequestSettings settings = new YouTubeRequestSettings("ytAnnotations",
            developerKey,
            tbLogin.Text, tbPw.Text);
            YouTubeRequest request = new YouTubeRequest(settings);

            string feedUrl = "https://gdata.youtube.com/feeds/api/users/default/uploads/?start-index={0}&max-results=50";
            Feed<Video> videoFeed = request.Get<Video>(new Uri(string.Format(feedUrl, 1)));
            int i = 0;
            while (videoFeed.Entries.Count() != 0)
            {
                foreach (Video video in videoFeed.Entries)
                {
                    dgvMain.Invoke(new Action(() => dgvMain.Rows.Add()));
                    DataGridViewRow dgvR = dgvMain.Rows[dgvMain.Rows.Count - 1];
                    dgvMain.Invoke(new Action(() => (dgvMain.Rows[dgvMain.Rows.Count - 1].Cells[0] as DataGridViewCheckBoxCell).Value = false));
                    string path = video.WatchPage.AbsoluteUri.Remove(video.WatchPage.AbsoluteUri.IndexOf('&'));
                    dgvMain.Invoke(new Action(() => dgvMain.Rows[dgvMain.Rows.Count - 1].Cells[1].Value = path));
                    dgvMain.Invoke(new Action(() => dgvMain.Rows[dgvMain.Rows.Count - 1].Cells[2].Value = video.Title));
                    TimeSpan ts = TimeSpan.FromSeconds(Convert.ToDouble(video.Media.Duration.Seconds));
                    dgvMain.Invoke(new Action(() => dgvMain.Rows[dgvMain.Rows.Count - 1].Cells[3].Value = ts.ToString()));
                    dgvMain.Invoke(new Action(() => dgvMain.Rows[dgvMain.Rows.Count - 1].Cells[4].Value = video.ViewCount));
                    dgvMain.Invoke(new Action(() => dgvMain.Rows[dgvMain.Rows.Count - 1].Cells[5].Value = video.CommmentCount));
                }
                i += 50;
                videoFeed = request.Get<Video>(new Uri(string.Format(feedUrl, i)));
            }
            lblLoginingStatus.Invoke(new Action(() => lblLoginingStatus.Text = "loaded."));
        }

        bool TemplateFileExist()
        {
            string fname = Directory.GetFiles(Directory.GetCurrentDirectory()).Where(s =>
            {
                string tmp = "";
                string[] tmpArr = s.Split('\\');
                tmp = tmpArr.Where(s1 => s1 == "Templates.xml").SingleOrDefault();
                if (tmp == "Templates.xml") return true;
                return false;
            }
                ).SingleOrDefault();
            return !(fname == null);
        }

        string getUserName()
        {
            string userID = Regex.Match(wb.DocumentText, "href=\"/channel/(.*?)\"").Groups[1].Value;

            wb.Navigate("https://youtube.com/channel/" + userID);
            while (wb.ReadyState != WebBrowserReadyState.Complete)
                System.Windows.Forms.Application.DoEvents();

            return Regex.Match(wb.DocumentText, "href=\"https://www.youtube.com/user/(.*?)\"").Groups[1].Value;
        }

        void setLogined()
        {
            btnLogin.Enabled = false;
            btnLogout.Enabled = true;
        }

        void setLogouted()
        {
            btnLogin.Enabled = true;
            btnLogout.Enabled = false;
            lblStatus.Text = "";
            dgvMain.Rows.Clear();
        }

        void setWaitState()
        {
            btnLogin.Enabled = false;
            btnLogout.Enabled = false;
        }

        bool logined()
        {
            wb.Navigate("https://youtube.com");
            while (wb.ReadyState != WebBrowserReadyState.Complete)
                System.Windows.Forms.Application.DoEvents();
            int auth = Regex.Matches(wb.DocumentText, "<div class=\"yt-masthead-picker-name\" dir=\"ltr\">(.*?)</div>").Count;
            return auth != 0;
        }

        #endregion

        #endregion

        #region Page Two

        bool cancelAction = false;

        private void cbAnnotationName_SelectedIndexChanged(object sender, EventArgs e)
        {
            XElement selectedXE = doc.Element("Templates").Element(cbAnnotationName.SelectedItem.ToString()).Element("document");
            try
            {
                cbAnnotationType.Text = selectedXE.Element("updatedItems").Element("annotation").Attribute("style").Value;
            }
            catch (NullReferenceException)
            {
                cbAnnotationType.Text = "";
            }

            try
            {
                string targetable = selectedXE.Element("updatedItems").Element("annotation").Element("appearance").Attribute("effects").Value;
                chbTargetable.Checked = targetable != "";
            }
            catch (NullReferenceException)
            {
                chbTargetable.Checked = false;
            }

            try
            {
                cbTextSize.Text = selectedXE.Element("updatedItems").Element("annotation").Element("appearance").Attribute("textSize").Value;
            }
            catch (NullReferenceException)
            {
                cbTextSize.Text = "";
            }

            try
            {
                pAnnotation.ForeColor = Color.FromArgb(Convert.ToInt32(selectedXE.Element("updatedItems").Element("annotation").Element("appearance").Attribute("fgColor").Value));
            }
            catch (NullReferenceException)
            {
                pAnnotation.ForeColor = Color.Black;
            }

            try
            {
                pAnnotation.BackColor = Color.FromArgb(Convert.ToInt32(selectedXE.Element("updatedItems").Element("annotation").Element("appearance").Attribute("bgColor").Value));
            }
            catch (NullReferenceException)
            {
                pAnnotation.BackColor = Color.Yellow;
            }

            try
            {
                XElement[] xe = selectedXE.Element("updatedItems").Element("annotation").Element("segment").Element("movingRegion").Elements().ToArray();
                dtpStartTime.Value = Convert.ToDateTime(xe[0].Attribute("t").Value);
                pAnnotation.Top = Convert.ToInt32(xe[0].Attribute("y").Value) * 3;
                pAnnotation.Left = Convert.ToInt32(xe[0].Attribute("x").Value) * 5;
                pAnnotation.Width = Convert.ToInt32(xe[0].Attribute("w").Value) * 5;
                pAnnotation.Height = Convert.ToInt32(xe[0].Attribute("h").Value) * 3;

                dtpEndTime.Value = Convert.ToDateTime(xe[1].Attribute("t").Value);
            }
            catch (NullReferenceException)
            {
                dtpStartTime.Value = Convert.ToDateTime("00:00:00");
                pAnnotation.Top = 32;
                pAnnotation.Left = 32;
                pAnnotation.Width = 230;
                pAnnotation.Height = 45;

                dtpEndTime.Value = Convert.ToDateTime("00:00:00");
            }

            try
            {
                rtbAnnotation.Text = selectedXE.Element("updatedItems").Element("annotation").Element("TEXT").Value;
            }
            catch (NullReferenceException)
            {
                rtbAnnotation.Text = "";
            }

            try
            {
                chbReference.Checked = true;
                rtbReference.Text = selectedXE.Element("updatedItems").Element("annotation").Element("action").Element("url").Attribute("value").Value;
            }
            catch (NullReferenceException)
            {
                chbReference.Checked = false;
                rtbReference.Text = "";
            }


        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string id = "";
            try
            {
                id = doc.Element("Templates").Element(cbAnnotationName.SelectedItem.ToString()).Element("document").Element("updatedItems").Element("annotation").Attribute("id").Value;
            }
            catch
            {
                id = "";
            }
            doc.Element("Templates").Element(cbAnnotationName.SelectedItem.ToString()).Remove();
            doc.Element("Templates").Add(new XElement(cbAnnotationName.SelectedItem.ToString()));
            XElement selected = doc.Element("Templates").Element(cbAnnotationName.SelectedItem.ToString());
            selected.Add(new XElement("document"));
            selected = selected.Element("document");

            XElement reqHeader = new XElement("requestHeader");
            reqHeader.Add(new XAttribute("video_id", ""));
            selected.Add(reqHeader);

            XElement authHeader = new XElement("authenticationHeader");
            authHeader.Add(new XAttribute("auth_token", ""));
            authHeader.Add(new XAttribute("video_id", ""));
            selected.Add(authHeader);

            XElement updatedItems = new XElement("updatedItems");
            selected.Add(updatedItems);
            {
                XElement annotation = new XElement("annotation");
                annotation.Add(new XAttribute("author", ""));

                annotation.Add(new XAttribute("id", id));
                annotation.Add(new XAttribute("type", "text"));
                string anotType = cbAnnotationType.SelectedItem == null ? "popup" : cbAnnotationType.SelectedItem.ToString();
                annotation.Add(new XAttribute("style", anotType));
                selected.Element("updatedItems").Add(annotation);
                {
                    //appearance
                    XElement appearance = new XElement("appearance");
                    appearance.Add(new XAttribute("fgColor", lblAnnotationText.ForeColor.ToArgb()));
                    appearance.Add(new XAttribute("bgColor", pAnnotation.BackColor.ToArgb()));
                    appearance.Add(new XAttribute("bgAlpha", "0.6"));
                    appearance.Add(new XAttribute("highlightFontColor", lblAnnotationText.ForeColor.ToArgb()));
                    appearance.Add(new XAttribute("textSize", cbTextSize.SelectedItem == null ? "5" : cbTextSize.SelectedItem.ToString()));
                    appearance.Add(new XAttribute("effects", chbTargetable.Checked ? "bevel dropshadow textdropshadow" : ""));
                    annotation.Add(appearance);
                }
                {
                    //segment
                    XElement segment = new XElement("segment");
                    {
                        XElement movingRegion = new XElement("movingRegion");
                        movingRegion.Add(new XAttribute("type", "rect"));
                        movingRegion.Add(new XAttribute("pattern", "fixed"));
                        {
                            XElement rectRegion1 = new XElement("rectRegion");
                            rectRegion1.Add(new XAttribute("t", dtpStartTime.Value.ToString("HH:mm:ss"))); // need to edit time
                            rectRegion1.Add(new XAttribute("d", "0"));
                            rectRegion1.Add(new XAttribute("x", pAnnotation.Left / 5));
                            rectRegion1.Add(new XAttribute("y", pAnnotation.Top / 3));
                            rectRegion1.Add(new XAttribute("w", pAnnotation.Width / 5));
                            rectRegion1.Add(new XAttribute("h", pAnnotation.Height / 3));
                            movingRegion.Add(rectRegion1);

                            XElement rectRegion2 = new XElement("rectRegion");
                            rectRegion2.Add(new XAttribute("t", dtpEndTime.Value.ToString("HH:mm:ss"))); // need to edit time
                            rectRegion2.Add(new XAttribute("d", "0"));
                            rectRegion2.Add(new XAttribute("x", pAnnotation.Left / 5));
                            rectRegion2.Add(new XAttribute("y", pAnnotation.Top / 3));
                            rectRegion2.Add(new XAttribute("w", pAnnotation.Width / 5));
                            rectRegion2.Add(new XAttribute("h", pAnnotation.Height / 3));
                            movingRegion.Add(rectRegion2);
                        }
                        segment.Add(movingRegion);
                    }
                    annotation.Add(segment);
                }
                {
                    //text
                    XElement text = new XElement("TEXT", rtbAnnotation.Text);
                    annotation.Add(text);
                }
                if (chbReference.Checked)
                {
                    //action
                    XElement action = new XElement("action");
                    action.Add(new XAttribute("type", "openUrl"));
                    action.Add(new XAttribute("trigger", "click"));
                    XElement url = new XElement("url");
                    url.Add(new XAttribute("value", rtbReference.Text));
                    url.Add(new XAttribute("target", "new"));
                    action.Add(url);
                    annotation.Add(action);
                }
            }
            doc.Save("Templates.xml");
        }

        private void btnAddTemplate_Click(object sender, EventArgs e)
        {
            bool flag = true;
            foreach (var item in cbAnnotationName.Items)
            {
                if (item.ToString() == tbNewTemaplate.Text) flag = false;
            }
            if (flag)
            {
                if (tbNewTemaplate.Text == "") return;
                doc.Element("Templates").Add(new XElement(tbNewTemaplate.Text));
                cbAnnotationName.Items.Add(tbNewTemaplate.Text);
                cbAnnotationName.SelectedIndex = cbAnnotationName.Items.IndexOf(tbNewTemaplate.Text);
                doc.Save("Templates.xml");
                btnEdit.PerformClick();
                Random rand = new Random();
                string id = rand.Next(10000, 99999).ToString();
                id += rand.Next(10000, 99999).ToString();
                doc.Element("Templates").Element(tbNewTemaplate.Text).Element("document").Element("updatedItems").Element("annotation").Attribute("id").Value = id;
                tbNewTemaplate.Text = "";
                doc.Save("Templates.xml");
            }
            else MessageBox.Show("Such template allready exist");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            doc.Element("Templates").Element(cbAnnotationName.SelectedItem.ToString()).Remove();
            cbAnnotationName.Items.RemoveAt(cbAnnotationName.SelectedIndex);
            doc.Save("Templates.xml");
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            lblStatusBar.Text = "Preparing...";
            if (cbAnnotationName.SelectedItem == null)
            {
                MessageBox.Show("Select template first");
                lblStatusBar.Text = "fail";
                return;
            }

            XElement annot = doc.Element("Templates").Element(cbAnnotationName.SelectedItem.ToString()).Element("document");

            int fgColor = Convert.ToInt32(annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("fgColor").Value);
            int bgColor = Convert.ToInt32(annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("bgColor").Value);
            int highlightFontColor = Convert.ToInt32(annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("highlightFontColor").Value);
            annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("fgColor").Value = (fgColor & 0x00FFFFFF).ToString();
            annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("bgColor").Value = (bgColor & 0x00FFFFFF).ToString();
            annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("highlightFontColor").Value = (highlightFontColor & 0x00FFFFFF).ToString();

            List<List<string>> videos = new List<List<string>>();

            int k = 0;
            foreach (DataGridViewRow item in dgvMain.Rows)
            {
                if(cancelAction)
                {
                    cancelAction = false;
                    lblStatusBar.Text = "canceled";
                    return;
                }
                if ((item.Cells[0] as DataGridViewCheckBoxCell).Value.ToString() == "True")
                {
                    videos.Add(new List<string>());
                    videos[k].Add(item.Cells[1].Value.ToString().Replace("https://www.youtube.com/watch?v=", ""));
                    videos[k].Add(item.Cells[2].Value.ToString());
                    k++;
                }
            }

            for (int i = 0; i < videos.Count; i++)
            {
                if (cancelAction)
                {
                    cancelAction = false;
                    lblStatusBar.Text = "canceled";
                    return;
                }
                lblStatusBar.Text = (i + 1) + "/" + videos.Count + " :" + videos[i][1];
                wb.Navigate("https://www.youtube.com/my_videos_annotate?video_referrer=watch&v=" + videos[i][0]);
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                    System.Windows.Forms.Application.DoEvents();
                string authToken = Regex.Match(wb.DocumentText, "\"auth_token\":\"(.*?)\"").Groups[1].Value;

                annot.Element("requestHeader").Attribute("video_id").Value = videos[i][0];
                annot.Element("authenticationHeader").Attribute("auth_token").Value = authToken;
                annot.Element("authenticationHeader").Attribute("video_id").Value = videos[i][0];
                string data = annot.ToString();

                wb.Navigate("https://www.youtube.com/annotations_auth/update2", "_self", Encoding.UTF8.GetBytes(data), "Content-Type: application/x-www-form-urlencoded");
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                    System.Windows.Forms.Application.DoEvents();

                string anotPublisData = string.Format("<document>\n  <requestHeader video_id=\"{0}\"/>\n  <authenticationHeader auth_token=\"{1}\" video_id=\"{0}\"/>\n</document>", videos[i][0], authToken);
                wb.Navigate("https://www.youtube.com/annotations_auth/publish2", "_self", Encoding.UTF8.GetBytes(anotPublisData), "Content-Type: application/x-www-form-urlencoded");
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                    System.Windows.Forms.Application.DoEvents();
            }
            lblStatusBar.Text = "Complete";

        }

        private void btnDeleteFromSelected_Click(object sender, EventArgs e)
        {
            string sToSend = "<document>\n  <requestHeader video_id=\"{0}\"/>\n  <authenticationHeader video_id=\"{0}\" auth_token=\"{1}\"/>\n  <deletedItems>\n    <deletedItem id=\"{2}\" author=\"\"/>\n  </deletedItems>\n</document>";
            if (cbAnnotationName.SelectedItem == null)
            {
                MessageBox.Show("Select template first");
                return;
            }

            List<List<string>> videos = new List<List<string>>();

            int k = 0;
            foreach (DataGridViewRow item in dgvMain.Rows)
            {
                if (cancelAction)
                {
                    cancelAction = false;
                    lblStatusBar.Text = "canceled";
                    return;
                }
                if ((item.Cells[0] as DataGridViewCheckBoxCell).Value.ToString() == "True")
                {
                    videos.Add(new List<string>());
                    videos[k].Add(item.Cells[1].Value.ToString().Replace("https://www.youtube.com/watch?v=", ""));
                    videos[k].Add(item.Cells[2].Value.ToString());
                    k++;
                }
            }
            cbAnnotationName.Enabled = false;


            for (int i = 0; i < videos.Count; i++)
            {
                if (cancelAction)
                {
                    cancelAction = false;
                    lblStatusBar.Text = "canceled";
                    return;
                }
                lblStatusBar.Text = (i + 1) + "/" + videos.Count + " :" + videos[i][1];
                string videoID = videos[i][0].Replace("https://www.youtube.com/watch?v=", "");
                wb.Navigate("https://www.youtube.com/my_videos_annotate?video_referrer=watch&v=" + videoID);
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                    System.Windows.Forms.Application.DoEvents();
                string authToken = Regex.Match(wb.DocumentText, "\"auth_token\":\"(.*?)\"").Groups[1].Value;

                string anotID = doc.Element("Templates").Element(cbAnnotationName.SelectedItem.ToString()).Element("document").Element("updatedItems").Element("annotation").Attribute("id").Value;
                string data = string.Format(sToSend, videoID, authToken, anotID);

                wb.Navigate("https://www.youtube.com/annotations_auth/update2", "_self", Encoding.UTF8.GetBytes(data), "Content-Type: application/x-www-form-urlencoded");
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                    System.Windows.Forms.Application.DoEvents();
                string anotPublisData = string.Format("<document>\n  <requestHeader video_id=\"{0}\"/>\n  <authenticationHeader auth_token=\"{1}\" video_id=\"{0}\"/>\n</document>", videoID, authToken);
                wb.Navigate("https://www.youtube.com/annotations_auth/publish2", "_self", Encoding.UTF8.GetBytes(anotPublisData), "Content-Type: application/x-www-form-urlencoded");
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                    System.Windows.Forms.Application.DoEvents();
            }
            lblStatusBar.Text = "Complete";

            cbAnnotationName.Enabled = true;
        }

        #region Annotation Panel

        bool mouseResizeClicked = false;
        bool mouseReplaceClicked = false;

        private void picForResize_MouseDown(object sender, MouseEventArgs e)
        {
            mouseResizeClicked = true;
        }

        private void picForResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseResizeClicked)
            {
                this.pAnnotation.Height = picForResize.Top + e.Y;
                this.pAnnotation.Width = picForResize.Left + e.X;
                lblAnnotationText.MaximumSize = new Size(pAnnotation.Width - 15, 0);
            }
        }

        private void picForResize_MouseUp(object sender, MouseEventArgs e)
        {
            mouseResizeClicked = false;
        }

        private void btnTextColorPick_Click(object sender, EventArgs e)
        {
            if (cdFirst.ShowDialog() == DialogResult.OK)
            {
                lblAnnotationText.ForeColor = cdFirst.Color;
            }
        }

        private void btnForeColorPick_Click(object sender, EventArgs e)
        {
            if (cdFirst.ShowDialog() == DialogResult.OK)
            {
                pAnnotation.BackColor = cdFirst.Color;
            }
        }

        private void rtbAnnotation_TextChanged(object sender, EventArgs e)
        {
            lblAnnotationText.Text = rtbAnnotation.Text;
        }

        private void pbForReplace_MouseDown(object sender, MouseEventArgs e)
        {
            mouseReplaceClicked = true;
        }

        private void pbForReplace_MouseUp(object sender, MouseEventArgs e)
        {
            mouseReplaceClicked = false;
        }

        private void pbForReplace_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseReplaceClicked && pAnnotation.Top <= 0)
            {
                pAnnotation.Top = 5;
                mouseReplaceClicked = false;
            }
            if (mouseReplaceClicked && pAnnotation.Left < 0)
            {
                pAnnotation.Left = 5;
                mouseReplaceClicked = false;
            }
            if (mouseReplaceClicked && pAnnotation.Right > 500)
            {
                pAnnotation.Left = 495 - pAnnotation.Width;
                mouseReplaceClicked = false;
            }
            if (mouseReplaceClicked && pAnnotation.Bottom > 300)
            {
                pAnnotation.Top = 295 - pAnnotation.Height;
                mouseReplaceClicked = false;
            }

            if (mouseReplaceClicked)
            {
                this.pAnnotation.Top = pAnnotation.Top + e.Y;
                this.pAnnotation.Left = pAnnotation.Left + e.X;
            }
        }

        private void cbTextSize_SelectedValueChanged(object sender, EventArgs e)
        {
            lblAnnotationText.Font = new Font("Arial", (cbTextSize.SelectedIndex + 1) * 3);
        }


        #endregion

        private void btnStopAction_Click(object sender, EventArgs e)
        {
            cancelAction = true;
        }

        #endregion
        
    }
}
