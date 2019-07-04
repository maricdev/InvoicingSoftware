namespace Montesino_fakture.View
{
    partial class InterakcijaArtikal
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
            this.txtSifra = new System.Windows.Forms.TextBox();
            this.lblSifra = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.txtCena = new System.Windows.Forms.TextBox();
            this.txtOpis = new System.Windows.Forms.TextBox();
            this.lblNaziv = new System.Windows.Forms.Label();
            this.lblCena = new System.Windows.Forms.Label();
            this.lblOpis = new System.Windows.Forms.Label();
            this.cmbJM = new DevExpress.XtraEditors.LookUpEdit();
            this.cmbPS = new DevExpress.XtraEditors.LookUpEdit();
            this.lblPostaNaziv = new System.Windows.Forms.Label();
            this.lblJM = new System.Windows.Forms.Label();
            this.lblPS = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCenaSaPorezom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpKontakt = new System.Windows.Forms.GroupBox();
            this.rbUsluga = new System.Windows.Forms.RadioButton();
            this.rbArtikal = new System.Windows.Forms.RadioButton();
            this.grpTIP = new System.Windows.Forms.GroupBox();
            this.checkAktivan = new System.Windows.Forms.CheckBox();
            this.btnOdustani = new DevExpress.XtraEditors.SimpleButton();
            this.btnOcisti = new DevExpress.XtraEditors.SimpleButton();
            this.btnPotvrdi = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbJM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPS.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpKontakt.SuspendLayout();
            this.grpTIP.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSifra
            // 
            this.txtSifra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtSifra.Location = new System.Drawing.Point(80, 42);
            this.txtSifra.MaxLength = 16;
            this.txtSifra.Name = "txtSifra";
            this.txtSifra.Size = new System.Drawing.Size(197, 27);
            this.txtSifra.TabIndex = 3;
            this.txtSifra.WordWrap = false;
            this.txtSifra.TextChanged += new System.EventHandler(this.txtSifra_TextChanged);
            // 
            // lblSifra
            // 
            this.lblSifra.AutoSize = true;
            this.lblSifra.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblSifra.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSifra.Location = new System.Drawing.Point(21, 46);
            this.lblSifra.Name = "lblSifra";
            this.lblSifra.Size = new System.Drawing.Size(45, 20);
            this.lblSifra.TabIndex = 24;
            this.lblSifra.Text = "Šifra*";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNaziv.Location = new System.Drawing.Point(80, 88);
            this.txtNaziv.MaxLength = 255;
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(197, 27);
            this.txtNaziv.TabIndex = 4;
            this.txtNaziv.WordWrap = false;
            this.txtNaziv.TextChanged += new System.EventHandler(this.txtNaziv_TextChanged);
            // 
            // txtCena
            // 
            this.txtCena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtCena.Location = new System.Drawing.Point(80, 131);
            this.txtCena.Name = "txtCena";
            this.txtCena.Size = new System.Drawing.Size(197, 27);
            this.txtCena.TabIndex = 5;
            this.txtCena.WordWrap = false;
            this.txtCena.TextChanged += new System.EventHandler(this.txtCena_TextChanged);
            this.txtCena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCena_KeyPress);
            this.txtCena.Leave += new System.EventHandler(this.txtCena_Leave);
            // 
            // txtOpis
            // 
            this.txtOpis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOpis.Location = new System.Drawing.Point(80, 171);
            this.txtOpis.Multiline = true;
            this.txtOpis.Name = "txtOpis";
            this.txtOpis.Size = new System.Drawing.Size(508, 76);
            this.txtOpis.TabIndex = 9;
            this.txtOpis.TextChanged += new System.EventHandler(this.txtOpis_TextChanged);
            // 
            // lblNaziv
            // 
            this.lblNaziv.AutoSize = true;
            this.lblNaziv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNaziv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNaziv.Location = new System.Drawing.Point(13, 89);
            this.lblNaziv.Name = "lblNaziv";
            this.lblNaziv.Size = new System.Drawing.Size(52, 20);
            this.lblNaziv.TabIndex = 28;
            this.lblNaziv.Text = "Naziv*";
            // 
            // lblCena
            // 
            this.lblCena.AutoSize = true;
            this.lblCena.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCena.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCena.Location = new System.Drawing.Point(17, 134);
            this.lblCena.Name = "lblCena";
            this.lblCena.Size = new System.Drawing.Size(48, 20);
            this.lblCena.TabIndex = 29;
            this.lblCena.Text = "Cena*";
            // 
            // lblOpis
            // 
            this.lblOpis.AutoSize = true;
            this.lblOpis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblOpis.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOpis.Location = new System.Drawing.Point(27, 175);
            this.lblOpis.Name = "lblOpis";
            this.lblOpis.Size = new System.Drawing.Size(39, 20);
            this.lblOpis.TabIndex = 30;
            this.lblOpis.Text = "Opis";
            // 
            // cmbJM
            // 
            this.cmbJM.Location = new System.Drawing.Point(402, 43);
            this.cmbJM.Name = "cmbJM";
            this.cmbJM.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.cmbJM.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbJM.Properties.Appearance.Options.UseFont = true;
            this.cmbJM.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.cmbJM.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbJM.Properties.NullText = "Izaberite JM";
            this.cmbJM.Size = new System.Drawing.Size(186, 26);
            this.cmbJM.TabIndex = 7;
            this.cmbJM.EditValueChanged += new System.EventHandler(this.cmbJM_EditValueChanged);
            // 
            // cmbPS
            // 
            this.cmbPS.Location = new System.Drawing.Point(402, 88);
            this.cmbPS.Name = "cmbPS";
            this.cmbPS.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.cmbPS.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbPS.Properties.Appearance.Options.UseFont = true;
            this.cmbPS.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.cmbPS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPS.Properties.NullText = "Izaberite poresku stopu";
            this.cmbPS.Size = new System.Drawing.Size(186, 26);
            this.cmbPS.TabIndex = 8;
            this.cmbPS.EditValueChanged += new System.EventHandler(this.cmbPS_EditValueChanged);
            // 
            // lblPostaNaziv
            // 
            this.lblPostaNaziv.AutoSize = true;
            this.lblPostaNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPostaNaziv.Location = new System.Drawing.Point(382, 314);
            this.lblPostaNaziv.Name = "lblPostaNaziv";
            this.lblPostaNaziv.Size = new System.Drawing.Size(0, 17);
            this.lblPostaNaziv.TabIndex = 33;
            // 
            // lblJM
            // 
            this.lblJM.AutoSize = true;
            this.lblJM.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblJM.Location = new System.Drawing.Point(357, 47);
            this.lblJM.Name = "lblJM";
            this.lblJM.Size = new System.Drawing.Size(33, 20);
            this.lblJM.TabIndex = 32;
            this.lblJM.Text = "JM*";
            // 
            // lblPS
            // 
            this.lblPS.AutoSize = true;
            this.lblPS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPS.Location = new System.Drawing.Point(301, 90);
            this.lblPS.Name = "lblPS";
            this.lblPS.Size = new System.Drawing.Size(82, 20);
            this.lblPS.TabIndex = 31;
            this.lblPS.Text = "Por. Stopa*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCenaSaPorezom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSifra);
            this.groupBox1.Controls.Add(this.cmbJM);
            this.groupBox1.Controls.Add(this.txtNaziv);
            this.groupBox1.Controls.Add(this.cmbPS);
            this.groupBox1.Controls.Add(this.lblJM);
            this.groupBox1.Controls.Add(this.txtCena);
            this.groupBox1.Controls.Add(this.lblSifra);
            this.groupBox1.Controls.Add(this.txtOpis);
            this.groupBox1.Controls.Add(this.lblPS);
            this.groupBox1.Controls.Add(this.lblNaziv);
            this.groupBox1.Controls.Add(this.lblOpis);
            this.groupBox1.Controls.Add(this.lblCena);
            this.groupBox1.Location = new System.Drawing.Point(11, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(605, 264);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " ";
            // 
            // txtCenaSaPorezom
            // 
            this.txtCenaSaPorezom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtCenaSaPorezom.Location = new System.Drawing.Point(402, 131);
            this.txtCenaSaPorezom.Name = "txtCenaSaPorezom";
            this.txtCenaSaPorezom.ReadOnly = true;
            this.txtCenaSaPorezom.Size = new System.Drawing.Size(186, 27);
            this.txtCenaSaPorezom.TabIndex = 37;
            this.txtCenaSaPorezom.TabStop = false;
            this.txtCenaSaPorezom.Text = "0.00 HRK";
            this.txtCenaSaPorezom.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(293, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "Sa porezom";
            // 
            // grpKontakt
            // 
            this.grpKontakt.Controls.Add(this.rbUsluga);
            this.grpKontakt.Controls.Add(this.rbArtikal);
            this.grpKontakt.Location = new System.Drawing.Point(11, 8);
            this.grpKontakt.Name = "grpKontakt";
            this.grpKontakt.Size = new System.Drawing.Size(370, 67);
            this.grpKontakt.TabIndex = 36;
            this.grpKontakt.TabStop = false;
            this.grpKontakt.Text = "Vrsta";
            // 
            // rbUsluga
            // 
            this.rbUsluga.AutoSize = true;
            this.rbUsluga.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbUsluga.Location = new System.Drawing.Point(202, 27);
            this.rbUsluga.Name = "rbUsluga";
            this.rbUsluga.Size = new System.Drawing.Size(75, 24);
            this.rbUsluga.TabIndex = 1;
            this.rbUsluga.Text = "Usluga";
            this.rbUsluga.UseVisualStyleBackColor = true;
            this.rbUsluga.CheckedChanged += new System.EventHandler(this.rbUsluga_CheckedChanged);
            // 
            // rbArtikal
            // 
            this.rbArtikal.AutoSize = true;
            this.rbArtikal.Checked = true;
            this.rbArtikal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rbArtikal.Location = new System.Drawing.Point(88, 26);
            this.rbArtikal.Name = "rbArtikal";
            this.rbArtikal.Size = new System.Drawing.Size(73, 24);
            this.rbArtikal.TabIndex = 0;
            this.rbArtikal.TabStop = true;
            this.rbArtikal.Text = "Artikal";
            this.rbArtikal.UseVisualStyleBackColor = true;
            this.rbArtikal.CheckedChanged += new System.EventHandler(this.rbArtikal_CheckedChanged);
            // 
            // grpTIP
            // 
            this.grpTIP.Controls.Add(this.checkAktivan);
            this.grpTIP.Location = new System.Drawing.Point(402, 8);
            this.grpTIP.Name = "grpTIP";
            this.grpTIP.Size = new System.Drawing.Size(215, 67);
            this.grpTIP.TabIndex = 37;
            this.grpTIP.TabStop = false;
            this.grpTIP.Text = "Stanje";
            // 
            // checkAktivan
            // 
            this.checkAktivan.AutoSize = true;
            this.checkAktivan.Checked = true;
            this.checkAktivan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkAktivan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkAktivan.Location = new System.Drawing.Point(77, 29);
            this.checkAktivan.Name = "checkAktivan";
            this.checkAktivan.Size = new System.Drawing.Size(80, 24);
            this.checkAktivan.TabIndex = 2;
            this.checkAktivan.Text = "Aktivan";
            this.checkAktivan.UseVisualStyleBackColor = true;
            this.checkAktivan.CheckedChanged += new System.EventHandler(this.checkAktivan_CheckedChanged);
            // 
            // btnOdustani
            // 
            this.btnOdustani.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOdustani.Appearance.Options.UseFont = true;
            this.btnOdustani.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOdustani.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(423, 351);
            this.btnOdustani.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnOdustani.LookAndFeel.UseWindowsXPTheme = true;
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(94, 29);
            this.btnOdustani.TabIndex = 11;
            this.btnOdustani.Text = "Odustani";
            // 
            // btnOcisti
            // 
            this.btnOcisti.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOcisti.Appearance.Options.UseFont = true;
            this.btnOcisti.Location = new System.Drawing.Point(11, 351);
            this.btnOcisti.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnOcisti.LookAndFeel.UseWindowsXPTheme = true;
            this.btnOcisti.Name = "btnOcisti";
            this.btnOcisti.Size = new System.Drawing.Size(94, 29);
            this.btnOcisti.TabIndex = 10;
            this.btnOcisti.Text = "Očisti polja";
            this.btnOcisti.Click += new System.EventHandler(this.btnOcisti_Click);
            // 
            // btnPotvrdi
            // 
            this.btnPotvrdi.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPotvrdi.Appearance.Options.UseFont = true;
            this.btnPotvrdi.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnPotvrdi.Location = new System.Drawing.Point(523, 351);
            this.btnPotvrdi.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnPotvrdi.LookAndFeel.UseWindowsXPTheme = true;
            this.btnPotvrdi.Name = "btnPotvrdi";
            this.btnPotvrdi.Size = new System.Drawing.Size(94, 29);
            this.btnPotvrdi.TabIndex = 12;
            this.btnPotvrdi.Text = "Potvrdi";
            this.btnPotvrdi.Click += new System.EventHandler(this.btnPotvrdi_Click);
            // 
            // InterakcijaArtikal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 385);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnOcisti);
            this.Controls.Add(this.btnPotvrdi);
            this.Controls.Add(this.grpTIP);
            this.Controls.Add(this.grpKontakt);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblPostaNaziv);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InterakcijaArtikal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InterakcijaArtikal";
            ((System.ComponentModel.ISupportInitialize)(this.cmbJM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPS.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpKontakt.ResumeLayout(false);
            this.grpKontakt.PerformLayout();
            this.grpTIP.ResumeLayout(false);
            this.grpTIP.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSifra;
        private System.Windows.Forms.Label lblSifra;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.TextBox txtCena;
        private System.Windows.Forms.TextBox txtOpis;
        private System.Windows.Forms.Label lblNaziv;
        private System.Windows.Forms.Label lblCena;
        private System.Windows.Forms.Label lblOpis;
        private DevExpress.XtraEditors.LookUpEdit cmbJM;
        private DevExpress.XtraEditors.LookUpEdit cmbPS;
        private System.Windows.Forms.Label lblPostaNaziv;
        private System.Windows.Forms.Label lblJM;
        private System.Windows.Forms.Label lblPS;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpKontakt;
        private System.Windows.Forms.RadioButton rbUsluga;
        private System.Windows.Forms.RadioButton rbArtikal;
        private System.Windows.Forms.GroupBox grpTIP;
        private System.Windows.Forms.CheckBox checkAktivan;
        private System.Windows.Forms.TextBox txtCenaSaPorezom;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnOdustani;
        private DevExpress.XtraEditors.SimpleButton btnOcisti;
        private DevExpress.XtraEditors.SimpleButton btnPotvrdi;
    }
}