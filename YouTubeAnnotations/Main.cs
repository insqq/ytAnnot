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
using Newtonsoft.Json;
using System.Runtime;
using System.Web;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

namespace YouTubeAnnotations
{
    public partial class Main : Form
    {

        XDocument doc;
        string developerKey = "AI39si7b0mdxvmauCnJDjsxsyiBYpWstXFC38n9qbr-7nGoEC5zQXaiDdVzuH0qmhYriNJ64FYsTQCOkXFUBYGKVF6AIAhZ9kw";
        string youtubeLoginUrl = "https://accounts.google.com/ServiceLogin?continue=https%3A%2F%2Fwww.youtube.com%2Fsignin%3Fnext%3D%252F%26action_handle_signin%3Dtrue%26feature%3Dsign_in_button%26hl%3Dru%26app%3Ddesktop&passive=true&uilel=3&hl=ru&service=youtube";
        Browser wb = new Browser();
        List<Thread> tGetVideoInfo = new List<Thread>();
        bool LoadingVideosInfo = false;

        public Main()
        {
            InitializeComponent();

            TabPage testings = new TabPage("test browser");
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
            //wb.ScriptErrorsSuppressed = true;
            btnLogout_Click((object)wb, new EventArgs());





            //tbLogin.Text = "bivolghenadie@gmail.com";
            //tbPw.Text = "inguta81s";*/
            //tbLogin.Text = "beuker93@gmail.com";
            //tbPw.Text = "Cdzndeyl11";
            Thread t = new Thread(getFeed);
            t.Start();
        }

        string getAccessToken(string email, string pswd)
        {
            string page = wb.Navigate(youtubeLoginUrl);
            page = Regex.Replace(page, "" + (char)10, "");
            string data = "";
            foreach (Match input in Regex.Matches(page, "<input(.*?)>"))
            {
                string tmp = input.Value;
                string name = Regex.Match(tmp, "name=\"(.*?)\"").Groups[1].Value;
                data += name + "=";
                if (name == "Email")
                {
                    data += email + "&";
                    continue;
                }
                if (name == "Passwd")
                {
                    data += pswd + "&";
                    continue;
                }
                data += Regex.Match(tmp, "value=\"(.*?)\"").Groups[1].Value + "&";
            }
            data = data.Remove(data.Length - 1, 1);
            data = data.Replace("amp;", "");

            // login
            wb.Navigate(youtubeLoginUrl, data);

            // pick accounts
            page = wb.Navigate("https://accounts.google.com/o/oauth2/auth?client_id=357960218741-elgp2dlo51001lqk6fu3riuq7pib97d2.apps.googleusercontent.com&redirect_uri=urn:ietf:wg:oauth:2.0:oob&scope=https://www.googleapis.com/auth/youtube&response_type=code&access_type=offline");

            page = Regex.Replace(page, "" + (char)10, "");

            string liAccount = "<li id=\"account-(.*?)</li>";
            if (Regex.IsMatch(page, liAccount))
            {
                foreach (Match item in Regex.Matches(page, liAccount))
                {
                }
                Match q = Regex.Match(Regex.Match(page, liAccount).Groups[0].Value, "<a href=\"(.*?)\"");
                page = wb.Navigate(q.Groups[1].Value.Replace("amp;", ""));
                // end picking first account
                page = Regex.Replace(page, "" + (char)10, "");
            }

            string form = Regex.Match(page, "<form id=\"connect-approve\"(.*?)</form>").Groups[1].Value;
            data = "";
            foreach (Match input in Regex.Matches(form, "<input (.*?)>"))
            {
                string igv = input.Groups[1].Value;
                if (Regex.IsMatch(igv, "id=\"state_wrapper\""))
                {
                    data += Regex.Match(igv, "name=\"(.*?)\"").Groups[1].Value + "=";
                    data += Regex.Match(igv, "value=\"(.*?)\"").Groups[1].Value + "&";
                }
            }
            data += "submit_access=true";

            string url = Regex.Match(form, "action=\"(.*?)\"").Groups[1].Value.Replace("amp;", "");
            page = wb.Navigate(url, data);
            string code = Regex.Match(page, "<input(.*?)value=\"(.*?)\"").Groups[2].Value;
            string client_id = "357960218741-elgp2dlo51001lqk6fu3riuq7pib97d2.apps.googleusercontent.com";
            string client_secret = "61q7mVh5wPeSuss0dqN3J9D8";
            string redirect_url = "urn:ietf:wg:oauth:2.0:oob";

            data = string.Format("code={0}&client_id={1}&client_secret={2}&redirect_uri={3}&grant_type=authorization_code", code, client_id, client_secret, redirect_url);
            url = "https://accounts.google.com/o/oauth2/token";

            page = wb.Navigate(url, data);

            return (string)JObject.Parse(page)["access_token"];
        }

        #region Page Account

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                Process.Start(dgvMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoadingVideosInfo = false;
            setWaitState();

            wb.Navigate("https://youtube.com/logout");
            lblStatus.Text = "";
            setLogouted();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblLoginingStatus.Text = "loading";
            // go to login page
            string page = wb.Navigate(youtubeLoginUrl);

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
            page = wb.Navigate(youtubeLoginUrl, data);

            // get channel name
            string channelName = Regex.Match(page, "<div class=\"yt-masthead-picker-name\" dir=\"ltr\">(.*?)</div>").Groups[1].Value;
            if (channelName == "")
            {
                setLogouted();
                lblStatus.Text = "Incorrect email or password\n or try to login throught browser";
                lblLoginingStatus.Text = "";
                return;
            }
            setLogined();

            Thread loadVidThread = new Thread(() => setVideoInformation(getAccessToken(tbLogin.Text, tbPw.Text)));
            loadVidThread.Start();

        }

        #region func helpers

        void setVideoInformation(string token)
        {
            LoadingVideosInfo = true;
            foreach (string channel in getChannels(token))
            {
                string url = "https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId={0}&mine=true&access_token={1}&{2}";
                VideosJson videos = JsonConvert.DeserializeObject<VideosJson>(wb.NavigateJson(string.Format(url, channel, token, "")));
                string npt = null;
                do
                {
                    npt = videos.nextPageToken == null ? null : "pageToken=" + videos.nextPageToken;
                    foreach (VideosJson.itms video in videos.items)
                    {
                        if (!LoadingVideosInfo) return;
                        dgvMain.Invoke(new Action(() => dgvMain.Rows.Add()));
                        DataGridViewRow dgvR = dgvMain.Rows[dgvMain.Rows.Count - 1];
                        dgvMain.Invoke(new Action(() => (dgvMain.Rows[dgvMain.Rows.Count - 1].Cells[0] as DataGridViewCheckBoxCell).Value = false));
                        string path = "https://www.youtube.com/watch?v=" + video.snippet.resourceId.videoId;
                        dgvMain.Invoke(new Action(() => dgvMain.Rows[dgvMain.Rows.Count - 1].Cells[1].Value = path));
                        dgvMain.Invoke(new Action(() => dgvMain.Rows[dgvMain.Rows.Count - 1].Cells[2].Value = video.snippet.title));
                    }
                    videos = JsonConvert.DeserializeObject<VideosJson>(wb.NavigateJson(string.Format(url, channel, token, npt)));
                }
                while (npt != null);
                lblLoginingStatus.Invoke(new Action(() => lblLoginingStatus.Text = "loaded."));
            }
        }

        private List<string> getChannels(string token)
        {
            string channelsUrl = "https://www.googleapis.com/youtube/v3/channels?part=contentDetails&mine=true&access_token=" + token;
            return ((JArray)JObject.Parse(wb.NavigateJson(channelsUrl))["items"])
            .Select(jobj => (string)jobj["contentDetails"]["relatedPlaylists"]["uploads"])
            .ToList();
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

            return Regex.Match(wb.DocumentText, "href=\"https://www.youtube.com/user/(.*?)\"").Groups[1].Value;
        }

        void setLogined()
        {
            lblStatus.Text = "";
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
            int auth = Regex.Matches(wb.DocumentText, "<div class=\"yt-masthead-picker-name\" dir=\"ltr\">(.*?)</div>").Count;
            return auth != 0;
        }

        #endregion

        #endregion

        #region Page Templates

        bool cancelAction = false;

        private void btnDeleteCopyrightVideos_Click(object sender, EventArgs e)
        {
            ActionInProgress();

            Thread t = new Thread(() =>
            {
                wb.Navigate("https://www.youtube.com/my_videos_copyright");

                List<string> l = new List<string>();
                string token = Regex.Match(wb.DocumentText, "'XSRF_TOKEN': \"(.*?)\"").Groups[1].Value.ToString();

                foreach (Match item in Regex.Matches(wb.DocumentText, "<li id=\"vm-video-(.*?)\""))
                {
                    l.Add(item.Groups[1].Value.ToString());
                }

                int i = 1;
                foreach (string item in l)
                {
                    if (lblStatusBar.InvokeRequired)
                        lblStatusBar.Invoke(new Action(() => lblStatusBar.Text = i + "/" + l.Count + "  video ID: " + item));
                    else
                        lblStatusBar.Text = i + "/" + l.Count + "  video ID: " + item;
                    string data = "v=" + item + "&session_token=" + token;

                    wb.Navigate("https://www.youtube.com/video_ajax?num_videos=1&action_delete_videos=1&o=U", data);
                    i++;
                }

                if (lblStatusBar.InvokeRequired)
                    lblStatusBar.Invoke(new Action(() => lblStatusBar.Text = "Complete"));
                else
                    lblStatusBar.Text = "Complete";
                ActionOutProgress();
            });
            t.Start();

        }

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
                if (tbNewTemaplate.Text.IndexOf(' ') != -1)
                {
                    MessageBox.Show("Temaplate's name can't include space chars");
                    return;
                }
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
            ActionInProgress();
            Thread t = new Thread(() =>
            {
                XElement annot;
                lock (doc)
                {
                    string anotName = "";
                    if (cbAnnotationName.InvokeRequired)
                        cbAnnotationName.Invoke(new Action(() => anotName = cbAnnotationName.SelectedItem.ToString()));
                    else
                        anotName = cbAnnotationName.SelectedItem.ToString();
                    annot = doc.Element("Templates").Element(anotName).Element("document");
                }

                int fgColor = Convert.ToInt32(annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("fgColor").Value);
                int bgColor = Convert.ToInt32(annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("bgColor").Value);
                int highlightFontColor = Convert.ToInt32(annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("highlightFontColor").Value);
                annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("fgColor").Value = (fgColor & 0x00FFFFFF).ToString();
                annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("bgColor").Value = (bgColor & 0x00FFFFFF).ToString();
                annot.Element("updatedItems").Element("annotation").Element("appearance").Attribute("highlightFontColor").Value = (highlightFontColor & 0x00FFFFFF).ToString();

                int i = 0;
                int max = 0;
                if (!cbSelectAllVideos.Checked)
                    max = dgvMain.Rows.Cast<DataGridViewRow>().Where((r) => r.Cells[0].Value.ToString() == "True").Count();
                else
                    max = dgvMain.Rows.Count;
                foreach (DataGridViewRow item in dgvMain.Rows)
                {
                    if (cancelAction)
                    {
                        cancelAction = false;
                        if (lblStatusBar.InvokeRequired)
                            lblStatusBar.Invoke(new Action(() => lblStatusBar.Text = "canceled"));
                        else
                            lblStatusBar.Text = "canceled";
                        ActionOutProgress();
                        return;
                    }
                    if ((item.Cells[0] as DataGridViewCheckBoxCell).Value.ToString() == "True" || cbSelectAllVideos.Checked)
                    {
                        if (lblStatusBar.InvokeRequired)
                            lblStatusBar.Invoke(new Action(() => lblStatusBar.Text = (i + 1) + "/" + max + " :" + item.Cells[2].Value));
                        else
                            lblStatusBar.Text = (i + 1) + "/" + max + " :" + item.Cells[2].Value;
                        string videoID = item.Cells[1].Value.ToString().Replace("https://www.youtube.com/watch?v=", "");
                        wb.Navigate("https://www.youtube.com/my_videos_annotate?video_referrer=watch&v=" + videoID);
                        string authToken = Regex.Match(wb.DocumentText, "\"auth_token\":\"(.*?)\"").Groups[1].Value;

                        annot.Element("requestHeader").Attribute("video_id").Value = videoID;
                        annot.Element("authenticationHeader").Attribute("auth_token").Value = authToken;
                        annot.Element("authenticationHeader").Attribute("video_id").Value = videoID;
                        string data = annot.ToString();

                        wb.Navigate("https://www.youtube.com/annotations_auth/update2", data);


                        string anotPublisData = string.Format("<document>\n  <requestHeader video_id=\"{0}\"/>\n  <authenticationHeader auth_token=\"{1}\" video_id=\"{0}\"/>\n</document>", videoID, authToken);
                        wb.Navigate("https://www.youtube.com/annotations_auth/publish2", anotPublisData);
                        i++;
                    }

                }
                if (lblStatusBar.InvokeRequired)
                    lblStatusBar.Invoke(new Action(() => lblStatusBar.Text = "Complete"));
                else
                    lblStatusBar.Text = "Complete";
                ActionOutProgress();
            });

            t.Start();
        }

        private void btnDeleteFromSelected_Click(object sender, EventArgs e)
        {
            if (cbAnnotationName.SelectedItem == null)
            {
                MessageBox.Show("Select template first");
                return;
            }
            ActionInProgress();

            Thread t = new Thread(() =>
            {
                int i = 0;
                int max = 0;
                if (!cbSelectAllVideos.Checked)
                    max = dgvMain.Rows.Cast<DataGridViewRow>().Where((r) => r.Cells[0].Value.ToString() == "True").Count();
                else
                    max = dgvMain.Rows.Count;

                foreach (DataGridViewRow item in dgvMain.Rows)
                {
                    if (cancelAction)
                    {
                        cancelAction = false;
                        if (lblStatusBar.InvokeRequired)
                            lblStatusBar.Invoke(new Action(() => lblStatusBar.Text = "canceled"));
                        else
                            lblStatusBar.Text = "canceled";

                        ActionOutProgress();
                        return;
                    }
                    if ((item.Cells[0] as DataGridViewCheckBoxCell).Value.ToString() == "True" || cbSelectAllVideos.Checked)
                    {
                        if (lblStatusBar.InvokeRequired)
                            lblStatusBar.Invoke(new Action(() => lblStatusBar.Text = (i + 1) + "/" + max + " :" + item.Cells[2].Value));
                        else
                            lblStatusBar.Text = (i + 1) + "/" + max + " :" + item.Cells[2].Value;
                        string videoID = item.Cells[1].Value.ToString().Replace("https://www.youtube.com/watch?v=", "");
                        wb.Navigate("https://www.youtube.com/my_videos_annotate?video_referrer=watch&v=" + videoID);
                        string authToken = Regex.Match(wb.DocumentText, "\"auth_token\":\"(.*?)\"").Groups[1].Value;

                        string anotName = "";
                        if (cbAnnotationName.InvokeRequired)
                            cbAnnotationName.Invoke(new Action(() => anotName = cbAnnotationName.SelectedItem.ToString()));
                        else
                            anotName = cbAnnotationName.SelectedItem.ToString();


                        string anotID = doc.Element("Templates").Element(anotName).Element("document").Element("updatedItems").Element("annotation").Attribute("id").Value;
                        string sToSend = "<document>\n  <requestHeader video_id=\"{0}\"/>\n  <authenticationHeader video_id=\"{0}\" auth_token=\"{1}\"/>\n  <deletedItems>\n    <deletedItem id=\"{2}\" author=\"\"/>\n  </deletedItems>\n</document>";
                        string data = string.Format(sToSend, videoID, authToken, anotID);

                        wb.Navigate("https://www.youtube.com/annotations_auth/update2", data);
                        string anotPublisData = string.Format("<document>\n  <requestHeader video_id=\"{0}\"/>\n  <authenticationHeader auth_token=\"{1}\" video_id=\"{0}\"/>\n</document>", videoID, authToken);
                        wb.Navigate("https://www.youtube.com/annotations_auth/publish2", anotPublisData);
                        i++;
                    }
                }
                if (lblStatusBar.InvokeRequired)
                    lblStatusBar.Invoke(new Action(() => lblStatusBar.Text = "Complete"));
                else
                    lblStatusBar.Text = "Complete";
                ActionOutProgress();
            });
            t.Start();


        }

        void ActionInProgress()
        {
            cbAnnotationName.Enabled = false;
            btnDelete.Enabled = false;
            btnAddTemplate.Enabled = false;
            btnDeleteFromSelected.Enabled = false;
            btnApply.Enabled = false;
            btnDeleteCopyrightVideos.Enabled = false;
        }

        void ActionOutProgress()
        {
            if (cbAnnotationName.InvokeRequired)
                cbAnnotationName.Invoke(new Action(() => cbAnnotationName.Enabled = true));
            else
                cbAnnotationName.Enabled = true;

            if (btnDelete.InvokeRequired)
                btnDelete.Invoke(new Action(() => btnDelete.Enabled = true));
            else
                btnDelete.Enabled = true;

            if (btnAddTemplate.InvokeRequired)
                btnAddTemplate.Invoke(new Action(() => btnAddTemplate.Enabled = true));
            else
                btnAddTemplate.Enabled = true;

            if (btnDeleteFromSelected.InvokeRequired)
                btnDeleteFromSelected.Invoke(new Action(() => btnDeleteFromSelected.Enabled = true));
            else
                btnDeleteFromSelected.Enabled = true;

            if (btnApply.InvokeRequired)
                btnApply.Invoke(new Action(() => btnApply.Enabled = true));
            else
                btnApply.Enabled = true;

            if (btnDeleteCopyrightVideos.InvokeRequired)
                btnDeleteCopyrightVideos.Invoke(new Action(() => btnDeleteCopyrightVideos.Enabled = true));
            else
                btnDeleteCopyrightVideos.Enabled = true;
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

        #region Page News

        void getFeed()
        {
            LinkLabel vkUrl = new LinkLabel();
            vkUrl.Text = "vk.com/commercenetwork" + Environment.NewLine + Environment.NewLine;
            vkUrl.AutoSize = true;
            vkUrl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#5BB1E6");
            vkUrl.Click += (s, ev) => { Process.Start("https://vk.com/commercenetwork"); };

            flpMainContent.Invoke(new Action(() => flpMainContent.Controls.Add(vkUrl)));

            Label tmp = new Label();
            tmp.Text = "Loading news...";
            flpMainContent.Invoke(new Action(() => flpMainContent.Controls.Add(tmp)));

            var request = (HttpWebRequest)WebRequest.Create("https://api.vk.com/method/wall.get.xml?domain=commercenetwork&count=30");
            var response = (HttpWebResponse)request.GetResponse();
            string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            List<Post> l = new List<Post>();
            XDocument news = XDocument.Parse(responseString);

            foreach (XElement item in news.Element("response").Elements("post"))
            {
                string Text = item.Element("text").Value.ToString();

                double unixDate;
                double.TryParse(item.Element("date").Value, out unixDate);
                DateTime date = UnixTimeStampToDateTime(unixDate);

                string id = item.Element("from_id").Value.ToString() + "_" + item.Element("id").Value.ToString();
                string link = "https://vk.com/wall" + id;
                XElement attach = item.Element("attachments");

                Post p = new Post(Text, link, date, attach);
                l.Add(p);
            }


            foreach (Post item in l)
            {
                flpMainContent.Invoke(new Action(() => flpMainContent.Controls.Add(item)));
            }
            flpMainContent.Invoke(new Action(() => flpMainContent.Controls.Remove(tmp)));
            flpMainContent.Invoke(new Action(() => flpMainContent.Focus()));
        }

        DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        #endregion

        #region Page Monetize

        private void btnMonetizeAdd_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() =>
            {
                int i = 0;
                int max = 0;
                if (!cbSelectAllVideos.Checked)
                    max = dgvMain.Rows.Cast<DataGridViewRow>().Where((r) => r.Cells[0].Value.ToString() == "True").Count();
                else
                    max = dgvMain.Rows.Count;
                foreach (DataGridViewRow item in dgvMain.Rows)
                {
                    if (cancelAction)
                    {
                        cancelAction = false;
                        //lblStatusBar.Text = "canceled";
                        //ActionOutProgress();
                        return;
                    }

                    string data = "";
                    if ((item.Cells[0] as DataGridViewCheckBoxCell).Value.ToString() == "True" || cbSelectAllVideos.Checked)
                    {
                        if (lblStatusPage4.InvokeRequired)
                            lblStatusPage4.Invoke(new Action(() => lblStatusPage4.Text = (i + 1) + "/" + max + " :" + item.Cells[2].Value));
                        else
                            lblStatusPage4.Text = (i + 1) + "/" + max + " :" + item.Cells[2].Value;
                        string videoID = item.Cells[1].Value.ToString().Replace("https://www.youtube.com/watch?v=", "");
                        wb.Navigate("https://www.youtube.com/edit?video_id=" + videoID);

                        string page = Regex.Replace(wb.DocumentText, "" + (char)10, "");
                        string authToken = Regex.Match(wb.DocumentText, "sessionToken\': \"(.*?)\"").Groups[1].Value;
                        string title = Regex.Match(wb.DocumentText, "name=\"web_title\" value=\"(.*?)\"").Groups[1].Value;
                        string description = Regex.Match(page, "textarea class=\"yt-uix-form-input-textarea video-settings-description\" name=\"description\"(.*?)>(.*?)<").Groups[2].Value;
                        string keywords = Regex.Match(page, "<input type=\"hidden\" name=\"keywords\" class=\"video-settings-tags\" value=\"(.*?)\"").Groups[1].Value;

                        data = "title=" + title + "&";
                        data += "description=" + description + "&";
                        data += "keywords=" + keywords + "&" + "privacy=public&privacy_draft=none&still_id=2&claim_style=ads&claim_usage_privacy=SRN8mwLm3LM&allow_comments=yes&allow_comments_detail=all&allow_ratings=yes&reuse=creative_commons&syndication=everywhere&allow_embedding=yes&";
                        string category = Regex.Match(page, "name=\"category\"(.*?)</select>").Groups[0].Value;
                        foreach (Match m in Regex.Matches(category, "<option value=\"(.*?)\"(.*?)</option>"))
                        {
                            if (m.Value.IndexOf("selected") != -1)
                            {
                                category = m.Groups[1].Value;
                            }
                        }
                        string formats = "{\"has_overlay_ads\":" + cbOverlay.Checked.ToString().ToLower() + ",\"has_skippable_video_ads\":" +
                            cbSkippedAnonth.Checked.ToString().ToLower() + ",\"has_non_skippable_video_ads\":" +
                            cbNonSkippedAnnonth.Checked.ToString().ToLower() + ",\"has_long_non_skippable_video_ads\":" +
                            cbLowRange.Checked.ToString().ToLower() + "}&";

                        data += "category=" + category + "&threed_type=default&threed_layout=1&allow_public_stats=yes&creator_share_gplus=no&creator_share_twitter=no&creator_share_feeds=yes&self_racy=no&ad_formats=" + formats;
                        data += "modified_fields=ad_formats&video_id=" + videoID + "&session_token=" + authToken;


                        wb.Navigate("https://www.youtube.com/metadata_ajax?action_edit_video=1", data);

                        i++;
                    }

                    if (lblStatusPage4.InvokeRequired)
                        lblStatusPage4.Invoke(new Action(() => lblStatusPage4.Text = "Complete"));
                    else
                        lblStatusPage4.Text = "Complete";
                }

            });
            t.Start();
        }
        #endregion

        #region Page Tips

        bool tipCancelAction = false;

        private void cbTipType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (sender as ComboBox).SelectedIndex;
            if (index == 0)
            {
                tbUrl.Enabled = true;
                tbStartTime.Enabled = true;
                tbTitle.Enabled = false;
                tbTeaserText.Enabled = false;
                tbCallToAction.Enabled = false;
            }
            else if (index == 1)
            {
                tbUrl.Enabled = true;
                tbStartTime.Enabled = true;
                tbTitle.Enabled = true;
                tbTeaserText.Enabled = true;
                tbCallToAction.Enabled = true;
            }
        }

        private void btnAddTips_Click(object sender, EventArgs e)
        {
            if (tbUrl.Text == "")
            {
                MessageBox.Show("Please input link");
                return;
            }
            if (cbTipType.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose tip type");
                return;
            }
            if (tbTitle.Text == "" && cbTipType.SelectedIndex == 1)
            {
                MessageBox.Show("Please input title");
                return;
            }
            if (tbTeaserText.Text == "" && cbTipType.SelectedIndex == 1)
            {
                MessageBox.Show("Please input teaser text");
                return;
            }
            if (tbCallToAction.Text == "" && cbTipType.SelectedIndex == 1)
            {
                MessageBox.Show("Please input call to action");
                return;
            }
            Thread t;
            int index = cbTipType.SelectedIndex;
            if (index == 0) t = new Thread(createTipToVideo);
            else t = new Thread(createTipToSite);
            t.Start();
        }

        private void createTipToVideo()
        {
            tipsInProgress();
            tipCancelAction = false;
            int i = 0;
            int max = 0;
            if (!cbSelectAllVideos.Checked)
                max = dgvMain.Rows.Cast<DataGridViewRow>().Where((r) => r.Cells[0].Value.ToString() == "True").Count();
            else
                max = dgvMain.Rows.Count;
            foreach (DataGridViewRow item in dgvMain.Rows)
            {
                if (tipCancelAction)
                {
                    lblTipsStatusBar.Invoke(new Action(() => lblTipsStatusBar.Text = "canceled"));
                    tipsOutProgress();
                    return;
                }

                if ((item.Cells[0] as DataGridViewCheckBoxCell).Value.ToString() == "True" || cbSelectAllVideos.Checked)
                {
                    if (lblTipsStatusBar.InvokeRequired)
                        lblTipsStatusBar.Invoke(new Action(() => lblTipsStatusBar.Text = (i + 1) + "/" + max + " :" + item.Cells[2].Value));
                    else
                        lblTipsStatusBar.Text = (i + 1) + "/" + max + " :" + item.Cells[2].Value;
                    string videoID = item.Cells[1].Value.ToString().Replace("https://www.youtube.com/watch?v=", "");
                    string videoUrl = item.Cells[1].Value.ToString();
                    wb.Navigate(string.Format("https://www.youtube.com/cards?v={0}&video_referrer=watch#", videoID));
                    string sessionToken = Regex.Match(wb.DocumentText, "'XSRF_TOKEN': \"(.*?)\"").Groups[1].Value;

                    string data = "";
                    addMultipartParam("key", "", ref data);
                    addMultipartParam("type", "video", ref data);
                    addMultipartParam("start_ms", tbStartTime.Text, ref data);
                    addMultipartParam("show_warnings", "true", ref data);
                    addMultipartParam("video_url", tbUrl.Text, ref data);
                    addMultipartParam("action_create_video", "1", ref data);
                    addMultipartParam("session_token", sessionToken, ref data, true);
                    wb.NavigateMultipart("https://www.youtube.com/cards_ajax?v=" + videoID, data);
                    i++;
                }

            }
            tipsOutProgress();
            lblTipsStatusBar.Invoke(new Action(() => lblTipsStatusBar.Text = "tips added to " + i + " videos"));
        }

        private void createTipToSite()
        {
            tipsInProgress();
            tipCancelAction = false;
            int i = 0;
            int max = 0;
            if (!cbSelectAllVideos.Checked)
                max = dgvMain.Rows.Cast<DataGridViewRow>().Where((r) => r.Cells[0].Value.ToString() == "True").Count();
            else
                max = dgvMain.Rows.Count;
            string res = "";
            try
            {
                foreach (DataGridViewRow item in dgvMain.Rows)
                {
                    if (tipCancelAction)
                    {
                        lblTipsStatusBar.Invoke(new Action(() => lblTipsStatusBar.Text = "canceled"));
                        tipsOutProgress();
                        return;
                    }
                    if ((item.Cells[0] as DataGridViewCheckBoxCell).Value.ToString() == "True" || cbSelectAllVideos.Checked)
                    {
                        if (lblTipsStatusBar.InvokeRequired)
                            lblTipsStatusBar.Invoke(new Action(() => lblTipsStatusBar.Text = (i + 1) + "/" + max + " :" + item.Cells[2].Value));
                        else
                            lblTipsStatusBar.Text = (i + 1) + "/" + max + " :" + item.Cells[2].Value;
                        string videoID = item.Cells[1].Value.ToString().Replace("https://www.youtube.com/watch?v=", "");
                        string videoUrl = item.Cells[1].Value.ToString();
                        wb.Navigate(string.Format("https://www.youtube.com/cards?v={0}&video_referrer=watch#", videoID));
                        string sessionToken = Regex.Match(wb.DocumentText, "'XSRF_TOKEN': \"(.*?)\"").Groups[1].Value;

                        string postData = "target_url=" + tbUrl.Text;
                        postData += "&action_get_url_info=1";
                        postData += "&type=link";
                        postData += "&session_token=" + sessionToken;
                        res = wb.Navigate("https://www.youtube.com/cards_ajax?v=" + videoID, postData);
                        JObject rawJson = JObject.Parse(res);

                        string data = "";
                        addMultipartParam("key", "", ref data);
                        addMultipartParam("type", rawJson["type"].ToString(), ref data);
                        addMultipartParam("start_ms", tbStartTime.Text, ref data);
                        addMultipartParam("image_url", rawJson["url_metadata"]["thumbnails"][0]["url"].ToString(), ref data); // change this
                        addMultipartParam("target_url", rawJson["url"].ToString(), ref data);
                        addMultipartParam("title", tbTitle.Text, ref data);
                        addMultipartParam("custom_message", tbCallToAction.Text, ref data);
                        addMultipartParam("teaser_text", tbTeaserText.Text, ref data);
                        addMultipartParam("action_create_associated", "1", ref data);
                        addMultipartParam("session_token", sessionToken, ref data, true);
                        wb.NavigateMultipart("https://www.youtube.com/cards_ajax?v=" + videoID, data);
                        i++;
                    }
                }
            }
            catch(Exception)
            {
                JObject rawJson = JObject.Parse(res);
                MessageBox.Show(rawJson["errors"].ToString(), "error");
            }
            tipsOutProgress();
            lblTipsStatusBar.Invoke(new Action(() => lblTipsStatusBar.Text = "tips added to " + i + " videos"));
        }

        private void addMultipartParam(string key, string value, ref string data, bool last = false)
        {
            string boundary = "-----------------------------17101148691947789575814176401" + Environment.NewLine;
            string keyValuePattern = string.Format("Content-Disposition: form-data; name=\"{0}\""
                + Environment.NewLine + Environment.NewLine + value + Environment.NewLine, key);
            data += boundary + keyValuePattern;
            if (last) data += "-----------------------------17101148691947789575814176401--" + Environment.NewLine;
        }

        private void btnCancelTipAction_Click(object sender, EventArgs e)
        {
            tipCancelAction = true;
        }

        private void btnRemoveTips_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(removeTips);
            t.Start();
        }

        private void removeTips()
        {
            tipsInProgress();
            string linkPattern = "https://www.youtube.com/cards_ajax?v={0}&action_list=1";
            tipCancelAction = false;
            int i = 0;
            foreach (DataGridViewRow item in dgvMain.Rows)
            {
                if (tipCancelAction)
                {
                    lblTipsStatusBar.Invoke(new Action(() => lblTipsStatusBar.Text = "canceled"));
                    tipsOutProgress();
                    return;
                }
                if ((item.Cells[0] as DataGridViewCheckBoxCell).Value.ToString() == "True" || cbSelectAllVideos.Checked)
                {
                    lblTipsStatusBar.Invoke(new Action(() => lblTipsStatusBar.Text = (i + 1) + " - tips removed, current video: " + item.Cells[2].Value));
                    string videoID = item.Cells[1].Value.ToString().Replace("https://www.youtube.com/watch?v=", "");
                    string videoUrl = item.Cells[1].Value.ToString();
                    wb.Navigate(string.Format(linkPattern, videoID));
                    JObject tipsJson = JObject.Parse(wb.DocumentText);
                    wb.Navigate(string.Format("https://www.youtube.com/cards?v={0}&video_referrer=watch#", videoID));
                    string sessionToken = Regex.Match(wb.DocumentText, "'XSRF_TOKEN': \"(.*?)\"").Groups[1].Value;

                    foreach (JObject tipJson in tipsJson["feature_templates"])
                    {
                        i++;
                        string data = "";
                        addMultipartParam("key", tipJson["key"].ToString(), ref data);
                        addMultipartParam("type", tipJson["type"].ToString(), ref data);
                        addMultipartParam("start_ms", tipJson["start_ms"].ToString(), ref data);
                        addMultipartParam("image_url", tipJson["image_url"] != null ? tipJson["image_url"].ToString() : "", ref data); // change this
                        addMultipartParam("target_url", tipJson["target_url"].ToString(), ref data);
                        addMultipartParam("title", tipJson["title"].ToString(), ref data);
                        addMultipartParam("teaser_text", tipJson["teaser_text"] != null ? tipJson["teaser_text"].ToString() : "", ref data);
                        int ms = Convert.ToInt32(tipJson["start_ms"].ToString());
                        TimeSpan ts = new TimeSpan(0, 0, 0, 0, ms);
                        string start_time = ts.Hours.ToString("hh") + ":" + ts.Seconds.ToString("ss");
                        addMultipartParam("start_time", start_time, ref data);
                        addMultipartParam("session_token", sessionToken, ref data, true);
                        wb.NavigateMultipart(string.Format("https://www.youtube.com/cards_ajax?v={0}&action_delete=1", videoID), data);
                    }

                }

            }
            tipsOutProgress();
            lblTipsStatusBar.Invoke(new Action(() => lblTipsStatusBar.Text = i + " tips was removed"));
        }

        private void tipsInProgress()
        {
            btnAddTips.Invoke(new Action(() => btnAddTips.Enabled = false));
            btnRemoveTips.Invoke(new Action(() => btnRemoveTips.Enabled = false));
            cbTipType.Invoke(new Action(() => cbTipType.Enabled = false));
        }

        private void tipsOutProgress()
        {
            btnAddTips.Invoke(new Action(() => btnAddTips.Enabled = true));
            btnRemoveTips.Invoke(new Action(() => btnRemoveTips.Enabled = true));
            cbTipType.Invoke(new Action(() => cbTipType.Enabled = true));
        }

        #endregion
    }
}
