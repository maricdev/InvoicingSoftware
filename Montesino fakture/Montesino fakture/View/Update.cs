using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Montesino_fakture.View
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }

        private void btnProveri_Click(object sender, EventArgs e)
        {
            if (Uri.IsWellFormedUriString(txtLink.Text.Trim(), UriKind.Absolute))
            {
                Process.Start(txtLink.Text.Trim());
            }
            else
                MessageBox.Show("LINK nije u pravilnom obliku. Primer ispravnog linka: https://www.google.com/", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            if (Uri.IsWellFormedUriString(txtLink.Text.Trim(), UriKind.Absolute))
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["Update"].ConnectionString = txtLink.Text.Trim();
                connectionStringsSection.ConnectionStrings["Update"].ConnectionString = System.Net.WebUtility.HtmlDecode(connectionStringsSection.ConnectionStrings["Update"].ConnectionString);
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");               
                this.Close();
            }
            else
                MessageBox.Show("LINK nije u pravilnom obliku. Primer ispravnog linka: https://www.google.com/", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
