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
    public partial class Picking : Form
    {
        enum EStatus { Ordre,Palettes};
        Reservations.Reservations_PortClient clientReservation;
        ServiceReference1.Pallets_PortClient clientPalette;
        ScanCU.Scan_PortClient scanCU;
        EStatus Status;
        List<PalettesPicking> ToPick = new List<PalettesPicking>();
        List<PalettesPicking> Possible = new List<PalettesPicking>();
        String Ordre = "";

        public Picking()
        {
            InitializeComponent();
        }

        private void Picking_Load(object sender, EventArgs e)
        {
            clientReservation = new Reservations.Reservations_PortClient();
            BasicHttpBinding basicHttpBinding1 = new BasicHttpBinding();
            basicHttpBinding1.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            basicHttpBinding1.Security.Transport.ClientCredentialType = HttpClientCredentialType.Ntlm;
            basicHttpBinding1.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            basicHttpBinding1.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            basicHttpBinding1.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;
            basicHttpBinding1.MaxReceivedMessageSize = 25 * 1024 * 1024;
            clientReservation.ChannelFactory.Endpoint.Binding = basicHttpBinding1;
            clientReservation.ChannelFactory.Endpoint.Address = new EndpointAddress(Config.BaseUrl+"Page/Reservations");
            clientReservation.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.None;
            clientReservation.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("WSScan", "25erG68", "EUROPACUISSON");

            clientPalette = new ServiceReference1.Pallets_PortClient();
            clientPalette.ChannelFactory.Endpoint.Binding = basicHttpBinding1;
            clientPalette.ChannelFactory.Endpoint.Address = new EndpointAddress(Config.BaseUrl + "Page/Pallets");
            clientPalette.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.None;
            clientPalette.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("WSScan", "25erG68", "EUROPACUISSON");

            scanCU = new ScanCU.Scan_PortClient();
            scanCU.ChannelFactory.Endpoint.Binding = basicHttpBinding1;
            scanCU.ChannelFactory.Endpoint.Address = new EndpointAddress(Config.BaseUrl + "Codeunit/Scan");
            scanCU.ClientCredentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;
            scanCU.ClientCredentials.Windows.ClientCredential = new System.Net.NetworkCredential("WSScan", "25erG68", "EUROPACUISSON");

            lTitle.Text = "Scanner l'ordre";
            Status = EStatus.Ordre;
        }

        public void GetPalettes()
        {
            List<Reservations.Reservations_Filter> filterReservation = new List<Reservations.Reservations_Filter>();
            Reservations.Reservations_Filter f = new Reservations.Reservations_Filter();
            f.Field = Reservations.Reservations_Fields.Source_ID;
            f.Criteria = input_scan.Text;
            filterReservation.Add(f);

            Ordre = input_scan.Text;

            foreach (Reservations.Reservations r in clientReservation.ReadMultiple(filterReservation.ToArray(), "", 100))
            {
                System.Diagnostics.Debug.WriteLine("---" + r.Lot_No + "     "+r.Source_Type+"  "+ r.Source_Subtype);
                if (((r.Source_Type == 37) && (r.Source_Subtype == Reservations.Source_Subtype._x0031_)) || ((r.Source_Type == 5741) && (r.Source_Subtype == Reservations.Source_Subtype._x0030_)))
                {
                    System.Diagnostics.Debug.WriteLine("->>--" + r.Lot_No);
                    PalettesPicking pal1 = new PalettesPicking();
                    pal1.ItemNo = r.Item_No;
                    pal1.LineNo = r.Source_Ref_No;
                    pal1.LotNo = r.Lot_No;
                    pal1.OrderNo = r.Source_ID;
                    pal1.QtyToPick = Math.Abs(r.Qty_to_Handle_Base);
                    ToPick.Add(pal1);

                    List<ServiceReference1.Pallets_Filter> filterPal = new List<ServiceReference1.Pallets_Filter>();
                    ServiceReference1.Pallets_Filter f_pal = new ServiceReference1.Pallets_Filter();
                    f_pal.Field = ServiceReference1.Pallets_Fields.Item_No;
                    f_pal.Criteria = r.Item_No;
                    filterPal.Add(f_pal);
                    f_pal = new ServiceReference1.Pallets_Filter();
                    f_pal.Field = ServiceReference1.Pallets_Fields.Lot_No;
                    f_pal.Criteria = r.Lot_No.Replace('<', '?').Replace('>', '?').Replace('*','?').Replace('(','?').Replace(')','?');

                    filterPal.Add(f_pal);
                    f_pal = new ServiceReference1.Pallets_Filter();
                    f_pal.Field = ServiceReference1.Pallets_Fields.Bin_code;
                    f_pal.Criteria = "<>''";
                    f_pal = new ServiceReference1.Pallets_Filter();
                    f_pal.Field = ServiceReference1.Pallets_Fields.Current_Quantity;
                    f_pal.Criteria = ">0";
                    filterPal.Add(f_pal);
                    f_pal = new ServiceReference1.Pallets_Filter();
                    f_pal.Field = ServiceReference1.Pallets_Fields.Pallet_Journal_Line_Exist;
                    f_pal.Criteria = "false";
                    filterPal.Add(f_pal);
                    foreach (ServiceReference1.Pallets p in clientPalette.ReadMultiple(filterPal.ToArray(), null, 100))
                    {
                        PalettesPicking pal = new PalettesPicking();
                        pal.BinNo = p.Bin_code;
                        pal.ItemNo = p.Item_No;
                        pal.LineNo = r.Source_Ref_No;
                        pal.LotNo = p.Lot_No;
                        pal.OrderNo = r.Source_ID;
                        pal.PalletteSSCC = p.SSCC_No;
                        pal.Picked = false;
                        pal.Qty = Math.Abs(p.Current_Quantity);
                        pal.QtyToPick = Math.Abs(r.Qty_to_Handle_Base);
                        pal.LotBloque = p.Lot_bloque;
                        Possible.Add(pal);                      

                        //System.Diagnostics.Debug.WriteLine(p.SSCC_No + "  " + p.Lot_No + " " + p.Bin_code + "  " + p.Current_Quantity);
                    }
                }
            }
            refreshList();
            Status = EStatus.Palettes;            
            lTitle.Text = "Scanner les palettes";
            input_scan.Text = "";
            
            
        }

        private void refreshList()
        {
            listPalettes.Items.Clear();
            foreach (PalettesPicking pal in Possible.OrderBy(p => p.BinNo).Where(p => (p.Picked.Equals(false) && (Math.Abs(p.QtyToPick) > 0))).ToList())
            { 
                listPalettes.Items.Add(new ListViewItem( pal.BinNo + " / " + pal.PalletteSSCC + " / " + pal.Qty + " " +pal.LotNo + ""));                
            }
            listPalettes.View = View.Details;
            listPalettes.Columns[0].Width = listPalettes.Width;
            listPalettes.Visible = true;

            listLots.Items.Clear();

            foreach (PalettesPicking pal in ToPick.Where(p => (Math.Abs(p.QtyToPick) > 0)))
            {
                listLots.Items.Add(new ListViewItem(pal.LotNo + "  :   " + Math.Abs(pal.QtyToPick)));
            }
            listLots.View = View.Details;
            listLots.Columns[0].Width = listLots.Width;
            listLots.Visible = true;
        }
        private void label8_Click(object sender, EventArgs e)
        {
           
        }

        private void input_scan_TextChanged(object sender, EventArgs e)
        {
            if (Status == EStatus.Palettes)
            {
                if (input_scan.Text.Length >= 12)
                {
                    PalettesPicking l = Possible.Find(p => p.PalletteSSCC.Equals(input_scan.Text));
                    if (l == null)
                    {
                        //MessageBox.Show("Palette non trouvée");
                    }
                    else
                    {
                        ScanOK();
                    }

                }
            }
        }
        private void PalletPicked(PalettesPicking pallet)
        {
            if (!Ordre.ToUpper().StartsWith("OTS") && (pallet.LotBloque))
            {
                MessageBox.Show("!!!    Lot bloqué   !!!");
                return;
            }
            ConfirmPalletPicking confirm = new ConfirmPalletPicking();
            confirm.pallet = pallet;
            if (confirm.ShowDialog() == DialogResult.OK)
            {
                decimal Qty = confirm.GetQuantity();
                pallet.Picked = true;
                if (pallet.Qty > Qty)
                {  // Il restera encore sur la palette, on peut éventuellement la reprendre sur une autre ligne
                    foreach (PalettesPicking p in Possible.Where(p1 => p1.PalletteSSCC.Equals(pallet.PalletteSSCC) && !p1.LineNo.Equals(pallet.LineNo) && !p1.Picked))
                    {
                        p.Qty -= Qty;
                    }
                }

                pallet.Qty = Qty;

                foreach (PalettesPicking p in Possible.Where(p1 => p1.LotNo.Equals(pallet.LotNo) && p1.LineNo.Equals(pallet.LineNo)))
                {
                    p.QtyToPick -= Qty;
                }
                PalettesPicking l = ToPick.Find(p => p.LotNo.Equals(pallet.LotNo) && p.LineNo.Equals(pallet.LineNo));
                l.QtyToPick -= Qty;
                refreshList();

                if (ToPick.Where(p => (Math.Abs(p.QtyToPick) != 0)).Count() == 0)
                {
                    BtPickingOk.Visible = true;
                    listLots.Visible = false;
                    listPalettes.Visible = false;
                }
            }
        }

        private void BtPickingOk_Click(object sender, EventArgs e)
        {
            try
            {
                String OrderNo = "";
                foreach (PalettesPicking pal in Possible.Where(p => (p.Picked)))
                {
                    OrderNo = pal.OrderNo;
                    scanCU.AddPickingPallet(pal.PalletteSSCC, pal.OrderNo, pal.LineNo, pal.Qty);
                }
                scanCU.ValidatePicking(OrderNo);
                this.Close();
            }
            catch (Exception exc)
            {
                try
                {
                    System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();
                    MailMsg.From = new System.Net.Mail.MailAddress("ECScan@europacuisson.com");                    
                    MailMsg.To.Add(new System.Net.Mail.MailAddress("g.dangremont@europacuisson.com"));
                    MailMsg.Subject = "Erreur picking";
                    MailMsg.Body = exc.Message;
                    System.Net.Mail.SmtpClient SmtpCli = new System.Net.Mail.SmtpClient("192.168.5.30");
                    SmtpCli.Send(MailMsg);
                }
                catch (Exception exc1) { }
                MessageBox.Show(exc.Message);
            }
        }

        private void BtAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ScanOK()
        {
            if (Status == EStatus.Palettes)
            {
                PalettesPicking l = Possible.Find(p => p.PalletteSSCC.Equals(input_scan.Text) && !p.Picked && Math.Abs(p.QtyToPick) > 0);
                if (l == null)
                {
                    MessageBox.Show("Palette déjà scannée ou non trouvée");
                }
                else
                {
                    PalletPicked(l);
                }

                input_scan.Text = "";
            }
            if (Status == EStatus.Ordre)
            {
                GetPalettes();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ScanOK();
        }

        private void BtEnlogement_Click(object sender, EventArgs e)
        {
            Enlogement fEnlogement = new Enlogement();
            fEnlogement.ShowDialog();
        }
    }
}
