namespace ScanCongel
{
    partial class Picking
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Picking));
            this.input_scan = new System.Windows.Forms.TextBox();
            this.lTitle = new System.Windows.Forms.Label();
            this.listPalettes = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listLots = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.BtPickingOk = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // input_scan
            // 
            this.input_scan.BackColor = System.Drawing.Color.Lime;
            this.input_scan.Location = new System.Drawing.Point(12, 30);
            this.input_scan.Name = "input_scan";
            this.input_scan.Size = new System.Drawing.Size(188, 20);
            this.input_scan.TabIndex = 4;
            this.input_scan.TextChanged += new System.EventHandler(this.input_scan_TextChanged);
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.Location = new System.Drawing.Point(12, 9);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(52, 18);
            this.lTitle.TabIndex = 5;
            this.lTitle.Text = "label1";
            // 
            // listPalettes
            // 
            this.listPalettes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listPalettes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listPalettes.HideSelection = false;
            this.listPalettes.LabelWrap = false;
            this.listPalettes.Location = new System.Drawing.Point(4, 55);
            this.listPalettes.Name = "listPalettes";
            this.listPalettes.Size = new System.Drawing.Size(235, 118);
            this.listPalettes.TabIndex = 6;
            this.listPalettes.UseCompatibleStateImageBehavior = false;
            this.listPalettes.Visible = false;
            // 
            // listLots
            // 
            this.listLots.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listLots.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listLots.HideSelection = false;
            this.listLots.LabelWrap = false;
            this.listLots.Location = new System.Drawing.Point(4, 182);
            this.listLots.Name = "listLots";
            this.listLots.Size = new System.Drawing.Size(236, 77);
            this.listLots.TabIndex = 7;
            this.listLots.UseCompatibleStateImageBehavior = false;
            this.listLots.Visible = false;
            // 
            // BtPickingOk
            // 
            this.BtPickingOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(114)))), ((int)(((byte)(114)))));
            this.BtPickingOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.BtPickingOk.ForeColor = System.Drawing.Color.White;
            this.BtPickingOk.Location = new System.Drawing.Point(12, 136);
            this.BtPickingOk.Name = "BtPickingOk";
            this.BtPickingOk.Size = new System.Drawing.Size(216, 48);
            this.BtPickingOk.TabIndex = 8;
            this.BtPickingOk.Text = "Picking Terminé";
            this.BtPickingOk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtPickingOk.Visible = false;
            this.BtPickingOk.Click += new System.EventHandler(this.BtPickingOk_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(207, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(178, 265);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(50, 50);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.BtEnlogement_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(12, 265);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(50, 50);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 30;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.BtAnnuler_Click);
            // 
            // Picking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(240, 320);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BtPickingOk);
            this.Controls.Add(this.listLots);
            this.Controls.Add(this.listPalettes);
            this.Controls.Add(this.lTitle);
            this.Controls.Add(this.input_scan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Picking";
            this.Text = "Picking";
            this.Load += new System.EventHandler(this.Picking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox input_scan;
        private System.Windows.Forms.Label lTitle;
        private System.Windows.Forms.ListView listPalettes;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView listLots;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label BtPickingOk;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}