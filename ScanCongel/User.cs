using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanCongel
{
    public partial class User : Form
    {
        public static String UserCode="";
        public static String UserName = "";
        public static bool Inventory = false;

        NAVUsers.Users_PortClient client;
        public User()
        {
            InitializeComponent();
        }

        private void input_scan_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (input_scan.Text.Length > 5)
                {
                    List<NAVUsers.Users_Filter> filter = new List<NAVUsers.Users_Filter>();
                    NAVUsers.Users_Filter f = new NAVUsers.Users_Filter();
                    f.Field = NAVUsers.Users_Fields.Barcode;
                    f.Criteria = input_scan.Text;
                    filter.Add(f);

                    NAVUsers.Users[] users = client.ReadMultiple(filter.ToArray(), null, 100);
                    if (users.Count() > 0)
                    {
                        UserCode = users[0].User;
                        UserName = users[0].Last_Name + " " + users[0].First_Name;
                        Inventory = users[0].InventoryScan;
                        this.Close();

                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void User_Load(object sender, EventArgs e)
        {
            client = new NAVUsers.Users_PortClient();
            BasicHttpBinding basicHttpBinding1 = new BasicHttpBinding();
            basicHttpBinding1.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            basicHttpBinding1.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            basicHttpBinding1.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            basicHttpBinding1.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            basicHttpBinding1.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;
            basicHttpBinding1.MaxReceivedMessageSize = 25 * 1024 * 1024;
            client.ChannelFactory.Endpoint.Binding = basicHttpBinding1;
            client.ChannelFactory.Endpoint.Address = new EndpointAddress(Config.BaseUrl + "Page/Users");
            client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.None;


            client.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("WSScan", "25erG68", "EUROPACUISSON");

            /* client = new NAVUsers.Users_PortClient();
            BasicHttpsBinding basicHttpBinding1 = new BasicHttpsBinding();
            // basicHttpBinding1.Security.Mode =  BasicHttpsSecurityMode.Transport;
            basicHttpBinding1.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            // basicHttpBinding1.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            basicHttpBinding1.MaxReceivedMessageSize = 25 * 1024 * 1024;
            client.ChannelFactory.Endpoint.Binding = basicHttpBinding1;
            client.ChannelFactory.Endpoint.Address = new EndpointAddress(Config.BaseUrl + "Page/Users");
            client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.None;

            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication = new System.ServiceModel.Security.X509ServiceCertificateAuthentication();
            client.ClientCredentials.ServiceCertificate.SslCertificateAuthentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;


            client.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("WSScan", "25erG68", "EUROPACUISSON");*/



        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserCode = "";
            UserName = "";
            Inventory = false;
            this.Close();
        }
    }
}
