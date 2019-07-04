using Montesino_fakture.Model;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace Montesino_fakture.View
{
    public partial class Firma : Form
    {
        public Firma()
        {
            try
            {
                InitializeComponent();
                popuniSubjekte();
                popuniValute();
                popuniNP();
                popuniOdgovorneOsobe();
                popuniStatusPredracun();
                popuniStatusRacun();
                popuniDefault();
                txtAdresa.SelectAll();
                txtEmail.SelectAll();
                txtImeIPrezime.SelectAll();
                txtOIB.SelectAll();
                txtPunNaziv.SelectAll();
                txtTelefon.SelectAll();
                txtValutaNaziv.SelectAll();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }

        private void popuniDefault()
        {
            try
            {
                if (MainForm.getData.GetTablePodesavanja().Rows.Count > 0)
                {
                    DataRow sub = MainForm.getData.GetTablePodesavanja().Rows[0];
                    cmbSubjekat.EditValue = sub["Subjekat_ID"].ToString().Trim();
                    cmbValuta.EditValue = sub["Valuta_ID"].ToString().Trim();
                    cmbOdgovornaOsoba.EditValue = sub["OdgovorneOsobe_ID"].ToString().Trim();
                    cmbNP.EditValue = sub["NP_ID"].ToString().Trim();
                    cmbStatusPredracun.EditValue = sub["StatusPredracun_ID"].ToString().Trim();
                    cmbStatusRacun.EditValue = sub["StatusRacun_ID"].ToString().Trim();
                    txtRokVazenja.Text = sub["RokVazenja"].ToString().Trim();
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }

        private void popuniSubjekte() // POPUNJAVA SUBJEKTE U LOOKUPEDIT
        {
            try
            {
                cmbSubjekat.Properties.DataSource = MainForm.getData.GetTableSubjekti();
                cmbSubjekat.Properties.ValueMember = "Subjekat_ID";
                cmbSubjekat.Properties.DisplayMember = "Naziv";
                cmbSubjekat.Properties.ForceInitialize();
                cmbSubjekat.Properties.PopulateColumns();
                cmbSubjekat.Properties.Columns["Subjekat_ID"].Visible = false;
                cmbSubjekat.Properties.Columns["jeKupac"].Visible = false;
                cmbSubjekat.Properties.Columns["jeDobavljac"].Visible = false;
                cmbSubjekat.Properties.Columns["Drzava_Naziv"].Visible = false;
                cmbSubjekat.Properties.Columns["Posta_Naziv"].Visible = false;
                cmbSubjekat.Properties.Columns["Posta_ID"].Visible = false;
                cmbSubjekat.Properties.Columns["Drzava_ID"].Visible = false;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }
        private void popuniStatusPredracun()
        {
            try
            {
                cmbStatusPredracun.Properties.DataSource = MainForm.getData.GetTableStatusi("PON");
                cmbStatusPredracun.Properties.ValueMember = "Status_ID";
                cmbStatusPredracun.Properties.DisplayMember = "Naziv";
                cmbStatusPredracun.Properties.ForceInitialize();
                cmbStatusPredracun.Properties.PopulateColumns();
                cmbStatusPredracun.Properties.Columns["Status_ID"].Visible = false;
                cmbStatusPredracun.Properties.Columns["jePredracun"].Visible = false;
                cmbStatusPredracun.Properties.Columns["jeRacun"].Visible = false;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }
        private void popuniStatusRacun()
        {
            try
            {
                cmbStatusRacun.Properties.DataSource = MainForm.getData.GetTableStatusi("RAC");
                cmbStatusRacun.Properties.ValueMember = "Status_ID";
                cmbStatusRacun.Properties.DisplayMember = "Naziv";
                cmbStatusRacun.Properties.ForceInitialize();
                cmbStatusRacun.Properties.PopulateColumns();
                cmbStatusRacun.Properties.Columns["Status_ID"].Visible = false;
                cmbStatusRacun.Properties.Columns["jePredracun"].Visible = false;
                cmbStatusRacun.Properties.Columns["jeRacun"].Visible = false;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }
        private void popuniValute() // POPUNJAVA VALUTE U LOOKUPEDIT
        {
            try
            {
                cmbValuta.Properties.DataSource = MainForm.getData.GetTableValute();
                cmbValuta.Properties.ValueMember = "Valuta_ID";
                cmbValuta.Properties.DisplayMember = "Oznaka";
                cmbValuta.Properties.ForceInitialize();
                cmbValuta.Properties.PopulateColumns();
                cmbValuta.Properties.Columns["Valuta_ID"].Visible = false;
                cmbValuta.Enabled = false;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }

        private void popuniOdgovorneOsobe() // POPUNJAVA ODGOVORNE OSOBE U LOOKUPEDIT
        {
            try
            {
                cmbOdgovornaOsoba.Properties.DataSource = MainForm.getData.GetTableOdgovorneOsobe();
                cmbOdgovornaOsoba.Properties.ValueMember = "OdgovorneOsobe_ID";
                cmbOdgovornaOsoba.Properties.DisplayMember = "Naziv";
                cmbOdgovornaOsoba.Properties.ForceInitialize();
                cmbOdgovornaOsoba.Properties.PopulateColumns();
                cmbOdgovornaOsoba.Properties.Columns["OdgovorneOsobe_ID"].Visible = false;
                cmbOdgovornaOsoba.Properties.Columns["Naziv"].Caption = "Ime i Prezime";
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }
        private void popuniNP() // POPUNJAVA ODGOVORNE OSOBE U LOOKUPEDIT
        {
            try
            {
                cmbNP.Properties.DataSource = MainForm.getData.GetTableNP();
                cmbNP.Properties.ValueMember = "NP_ID";
                cmbNP.Properties.DisplayMember = "Nacin";
                cmbNP.Properties.ForceInitialize();
                cmbNP.Properties.PopulateColumns();
                cmbNP.Properties.Columns["NP_ID"].Visible = false;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }
        private void cmbSubjekat_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)cmbSubjekat.GetSelectedDataRow();
                if (rowView != null)
                {
                    DataRow red = rowView.Row;
                    txtPunNaziv.Text = red["Naziv2"].ToString();
                    txtOIB.Text = red["OIB"].ToString();
                    txtAdresa.Text = red["Adresa"].ToString();
                    txtEmail.Text = red["Email"].ToString();
                    txtTelefon.Text = red["Telefon"].ToString();
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }

        private void cmbValuta_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)cmbValuta.GetSelectedDataRow();
                if (rowView != null)
                {
                    DataRow red = rowView.Row;
                    txtValutaNaziv.Text = red["Naziv"].ToString();
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }

        private void cmbOdgovornaOsoba_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)cmbOdgovornaOsoba.GetSelectedDataRow();
                if (rowView != null)
                {
                    DataRow red = rowView.Row;
                    txtImeIPrezime.Text = red["Naziv"].ToString();
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }

        private void btnPotvrdi_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSubjekat.Text.Trim() != cmbSubjekat.Properties.NullText.Trim() && cmbSubjekat.Text.Trim() != "" && cmbValuta.Text.Trim() != cmbValuta.Properties.NullText.Trim() && cmbValuta.Text.Trim() != "" && cmbOdgovornaOsoba.Text.Trim() != cmbOdgovornaOsoba.Properties.NullText.Trim() && cmbOdgovornaOsoba.Text.Trim() != "")
                {
                    DataRowView rowViewSubjekat = (DataRowView)cmbSubjekat.GetSelectedDataRow();
                    DataRowView rowViewValuta = (DataRowView)cmbValuta.GetSelectedDataRow();
                    DataRowView rowViewOdgovornaOsoba = (DataRowView)cmbOdgovornaOsoba.GetSelectedDataRow();
                    DataRowView rowViewStatusPredracun = (DataRowView)cmbStatusPredracun.GetSelectedDataRow();
                    DataRowView rowViewStatusRacun = (DataRowView)cmbStatusRacun.GetSelectedDataRow();
                    DataRow redSubjekat = rowViewSubjekat.Row;
                    DataRow redValuta = rowViewValuta.Row;
                    DataRow redOdgovornaOsoba = rowViewOdgovornaOsoba.Row;
                    DataRow redStatusPredracun = rowViewStatusPredracun.Row;
                    DataRow redStatusRacun = rowViewStatusRacun.Row;

                    if (MainForm.getData.GetTablePodesavanja().Rows.Count > 0) // IZMENI PODESAVANJA
                    {
                        using (var con = new MONTESINOEntities())
                        {
                            var temp = con.Podesavanjes.First();
                            temp.Subjekat_ID = Convert.ToInt32(redSubjekat["Subjekat_ID"]);
                            temp.Valuta_ID = Convert.ToInt32(redValuta["Valuta_ID"]);
                            temp.OdgovorneOsobe_ID = Convert.ToInt32(redOdgovornaOsoba["OdgovorneOsobe_ID"]);
                            temp.StatusPredracun_ID = Convert.ToInt32(redStatusPredracun["Status_ID"]);
                            temp.StatusRacun_ID = Convert.ToInt32(redStatusRacun["Status_ID"]);
                            temp.RokVazenja = Convert.ToInt16(txtRokVazenja.Text.Trim());
                            con.SaveChanges();
                            this.Close();
                        }
                    }
                    else // NAPRAVI PODESAVANJA
                    {
                        using (var con = new MONTESINOEntities())
                        {
                            var novaPod = new Podesavanje()
                            {
                                Subjekat_ID = Convert.ToInt32(redSubjekat["Subjekat_ID"]),
                                Valuta_ID = Convert.ToInt32(redValuta["Valuta_ID"]),
                                OdgovorneOsobe_ID = Convert.ToInt32(redOdgovornaOsoba["OdgovorneOsobe_ID"]),
                                RokVazenja = Convert.ToInt16(txtRokVazenja.Text.Trim())
                            };
                            con.Podesavanjes.Add(novaPod);
                            con.SaveChanges();
                            this.Close();
                        }
                    }
                }
                else
                    MessageBox.Show("Morate popuniti sva polja!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }

        private void cmbNP_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)cmbNP.GetSelectedDataRow();
                if (rowView != null)
                {
                    DataRow red = rowView.Row;
                    txtNP.Text = red["Nacin"].ToString();
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }

        private void txtRokVazenja_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cmbStatusPredracun_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)cmbStatusPredracun.GetSelectedDataRow();
                if (rowView != null)
                {
                    DataRow red = rowView.Row;
                    txtStatusPredracun.Text = red["Naziv"].ToString();
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }

        private void cmbStatusRacun_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)cmbStatusRacun.GetSelectedDataRow();
                if (rowView != null)
                {
                    DataRow red = rowView.Row;
                    txtStatusRacun.Text = red["Naziv"].ToString();
                }
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
            }
        }
    }
}