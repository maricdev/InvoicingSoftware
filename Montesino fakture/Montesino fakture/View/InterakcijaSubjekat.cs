using Montesino_fakture.Model;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace Montesino_fakture.View
{
    public partial class InterakcijaSubjekat : Form
    {
        private Subjekat sub = null;

        public InterakcijaSubjekat() // KADA SE KREIRA SUBJEKAT
        {
            try
            {
                //PODESAVANJE POCETNO
                InitializeComponent();
                Text = "Kreiranje novog subjekta";
                checkKupac.Checked = true;
                popuniDrzave();
                txtNaziv.SelectAll();
                txtPunNaziv.SelectAll();
                txtOIB.SelectAll();
                txtEmail.SelectAll();
                txtAdresa.SelectAll();
                txtTelefon.SelectAll();
                lblPostaNaziv.Text = "";
                this.sub = null;
                //PODESAVANJE POCETNO
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

        public InterakcijaSubjekat(Subjekat sub) // KADA SE PRAVI IZMENA NA SUBJEKTU
        {
            try
            {
                //PODESAVANJE POCETNO
                InitializeComponent();
                Text = "Izmena subjekta";
                this.sub = sub;
                btnOcisti.Visible = false;
                btnPotvrdi.Enabled = false;
                //PODESAVANJE POCETNO
                popuniDrzave();
                popuniPoste(Convert.ToInt32(sub.Drzava_ID));

                txtNaziv.Text = sub.Naziv;
                txtPunNaziv.Text = sub.Naziv2;
                txtOIB.Text = sub.OIB;
                txtAdresa.Text = sub.Adresa;
                checkKupac.Checked = Convert.ToBoolean(Convert.ToInt32(sub.jeKupac));
                checkDobavljac.Checked = Convert.ToBoolean(Convert.ToInt32(sub.jeDobavljac));
                txtTelefon.Text = sub.Telefon;
                txtEmail.Text = sub.Email;

                ProveraPromene();
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
                if (sub == null)
                    NapraviSubjekta();
                else
                    IzmeniSubjekta();
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

        private void btnOcisti_Click(object sender, EventArgs e) // BRISE SVA POLJA
        {
            try
            {
                txtNaziv.Clear();
                txtPunNaziv.Clear();
                txtOIB.Clear();
                txtAdresa.Clear();
                checkKupac.CheckState = 0;
                checkDobavljac.CheckState = 0;
                txtTelefon.Clear();
                txtEmail.Clear();
                cmbDrzava.EditValue = null;
                cmbPosta.EditValue = null;
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

        private void popuniDrzave() // POPUNJAVA DRZAVE U LOOKUPEDIT
        {
            try
            {
                cmbDrzava.Properties.DataSource = MainForm.getData.GetTableDrzava();
                cmbDrzava.Properties.ValueMember = "Drzava_ID";
                cmbDrzava.Properties.DisplayMember = "Naziv";
                cmbDrzava.Properties.ForceInitialize();
                cmbDrzava.Properties.PopulateColumns();
                cmbDrzava.Properties.Columns["Drzava_ID"].Visible = false;

                if (sub != null)
                {
                    int brojac = 0;
                    foreach (DataRow red in MainForm.getData.GetTableDrzava().Rows)
                    {
                        if (red["Drzava_ID"].ToString().Trim() == this.sub.Drzava_ID.ToString().Trim())
                        {
                            cmbDrzava.ItemIndex = brojac;
                            cmbDrzava.EditValue = red["Drzava_ID"].ToString().Trim();
                            break;
                        }
                        brojac++;
                    }

                    ProveraPromene();
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

        private void popuniPoste(int Drzava_ID) // POPUNJAVA POSTE PO DRZAVA_ID U LOOKUPEDIT
        {
            try
            {
                cmbPosta.Properties.DataSource = MainForm.getData.GetTablePosteByDrzavaID(Drzava_ID);
                cmbPosta.Properties.ValueMember = "Posta_ID";
                cmbPosta.Properties.DisplayMember = "Broj";
                cmbPosta.Properties.ForceInitialize();
                cmbPosta.Properties.PopulateColumns();
                cmbPosta.Properties.Columns["Posta_ID"].Visible = false;
                cmbPosta.Properties.Columns["Drzava_ID"].Visible = false;

                if (sub != null)
                {
                    int brojac = 0;
                    foreach (DataRow red in MainForm.getData.GetTablePoste().Rows)
                    {
                        if (red["Posta_ID"].ToString().Trim() == this.sub.Posta_ID.ToString().Trim())
                        {
                            cmbPosta.ItemIndex = brojac;
                            cmbPosta.EditValue = red["Posta_ID"].ToString().Trim();
                            break;
                        }
                        brojac++;
                    }
                }
                ProveraPromene();
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

        private void cmbPosta_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)cmbPosta.GetSelectedDataRow();
                if (rowView != null)
                {
                    DataRow row = rowView.Row;
                    lblPostaNaziv.Text = row["Naziv"].ToString().Trim();
                }
                else
                    lblPostaNaziv.Text = "";

                ProveraPromene();
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

        private void cmbDrzava_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowView = (DataRowView)cmbDrzava.GetSelectedDataRow();
                if (rowView != null)
                {
                    DataRow row = rowView.Row;
                    popuniPoste(Convert.ToInt32(row["Drzava_ID"]));
                    if (cmbPosta.Text.Trim() != "" && cmbPosta.Text.Trim() != cmbPosta.Properties.NullText.Trim())
                    {
                        DataRowView rowViewPosta = (DataRowView)cmbPosta.GetSelectedDataRow();
                        DataRow rowPosta = rowViewPosta.Row;
                        lblPostaNaziv.Text = rowPosta["Naziv"].ToString().Trim();
                    }
                    else
                        lblPostaNaziv.Text = "";
                }
                else
                    lblPostaNaziv.Text = "";

                ProveraPromene();
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

        private void NapraviSubjekta()
        {
            try
            {
                //PROVERA DA LI SU SVA POLJA POPUNJENA
                if (txtNaziv.Text.Trim() != "" && txtPunNaziv.Text.Trim() != "" && txtOIB.Text.Trim() != "" && txtAdresa.Text.Trim() != "" && txtTelefon.Text.Trim() != "" && txtEmail.Text.Trim() != "" && cmbDrzava.Text.Trim() != cmbDrzava.Properties.NullText.Trim() && cmbPosta.Text.Trim() != cmbPosta.Properties.NullText.Trim() && cmbDrzava.Text.Trim() != "" && cmbPosta.Text.Trim() != "")
                {
                    if (txtOIB.Text.Trim().Length == 11)
                    {
                        //OVDE POCINJE UPISIVANJE KORSNIKA
                        using (var con = new MONTESINOEntities())
                        {
                            var oib = con.Subjekats.SingleOrDefault(x => x.OIB.ToString().Trim() == txtOIB.Text.Trim());

                            if (oib == null)
                            {
                                DataRowView rowView1 = (DataRowView)cmbDrzava.GetSelectedDataRow();
                                DataRow redDrzava = rowView1.Row;
                                DataRowView rowView2 = (DataRowView)cmbPosta.GetSelectedDataRow();
                                DataRow redPosta = rowView2.Row;

                                var noviSub = new Subjekat()
                                {
                                    Naziv = txtNaziv.Text.Trim(),
                                    Naziv2 = txtPunNaziv.Text.Trim(),
                                    jeKupac = (checkKupac.Checked ? "1" : "0").Trim(),
                                    jeDobavljac = (checkDobavljac.Checked ? "1" : "0").Trim(),
                                    OIB = txtOIB.Text.Trim().Trim(),
                                    Adresa = txtAdresa.Text.Trim(),
                                    Posta_ID = Convert.ToInt32(redPosta["Posta_ID"].ToString().Trim()),
                                    Telefon = txtTelefon.Text.Trim(),
                                    Email = txtEmail.Text.Trim(),
                                    Drzava_ID = Convert.ToInt32(redDrzava["Drzava_ID"].ToString().Trim())
                                };
                                con.Subjekats.Add(noviSub);
                                con.SaveChanges();
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                                MessageBox.Show("Subjekat sa unetim OIB-om već postoji!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                        MessageBox.Show("OIB mora da ima tačno 11 karaktera!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void IzmeniSubjekta()
        {
            try
            {
                //PROVERA DA LI SU SVA POLJA POPUNJENA
                if (txtNaziv.Text.Trim() != "" && txtPunNaziv.Text.Trim() != "" && txtOIB.Text.Trim() != "" && txtAdresa.Text.Trim() != "" && txtTelefon.Text.Trim() != "" && txtEmail.Text.Trim() != "" && cmbDrzava.Text.Trim() != cmbDrzava.Properties.NullText.Trim() && cmbPosta.Text.Trim() != cmbPosta.Properties.NullText.Trim() && cmbDrzava.Text.Trim() != "" && cmbPosta.Text.Trim() != "")
                {
                    if (txtOIB.Text.Trim().Length == 11)
                    {
                        //OVDE POCINJE UPISIVANJE KORSNIKA
                        using (var con = new MONTESINOEntities())
                        {
                            //PROVERA PRILIKOM MENJANJA OIB-A, DA LI KORISNIK SA DRUGIM SUBJEKAT_ID-OM IMA ISTI OIB
                            var provera = con.Subjekats.SingleOrDefault(x => x.Subjekat_ID.ToString().Trim() != sub.Subjekat_ID.ToString().Trim() && x.OIB.ToString().Trim() == txtOIB.Text.Trim());

                            if (provera == null)
                            {
                                DataRowView rowView1 = (DataRowView)cmbDrzava.GetSelectedDataRow();
                                DataRow redDrzava = rowView1.Row;
                                DataRowView rowView2 = (DataRowView)cmbPosta.GetSelectedDataRow();
                                DataRow redPosta = rowView2.Row;

                                var oib = con.Subjekats.SingleOrDefault(b => b.Subjekat_ID.ToString().Trim() == sub.Subjekat_ID.ToString().Trim());

                                oib.Naziv = txtNaziv.Text.Trim();
                                oib.Naziv2 = txtPunNaziv.Text.Trim();
                                oib.jeKupac = (checkKupac.Checked ? "1" : "0").Trim();
                                oib.jeDobavljac = (checkDobavljac.Checked ? "1" : "0").Trim();
                                oib.OIB = txtOIB.Text.Trim().Trim();
                                oib.Adresa = txtAdresa.Text.Trim();
                                oib.Posta_ID = Convert.ToInt32(redPosta["Posta_ID"].ToString().Trim());
                                oib.Telefon = txtTelefon.Text.Trim();
                                oib.Email = txtEmail.Text.Trim();
                                oib.Drzava_ID = Convert.ToInt32(redPosta["Drzava_ID"].ToString().Trim());
                                con.SaveChanges();
                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                                MessageBox.Show("Subjekat " + provera.Naziv2 + " ima isti OIB!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                        MessageBox.Show("OIB mora da ima tačno 11 karaktera!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void ProveraPromene()
        {
            try
            {
                if (sub != null && txtNaziv.Text.Trim() != "" && txtPunNaziv.Text.Trim() != "" && txtOIB.Text.Trim() != "" && txtAdresa.Text.Trim() != "" && txtTelefon.Text.Trim() != "" && txtEmail.Text.Trim() != "" && cmbDrzava.Text.Trim() != cmbDrzava.Properties.NullText.Trim() && cmbPosta.Text.Trim() != cmbPosta.Properties.NullText.Trim() && cmbDrzava.Text.Trim() != "" && cmbPosta.Text.Trim() != "")
                {
                    if (txtNaziv.Text.Trim() != sub.Naziv.Trim() || txtPunNaziv.Text.Trim() != sub.Naziv2.Trim() || txtOIB.Text.Trim() != sub.OIB.Trim() || txtEmail.Text.Trim() != sub.Email.Trim() || txtAdresa.Text.Trim() != sub.Adresa.Trim() || txtTelefon.Text.Trim() != sub.Telefon.Trim() || (checkKupac.Checked ? "1" : "0").ToString().Trim() != sub.jeKupac.Trim() || (checkDobavljac.Checked ? "1" : "0").ToString().Trim() != sub.jeDobavljac.Trim() || cmbDrzava.EditValue.ToString().Trim() != sub.Drzava_ID.ToString().Trim() || cmbPosta.EditValue.ToString().Trim() != sub.Posta_ID.ToString().Trim())
                        btnPotvrdi.Enabled = true;
                    else
                        btnPotvrdi.Enabled = false;
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

        private void txtNaziv_TextChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void txtPunNaziv_TextChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void txtOIB_TextChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void txtAdresa_TextChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void txtTelefon_TextChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void checkKupac_CheckedChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void checkDobavljac_CheckedChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }
    }
}