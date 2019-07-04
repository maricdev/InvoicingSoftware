namespace Montesino_fakture.View
{
    partial class InterakcijaSubjekat
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
            this.grpOsnovniPodaci = new System.Windows.Forms.GroupBox();
            this.cmbPosta = new DevExpress.XtraEditors.LookUpEdit();
            this.lblDrzava = new System.Windows.Forms.Label();
            this.cmbDrzava = new DevExpress.XtraEditors.LookUpEdit();
            this.txtOIB = new System.Windows.Forms.TextBox();
            this.lblPostaNaziv = new System.Windows.Forms.Label();
            this.lblOIB = new System.Windows.Forms.Label();
            this.lblAdresa = new System.Windows.Forms.Label();
            this.txtAdresa = new System.Windows.Forms.TextBox();
            this.lblPosta = new System.Windows.Forms.Label();
            this.txtPunNaziv = new System.Windows.Forms.TextBox();
            this.lblPunNaziv = new System.Windows.Forms.Label();
            this.txtNaziv = new System.Windows.Forms.TextBox();
            this.lblNaziv = new System.Windows.Forms.Label();
            this.grpTIP = new System.Windows.Forms.GroupBox();
            this.checkDobavljac = new System.Windows.Forms.CheckBox();
            this.checkKupac = new System.Windows.Forms.CheckBox();
            this.grpKontakt = new System.Windows.Forms.GroupBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTelefon = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtTelefon = new System.Windows.Forms.TextBox();
            this.btnPotvrdi = new DevExpress.XtraEditors.SimpleButton();
            this.btnOcisti = new DevExpress.XtraEditors.SimpleButton();
            this.btnOdustani = new DevExpress.XtraEditors.SimpleButton();
            this.grpOsnovniPodaci.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPosta.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDrzava.Properties)).BeginInit();
            this.grpTIP.SuspendLayout();
            this.grpKontakt.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpOsnovniPodaci
            // 
            this.grpOsnovniPodaci.Controls.Add(this.cmbPosta);
            this.grpOsnovniPodaci.Controls.Add(this.lblDrzava);
            this.grpOsnovniPodaci.Controls.Add(this.cmbDrzava);
            this.grpOsnovniPodaci.Controls.Add(this.txtOIB);
            this.grpOsnovniPodaci.Controls.Add(this.lblPostaNaziv);
            this.grpOsnovniPodaci.Controls.Add(this.lblOIB);
            this.grpOsnovniPodaci.Controls.Add(this.lblAdresa);
            this.grpOsnovniPodaci.Controls.Add(this.txtAdresa);
            this.grpOsnovniPodaci.Controls.Add(this.lblPosta);
            this.grpOsnovniPodaci.Controls.Add(this.txtPunNaziv);
            this.grpOsnovniPodaci.Controls.Add(this.lblPunNaziv);
            this.grpOsnovniPodaci.Controls.Add(this.txtNaziv);
            this.grpOsnovniPodaci.Controls.Add(this.lblNaziv);
            this.grpOsnovniPodaci.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grpOsnovniPodaci.Location = new System.Drawing.Point(11, 12);
            this.grpOsnovniPodaci.Name = "grpOsnovniPodaci";
            this.grpOsnovniPodaci.Size = new System.Drawing.Size(395, 333);
            this.grpOsnovniPodaci.TabIndex = 13;
            this.grpOsnovniPodaci.TabStop = false;
            this.grpOsnovniPodaci.Text = "Osnovni podaci";
            // 
            // cmbPosta
            // 
            this.cmbPosta.Location = new System.Drawing.Point(122, 294);
            this.cmbPosta.Name = "cmbPosta";
            this.cmbPosta.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.cmbPosta.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbPosta.Properties.Appearance.Options.UseFont = true;
            this.cmbPosta.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.cmbPosta.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPosta.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbPosta.Properties.LookAndFeel.UseWindowsXPTheme = true;
            this.cmbPosta.Properties.NullText = "";
            this.cmbPosta.Size = new System.Drawing.Size(197, 26);
            this.cmbPosta.TabIndex = 5;
            this.cmbPosta.EditValueChanged += new System.EventHandler(this.cmbPosta_EditValueChanged);
            // 
            // lblDrzava
            // 
            this.lblDrzava.AutoSize = true;
            this.lblDrzava.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDrzava.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblDrzava.Location = new System.Drawing.Point(50, 244);
            this.lblDrzava.Name = "lblDrzava";
            this.lblDrzava.Size = new System.Drawing.Size(55, 20);
            this.lblDrzava.TabIndex = 15;
            this.lblDrzava.Text = "Država";
            // 
            // cmbDrzava
            // 
            this.cmbDrzava.Location = new System.Drawing.Point(122, 242);
            this.cmbDrzava.Name = "cmbDrzava";
            this.cmbDrzava.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.cmbDrzava.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cmbDrzava.Properties.Appearance.Options.UseFont = true;
            this.cmbDrzava.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.cmbDrzava.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbDrzava.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.cmbDrzava.Properties.LookAndFeel.UseWindowsXPTheme = true;
            this.cmbDrzava.Properties.NullText = "";
            this.cmbDrzava.Size = new System.Drawing.Size(197, 26);
            this.cmbDrzava.TabIndex = 4;
            this.cmbDrzava.EditValueChanged += new System.EventHandler(this.cmbDrzava_EditValueChanged);
            // 
            // txtOIB
            // 
            this.txtOIB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtOIB.Location = new System.Drawing.Point(122, 145);
            this.txtOIB.MaxLength = 11;
            this.txtOIB.Name = "txtOIB";
            this.txtOIB.Size = new System.Drawing.Size(197, 27);
            this.txtOIB.TabIndex = 2;
            this.txtOIB.WordWrap = false;
            this.txtOIB.TextChanged += new System.EventHandler(this.txtOIB_TextChanged);
            // 
            // lblPostaNaziv
            // 
            this.lblPostaNaziv.AutoSize = true;
            this.lblPostaNaziv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.2F);
            this.lblPostaNaziv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPostaNaziv.Location = new System.Drawing.Point(122, 275);
            this.lblPostaNaziv.Name = "lblPostaNaziv";
            this.lblPostaNaziv.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPostaNaziv.Size = new System.Drawing.Size(92, 17);
            this.lblPostaNaziv.TabIndex = 19;
            this.lblPostaNaziv.Text = "lvlPostaNaziv";
            // 
            // lblOIB
            // 
            this.lblOIB.AutoSize = true;
            this.lblOIB.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblOIB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblOIB.Location = new System.Drawing.Point(72, 147);
            this.lblOIB.Name = "lblOIB";
            this.lblOIB.Size = new System.Drawing.Size(33, 20);
            this.lblOIB.TabIndex = 11;
            this.lblOIB.Text = "OIB";
            // 
            // lblAdresa
            // 
            this.lblAdresa.AutoSize = true;
            this.lblAdresa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblAdresa.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAdresa.Location = new System.Drawing.Point(50, 195);
            this.lblAdresa.Name = "lblAdresa";
            this.lblAdresa.Size = new System.Drawing.Size(55, 20);
            this.lblAdresa.TabIndex = 14;
            this.lblAdresa.Text = "Adresa";
            // 
            // txtAdresa
            // 
            this.txtAdresa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtAdresa.Location = new System.Drawing.Point(122, 193);
            this.txtAdresa.MaxLength = 60;
            this.txtAdresa.Name = "txtAdresa";
            this.txtAdresa.Size = new System.Drawing.Size(197, 27);
            this.txtAdresa.TabIndex = 3;
            this.txtAdresa.WordWrap = false;
            this.txtAdresa.TextChanged += new System.EventHandler(this.txtAdresa_TextChanged);
            // 
            // lblPosta
            // 
            this.lblPosta.AutoSize = true;
            this.lblPosta.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPosta.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPosta.Location = new System.Drawing.Point(5, 296);
            this.lblPosta.Name = "lblPosta";
            this.lblPosta.Size = new System.Drawing.Size(100, 20);
            this.lblPosta.TabIndex = 18;
            this.lblPosta.Text = "Poštanski broj";
            // 
            // txtPunNaziv
            // 
            this.txtPunNaziv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtPunNaziv.Location = new System.Drawing.Point(122, 82);
            this.txtPunNaziv.MaxLength = 255;
            this.txtPunNaziv.Multiline = true;
            this.txtPunNaziv.Name = "txtPunNaziv";
            this.txtPunNaziv.Size = new System.Drawing.Size(252, 43);
            this.txtPunNaziv.TabIndex = 1;
            this.txtPunNaziv.TextChanged += new System.EventHandler(this.txtPunNaziv_TextChanged);
            // 
            // lblPunNaziv
            // 
            this.lblPunNaziv.AutoSize = true;
            this.lblPunNaziv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblPunNaziv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPunNaziv.Location = new System.Drawing.Point(34, 84);
            this.lblPunNaziv.Name = "lblPunNaziv";
            this.lblPunNaziv.Size = new System.Drawing.Size(71, 20);
            this.lblPunNaziv.TabIndex = 6;
            this.lblPunNaziv.Text = "Pun naziv";
            // 
            // txtNaziv
            // 
            this.txtNaziv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNaziv.Location = new System.Drawing.Point(122, 36);
            this.txtNaziv.MaxLength = 30;
            this.txtNaziv.Name = "txtNaziv";
            this.txtNaziv.Size = new System.Drawing.Size(197, 27);
            this.txtNaziv.TabIndex = 0;
            this.txtNaziv.WordWrap = false;
            this.txtNaziv.TextChanged += new System.EventHandler(this.txtNaziv_TextChanged);
            // 
            // lblNaziv
            // 
            this.lblNaziv.AutoSize = true;
            this.lblNaziv.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNaziv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblNaziv.Location = new System.Drawing.Point(58, 39);
            this.lblNaziv.Name = "lblNaziv";
            this.lblNaziv.Size = new System.Drawing.Size(46, 20);
            this.lblNaziv.TabIndex = 5;
            this.lblNaziv.Text = "Naziv";
            // 
            // grpTIP
            // 
            this.grpTIP.Controls.Add(this.checkDobavljac);
            this.grpTIP.Controls.Add(this.checkKupac);
            this.grpTIP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grpTIP.Location = new System.Drawing.Point(425, 11);
            this.grpTIP.Name = "grpTIP";
            this.grpTIP.Size = new System.Drawing.Size(302, 114);
            this.grpTIP.TabIndex = 15;
            this.grpTIP.TabStop = false;
            this.grpTIP.Text = "Tip";
            // 
            // checkDobavljac
            // 
            this.checkDobavljac.AutoSize = true;
            this.checkDobavljac.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkDobavljac.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkDobavljac.Location = new System.Drawing.Point(162, 49);
            this.checkDobavljac.Name = "checkDobavljac";
            this.checkDobavljac.Size = new System.Drawing.Size(98, 24);
            this.checkDobavljac.TabIndex = 7;
            this.checkDobavljac.Text = "Dobavljač";
            this.checkDobavljac.UseVisualStyleBackColor = true;
            this.checkDobavljac.CheckedChanged += new System.EventHandler(this.checkDobavljac_CheckedChanged);
            // 
            // checkKupac
            // 
            this.checkKupac.AutoSize = true;
            this.checkKupac.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.checkKupac.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkKupac.Location = new System.Drawing.Point(49, 49);
            this.checkKupac.Name = "checkKupac";
            this.checkKupac.Size = new System.Drawing.Size(72, 24);
            this.checkKupac.TabIndex = 6;
            this.checkKupac.Text = "Kupac";
            this.checkKupac.UseVisualStyleBackColor = true;
            this.checkKupac.CheckedChanged += new System.EventHandler(this.checkKupac_CheckedChanged);
            // 
            // grpKontakt
            // 
            this.grpKontakt.Controls.Add(this.lblEmail);
            this.grpKontakt.Controls.Add(this.lblTelefon);
            this.grpKontakt.Controls.Add(this.txtEmail);
            this.grpKontakt.Controls.Add(this.txtTelefon);
            this.grpKontakt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.grpKontakt.Location = new System.Drawing.Point(425, 141);
            this.grpKontakt.Name = "grpKontakt";
            this.grpKontakt.Size = new System.Drawing.Size(302, 204);
            this.grpKontakt.TabIndex = 16;
            this.grpKontakt.TabStop = false;
            this.grpKontakt.Text = "Kontakt";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblEmail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEmail.Location = new System.Drawing.Point(19, 133);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(46, 20);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email";
            // 
            // lblTelefon
            // 
            this.lblTelefon.AutoSize = true;
            this.lblTelefon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTelefon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTelefon.Location = new System.Drawing.Point(8, 70);
            this.lblTelefon.Name = "lblTelefon";
            this.lblTelefon.Size = new System.Drawing.Size(58, 20);
            this.lblTelefon.TabIndex = 2;
            this.lblTelefon.Text = "Telefon";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtEmail.Location = new System.Drawing.Point(81, 130);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(197, 27);
            this.txtEmail.TabIndex = 9;
            this.txtEmail.WordWrap = false;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // txtTelefon
            // 
            this.txtTelefon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtTelefon.Location = new System.Drawing.Point(81, 67);
            this.txtTelefon.MaxLength = 50;
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(197, 27);
            this.txtTelefon.TabIndex = 8;
            this.txtTelefon.WordWrap = false;
            this.txtTelefon.TextChanged += new System.EventHandler(this.txtTelefon_TextChanged);
            // 
            // btnPotvrdi
            // 
            this.btnPotvrdi.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnPotvrdi.Appearance.Options.UseFont = true;
            this.btnPotvrdi.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnPotvrdi.Location = new System.Drawing.Point(633, 351);
            this.btnPotvrdi.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnPotvrdi.LookAndFeel.UseWindowsXPTheme = true;
            this.btnPotvrdi.Name = "btnPotvrdi";
            this.btnPotvrdi.Size = new System.Drawing.Size(94, 29);
            this.btnPotvrdi.TabIndex = 12;
            this.btnPotvrdi.Text = "Potvrdi";
            this.btnPotvrdi.Click += new System.EventHandler(this.btnPotvrdi_Click);
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
            // btnOdustani
            // 
            this.btnOdustani.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOdustani.Appearance.Options.UseFont = true;
            this.btnOdustani.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOdustani.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnOdustani.Location = new System.Drawing.Point(533, 351);
            this.btnOdustani.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnOdustani.LookAndFeel.UseWindowsXPTheme = true;
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(94, 29);
            this.btnOdustani.TabIndex = 11;
            this.btnOdustani.Text = "Odustani";
            // 
            // InterakcijaSubjekat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOdustani;
            this.ClientSize = new System.Drawing.Size(734, 385);
            this.Controls.Add(this.btnOdustani);
            this.Controls.Add(this.btnOcisti);
            this.Controls.Add(this.btnPotvrdi);
            this.Controls.Add(this.grpKontakt);
            this.Controls.Add(this.grpTIP);
            this.Controls.Add(this.grpOsnovniPodaci);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InterakcijaSubjekat";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InterakcijaSubjekat";
            this.grpOsnovniPodaci.ResumeLayout(false);
            this.grpOsnovniPodaci.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPosta.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDrzava.Properties)).EndInit();
            this.grpTIP.ResumeLayout(false);
            this.grpTIP.PerformLayout();
            this.grpKontakt.ResumeLayout(false);
            this.grpKontakt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpOsnovniPodaci;
        private DevExpress.XtraEditors.LookUpEdit cmbPosta;
        private DevExpress.XtraEditors.LookUpEdit cmbDrzava;
        private System.Windows.Forms.Label lblPostaNaziv;
        private System.Windows.Forms.Label lblPosta;
        private System.Windows.Forms.Label lblDrzava;
        private System.Windows.Forms.Label lblAdresa;
        private System.Windows.Forms.TextBox txtAdresa;
        private System.Windows.Forms.Label lblOIB;
        private System.Windows.Forms.TextBox txtOIB;
        private System.Windows.Forms.TextBox txtPunNaziv;
        private System.Windows.Forms.Label lblPunNaziv;
        private System.Windows.Forms.TextBox txtNaziv;
        private System.Windows.Forms.Label lblNaziv;
        private System.Windows.Forms.GroupBox grpTIP;
        private System.Windows.Forms.CheckBox checkDobavljac;
        private System.Windows.Forms.CheckBox checkKupac;
        private System.Windows.Forms.GroupBox grpKontakt;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTelefon;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtTelefon;
        private DevExpress.XtraEditors.SimpleButton btnPotvrdi;
        private DevExpress.XtraEditors.SimpleButton btnOcisti;
        private DevExpress.XtraEditors.SimpleButton btnOdustani;
    }
}