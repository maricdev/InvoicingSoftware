using System;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Montesino_fakture.View
{
    public partial class Baza : Form
    {
        private bool Ugasi = true;
        public Baza()
        {
            InitializeComponent();
        }

        private bool Test()
        {
            try
            {
                string strCon =
        "Data Source=" + txtServer.Text + ";" +
        "Initial Catalog=" + txtBaza.Text + ";" +
        "User id=" + txtKorisnickoIme.Text + ";" +
        "Password=" + txtLozinka.Text + ";";
                using (SqlConnection connection = new SqlConnection(strCon))
                {
                    connection.Open();
                    if (connection.State == ConnectionState.Open)
                    {
                        return true;
                    }
                    connection.Close();
                    if (connection.State == ConnectionState.Closed)
                    {
                        return false;
                    }
                    else
                        return false;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (Test() == true)
                MessageBox.Show("Veza je uspešno ostvarena!", "Povezano", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Neuspešno povezivanje na server!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void btnPotvrdi_Click(object sender, EventArgs e)
        {
            if (Test() == true)
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["MONTESINOEntities"].ConnectionString = "metadata=res://*/Model.Model.csdl|res://*/Model.Model.ssdl|res://*/Model.Model.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=" + txtServer.Text + ";initial catalog=" + txtBaza.Text + ";persist security info=True;user id=" + txtKorisnickoIme.Text + ";password=" + txtLozinka.Text + ";MultipleActiveResultSets=True;App=EntityFramework&quot;";
                connectionStringsSection.ConnectionStrings["MONTESINOEntities"].ConnectionString = System.Net.WebUtility.HtmlDecode(connectionStringsSection.ConnectionStrings["MONTESINOEntities"].ConnectionString);
                connectionStringsSection.ConnectionStrings["ispis"].ConnectionString = "Data Source=" + txtServer.Text + ";Initial Catalog=" + txtBaza.Text + ";Integrated Security=False;Persist Security Info=False;User ID=" + txtKorisnickoIme.Text + ";Password=" + txtLozinka.Text;
                connectionStringsSection.ConnectionStrings["ispis"].ConnectionString = System.Net.WebUtility.HtmlDecode(connectionStringsSection.ConnectionStrings["ispis"].ConnectionString);
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                Ugasi = false;
                this.Close();
            }
            else
                MessageBox.Show("Neuspešno povezivanje na server!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Baza_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(Ugasi == true) 
                Application.Exit();           
        }
    }
}