using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ytAnnotations
{
    public partial class Login : Form
    {
        string clientID = "357960218741-elgp2dlo51001lqk6fu3riuq7pib97d2.apps.googleusercontent.com";
        string clientSectet = "61q7mVh5wPeSuss0dqN3J9D8";
        public UserCredential credential {get;set; }
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                         new ClientSecrets { ClientId = clientID, ClientSecret = clientSectet },
                         new[] { YouTubeService.Scope.Youtube },
                         tbChannelName.Text,
                         CancellationToken.None,
                         new FileDataStore(this.GetType().ToString())).Result;
            if (credential.Token == null)
            {
                MessageBox.Show("Неверний логин или пароль");
                return;
            }
            MessageBox.Show("Канал авторизирован");
            this.Close();
        }
    }
}
