using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanCongel
{
    public partial class ConfirmPalletPicking : Form
    {
        public PalettesPicking pallet;
        public ConfirmPalletPicking()
        {
            InitializeComponent();
        }

        private void ConfirmPalletPicking_Load(object sender, EventArgs e)
        {
            input_Quantity.Text = pallet.Qty.ToString();
            input_qty_restant.Text = "0";
            l_Emplacement.Text = pallet.BinNo;
            l_Lot.Text = pallet.LotNo;
            l_NumArticle.Text = pallet.ItemNo;
            l_SSCC.Text = pallet.PalletteSSCC;
            l_Qty.Text = pallet.Qty.ToString();
            if (pallet.Qty > pallet.QtyToPick)
            {
                input_Quantity.Text = pallet.QtyToPick.ToString();
                input_qty_restant.Text = (pallet.Qty - pallet.QtyToPick).ToString();
                l_Qty.ForeColor = Color.Red;
            }
        }

        public decimal GetQuantity()
        {
            return Convert.ToDecimal(input_Quantity.Text);
        }

        private void BtValid_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(input_Quantity.Text) > pallet.Qty)
            {
                MessageBox.Show("Erreur de quantité. La palette ne contient pas assez !");
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            if ((Convert.ToDecimal(input_Quantity.Text) + Convert.ToDecimal(input_qty_restant.Text)) != pallet.Qty)
            {
                System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();
                MailMsg.From = new System.Net.Mail.MailAddress("ECScan@europacuisson.com");
                MailMsg.To.Add(new System.Net.Mail.MailAddress("congelateur@europacuisson.com"));
                MailMsg.Subject = "Erreur stock sur palette";
                MailMsg.Body = "Article : "+pallet.ItemNo+"\nEmplacement "+pallet.BinNo+"\n  Palette "+pallet.PalletteSSCC+" \n Avant picking : "+pallet.Qty +"\n Pris : "+input_Quantity.Text+"\n Restant : "+input_qty_restant.Text;
                System.Net.Mail.SmtpClient SmtpCli = new System.Net.Mail.SmtpClient("192.168.5.30");
                SmtpCli.Send(MailMsg);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void input_Quantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                input_qty_restant.Text = (pallet.Qty - Convert.ToDecimal(input_Quantity.Text)).ToString();
            }
            catch (Exception exc)
            { }
        }
    }
}
