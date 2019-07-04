namespace Montesino_fakture.View
{
    partial class Update
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.btnProveri = new DevExpress.XtraEditors.SimpleButton();
            this.btnSacuvaj = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "LINK";
            // 
            // txtLink
            // 
            this.txtLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtLink.Location = new System.Drawing.Point(58, 19);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(292, 24);
            this.txtLink.TabIndex = 1;
            // 
            // btnProveri
            // 
            this.btnProveri.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnProveri.Appearance.Options.UseFont = true;
            this.btnProveri.Location = new System.Drawing.Point(356, 16);
            this.btnProveri.LookAndFeel.SkinName = "Office 2013 Light Gray";
            this.btnProveri.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnProveri.Name = "btnProveri";
            this.btnProveri.Size = new System.Drawing.Size(74, 29);
            this.btnProveri.TabIndex = 2;
            this.btnProveri.Text = "Proveri";
            this.btnProveri.Click += new System.EventHandler(this.btnProveri_Click);
            // 
            // btnSacuvaj
            // 
            this.btnSacuvaj.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnSacuvaj.Appearance.Options.UseFont = true;
            this.btnSacuvaj.Location = new System.Drawing.Point(436, 16);
            this.btnSacuvaj.LookAndFeel.SkinName = "Office 2013 Light Gray";
            this.btnSacuvaj.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSacuvaj.Name = "btnSacuvaj";
            this.btnSacuvaj.Size = new System.Drawing.Size(74, 29);
            this.btnSacuvaj.TabIndex = 3;
            this.btnSacuvaj.Text = "Sačuvaj";
            this.btnSacuvaj.Click += new System.EventHandler(this.btnSacuvaj_Click);
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 61);
            this.Controls.Add(this.btnSacuvaj);
            this.Controls.Add(this.btnProveri);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Update";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Podešavanje linka za ažuriranje";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLink;
        private DevExpress.XtraEditors.SimpleButton btnProveri;
        private DevExpress.XtraEditors.SimpleButton btnSacuvaj;
    }
}