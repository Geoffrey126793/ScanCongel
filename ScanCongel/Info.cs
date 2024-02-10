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
    public partial class Info : Form
    {
        ServiceReference1.Pallets_PortClient client;
        ScanCU.Scan_PortClient scanCU;

        public Info()
        {
            InitializeComponent();
        }
          

      
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void input_Palette_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void input_scan_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                List<ServiceReference1.Pallets_Filter> filter = new List<ServiceReference1.Pallets_Filter>();
                ServiceReference1.Pallets_Filter f = new ServiceReference1.Pallets_Filter();
                f.Field = ServiceReference1.Pallets_Fields.SSCC_No;
                f.Criteria = input_scan.Text;
                filter.Add(f);

                ServiceReference1.Pallets[] pallets = client.ReadMultiple(filter.ToArray(), null, 100);
                if (pallets.Count() > 0)
                {
                    l_Emplacement.Text = pallets[0].Bin_code;
                    l_Lot.Text = pallets[0].Lot_No;
                    l_NumArticle.Text = pallets[0].Item_No.ToString();
                    l_Quantite.Text = pallets[0].Current_Quantity.ToString();
                    l_DLC.Text = pallets[0].DLC.ToShortDateString();
                    panel_emplacement.Visible = false;
                    panel_palette.Visible = true;
                }
                else
                {
                    List<ServiceReference1.Pallets_Filter> filter2 = new List<ServiceReference1.Pallets_Filter>();
                    ServiceReference1.Pallets_Filter f2 = new ServiceReference1.Pallets_Filter();
                    f.Field = ServiceReference1.Pallets_Fields.Bin_code;
                    f.Criteria = input_scan.Text;
                    filter2.Add(f);

                    richTextBox1.Left = 0;
                    richTextBox1.Top = 0;
                    richTextBox1.Size = panel_emplacement.Size;
                    richTextBox1.Enabled = false;
                    pallets = client.ReadMultiple(filter.ToArray(), null, 100);
                    foreach (ServiceReference1.Pallets p in pallets)
                    {
                        richTextBox1.AppendText(p.SSCC_No + "\t" + p.Item_No + "\t" + p.Current_Quantity+"\r\n");
                    }
                    panel_emplacement.Visible = true;
                    panel_palette.Visible = false;

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }

        private void Enlogement_Load(object sender, EventArgs e)
        {
            client = new ServiceReference1.Pallets_PortClient();
            BasicHttpBinding basicHttpBinding1 = new BasicHttpBinding();
            basicHttpBinding1.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            basicHttpBinding1.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            basicHttpBinding1.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            basicHttpBinding1.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            basicHttpBinding1.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;
            basicHttpBinding1.MaxReceivedMessageSize = 25 * 1024 * 1024;
            client.ChannelFactory.Endpoint.Binding = basicHttpBinding1;
            client.ChannelFactory.Endpoint.Address = new EndpointAddress(Config.BaseUrl + "Page/Pallets");
            client.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.None;


            client.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("WSScan", "25erG68", "EUROPACUISSON");

            scanCU = new ScanCU.Scan_PortClient();           
            scanCU.ChannelFactory.Endpoint.Binding = basicHttpBinding1;
            scanCU.ChannelFactory.Endpoint.Address = new EndpointAddress(Config.BaseUrl + "Codeunit/Scan");

            scanCU.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;

            scanCU.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("WSScan", "25erG68", "EUROPACUISSON");

            ResetScreen();
           
        }

        private void input_Emplacement_Validating(object sender, CancelEventArgs e)
        {
            
        }

        public void ResetScreen()
        {
            input_scan.BackColor = Color.Lime;
            input_scan.Text = "";
            l_Emplacement.Text = "";
            l_Lot.Text = "";
            l_NumArticle.Text = "";
            l_Quantite.Text = "";
            l_DLC.Text = "";
                
        }

        private void label8_Click(object sender, EventArgs e)
        {
            try
            {
                ResetScreen();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
