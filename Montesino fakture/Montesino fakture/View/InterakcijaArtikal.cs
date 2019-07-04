using Montesino_fakture.Model;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Montesino_fakture.View
{
    public partial class InterakcijaArtikal : Form
    {
        private Artikal art = null;
        public InterakcijaArtikal()
        {
            try
            {
                InitializeComponent();
                Text = "Kreiranje novog identa";
                popuniPS();
                popuniJM();
                txtSifra.SelectAll();
                txtNaziv.SelectAll();
                txtCena.SelectAll();
                txtOpis.SelectAll();
                txtCenaSaPorezom.SelectAll();
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

        public InterakcijaArtikal(Artikal art)
        {
            try
            {
                InitializeComponent();
                Text = "Izmena identa";
                this.art = art;
                btnOcisti.Visible = false;
                btnPotvrdi.Enabled = false;
                txtNaziv.SelectAll();
                txtSifra.SelectAll();
                txtOpis.SelectAll();
                txtCena.SelectAll();
                txtCenaSaPorezom.SelectAll();

                popuniPS();
                popuniJM();
                txtSifra.Text = this.art.Sifra.ToString();
                txtNaziv.Text = this.art.Naziv.ToString();
                txtOpis.Text = this.art.Opis.ToString();
                txtCena.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(this.art.Cena.ToString()));

                checkAktivan.Checked = Convert.ToBoolean(Convert.ToInt32(this.art.Aktivan));

                Console.WriteLine((checkAktivan.Checked ? "1" : "0").ToString().Trim());
                Console.WriteLine(this.art.Aktivan);

                if (this.art.Vrsta.ToString().Trim() == "A")
                {
                    rbArtikal.Checked = true;
                    rbUsluga.Checked = false;
                }
                else
                {
                    rbArtikal.Checked = false;
                    rbUsluga.Checked = true;
                }
                IzracunajCenu();
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
                if (art == null)
                    NapraviArtikal();
                else
                    IzmeniArtikal();
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

        private void IzmeniArtikal()
        {
            try
            {
                //PROVERA DA LI SU SVA POLJA POPUNJENA
                if (txtNaziv.Text.Trim() != "" && txtSifra.Text.Trim() != "" && txtCena.Text.Trim() != "" && cmbPS.Text.Trim() != cmbPS.Properties.NullText.Trim() && cmbJM.Text.Trim() != cmbJM.Properties.NullText.Trim() && cmbJM.Text.Trim() != "" && cmbPS.Text.Trim() != "")
                {
                    //OVDE POCINJE UPISIVANJE KORSNIKA
                    using (var con = new MONTESINOEntities())
                    {
                        //PROVERA PRILIKOM MENJANJA SIFRE, DA LI POSTOJI PROIZVOD SA DRUGIM ARTIKAL-ID-OM KOJI IMA ISTU SIFRU
                        var provera = con.Artikals.SingleOrDefault(x => x.Artikal_ID.ToString().Trim() != this.art.Artikal_ID.ToString().Trim() && x.Sifra.ToString().Trim() == txtSifra.Text.Trim());

                        if (provera == null)
                        {
                            var sifra = con.Artikals.SingleOrDefault(x => x.Artikal_ID.ToString().Trim() == this.art.Artikal_ID.ToString().Trim());

                            DataRowView rowView1 = (DataRowView)cmbPS.GetSelectedDataRow();
                            DataRow redPS = rowView1.Row;
                            DataRowView rowView2 = (DataRowView)cmbJM.GetSelectedDataRow();
                            DataRow redJM = rowView2.Row;

                            sifra.Sifra = txtSifra.Text.Trim();
                            sifra.Naziv = txtNaziv.Text.Trim();
                            sifra.Opis = txtOpis.Text.Trim();
                            sifra.Cena = Convert.ToDecimal(txtCena.Text.Trim());
                            sifra.PS_ID = Convert.ToInt32(redPS["PS_ID"].ToString().Trim());
                            sifra.JM_ID = Convert.ToInt32(redJM["JM_ID"].ToString().Trim());
                            sifra.Vrsta = (rbArtikal.Checked ? "A" : "U").Trim();
                            sifra.Aktivan = (checkAktivan.Checked ? "1" : "0").Trim();

                            con.SaveChanges();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            MessageBox.Show("Ident sa unetom šifrom " + provera.Sifra + " već postoji! (" + provera.Naziv + ")", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    MessageBox.Show("Morate popuniti sva polja sa zvezdicom!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void NapraviArtikal()
        {
            try
            {
                //PROVERA DA LI SU SVA POLJA POPUNJENA
                if (txtNaziv.Text.Trim() != "" && txtSifra.Text.Trim() != "" && txtCena.Text.Trim() != "" && cmbPS.Text.Trim() != cmbPS.Properties.NullText.Trim() && cmbJM.Text.Trim() != cmbJM.Properties.NullText.Trim() && cmbPS.Text.Trim() != "" && cmbJM.Text.Trim() != "")
                {
                    //OVDE POCINJE UPISIVANJE KORSNIKA
                    using (var con = new MONTESINOEntities())
                    {
                        var sifra = con.Artikals.SingleOrDefault(x => x.Sifra.ToString().Trim() == txtSifra.Text.Trim());

                        if (sifra == null)
                        {
                            DataRowView rowView1 = (DataRowView)cmbPS.GetSelectedDataRow();
                            DataRow redPS = rowView1.Row;
                            DataRowView rowView2 = (DataRowView)cmbJM.GetSelectedDataRow();
                            DataRow redJM = rowView2.Row;

                            var noviArt = new Artikal()
                            {
                                Sifra = txtSifra.Text.Trim(),
                                Naziv = txtNaziv.Text.Trim(),
                                Opis = txtOpis.Text.Trim(),
                                Cena = Convert.ToDecimal(txtCena.Text.Trim()),
                                PS_ID = Convert.ToInt32(redPS["PS_ID"].ToString().Trim()),
                                JM_ID = Convert.ToInt32(redJM["JM_ID"].ToString().Trim()),
                                Vrsta = (rbArtikal.Checked ? "A" : "U").Trim(),
                                Aktivan = (checkAktivan.Checked ? "1" : "0").Trim()
                            };
                            con.Artikals.Add(noviArt);
                            con.SaveChanges();
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            MessageBox.Show("Ident sa unetom šifrom " + sifra.Naziv + " već postoji!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    MessageBox.Show("Morate popuniti sva polja sa zvezdicom!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnOcisti_Click(object sender, EventArgs e)
        {
            try
            {
                txtNaziv.Clear();
                txtCena.Clear();
                txtOpis.Clear();
                txtSifra.Clear();
                txtCenaSaPorezom.Text = "0.00 HRK";
                checkAktivan.CheckState = 0;
                rbArtikal.Checked = true;
                rbUsluga.Checked = false;
                cmbPS.EditValue = null;
                cmbJM.EditValue = null;
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

        private void popuniPS()
        {
            try
            {
                cmbPS.Properties.DataSource = MainForm.getData.GetTablePS();
                cmbPS.Properties.ValueMember = "PS_ID";
                cmbPS.Properties.DisplayMember = "Kod";
                cmbPS.Properties.ForceInitialize();
                cmbPS.Properties.PopulateColumns();
                cmbPS.Properties.Columns["PS_ID"].Visible = false;

                if (art != null)
                {
                    int brojac = 0;
                    foreach (DataRow sub in MainForm.getData.GetTablePS().Rows)
                    {
                        if (sub["PS_ID"].ToString().Trim() == this.art.PS_ID.ToString().Trim())
                        {
                            cmbPS.ItemIndex = brojac;
                            cmbPS.EditValue = sub["PS_ID"].ToString().Trim();
                            break;
                        }
                        brojac++;
                    }
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

        private void ProveraPromene()
        {
            try
            {
                if (art != null && txtNaziv.Text.Trim() != "" && txtSifra.Text.Trim() != "" && txtCena.Text.Trim() != "" && cmbPS.Text.Trim() != cmbPS.Properties.NullText.Trim() && cmbJM.Text.Trim() != cmbJM.Properties.NullText.Trim() && cmbJM.Text.Trim() != "" && cmbPS.Text.Trim() != "")
                {
                    if (txtNaziv.Text.Trim() != this.art.Naziv.ToString().Trim() || txtSifra.Text.Trim() != this.art.Sifra.ToString().Trim() || txtOpis.Text.Trim() != this.art.Opis.ToString().Trim() ||
                    txtCena.Text.Trim() != MainForm.getData.Formatiraj(Convert.ToDecimal(this.art.Cena.ToString())) || (checkAktivan.Checked ? "1" : "0").ToString().Trim() != this.art.Aktivan.ToString().Trim() ||
                    (rbArtikal.Checked ? "A" : "U").ToString().Trim() != this.art.Vrsta.ToString().Trim() || (rbUsluga.Checked ? "U" : "A").ToString().Trim() != this.art.Vrsta.ToString().Trim() || cmbJM.EditValue.ToString().Trim() != this.art.JM_ID.ToString().Trim() || cmbPS.EditValue.ToString().Trim() != this.art.PS_ID.ToString().Trim())
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

        private void popuniJM() // POPUNJAVANJE JM
        {
            try
            {
                cmbJM.Properties.DataSource = MainForm.getData.GetTableJM();
                cmbJM.Properties.ValueMember = "JM_ID";
                cmbJM.Properties.DisplayMember = "Kod";
                cmbJM.Properties.ForceInitialize();
                cmbJM.Properties.PopulateColumns();
                cmbJM.Properties.Columns["JM_ID"].Visible = false;

                if (art != null)
                {
                    int brojac = 0;
                    foreach (DataRow sub in MainForm.getData.GetTableJM().Rows)
                    {
                        if (sub["JM_ID"].ToString().Trim() == this.art.JM_ID.ToString().Trim())
                        {
                            cmbJM.ItemIndex = brojac;
                            cmbJM.EditValue = sub["JM_ID"].ToString().Trim();
                            break;
                        }
                        brojac++;
                    }
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

        private void IzracunajCenu() // RACUNANJE UNETE CENE SA POREZOM
        {
            try
            {
                DataRowView rowView = (DataRowView)cmbPS.GetSelectedDataRow();
                if (rowView != null)
                {
                    DataRow porez = rowView.Row;
                    decimal ukupno = 0;
                    if (cmbPS.Text.Trim() != cmbPS.Properties.NullText.Trim() && cmbPS.Text.Trim() != "" && txtCena.Text.Trim() != "")
                    {
                        //  ukupno = Convert.ToDecimal(txtCena.Text) + Convert.ToDecimal(txtCena.Text) * Convert.ToDecimal(porez["Vrednost"]) / 100;
                        ukupno = MainForm.getData.IzracunajCenaPDV(Convert.ToDecimal(txtCena.Text), Convert.ToDecimal(porez["Vrednost"]));
                        txtCenaSaPorezom.Text = MainForm.getData.Formatiraj(ukupno) + " HRK";
                    }
                    else
                    {
                        txtCenaSaPorezom.Text = "0.0000 HRK";
                    }
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

        private void cmbPS_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                IzracunajCenu();
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

        private void txtCena_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCena.Text.Trim() == "" || txtCena == null)
                    txtCena.Text = "0.0000";

                if (txtCena.Text.Trim() != "")
                {
                    txtCena.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtCena.Text));
                    IzracunajCenu();
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

        private void txtCena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!jeBroj(e.KeyChar, txtCena.Text))
                e.Handled = true;
        }

        public bool jeBroj(char ch, string text)
        {
            bool res = true;
            char decimalChar = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

            //check if it´s a decimal separator and if doesn´t already have one in the text string
            if (ch == decimalChar && text.IndexOf(decimalChar) != -1)
            {
                res = false;
                return res;
            }

            //check if it´s a digit, decimal separator and backspace
            if (!Char.IsDigit(ch) && ch != decimalChar && ch != (char)Keys.Back)
                res = false;

            return res;
        }

        private void rbArtikal_CheckedChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void rbUsluga_CheckedChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void txtSifra_TextChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void txtNaziv_TextChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void txtCena_TextChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void txtOpis_TextChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void cmbJM_EditValueChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }

        private void checkAktivan_CheckedChanged(object sender, EventArgs e)
        {
            ProveraPromene();
        }
    }
}