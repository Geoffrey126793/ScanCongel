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
    public partial class Inventaire : Form
    {
        ServiceReference1.Pallets_PortClient client;
        ScanCU.Scan_PortClient scanCU;
        String palSSC="";

        public Inventaire()
        {
            InitializeComponent();
        }
          

      
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void input_Palette_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void input_Palette_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                List<ServiceReference1.Pallets_Filter> filter = new List<ServiceReference1.Pallets_Filter>();
                ServiceReference1.Pallets_Filter f = new ServiceReference1.Pallets_Filter();
                f.Field = ServiceReference1.Pallets_Fields.SSCC_No;
                f.Criteria = input_Palette.Text;
                filter.Add(f);

                ServiceReference1.Pallets[] pallets = client.ReadMultiple(filter.ToArray(), null, 100);
                if (pallets.Count() > 0)
                {
                    l_Emplacement.Text = pallets[0].Bin_code;
                    l_Lot.Text = pallets[0].Lot_No;
                    l_NumArticle.Text = pallets[0].Item_No.ToString();
                    l_Quantite.Text = pallets[0].Current_Quantity.ToString();
                    input_Qty.Text = pallets[0].Current_Quantity.ToString();
                    palSSC = pallets[0].SSCC_No;
 
                    input_Palette.BackColor = Color.Empty;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }

        private void Inventaire_Load(object sender, EventArgs e)
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
            input_Palette.BackColor = Color.Lime;
            input_Qty.BackColor = Color.Empty;
            input_Palette.Text = "";
            input_Qty.Text = "";
            l_Emplacement.Text = "";
            l_Lot.Text = "";
            l_NumArticle.Text = "";
            l_Quantite.Text = "";
            palSSC = "";
            //input_Comment.Text = "";
            input_Palette.Focus();
                
        }

        private void label8_Click(object sender, EventArgs e)
        {
            try
            {
                if (!input_Qty.Text.Equals(l_Quantite.Text))
                {
                    scanCU.PalletInventory(palSSC,decimal.Parse(input_Qty.Text),User.UserCode);
                    /*
                    System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();
                    MailMsg.From = new System.Net.Mail.MailAddress("ECScan@europacuisson.com");
                    //MailMsg.To.Add(new System.Net.Mail.MailAddress("congelateur@europacuisson.com"));
                    MailMsg.To.Add(new System.Net.Mail.MailAddress("g.dangremont@europacuisson.com"));
                    MailMsg.Subject = "Erreur stock sur palette";
                    MailMsg.Body = "Article : "+ l_NumArticle.Text+"\nEmplacement "+ l_Emplacement.Text + "\nPalette " + palSSC + " \n Quantité en stock : " + l_Quantite.Text + "\n Inventaire : " + input_Qty.Text;
                    System.Net.Mail.SmtpClient SmtpCli = new System.Net.Mail.SmtpClient("192.168.5.30");
                    SmtpCli.Send(MailMsg);
                    */
                }
                /*
                if (!input_Comment.Text.Equals(""))
                {
                    System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();
                    MailMsg.From = new System.Net.Mail.MailAddress("ECScan@europacuisson.com");
                    //MailMsg.To.Add(new System.Net.Mail.MailAddress("congelateur@europacuisson.com"));
                    MailMsg.To.Add(new System.Net.Mail.MailAddress("g.dangremont@europacuisson.com"));
                    MailMsg.Subject = "Erreur sur palette";
                    MailMsg.Body = "Article : " + l_NumArticle.Text + "\nEmplacement " + l_Emplacement.Text + "\nPalette " + palSSC + " \n Commentaire : " + input_Comment.Text;
                    System.Net.Mail.SmtpClient SmtpCli = new System.Net.Mail.SmtpClient("192.168.5.30");
                    SmtpCli.Send(MailMsg);
                }
                */
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
