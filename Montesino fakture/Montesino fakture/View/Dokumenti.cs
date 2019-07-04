using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Windows.Forms;

namespace Montesino_fakture.View
{
    public partial class Dokumenti : Form
    {
        public string Broj { get; set; }
        private string tip;
        private string VrstaDokumenta = "01";
        public Dokumenti(string tip, string vrsta)
        {
            try
            {
                InitializeComponent();
                this.tip = tip;
                if (tip.ToString().Trim() == "PON")
                    ucitajTabeluPredracuni();
                else if (tip.ToString().Trim() == "RAC")
                {
                    this.VrstaDokumenta = vrsta;
                    ucitajTabeluRacuni();
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

        public void ucitajTabeluPredracuni() // POPUNJAVANJE TABELE SUBJEKTIMA
        {
            try
            {
                DataTable table = MainForm.getData.GetTablePredracuni();
                gridControl.DataSource = table;
               //s table.DefaultView.Sort = "[Broj] DESC";
                gridView.OptionsFind.FindNullPrompt = "Pretraga predračuna...";
                //SAKRIVANJE ODREDJENIH TABELA
                gridView.Columns["Predracun_ID"].Visible = false;
                gridView.Columns["Subjekat_ID"].Visible = false;
                gridView.Columns["Posta_ID"].Visible = false;
                gridView.Columns["Status_ID"].Visible = false;
                gridView.Columns["Valuta_ID"].Visible = false;
                gridView.Columns["NP_ID"].Visible = false;
                gridView.Columns["OdgovorneOsobe_ID"].Visible = false;
                gridView.Columns["Primilac_ID"].Visible = false;
                gridView.Columns["Ukupno"].Visible = false;
                gridView.Columns["PDV"].Visible = false;
                gridView.Columns["PopustBroj"].Visible = false;
                gridView.Columns["PopustProcenat"].Visible = false;
                //SAKRIVANJE ODREDJENIH TABELA

                //MENJANJE NAZIVA KOLONA
                gridView.Columns["Broj"].Caption = "Broj predračuna";
                gridView.Columns["Datum"].Caption = "Datum izdavanja";
                gridView.Columns["RokIsporuke"].Caption = "Rok isporuke";
                gridView.Columns["Subjekat_Naziv"].Caption = "Kupac";
                gridView.Columns["Primilac_Naziv"].Caption = "Primalac";
                gridView.Columns["RokVazenja"].Caption = "Rok važenja";
                gridView.Columns["Vrednost"].Caption = "Vrednost";
                gridView.Columns["ZaPlacanje"].Caption = "Za plaćanje";
                //MENJANJE NAZIVA KOLONA
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

        public void ucitajTabeluRacuni() // POPUNJAVANJE TABELE SUBJEKTIMA
        {
            try
            {
                DataTable table = MainForm.getData.GetTableRacuni(VrstaDokumenta);
                gridControl.DataSource = table;
              //  table.DefaultView.Sort = "[Broj] DESC";
                gridView.OptionsFind.FindNullPrompt = "Pretraga računa...";
                //SAKRIVANJE ODREDJENIH TABELA
                gridView.Columns["Promet_ID"].Visible = false;
                gridView.Columns["Predracun_ID"].Visible = false;
                gridView.Columns["Subjekat_ID"].Visible = false;
                gridView.Columns["Primilac_ID"].Visible = false;
                gridView.Columns["Posta_ID"].Visible = false;
                gridView.Columns["Status_ID"].Visible = false;
                gridView.Columns["Valuta_ID"].Visible = false;
                gridView.Columns["NP_ID"].Visible = false;
                gridView.Columns["OdgovorneOsobe_ID"].Visible = false;
                gridView.Columns["Ukupno"].Visible = false;
                gridView.Columns["PDV"].Visible = false;
                gridView.Columns["PopustBroj"].Visible = false;
                gridView.Columns["PopustProcenat"].Visible = false;
                gridView.Columns["VrstaDokumenta"].Visible = false;
                gridView.Columns["Napomena"].Visible = false;
                //SAKRIVANJE ODREDJENIH TABELA

                //MENJANJE NAZIVA KOLONA
                gridView.Columns["Broj"].Caption = "Broj računa";
                gridView.Columns["Otpremnica"].Caption = "Datum otpremnice";
                gridView.Columns["Racun"].Caption = "Datum računa";
                gridView.Columns["PDVDate"].Caption = "Datum PDVa";
                gridView.Columns["Subjekat_Naziv"].Caption = "Kupac";
                gridView.Columns["Primilac_Naziv"].Caption = "Primalac";
                gridView.Columns["RokVazenja"].Caption = "Rok važenja";
                gridView.Columns["Vrednost"].Caption = "Vrednost";
                gridView.Columns["ZaPlacanje"].Caption = "Za plaćanje";
                gridView.Columns["Dospece"].Caption = "Dospeće";
                //MENJANJE NAZIVA KOLONA
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

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                var red = gridView.GetFocusedDataRow();
                if (red != null)
                {
                    if (tip.ToString().Trim() == "PON")
                        this.Broj = red["Predracun_ID"].ToString().Trim();
                    else if (tip.ToString().Trim() == "RAC")
                        this.Broj = red["Promet_ID"].ToString().Trim();
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

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var red = gridView.GetFocusedDataRow();
                if (red != null)
                {
                    if (tip.ToString().Trim() == "PON")
                        this.Broj = red["Predracun_ID"].ToString().Trim();
                    else if (tip.ToString().Trim() == "RAC")
                        this.Broj = red["Promet_ID"].ToString().Trim();
                    this.DialogResult = DialogResult.OK;
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