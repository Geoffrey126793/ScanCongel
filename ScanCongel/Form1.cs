using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanCongel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ServiceReference1.Pallets_PortClient client = new ServiceReference1.Pallets_PortClient();
            BasicHttpBinding basicHttpBinding1 = new BasicHttpBinding();
            basicHttpBinding1.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            basicHttpBinding1.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            basicHttpBinding1.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            basicHttpBinding1.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            basicHttpBinding1.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;
            basicHttpBinding1.MaxReceivedMessageSize = 25*1024 * 1024;
            client.ChannelFactory.Endpoint.Binding = basicHttpBinding1;
            client.ChannelFactory.Endpoint.Address = new EndpointAddress("http://srvnavision.europacuisson.local:7057/DynamicsNav/WS/EC%20Démarrage/Page/Pallets");
            client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Impersonation;

            client.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("WSScan", "25erG68", "EUROPACUISSON");




            List<ServiceReference1.Pallets_Filter> filter = new List<ServiceReference1.Pallets_Filter>();
            ServiceReference1.Pallets_Filter f = new ServiceReference1.Pallets_Filter();
            f.Field = ServiceReference1.Pallets_Fields.SSCC_No;
            f.Criteria = "000000151166";
            filter.Add(f);

            ServiceReference1.Pallets[] pallets = client.ReadMultiple(filter.ToArray(),null, 100);
 
            foreach (ServiceReference1.Pallets p in pallets)
            {
                System.Diagnostics.Debug.WriteLine(p.SSCC_No + "  " + p.Bin_code + "  " + p.DLC + "   " + p.Lot_No);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ScanCU.Scan_PortClient scanCU = new ScanCU.Scan_PortClient();
            BasicHttpBinding basicHttpBinding1 = new BasicHttpBinding();
            basicHttpBinding1.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            basicHttpBinding1.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            basicHttpBinding1.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            basicHttpBinding1.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            basicHttpBinding1.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;
            basicHttpBinding1.MaxReceivedMessageSize = 25 * 1024 * 1024;
            scanCU.ChannelFactory.Endpoint.Binding = basicHttpBinding1;
            scanCU.ChannelFactory.Endpoint.Address = new EndpointAddress("http://srvnavision.europacuisson.local:7057/DynamicsNAV/WS/EC%20Démarrage/Codeunit/Scan");

            scanCU.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;


            scanCU.ClientCredentials.UserName.UserName = "EUROPACUISSON/WSScan";
            scanCU.ClientCredentials.UserName.Password = "25erG68";

            scanCU.PutAwayPallet("000000151166", "1-A-124-1");
        }

        private void label10_Click(object sender, EventArgs e)
        {
            if (CheckLogin())
            {
                Enlogement fEnlogement = new Enlogement();
                fEnlogement.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (CheckLogin())
            {
                Picking fPicking = new Picking();
                fPicking.ShowDialog();
            }
        }

        private void BtAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (CheckLogin())
            {
                Inventaire fInventaire = new Inventaire();
                fInventaire.ShowDialog();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (CheckLogin())
            {
                Info fInfo = new Info();
                fInfo.ShowDialog();
            }
        }

        private void userNotLogged_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.ShowDialog();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {            
            userNotLogged.Visible = User.UserCode.Equals("");
            userLogged.Visible = !User.UserCode.Equals("");
            pictureBox4.Enabled = User.Inventory;
        }

        private bool CheckLogin()
        {
            if (User.UserCode.Equals(""))
            {
                User u = new User();
                u.ShowDialog();
                return false;
            }
            else
            {
                return true;
            }

        }

        private void userLogged_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.ShowDialog();
        }
    }
}
