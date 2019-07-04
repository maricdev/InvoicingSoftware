using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using Montesino_fakture.Model;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace Montesino_fakture.View
{
    public partial class Racuni : Form
    {
        private Boolean izDokumenta = false;
        private string Promet_ID = null;
        private DataTable stavke;
        private RepositoryItemGridLookUpEdit cmbSifre = new RepositoryItemGridLookUpEdit();
        private RepositoryItemGridLookUpEdit cmbNaziv = new RepositoryItemGridLookUpEdit();
        private RepositoryItemGridLookUpEdit cmbJM = new RepositoryItemGridLookUpEdit();
        private RepositoryItemGridLookUpEdit cmbPS = new RepositoryItemGridLookUpEdit();
        private DataRow redic; //SLUZI ZA OSVEZAVANJE REDA U REALTIME-U KADA SE MENJA SIFRA ILI NAZIV
        private int redicBroj; //SLUZI ZA OSVEZAVANJE REDA U REALTIME-U KADA SE MENJA SIFRA ILI NAZIV
        private string VrstaDokumenta;  // 01 - faktura // 02- odobrenje
        private string blankoDokument = "??-??-??";
        public Racuni(string tip)
        {
            try
            {
                //OSNOVNA INICIJALIZACIJA
                InitializeComponent();

                if (tip == "" || tip == null || tip == "01")
                {
                    this.VrstaDokumenta = tip;
                    this.Text = VrstaDokumenta + " - Fakture / Otpremnice";
                    blankoDokument = "??-" + tip + "-01";
                    btnPrenesi.Visible = true;
                }
                else
                {
                    this.VrstaDokumenta = tip;
                    this.Text = VrstaDokumenta + " - Odobrenja";
                    blankoDokument = "??-" + tip + "-01";
                    btnPrenesi.Visible = false;
                    dbtnOtpremnica.Visible = false;
                    btnRacun.Location = dbtnOtpremnica.Location;
                    btnRacun.Text = "Odobrenje";
                }
                dospeceRacuna();
                popuniKupce();
                popuniPrimaoce();
                popuniValute();
                popuniNP();
                popuniMesto();
                popuniOdgovornuOsobu();
                popuniVezneDokumente();
                popuniStatus();
                cmbSifre.EditValueChanged += new System.EventHandler(this.cmbSifre_EditValueChanged); // EVENT ZA HVATANJE INSTANT PROMENE
                cmbNaziv.EditValueChanged += new System.EventHandler(this.cmbNaziv_EditValueChanged); // EVENT ZA HVATANJE INSTANT PROMENE
                //OSNOVNA INICIJALIZACIJA
                ucitajPoslednjiRacun();
                btnSacuvajPromene.Enabled = false;
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

        private void ucitajPoslednjiRacun()
        {
            try
            {
                if (MainForm.getData.GetTableRacuni(VrstaDokumenta).Rows.Count > 0) // DA LI POSTOJI RACUN UOPSTE (SAMO DOK SE NE NAPRAVI PRVI RACUN)
                {
                    using (var con = new MONTESINOEntities())
                    {
                        var poslednji = con.Promets.Where(a => a.VrstaDokumenta == this.VrstaDokumenta).Max(x => x.Promet_ID);
                        Promet_ID = poslednji.ToString().Trim();
                        PopuniRacunZaglavlje(poslednji.ToString().Trim(), "NE");
                        PopuniRacunStavke(poslednji.ToString().Trim());                       
                        proveraNavigacije();
                    }
                }
                else
                {
                    popuniBlankoRacun();
                    //UCITAJ POSLEDNJI RACUN (POKRECE SE SAMO NA POCETKU)
                    gridControl.Enabled = false;
                    cmbKupac.Enabled = false;
                    cmbPrimalac.Enabled = false;
                    cmbMesto.Enabled = false;
                    cmbNP.Enabled = false;
                    cmbOdgovornaOsoba.Enabled = false;
                    cmbValuta.Enabled = false;
                    cmbVezniDokument.Enabled = false;
                    cmbStatus.Enabled = false;
                    txtVazenje.Enabled = false;
                    txtNapomena.Enabled = false;
                    dateTimePickerOtpremnica.Enabled = false;
                    dateTimePickerRacun.Enabled = false;
                    dateTimePickerPDV.Enabled = false;
                    dateTimePickerDospece.Enabled = false;
                    btnNapredJedanput.Enabled = false;
                    btnNazadJedanput.Enabled = false;
                    btnSacuvajPromene.Enabled = false;
                    btnOtpremnica.Enabled = false;
                    dbtnOtpremnica.Enabled = false;
                    btnIzbrisi.Enabled = false;
                    btnRacuni.Enabled = false;
                    btnRacun.Enabled = false;
                    proveraNavigacije();
                    txtBrojRacuna.Text = blankoDokument;

                    if(VrstaDokumenta == "01")
                    {
                        MessageBox.Show("Izgleda da još niste napravili prvi račun. \nDa bi ste omogućili formu po prvi put, morate napraviti" +
                            " račun klikom na + (plus).", "Dobrodošli", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Izgleda da još niste napravili prvo odobrenje. \nDa bi ste omogućili formu po prvi put, morate napraviti" +
                            " odobrenje klikom na + (plus).", "Dobrodošli", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //UCITAJ POSLEDNJI RACUN (POKRECE SE SAMO NA POCETKU)
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

        private void PopuniRacunZaglavlje(string broj, string veza)
        {
            try
            {
                using (var con = new MONTESINOEntities())
                {
                    if (veza.ToString().Trim() == "NE")
                    {
                        var racun = con.Promets.Where(a => a.VrstaDokumenta == this.VrstaDokumenta).FirstOrDefault(x => x.Promet_ID.ToString().Trim() == broj.ToString().Trim());

                        txtBrojRacuna.Text = MainForm.getData.BrojRastaviDokumenta(racun.Broj); //BROJ RACUNA
                        cmbKupac.EditValue = racun.Subjekat_ID.ToString().Trim(); //KUPAC
                        cmbPrimalac.EditValue = racun.Primalac_ID.ToString().Trim(); //PRIMILAC
                        dateTimePickerOtpremnica.Value = racun.Otpremnica; //OTPREMNICA
                        dateTimePickerRacun.Value = racun.Racun; //RACUN
                        dateTimePickerPDV.Value = racun.PDVDate; //PDV Datum
                        dateTimePickerDospece.Value = racun.Dospece; //Dospece
                        txtVazenje.Text = racun.RokVazenja.ToString().Trim(); // TXTBOX VAZENJE

                        //OPCIONO
                        if (racun.Predracun_ID != null)
                            cmbVezniDokument.EditValue = racun.Predracun_ID.ToString().Trim(); // VEZNI DOKUMENT (PONUDA)
                        else
                        {
                            cmbVezniDokument.EditValue = null;
                            cmbVezniDokument.Reset();
                            cmbVezniDokument.ResetText();
                        }

                        if (racun.NP_ID != null)
                            cmbNP.EditValue = racun.NP_ID.ToString().Trim();// NACIN PLACANJA
                        else
                        {
                            cmbNP.EditValue = null;
                            cmbNP.Reset();
                            cmbNP.ResetText();
                        }
                        if (racun.Valuta_ID != null)
                        {
                            cmbValuta.EditValue = racun.Valuta_ID.ToString().Trim(); // VALUTA
                        }
                        else
                        {
                            cmbValuta.EditValue = null;
                            cmbValuta.Reset();
                            cmbValuta.ResetText();
                        }
                        if (racun.Posta_ID != null)
                            cmbMesto.EditValue = racun.Posta_ID.ToString().Trim(); // MESTO
                        else
                        {
                            cmbMesto.EditValue = null;
                            cmbMesto.Reset();
                            cmbMesto.ResetText();
                        }
                        if (racun.OdgovorneOsobe_ID != null)
                            cmbOdgovornaOsoba.EditValue = racun.OdgovorneOsobe_ID.ToString().Trim(); //ODGOVORNA OSOBA
                        else
                        {
                            cmbOdgovornaOsoba.EditValue = null;
                            cmbOdgovornaOsoba.Reset();
                            cmbOdgovornaOsoba.ResetText();
                        }
                        if (racun.Status_ID != null)
                            cmbStatus.EditValue = racun.Status_ID.ToString().Trim(); //STATUS
                        else
                        {
                            cmbStatus.EditValue = null;
                            cmbStatus.Reset();
                            cmbStatus.ResetText();
                        }
                        if (racun.Napomena != null)
                            txtNapomena.Text = racun.Napomena.ToString().Trim(); // NAPOMENA
                        else
                            txtNapomena.Text = null;
                        if (racun.Ukupno != null)
                        {
                            txtUkupno.Text = racun.Ukupno.ToString().Trim(); // UKUPNO
                            txtUkupno.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtUkupno.Text));
                        }
                        else
                            txtUkupno.Text = null;
                        if (racun.Vrednost != null)
                        {
                            txtVrednost.Text = racun.Vrednost.ToString().Trim(); // VREDNOST
                            txtVrednost.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtVrednost.Text));
                        }
                        else
                            txtVrednost.Text = null;
                        if (racun.PDV != null)
                        {
                            txtPDV.Text = racun.PDV.ToString().Trim(); // PDV
                            txtPDV.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtPDV.Text));
                        }
                        else
                            txtPDV.Text = null;
                        if (racun.PopustBroj != null)
                        {
                            txtPopustBroj.Text = racun.PopustBroj.ToString().Trim(); // POPUST BROJ
                            txtPopustBroj.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtPopustBroj.Text));
                        }
                        else
                            txtPopustBroj.Text = null;
                        if (racun.PopustProcenat != null)
                        {
                            txtPopustProcenat.Text = racun.PopustProcenat.ToString().Trim(); // POPUST PROCENAT
                            txtPopustProcenat.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtPopustProcenat.Text));
                        }
                        else
                            txtPopustProcenat.Text = null;
                        if (racun.ZaPlacanje != null)
                        {
                            txtZaPlacanje.Text = racun.ZaPlacanje.ToString().Trim(); // ZA PLACANJE
                            txtZaPlacanje.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtZaPlacanje.Text));
                        }
                        else
                            txtZaPlacanje.Text = null;
                        //OPCIONO
                    }
                    else if (veza.ToString().Trim() == "DA")
                    {
                        var predracun = con.Predracuns.FirstOrDefault(x => x.Predracun_ID.ToString().Trim() == broj.ToString().Trim());

                        cmbKupac.EditValue = predracun.Subjekat_ID.ToString().Trim(); //KUPAC
                        cmbPrimalac.EditValue = predracun.Primalac_ID.ToString().Trim(); //PRIMILAC
                        dateTimePickerOtpremnica.Value = DateTime.Today; //OTPREMNICA
                        dateTimePickerRacun.Value = DateTime.Today; //RACUN
                        dateTimePickerPDV.Value = DateTime.Today; //PDV Datum
                        dateTimePickerDospece.Value = DateTime.Today; //Dospece
                        txtVazenje.Text = "0"; // TXTBOX VAZENJE

                        //OPCIONO
                        if (predracun.NP_ID != null)
                            cmbNP.EditValue = predracun.NP_ID.ToString().Trim();// NACIN PLACANJA
                        else
                        {
                            cmbNP.EditValue = null;
                            cmbNP.Reset();
                            cmbNP.ResetText();
                        }
                        if (predracun.Valuta_ID != null)
                        {
                            cmbValuta.EditValue = predracun.Valuta_ID.ToString().Trim(); // VALUTA
                        }
                        else
                        {
                            cmbValuta.EditValue = null;
                            cmbValuta.Reset();
                            cmbValuta.ResetText();
                        }
                        if (predracun.Posta_ID != null)
                            cmbMesto.EditValue = predracun.Posta_ID.ToString().Trim(); // MESTO
                        else
                        {
                            cmbMesto.EditValue = null;
                            cmbMesto.Reset();
                            cmbMesto.ResetText();
                        }
                        if (predracun.OdgovorneOsobe_ID != null)
                            cmbOdgovornaOsoba.EditValue = predracun.OdgovorneOsobe_ID.ToString().Trim(); //ODGOVORNA OSOBA
                        else
                        {
                            cmbOdgovornaOsoba.EditValue = null;
                            cmbOdgovornaOsoba.Reset();
                            cmbOdgovornaOsoba.ResetText();
                        }
                        if (predracun.Status_ID != null)
                            cmbStatus.EditValue = predracun.Status_ID.ToString().Trim(); //STATUS
                        else
                        {
                            cmbStatus.EditValue = null;
                            cmbStatus.Reset();
                            cmbStatus.ResetText();
                        }
                        if (predracun.Napomena != null)
                            txtNapomena.Text = predracun.Napomena.ToString().Trim(); // NAPOMENA
                        else
                            txtNapomena.Text = null;
                        if (predracun.Ukupno != null)
                            txtUkupno.Text = predracun.Ukupno.ToString().Trim(); // UKUPNO
                        else
                            txtUkupno.Text = null;
                        if (predracun.Vrednost != null)
                            txtVrednost.Text = predracun.Vrednost.ToString().Trim(); // VREDNOST
                        else
                            txtVrednost.Text = null;
                        if (predracun.PDV != null)
                            txtPDV.Text = predracun.PDV.ToString().Trim(); // PDV
                        else
                            txtPDV.Text = null;
                        if (predracun.PopustBroj != null)
                            txtPopustBroj.Text = predracun.PopustBroj.ToString().Trim(); // POPUST BROJ
                        else
                            txtPopustBroj.Text = null;
                        if (predracun.PopustProcenat != null)
                            txtPopustProcenat.Text = predracun.PopustProcenat.ToString().Trim(); // POPUST PROCENTI
                        else
                            txtPopustProcenat.Text = null;
                        if (predracun.ZaPlacanje != null)
                            txtZaPlacanje.Text = predracun.ZaPlacanje.ToString().Trim(); //ZA PLACANJE
                        else
                            txtZaPlacanje.Text = null;
                        //OPCIONO
                    }
                    proveraNavigacije();
                }
              //  popuniFooter();
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

        private void proveraNavigacije()
        {
            try
            {
                if (Promet_ID != null)
                {
                    btnIzbrisi.Enabled = true;
                    btnOtpremnica.Enabled = true;
                    dbtnOtpremnica.Enabled = true;
                    btnRacun.Enabled = true;
                    // btnSacuvajPromene.Enabled = true;
                    using (var con = new MONTESINOEntities())
                    {
                        var racunTrenutni = Convert.ToInt32(Promet_ID.ToString().Trim());
                        var prethodni = (from x in con.Promets where x.Promet_ID < racunTrenutni orderby x.Promet_ID descending select x).Where(a => a.VrstaDokumenta == this.VrstaDokumenta).FirstOrDefault();
                        var sledeci = (from x in con.Promets where x.Promet_ID > racunTrenutni orderby x.Promet_ID ascending select x).Where(a => a.VrstaDokumenta == this.VrstaDokumenta).FirstOrDefault();

                        if (prethodni == null)
                        {
                            btnPrvi.Enabled = false;
                            btnNazadJedanput.Enabled = false;
                        }
                        else
                        {
                            btnPrvi.Enabled = true;
                            btnNazadJedanput.Enabled = true;
                        }
                        if (sledeci == null)
                        {
                            btnPoslednji.Enabled = false;
                            btnNapredJedanput.Enabled = false;
                        }
                        else
                        {
                            btnPoslednji.Enabled = true;
                            btnNapredJedanput.Enabled = true;
                        }
                    }
                }
                else
                {               
                    btnIzbrisi.Enabled = true;
                    btnPrvi.Enabled = true;
                    btnNazadJedanput.Enabled = true;

                    btnPoslednji.Enabled = false;
                    btnNapredJedanput.Enabled = false;

                    btnOtpremnica.Enabled = false;
                    dbtnOtpremnica.Enabled = false;
                    btnRacun.Enabled = false;
                    btnSacuvajPromene.Enabled = false;
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

        private void PopuniRacunStavke(string broj)
        {
            try
            {
                stavke = MainForm.getData.GetTableRacuniStavke(broj);
                gridControl.DataSource = stavke;
                // stavke.DefaultView.Sort = "[Pozicija] ASC";
                //SAKRIVANJE ODREDJENIH TABELA
                gridView.Columns["PrometStavke_ID"].Visible = false;
                gridView.Columns["Promet_ID"].Visible = false;
                gridView.Columns["Artikal_ID"].Visible = false;
                gridView.Columns["Opis"].Visible = false;
                gridView.Columns["Pozicija"].OptionsColumn.AllowEdit = false;
                gridView.Columns["Pozicija"].MaxWidth = 60;
                gridView.Columns["Pozicija"].MinWidth = 60;
                gridView.Columns["Naziv"].MinWidth = 200;
                //SAKRIVANJE ODREDJENIH TABELA
                gridView.Columns["Sifra"].Caption = "Šifra";
                gridView.Columns["Kolicina"].Caption = "Količina";
                gridView.Columns["CenaPDV"].Caption = "Za plaćanje";
                gridView.Columns["JM_ID"].Caption = "JM";
                gridView.Columns["PS_ID"].Caption = "PS";
                //POPUNJAVANJE STAVKI
                var riMemoExEdit = new RepositoryItemMemoExEdit();
                gridControl.RepositoryItems.Add(riMemoExEdit);
                gridView.Columns["Napomena"].ColumnEdit = riMemoExEdit;

                DataTable artikli = MainForm.getData.GetTableArtikli("AKTIVNI");
                cmbSifre.DataSource = artikli;
                cmbSifre.ValueMember = "Artikal_ID";
                cmbSifre.DisplayMember = "Sifra";
                cmbSifre.PopulateViewColumns();
                cmbSifre.View.Columns.ColumnByFieldName("Artikal_ID").Visible = false;
                cmbSifre.View.Columns.ColumnByFieldName("Aktivan").Visible = false;
                cmbSifre.View.Columns.ColumnByFieldName("JM_ID").Visible = false;
                cmbSifre.View.Columns.ColumnByFieldName("PS_ID").Visible = false;
                cmbSifre.View.Columns.ColumnByFieldName("Artikal").Visible = false;
                cmbSifre.View.Columns.ColumnByFieldName("Usluga").Visible = false;
                cmbSifre.View.Columns["JM_Kod"].Caption = "Jedinica mere";
                cmbSifre.View.Columns["PS_Naziv"].Caption = "Poreska stopa";
                cmbSifre.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
                cmbSifre.NullText = "";
                cmbSifre.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
                gridControl.RepositoryItems.Add(cmbSifre);
                gridView.Columns["Sifra"].ColumnEdit = cmbSifre;
                //   cmbSifre.ImmediatePopup = true;
                //   cmbNaziv.ImmediatePopup = true;

                cmbNaziv.DataSource = artikli;
                cmbNaziv.ValueMember = "Artikal_ID";
                cmbNaziv.DisplayMember = "Naziv";
                cmbNaziv.PopulateViewColumns();
                cmbNaziv.View.Columns.ColumnByFieldName("Artikal_ID").Visible = false;
                cmbNaziv.View.Columns.ColumnByFieldName("Aktivan").Visible = false;
                cmbNaziv.View.Columns.ColumnByFieldName("JM_ID").Visible = false;
                cmbNaziv.View.Columns.ColumnByFieldName("PS_ID").Visible = false;
                cmbNaziv.View.Columns.ColumnByFieldName("Artikal").Visible = false;
                cmbNaziv.View.Columns.ColumnByFieldName("Usluga").Visible = false;
                cmbNaziv.View.Columns["JM_Kod"].Caption = "Jedinica mere";
                cmbNaziv.View.Columns["PS_Naziv"].Caption = "Poreska stopa";
                cmbNaziv.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
                cmbNaziv.NullText = "";
                cmbNaziv.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
                gridControl.RepositoryItems.Add(cmbNaziv);
                gridView.Columns["Naziv"].ColumnEdit = cmbNaziv;

                cmbJM.DataSource = MainForm.getData.GetTableJM();
                cmbJM.ValueMember = "JM_ID";
                cmbJM.DisplayMember = "Kod";
                cmbJM.PopulateViewColumns();
                cmbJM.View.Columns.ColumnByFieldName("JM_ID").Visible = false;
                cmbJM.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
                cmbJM.NullText = "";
                cmbJM.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
                gridControl.RepositoryItems.Add(cmbJM);
                gridView.Columns["JM_ID"].ColumnEdit = cmbJM;

                cmbPS.DataSource = MainForm.getData.GetTablePS();
                cmbPS.ValueMember = "PS_ID";
                cmbPS.DisplayMember = "Kod";
                cmbPS.PopulateViewColumns();
                cmbPS.View.Columns.ColumnByFieldName("PS_ID").Visible = false;
                cmbPS.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
                cmbPS.NullText = "";
                cmbPS.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
                gridControl.RepositoryItems.Add(cmbPS);
                gridView.Columns["PS_ID"].ColumnEdit = cmbPS;

                gridView.Columns["Kolicina"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                gridView.Columns["Cena"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                gridView.Columns["Vrednost"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                gridView.Columns["CenaPDV"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                gridView.Columns["Rabat"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                gridView.Columns["Kolicina"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView.Columns["Kolicina"].DisplayFormat.FormatString = "n4";
                gridView.Columns["Cena"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView.Columns["Cena"].DisplayFormat.FormatString = "n4";
                gridView.Columns["Vrednost"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView.Columns["Vrednost"].DisplayFormat.FormatString = "n4";
                gridView.Columns["CenaPDV"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView.Columns["CenaPDV"].DisplayFormat.FormatString = "n4";
                gridView.Columns["Rabat"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView.Columns["Rabat"].DisplayFormat.FormatString = "n4";

                popuniTabeluDefaultVrednostima();
                popuniFooter();
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

        private void popuniTabeluDefaultVrednostima()
        {
            try
            {
                int brojacReda = 0;
                foreach (DataRow red in stavke.Rows)
                {
                    gridView.SetRowCellValue(brojacReda, "Sifra", red["Artikal_ID"]);
                    gridView.SetRowCellValue(brojacReda, "Naziv", red["Artikal_ID"]);
                    gridView.SetRowCellValue(brojacReda, "JM_ID", red["JM_ID"]);
                    gridView.SetRowCellValue(brojacReda, "PS_ID", red["PS_ID"]);
                    brojacReda++;
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

        private void popuniFooter()
        {
            try
            {
                decimal ukupno = 0;
                decimal vrednost = 0;
                decimal popustBroj = 0;
                decimal popustProcenat = 0;
                decimal PDV = 0;
                decimal zaPlacanje = 0;
                // RACUNAJ FOOTER

                Console.WriteLine("BROJ REDOVA: " + stavke.Rows.Count);
                if (stavke.Rows.Count > 0) 
                {
                    foreach (DataRow red in stavke.Rows)
                    {
                        ukupno += Convert.ToDecimal(red["Kolicina"].ToString().Trim()) * Convert.ToDecimal(red["Cena"].ToString().Trim());
                        vrednost += Convert.ToDecimal(red["Vrednost"]);
                        popustProcenat += Convert.ToDecimal(red["Rabat"]);
                        zaPlacanje += Convert.ToDecimal(red["CenaPDV"]);
                    }
                    popustBroj = ukupno - vrednost;
                    popustProcenat /= stavke.Rows.Count;
                    PDV = zaPlacanje - vrednost;

                    txtUkupno.Text = MainForm.getData.Formatiraj(ukupno);
                    txtVrednost.Text = MainForm.getData.Formatiraj(vrednost);
                    txtPopustBroj.Text = MainForm.getData.Formatiraj(popustBroj);
                    txtPopustProcenat.Text = MainForm.getData.Formatiraj(popustProcenat);
                    txtPDV.Text = MainForm.getData.Formatiraj(PDV);
                    txtZaPlacanje.Text = MainForm.getData.Formatiraj(zaPlacanje);
                }
                else
                {
                    txtUkupno.Text = MainForm.getData.Formatiraj(0);
                    txtVrednost.Text = MainForm.getData.Formatiraj(0);
                    txtPopustBroj.Text = MainForm.getData.Formatiraj(0);
                    txtPopustProcenat.Text = MainForm.getData.Formatiraj(0);
                    txtPDV.Text = MainForm.getData.Formatiraj(0);
                    txtZaPlacanje.Text = MainForm.getData.Formatiraj(0);
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

        private void popuniStatus()
        {
            try
            {
                cmbStatus.Properties.DataSource = MainForm.getData.GetTableStatusi("RAC");
                cmbStatus.Properties.ValueMember = "Status_ID";
                cmbStatus.Properties.DisplayMember = "Naziv";
                cmbStatus.Properties.ForceInitialize();
                cmbStatus.Properties.PopulateColumns();
                cmbStatus.Properties.Columns["Status_ID"].Visible = false;
                cmbStatus.Properties.Columns["jePredracun"].Visible = false;
                cmbStatus.Properties.Columns["jeRacun"].Visible = false;
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

        private void popuniVezneDokumente()
        {
            try
            {
                if(VrstaDokumenta == "01")
                {
                    cmbVezniDokument.Properties.DataSource = MainForm.getData.GetTableVezniDokument();
                    cmbVezniDokument.Properties.ValueMember = "Predracun_ID";
                    cmbVezniDokument.Properties.DisplayMember = "Broj";
                    cmbVezniDokument.ForceInitialize();
                    cmbVezniDokument.Properties.PopulateViewColumns();
                    cmbVezniDokument.Properties.View.Columns["Predracun_ID"].Visible = false;
                    cmbVezniDokument.Properties.View.Columns["Subjekat_ID"].Caption = "Kupac";
                    cmbVezniDokument.Properties.View.Columns["Primilac_ID"].Caption = "Primilac";
                    cmbVezniDokument.Properties.View.Columns["RokVazenja"].Caption = "Rok važenja - Dana";
                    cmbVezniDokument.Properties.View.Columns["RokIsporuke"].Caption = "Rok isporuke";
                    cmbVezniDokument.Properties.View.Columns["ZaPlacanje"].Caption = "Za plaćanje";
                }
                else
                {
                    cmbVezniDokument.Properties.DataSource = MainForm.getData.GetTableVezniDokumentZaOdobrenje();
                    cmbVezniDokument.Properties.ValueMember = "Promet_ID";
                    cmbVezniDokument.Properties.DisplayMember = "Broj";
                    cmbVezniDokument.ForceInitialize();
                    cmbVezniDokument.Properties.PopulateViewColumns();
                    cmbVezniDokument.Properties.View.Columns["Promet_ID"].Visible = false;
                    //cmbVezniDokument.Properties.View.Columns["Datum"].Caption = "Datum otpremnice";
                    //cmbVezniDokument.Properties.View.Columns["Racun"].Caption = "Datum računa";
                    //cmbVezniDokument.Properties.View.Columns["Subjekat_ID"].Caption = "Kupac";
                    //cmbVezniDokument.Properties.View.Columns["Primilac_ID"].Caption = "Primilac";
                    //cmbVezniDokument.Properties.View.Columns["ZaPlacanje"].Caption = "Za plaćanje";
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

        private void popuniOdgovornuOsobu()
        {
            try
            {
                cmbOdgovornaOsoba.Properties.DataSource = MainForm.getData.GetTableOdgovorneOsobe();
                cmbOdgovornaOsoba.Properties.ValueMember = "OdgovorneOsobe_ID";
                cmbOdgovornaOsoba.Properties.DisplayMember = "Naziv";
                cmbOdgovornaOsoba.Properties.ForceInitialize();
                cmbOdgovornaOsoba.Properties.PopulateColumns();
                cmbOdgovornaOsoba.Properties.Columns["OdgovorneOsobe_ID"].Visible = false;
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

        private void popuniMesto()
        {
            try
            {
                cmbMesto.Properties.DataSource = MainForm.getData.GetTablePoste();
                cmbMesto.Properties.ValueMember = "Posta_ID";
                cmbMesto.Properties.DisplayMember = "Naziv";
                cmbMesto.Properties.ForceInitialize();
                cmbMesto.Properties.PopulateColumns();
                cmbMesto.Properties.Columns["Posta_ID"].Visible = false;
                cmbMesto.Properties.Columns["Drzava_ID"].Visible = false;
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

        private void popuniNP()
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

        private void popuniValute()
        {
            try
            {
                cmbValuta.Properties.DataSource = MainForm.getData.GetTableValute();
                cmbValuta.Properties.ValueMember = "Valuta_ID";
                cmbValuta.Properties.DisplayMember = "Oznaka";
                cmbValuta.Properties.ForceInitialize();
                cmbValuta.Properties.PopulateColumns();
                cmbValuta.Properties.Columns["Valuta_ID"].Visible = false;
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

        private void popuniPrimaoce()
        {
            try
            {
                cmbPrimalac.Properties.DataSource = MainForm.getData.GetTableKupci();
                cmbPrimalac.Properties.ValueMember = "Subjekat_ID";
                cmbPrimalac.Properties.DisplayMember = "Naziv";
                cmbPrimalac.Properties.ForceInitialize();
                cmbPrimalac.Properties.PopulateColumns();
                cmbPrimalac.Properties.Columns["Subjekat_ID"].Visible = false;
                cmbPrimalac.Properties.Columns["jeKupac"].Visible = false;
                cmbPrimalac.Properties.Columns["jeDobavljac"].Visible = false;
                cmbPrimalac.Properties.Columns["Posta_ID"].Visible = false;
                cmbPrimalac.Properties.Columns["Posta_Naziv"].Visible = false;
                cmbPrimalac.Properties.Columns["Drzava_ID"].Visible = false;
                cmbPrimalac.Properties.Columns["Drzava_Naziv"].Visible = false;
                cmbPrimalac.Properties.Columns["Posta_Broj"].Caption = "Broj pošte";
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

        private void popuniKupce()
        {
            try
            {
                cmbKupac.Properties.DataSource = MainForm.getData.GetTableKupci();
                cmbKupac.Properties.ValueMember = "Subjekat_ID";
                cmbKupac.Properties.DisplayMember = "Naziv";
                cmbKupac.Properties.ForceInitialize();
                cmbKupac.Properties.PopulateColumns();
                cmbKupac.Properties.Columns["Subjekat_ID"].Visible = false;
                cmbKupac.Properties.Columns["jeKupac"].Visible = false;
                cmbKupac.Properties.Columns["jeDobavljac"].Visible = false;
                cmbKupac.Properties.Columns["Posta_ID"].Visible = false;
                cmbKupac.Properties.Columns["Posta_Naziv"].Visible = false;
                cmbKupac.Properties.Columns["Drzava_ID"].Visible = false;
                cmbKupac.Properties.Columns["Drzava_Naziv"].Visible = false;
                cmbKupac.Properties.Columns["Posta_Broj"].Caption = "Broj pošte";
                cmbKupac.Properties.Columns["Naziv2"].Caption = "Pun naziv";
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

        private void dospeceRacuna() //RACUNA DOSPECE PREMA DATUMU OTPREMNICE I TXTBOXA
        {
            try
            {
                int Dan = Convert.ToInt16(txtVazenje.Text.ToString().Trim());
                DateTime temp;
                temp = dateTimePickerOtpremnica.Value.AddDays(Dan);
                dateTimePickerDospece.Value = temp;
                dateTimePickerDospece.MinDate = dateTimePickerOtpremnica.Value;
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

        private void txtVazenje_Leave(object sender, EventArgs e) // UPISUJE 0 UKOLIKO JE POLJE PRAZNO
        {
            try
            {
                if (txtVazenje.Text == "" || txtVazenje.Text == null)
                    txtVazenje.Text = "0";
                dospeceRacuna();
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

        private void txtVazenje_KeyPress(object sender, KeyPressEventArgs e) //ZABRANJUJE SVE OSIM BROJEVA
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtVazenje_TextChanged(object sender, EventArgs e)
        {
            if (txtVazenje.Text != "" || txtVazenje.Text == null)
                dospeceRacuna();
            btnSacuvajPromene.Enabled = true;
        }

        private void cmbNaziv_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                BaseEdit edit = sender as BaseEdit;
                object artikal_ID = edit.EditValue;

                using (var con = new MONTESINOEntities())
                {
                    var artikal = con.Artikals.Find(Convert.ToInt32(artikal_ID.ToString().Trim()));
                    redic["Artikal_ID"] = artikal.Artikal_ID;
                    redic["Sifra"] = artikal.Artikal_ID;
                    redic["Naziv"] = artikal.Artikal_ID;
                    redic["Opis"] = artikal.Opis;
                    redic["JM_ID"] = artikal.JM_ID;
                    redic["Kolicina"] = MainForm.getData.Formatiraj(0);
                    redic["Cena"] = MainForm.getData.Formatiraj(artikal.Cena);
                    redic["Rabat"] = MainForm.getData.Formatiraj(0);
                    redic["Vrednost"] = MainForm.getData.Formatiraj(0);
                    redic["PS_ID"] = artikal.PS_ID;
                    redic["cenaPDV"] = MainForm.getData.Formatiraj(0);
                    redic["Napomena"] = null;
                    gridView.RefreshRow(redicBroj);
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

        private void cmbSifre_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                BaseEdit edit = sender as BaseEdit;
                object artikal_ID = edit.EditValue;

                using (var con = new MONTESINOEntities())
                {
                    var artikal = con.Artikals.Find(Convert.ToInt32(artikal_ID.ToString().Trim()));

                    redic["Artikal_ID"] = artikal.Artikal_ID.ToString().Trim();
                    redic["Sifra"] = artikal.Artikal_ID;
                    redic["Naziv"] = artikal.Artikal_ID;
                    redic["Opis"] = artikal.Opis.ToString().Trim(); ;
                    redic["JM_ID"] = artikal.JM_ID;
                    redic["Kolicina"] = MainForm.getData.Formatiraj(0);
                    redic["Cena"] = MainForm.getData.Formatiraj(artikal.Cena);
                    redic["Rabat"] = MainForm.getData.Formatiraj(0);
                    redic["Vrednost"] = MainForm.getData.Formatiraj(0);
                    redic["PS_ID"] = artikal.PS_ID;
                    redic["cenaPDV"] = MainForm.getData.Formatiraj(0);
                    redic["Napomena"] = null;
                    gridView.RefreshRow(redicBroj);
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

        private void dateTimePickerDospece_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int totalDays = Math.Abs(Convert.ToInt16((dateTimePickerOtpremnica.Value - dateTimePickerDospece.Value).TotalDays));
                txtVazenje.Text = totalDays.ToString();
                dateTimePickerDospece.MinDate = dateTimePickerOtpremnica.Value;
                dateTimePickerRacun.MinDate = dateTimePickerOtpremnica.Value;
                btnSacuvajPromene.Enabled = true;
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

        private void dateTimePickerOtpremnica_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePickerDospece.MinDate = dateTimePickerOtpremnica.Value;
                dateTimePickerRacun.MinDate = dateTimePickerOtpremnica.Value;
                dospeceRacuna();
                if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
                {
                    btnSacuvajPromene.Enabled = true;
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

        private void cmbKupac_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowViewKupac = (DataRowView)cmbKupac.GetSelectedDataRow();
                DataRowView rowViewPrimalac = (DataRowView)cmbPrimalac.GetSelectedDataRow();

                if (rowViewKupac != null && rowViewPrimalac == null) // AKO JE PRIMALAC PRAZAN
                {
                    DataRow red = rowViewKupac.Row;
                    txtNazivKupac.Text = red["Naziv2"].ToString();
                    txtAdresaKupac.Text = red["Adresa"].ToString();
                    txtOIBkupac.Text = red["OIB"].ToString();
                    txtTelefonKupac.Text = red["Telefon"].ToString();
                    txtPostaKupac.Text = red["Posta_Broj"].ToString();
                    txtPostaNazivKupac.Text = red["Posta_Naziv"].ToString();

                    txtNazivPrimalac.Text = red["Naziv2"].ToString();
                    txtAdresaPrimalac.Text = red["Adresa"].ToString();
                    txtOIBprimalac.Text = red["OIB"].ToString();
                    txtTelefonPrimalac.Text = red["Telefon"].ToString();
                    txtPostaPrimalac.Text = red["Posta_Broj"].ToString();
                    txtPostaNazivPrimalac.Text = red["Posta_Naziv"].ToString();

                    cmbPrimalac.ItemIndex = cmbKupac.ItemIndex;
                }
                else if (rowViewKupac != null) // AKO JE PRIMALAC PRAZAN
                {
                    if (izDokumenta == true)
                    {
                        DataRow red = rowViewKupac.Row;
                        txtNazivKupac.Text = red["Naziv2"].ToString();
                        txtAdresaKupac.Text = red["Adresa"].ToString();
                        txtOIBkupac.Text = red["OIB"].ToString();
                        txtTelefonKupac.Text = red["Telefon"].ToString();
                        txtPostaKupac.Text = red["Posta_Broj"].ToString();
                        txtPostaNazivKupac.Text = red["Posta_Naziv"].ToString();

                        txtNazivPrimalac.Text = red["Naziv2"].ToString();
                        txtAdresaPrimalac.Text = red["Adresa"].ToString();
                        txtOIBprimalac.Text = red["OIB"].ToString();
                        txtTelefonPrimalac.Text = red["Telefon"].ToString();
                        txtPostaPrimalac.Text = red["Posta_Broj"].ToString();
                        txtPostaNazivPrimalac.Text = red["Posta_Naziv"].ToString();

                        cmbPrimalac.ItemIndex = cmbKupac.ItemIndex;
                        izDokumenta = false;
                    }
                    else
                    {
                        DialogResult dialogResult = MessageBox.Show("Izmenjen je kupac na dokumentu, da li želite da promenite i primaoca?", "Potvrda", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            DataRow red = rowViewKupac.Row;
                            txtNazivKupac.Text = red["Naziv2"].ToString();
                            txtAdresaKupac.Text = red["Adresa"].ToString();
                            txtOIBkupac.Text = red["OIB"].ToString();
                            txtTelefonKupac.Text = red["Telefon"].ToString();
                            txtPostaKupac.Text = red["Posta_Broj"].ToString();
                            txtPostaNazivKupac.Text = red["Posta_Naziv"].ToString();

                            txtNazivPrimalac.Text = red["Naziv2"].ToString();
                            txtAdresaPrimalac.Text = red["Adresa"].ToString();
                            txtOIBprimalac.Text = red["OIB"].ToString();
                            txtTelefonPrimalac.Text = red["Telefon"].ToString();
                            txtPostaPrimalac.Text = red["Posta_Broj"].ToString();
                            txtPostaNazivPrimalac.Text = red["Posta_Naziv"].ToString();

                            cmbPrimalac.ItemIndex = cmbKupac.ItemIndex;
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            DataRow red = rowViewKupac.Row;
                            txtNazivKupac.Text = red["Naziv2"].ToString();
                            txtAdresaKupac.Text = red["Adresa"].ToString();
                            txtOIBkupac.Text = red["OIB"].ToString();
                            txtTelefonKupac.Text = red["Telefon"].ToString();
                            txtPostaKupac.Text = red["Posta_Broj"].ToString();
                            txtPostaNazivKupac.Text = red["Posta_Naziv"].ToString();
                        }
                    }
                }
                if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
                {
                    gridControl.Enabled = true;
                    btnSacuvajPromene.Enabled = true;
                }
                else
                    gridControl.Enabled = false;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.ToString().Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                        MessageBox.Show("Došlo je do greške prilikom brisanja. \nNije moguće obrsati elemnt koji se već koristi u drugoj tabeli.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message + "\"");
            }
        }

        private void cmbPrimalac_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowViewKupac = (DataRowView)cmbKupac.GetSelectedDataRow();
                DataRowView rowViewPrimalac = (DataRowView)cmbPrimalac.GetSelectedDataRow();

                if (rowViewKupac == null && rowViewPrimalac != null)
                {
                    DataRow red = rowViewPrimalac.Row;
                    txtNazivKupac.Text = red["Naziv2"].ToString();
                    txtAdresaKupac.Text = red["Adresa"].ToString();
                    txtOIBkupac.Text = red["OIB"].ToString();
                    txtTelefonKupac.Text = red["Telefon"].ToString();
                    txtPostaKupac.Text = red["Posta_Broj"].ToString();
                    txtPostaNazivKupac.Text = red["Posta_Naziv"].ToString();

                    txtNazivPrimalac.Text = red["Naziv2"].ToString();
                    txtAdresaPrimalac.Text = red["Adresa"].ToString();
                    txtOIBprimalac.Text = red["OIB"].ToString();
                    txtTelefonPrimalac.Text = red["Telefon"].ToString();
                    txtPostaPrimalac.Text = red["Posta_Broj"].ToString();
                    txtPostaNazivPrimalac.Text = red["Posta_Naziv"].ToString();

                    cmbKupac.ItemIndex = cmbPrimalac.ItemIndex;
                }
                else if (rowViewPrimalac != null)
                {
                    DataRow redPrimalac = rowViewPrimalac.Row;
                    txtNazivPrimalac.Text = redPrimalac["Naziv2"].ToString();
                    txtAdresaPrimalac.Text = redPrimalac["Adresa"].ToString();
                    txtOIBprimalac.Text = redPrimalac["OIB"].ToString();
                    txtTelefonPrimalac.Text = redPrimalac["Telefon"].ToString();
                    txtPostaPrimalac.Text = redPrimalac["Posta_Broj"].ToString();
                    txtPostaNazivPrimalac.Text = redPrimalac["Posta_Naziv"].ToString();
                }
                if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
                {
                    btnSacuvajPromene.Enabled = true;
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

        private void popuniBlankoRacun() // SETOVANJE DEFAULT PODESAVANJA
        {
            try
            {
                if (MainForm.getData.GetTablePodesavanja().Rows.Count > 0) // DA LI POSTOJE PREDEFINISANA PODESAVANJA
                {
                    DataRow sub = MainForm.getData.GetTablePodesavanja().Rows[0];
                    using (var con = new MONTESINOEntities())
                    {
                        var tempOdgovorneOsobe = con.OdgovorneOsobes.Find(Convert.ToInt32(sub["OdgovorneOsobe_ID"]));
                        var tempSubjekat = con.Subjekats.Find(Convert.ToInt32(sub["Subjekat_ID"]));
                        dateTimePickerOtpremnica.Value = DateTime.Today;
                        dateTimePickerRacun.Value = DateTime.Today;
                        dateTimePickerPDV.Value = DateTime.Today;
                        cmbOdgovornaOsoba.EditValue = tempOdgovorneOsobe.OdgovorneOsobe_ID.ToString().Trim();
                        cmbValuta.EditValue = sub["Valuta_ID"].ToString().Trim();
                        cmbMesto.EditValue = tempSubjekat.Posta_ID.ToString().Trim();
                        cmbNP.EditValue = sub["NP_ID"].ToString().Trim();
                        cmbStatus.EditValue = sub["StatusRacun_ID"].ToString().Trim();
                        txtVazenje.Text = sub["RokVazenja"].ToString().Trim();
                    }
                }
                else
                {
                    dateTimePickerOtpremnica.Value = DateTime.Today;
                    dateTimePickerRacun.Value = DateTime.Today;
                    dateTimePickerPDV.Value = DateTime.Today;
                    txtVazenje.Text = "0";
                    cmbOdgovornaOsoba.EditValue = null;
                    cmbOdgovornaOsoba.Reset();
                    cmbOdgovornaOsoba.ResetText();
                    cmbValuta.EditValue = null;
                    cmbValuta.Reset();
                    cmbValuta.ResetText();
                    cmbMesto.EditValue = null;
                    cmbMesto.Reset();
                    cmbMesto.ResetText();
                    cmbNP.EditValue = null;
                    cmbNP.Reset();
                    cmbNP.ResetText();
                    cmbStatus.EditValue = null;
                    cmbStatus.Reset();
                    cmbStatus.ResetText();
                }
                cmbKupac.EditValue = null;
                cmbKupac.Reset();
                cmbKupac.ResetText();

                cmbPrimalac.EditValue = null;
                cmbPrimalac.Reset();
                cmbPrimalac.ResetText();

                cmbVezniDokument.EditValue = null;
                cmbVezniDokument.Reset();
                cmbVezniDokument.ResetText();

                txtBrojRacuna.Text = blankoDokument;

                txtNazivKupac.Clear();
                txtAdresaKupac.Clear();
                txtOIBkupac.Clear();
                txtTelefonKupac.Clear();
                txtPostaKupac.Clear();
                txtPostaNazivKupac.Clear();

                txtNazivPrimalac.Clear();
                txtAdresaPrimalac.Clear();
                txtOIBprimalac.Clear();
                txtTelefonPrimalac.Clear();
                txtPostaPrimalac.Clear();
                txtPostaNazivPrimalac.Clear();

                txtNapomena.Clear();
                txtUkupno.Clear();
                txtVrednost.Clear();
                txtPDV.Clear();
                txtPopustProcenat.Clear();
                txtPopustBroj.Clear();
                txtZaPlacanje.Clear();
                txtHRK.Clear();

                PopuniRacunStavke("0");
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

        private void btnRacuni_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(VrstaDokumenta);
                using (var Racuni = new Dokumenti("RAC", VrstaDokumenta))
                {
                    Racuni.BringToFront();
                    Racuni.Activate();
                    var result = Racuni.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        izDokumenta = true;
                        Promet_ID = Racuni.Broj;
                        PopuniRacunZaglavlje(Promet_ID, "NE");
                        PopuniRacunStavke(Promet_ID);
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

        private void btnNapraviRacun_Click(object sender, EventArgs e)
        {
            try
            {
                cmbKupac.Enabled = true;
                cmbPrimalac.Enabled = true;
                cmbMesto.Enabled = true;
                cmbNP.Enabled = true;
                cmbOdgovornaOsoba.Enabled = true;
                //cmbValuta.Enabled = true;
                cmbVezniDokument.Enabled = true;
                cmbStatus.Enabled = true;
                txtVazenje.Enabled = true;
                txtNapomena.Enabled = true;
                dateTimePickerOtpremnica.Enabled = true;
                dateTimePickerRacun.Enabled = true;
                dateTimePickerPDV.Enabled = true;
                dateTimePickerDospece.Enabled = true;
                btnNapredJedanput.Enabled = true;
                btnNazadJedanput.Enabled = true;
                btnSacuvajPromene.Enabled = true;
                btnOtpremnica.Enabled = true;
                dbtnOtpremnica.Enabled = true;
                btnIzbrisi.Enabled = true;
                btnRacuni.Enabled = true;
                btnRacun.Enabled = true;
                popuniBlankoRacun();
                Promet_ID = null;
                izDokumenta = true;
                gridControl.Enabled = false;
                proveraNavigacije();
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

        private void gridView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                var Pozicija = gridView.GetFocusedRowCellValue("Pozicija").ToString().Trim();
                var Sifra = gridView.GetFocusedRowCellValue("Sifra").ToString().Trim();
                var Naziv = gridView.GetFocusedRowCellValue("Naziv").ToString().Trim();
                var Opis = gridView.GetFocusedRowCellValue("Opis").ToString().Trim();
                var Kolicina = gridView.GetFocusedRowCellValue("Kolicina").ToString().Trim();
                var Cena = gridView.GetFocusedRowCellValue("Cena").ToString().Trim();
                var CenaPDV = gridView.GetFocusedRowCellValue("CenaPDV").ToString().Trim();
                var Rabat = gridView.GetFocusedRowCellValue("Rabat").ToString().Trim();
                var JM = gridView.GetFocusedRowCellValue("JM_ID").ToString().Trim();
                var PS = gridView.GetFocusedRowCellValue("PS_ID").ToString().Trim();
                var Napomena = gridView.GetFocusedRowCellValue("Napomena").ToString().Trim();
                var Vrednost = gridView.GetFocusedRowCellValue("Vrednost").ToString().Trim();
                Boolean napravi = true;

                if (Sifra == "")
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Sifra"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (Naziv == "")
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Naziv"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (Kolicina == "")
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Količina"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (Cena == "")
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Cena"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (CenaPDV == "" || CenaPDV.Length > 40)
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["CenaPDV"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (Rabat == "" || Rabat.Length > 40)
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Rabat"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (JM == "" || JM.Length > 40)
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["JM_ID"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (PS == "" || PS.Length > 40)
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["PS_ID"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (Vrednost == "")
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Vrednost"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (gridView.IsNewItemRow(e.RowHandle) && napravi == true) //PRAVLJENJE NOVE STAVKE
                {
                    var red = gridView.GetDataRow(e.RowHandle);

                    using (var con = new MONTESINOEntities())
                    {
                        var artikal = con.Artikals.Find(Convert.ToInt32(red["Artikal_ID"]));

                        var stavka = new Model.PrometStavke()
                        {
                            Promet_ID = Convert.ToInt32(Promet_ID),
                            Pozicija = Convert.ToInt16(red["Pozicija"]),
                            Napomena = red["Napomena"].ToString().Trim(),
                            Artikal_ID = Convert.ToInt32(red["Artikal_ID"]),
                            Sifra = artikal.Sifra,
                            Naziv = artikal.Naziv,
                            JM_ID = Convert.ToInt16(red["JM_ID"]),
                            Opis = red["Opis"].ToString().Trim(),
                            Kolicina = Convert.ToDecimal(red["Kolicina"]),
                            Cena = Convert.ToDecimal(red["Cena"]),
                            Rabat = Convert.ToDecimal(red["Rabat"]),
                            Vrednost = MainForm.getData.IzracunajVrednost(Convert.ToDecimal(red["Kolicina"]), Convert.ToDecimal(red["Cena"]), Convert.ToDecimal(red["Rabat"])),
                            PS_ID = Convert.ToInt32(red["PS_ID"])
                        };
                        var ps_vrednost = con.Porezs.Find(Convert.ToInt32(red["PS_ID"]));
                        stavka.CenaPDV = MainForm.getData.IzracunajCenaPDV(stavka.Vrednost, ps_vrednost.Vrednost);

                        con.PrometStavkes.Add(stavka);
                        con.SaveChanges();
                        red["PrometStavke_ID"] = stavka.PrometStavke_ID;
                        popuniFooter();
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

        private void gridView_KeyDown(object sender, KeyEventArgs e) //BRISANJE STAVKE
        {
            try
            {
                var red = gridView.GetFocusedDataRow();
                if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control && red != null)
                {
                    if (MessageBox.Show("Da li ste sigurni da želite obrisati izabrani red? Akcija je neopoziva!", "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) !=
                      DialogResult.Yes)
                        return;

                    using (var con = new MONTESINOEntities())
                    {
                        var stavke_id = Convert.ToInt32(red["PrometStavke_ID"]);
                        con.PrometStavkes.RemoveRange(con.PrometStavkes.Where(x => x.PrometStavke_ID == stavke_id));
                        con.SaveChanges();
                        GridView view = sender as GridView;
                        view.DeleteRow(view.FocusedRowHandle);
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

        private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                var red = gridView.GetDataRow(e.RowHandle);
                Console.WriteLine("gridView_CellValueChanged");

                if (red["Kolicina"] == null || red["Kolicina"].ToString().Trim() == "")
                    red["Kolicina"] = MainForm.getData.Formatiraj(0);
                if (red["Cena"] == null || red["Cena"].ToString().Trim() == "")
                    red["Cena"] = MainForm.getData.Formatiraj(0);
                if (red["Rabat"] == null || red["Rabat"].ToString().Trim() == "")
                    red["Rabat"] = MainForm.getData.Formatiraj(0);
                if (red["Vrednost"] == null || red["Vrednost"].ToString().Trim() == "")
                    red["Vrednost"] = MainForm.getData.Formatiraj(0);
                if (red["CenaPDV"] == null || red["CenaPDV"].ToString().Trim() == "")
                    red["CenaPDV"] = MainForm.getData.Formatiraj(0);

                //AUTOMATSKO RACUNANJE CENE U DVA PRAVCA
                if ((red["Kolicina"] != null && red["Kolicina"].ToString().Trim() != "") && (red["Cena"] != null && red["Cena"].ToString().Trim() != "")
                    && (red["Rabat"] != null && red["Rabat"].ToString().Trim() != ""))
                {
                    //NORMALAN PRAVAC (NA OSNOVU KOLICINE CENE I RABATA RACUNA VREDNOST)
                    if (e.Column.FieldName.ToString().Trim() == "Kolicina" || e.Column.FieldName.ToString().Trim() == "Cena" || e.Column.FieldName.ToString().Trim() == "Rabat")
                        red["Vrednost"] = MainForm.getData.Formatiraj(MainForm.getData.IzracunajVrednost(Convert.ToDecimal(red["Kolicina"]), Convert.ToDecimal(red["Cena"]), Convert.ToDecimal(red["Rabat"])));
                    //KONTRA PRAVAC VREDNOST (NA OSNOVU VREDNOSTI RESETUJE RABAT I RACUNA CENU)
                    if (e.Column.FieldName.ToString().Trim() == "Vrednost")
                    {
                        if (Convert.ToDecimal(red["Kolicina"].ToString().Trim()) == 0)
                            red["Kolicina"] = 1;
                        red["Rabat"] = MainForm.getData.Formatiraj(0);
                        red["Cena"] = MainForm.getData.Formatiraj(MainForm.getData.IzracunajOdVrednosti(Convert.ToDecimal(red["Vrednost"]), Convert.ToDecimal(red["Kolicina"])));
                    }
                    //KONTRA PRAVAC SKROZ (NA OSNOVU CENE SA PDVOM RACUNA VREDNOST I NAKON TOGA OSTALO U NAZAD)
                    if (e.Column.FieldName.ToString().Trim() == "CenaPDV" && red["PS_ID"] != null && red["PS_ID"].ToString().Trim() != "")
                    {
                        using (var con = new MONTESINOEntities())
                        {
                            var ps = con.Porezs.Find(Convert.ToInt32(red["PS_ID"]));
                            red["Vrednost"] = MainForm.getData.Formatiraj(MainForm.getData.IzracunajOdCenaPDV(Convert.ToDecimal(red["CenaPDV"]), ps.Vrednost));
                            if (Convert.ToDecimal(red["Kolicina"].ToString().Trim()) == 0)
                                red["Kolicina"] = 1;
                            red["Rabat"] = MainForm.getData.Formatiraj(0);
                            red["Cena"] = MainForm.getData.Formatiraj(MainForm.getData.IzracunajOdVrednosti(Convert.ToDecimal(red["Vrednost"]), Convert.ToDecimal(red["Kolicina"])));
                        }
                    }
                    if (red["PS_ID"] != null && red["PS_ID"].ToString().Trim() != "")
                    {
                        using (var con = new MONTESINOEntities())
                        {
                            var ps = con.Porezs.Find(Convert.ToInt32(red["PS_ID"]));
                            red["CenaPDV"] = MainForm.getData.Formatiraj(MainForm.getData.IzracunajCenaPDV(Convert.ToDecimal(red["Vrednost"]), ps.Vrednost));
                        }
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

        private void gridView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                Console.WriteLine("gridView_RowUpdated:");
                if (gridView.IsNewItemRow(e.RowHandle) == false) //IZMENA
                {
                    //ZNACI TREBA DA SLUZI SAMO DA SNIMI RED U BAZU, NISTA DA RACUNA!
                    var red = gridView.GetFocusedDataRow();
                    using (var con = new MONTESINOEntities())
                    {
                        var formatiranBroj = MainForm.getData.Formatiraj(Convert.ToDecimal(red["Kolicina"].ToString().Trim()));
                        red["Kolicina"] = formatiranBroj;
                        formatiranBroj = MainForm.getData.Formatiraj(Convert.ToDecimal(red["Cena"].ToString().Trim()));
                        red["Cena"] = formatiranBroj;
                        formatiranBroj = MainForm.getData.Formatiraj(Convert.ToDecimal(red["Vrednost"].ToString().Trim()));
                        red["Vrednost"] = formatiranBroj;
                        formatiranBroj = MainForm.getData.Formatiraj(Convert.ToDecimal(red["CenaPDV"].ToString().Trim()));
                        red["CenaPDV"] = formatiranBroj;
                        formatiranBroj = MainForm.getData.FormatirajProcenat(Convert.ToDecimal(red["Rabat"].ToString().Trim()));
                        red["Rabat"] = formatiranBroj;

                        var stavka_ID = Convert.ToInt32(red["PrometStavke_ID"]);
                        var jm = con.PrometStavkes.SingleOrDefault(x => x.PrometStavke_ID == stavka_ID);
                        var artikal = con.Artikals.Find(Convert.ToInt32(red["Artikal_ID"]));

                        jm.Artikal_ID = Convert.ToInt32(red["Artikal_ID"]);
                        jm.Sifra = artikal.Sifra;
                        jm.Naziv = artikal.Naziv;
                        jm.Napomena = red["Napomena"].ToString().Trim();
                        jm.Opis = red["Opis"].ToString().Trim();
                        jm.Kolicina = Convert.ToDecimal(red["Kolicina"]);
                        jm.Cena = Convert.ToDecimal(red["Cena"]);
                        jm.Vrednost = Convert.ToDecimal(red["Vrednost"]);
                        jm.Rabat = Convert.ToDecimal(red["Rabat"]);
                        jm.JM_ID = Convert.ToInt16(red["JM_ID"]);
                        jm.PS_ID = Convert.ToInt32(red["PS_ID"]);
                        jm.CenaPDV = Convert.ToDecimal(red["CenaPDV"]);

                        con.SaveChanges();
                    }
                }
                else
                {
                    BeginInvoke(new Action(() => {
                        gridView.FocusedColumn = gridView.Columns["Sifra"];
                    }));
                }
                popuniFooter();
                Console.WriteLine("PROMET_ID: " + Promet_ID);
                kreirajAzurirajRacun();
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

        private void kreirajAzurirajRacun()
        {
            try
            {
                if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
                {
                    Console.WriteLine("gridControl_Click");
                    if (Promet_ID == null) // NAPRAVI NOVI UKOLIKO PROMET_ID NE POSTOJI
                    {
                        Console.WriteLine("IF");
                        using (var con = new MONTESINOEntities())
                        {
                            int poslednji = con.Promets.Where(a => a.VrstaDokumenta == this.VrstaDokumenta).DefaultIfEmpty().Max(x => x == null ? 0 : x.Promet_ID);
                            int Broj = 1;
                            if (poslednji != 0)
                            {
                                var racunObj = con.Promets.Find(Convert.ToInt32(poslednji));
                                Broj = Convert.ToInt32(racunObj.Broj.ToString().Substring(0, racunObj.Broj.ToString().Length-4)) + 1;
                            }
                            string noviBroj = Broj.ToString().Trim() + this.VrstaDokumenta + "01";
                            var racun = new Model.Promet()
                            {
                                VrstaDokumenta = this.VrstaDokumenta,
                                Broj = noviBroj,
                                Subjekat_ID = Convert.ToInt32(cmbKupac.EditValue),
                                Primalac_ID = Convert.ToInt32(cmbPrimalac.EditValue),
                                Otpremnica = dateTimePickerOtpremnica.Value,
                                Racun = dateTimePickerRacun.Value,
                                PDVDate = dateTimePickerPDV.Value,
                                RokVazenja = Convert.ToInt16(txtVazenje.Text),
                                Dospece = dateTimePickerDospece.Value,
                            };
                            if (cmbValuta.EditValue != null)
                                racun.Valuta_ID = Convert.ToInt32(cmbValuta.EditValue);
                            else
                                racun.Valuta_ID = null;
                            if (cmbNP.EditValue != null)
                                racun.NP_ID = Convert.ToInt32(cmbNP.EditValue);
                            else
                                racun.NP_ID = null;
                            if (cmbVezniDokument.EditValue != null && cmbVezniDokument.EditValue.ToString().Trim() != "")
                                racun.Predracun_ID = Convert.ToInt32(cmbVezniDokument.EditValue);
                            else
                                racun.Predracun_ID = null;
                            if (cmbMesto.EditValue != null)
                                racun.Posta_ID = Convert.ToInt32(cmbMesto.EditValue);
                            else
                                racun.Posta_ID = null;
                            if (cmbOdgovornaOsoba.EditValue != null)
                                racun.OdgovorneOsobe_ID = Convert.ToInt32(cmbOdgovornaOsoba.EditValue);
                            else
                                racun.OdgovorneOsobe_ID = null;
                            if (cmbStatus.EditValue != null)
                                racun.Status_ID = Convert.ToInt32(cmbStatus.EditValue);
                            else
                                racun.Status_ID = null;

                            //FOOTER
                            if (txtNapomena.Text != null && txtNapomena.Text.ToString().Trim() != "")
                                racun.Napomena = txtNapomena.Text.ToString().Trim();
                            else
                                racun.Napomena = null;
                            if (txtUkupno.Text != null && txtUkupno.Text.ToString().Trim() != "")
                                racun.Ukupno = Convert.ToDecimal(txtUkupno.Text);
                            else
                                racun.Ukupno = 0;
                            if (txtVrednost.Text != null && txtVrednost.Text.ToString().Trim() != "")
                                racun.Vrednost = Convert.ToDecimal(txtVrednost.Text);
                            else
                                racun.Vrednost = 0;
                            if (txtPopustBroj.Text != null && txtPopustBroj.Text.ToString().Trim() != "")
                                racun.PopustBroj = Convert.ToDecimal(txtPopustBroj.Text);
                            else
                                racun.PopustBroj = 0;
                            if (txtPopustProcenat.Text != null && txtPopustProcenat.Text.ToString().Trim() != "")
                                racun.PopustProcenat = Convert.ToDecimal(txtPopustProcenat.Text);
                            else
                                racun.PopustProcenat = 0;
                            if (txtPDV.Text != null && txtPDV.Text.ToString().Trim() != "")
                                racun.PDV = Convert.ToDecimal(txtPDV.Text);
                            else
                                racun.PDV = 0;
                            if (txtZaPlacanje.Text != null && txtZaPlacanje.Text.ToString().Trim() != "")
                                racun.ZaPlacanje = Convert.ToDecimal(txtZaPlacanje.Text);
                            else
                                racun.ZaPlacanje = 0;

                            con.Promets.Add(racun);
                            con.SaveChanges();
                            Promet_ID = racun.Promet_ID.ToString().Trim();
                            txtBrojRacuna.Text = MainForm.getData.BrojRastaviDokumenta(noviBroj);
                        }
                    }
                    else // AZURIRAJ POSTOJECI
                    {
                        using (var con = new MONTESINOEntities())
                        {
                            var racun_id = Convert.ToInt32(Promet_ID);
                            var racun = con.Promets.SingleOrDefault(x => x.Promet_ID == racun_id);

                            racun.Subjekat_ID = Convert.ToInt32(cmbKupac.EditValue);
                            racun.Primalac_ID = Convert.ToInt32(cmbPrimalac.EditValue);
                            racun.Otpremnica = dateTimePickerOtpremnica.Value;
                            racun.Racun = dateTimePickerRacun.Value;
                            racun.PDVDate = dateTimePickerPDV.Value;
                            racun.RokVazenja = Convert.ToInt16(txtVazenje.Text);
                            racun.Dospece = dateTimePickerDospece.Value;

                            // OPCIONO JER MOZE I NULL
                            if (cmbValuta.EditValue != null)
                                racun.Valuta_ID = Convert.ToInt32(cmbValuta.EditValue);
                            else
                                racun.Valuta_ID = null;
                            if (cmbNP.EditValue != null)
                                racun.NP_ID = Convert.ToInt32(cmbNP.EditValue);
                            else
                                racun.NP_ID = null;
                            if (cmbVezniDokument.EditValue != null && cmbVezniDokument.EditValue.ToString().Trim() != "")
                                racun.Predracun_ID = Convert.ToInt32(cmbVezniDokument.EditValue);
                            else
                                racun.Predracun_ID = null;
                            if (cmbMesto.EditValue != null)
                                racun.Posta_ID = Convert.ToInt32(cmbMesto.EditValue);
                            else
                                racun.Posta_ID = null;
                            if (cmbOdgovornaOsoba.EditValue != null)
                                racun.OdgovorneOsobe_ID = Convert.ToInt32(cmbOdgovornaOsoba.EditValue);
                            else
                                racun.OdgovorneOsobe_ID = null;
                            if (cmbStatus.EditValue != null)
                                racun.Status_ID = Convert.ToInt32(cmbStatus.EditValue);
                            else
                                racun.Status_ID = null;

                            //FOOTER
                            if (txtNapomena.Text != null && txtNapomena.Text.ToString().Trim() != "")
                                racun.Napomena = txtNapomena.Text.ToString().Trim();
                            else
                                racun.Napomena = null;
                            if (txtUkupno.Text != null && txtUkupno.Text.ToString().Trim() != "")
                                racun.Ukupno = Convert.ToDecimal(txtUkupno.Text);
                            else
                                racun.Ukupno = 0;
                            if (txtVrednost.Text != null && txtVrednost.Text.ToString().Trim() != "")
                                racun.Vrednost = Convert.ToDecimal(txtVrednost.Text);
                            else
                                racun.Vrednost = 0;
                            if (txtPopustBroj.Text != null && txtPopustBroj.Text.ToString().Trim() != "")
                                racun.PopustBroj = Convert.ToDecimal(txtPopustBroj.Text);
                            else
                                racun.PopustBroj = 0;
                            if (txtPopustProcenat.Text != null && txtPopustProcenat.Text.ToString().Trim() != "")
                                racun.PopustProcenat = Convert.ToDecimal(txtPopustProcenat.Text);
                            else
                                racun.PopustProcenat = 0;
                            if (txtPDV.Text != null && txtPDV.Text.ToString().Trim() != "")
                                racun.PDV = Convert.ToDecimal(txtPDV.Text);
                            else
                                racun.PDV = 0;
                            if (txtZaPlacanje.Text != null && txtZaPlacanje.Text.ToString().Trim() != "")
                                racun.ZaPlacanje = Convert.ToDecimal(txtZaPlacanje.Text);
                            else
                                racun.ZaPlacanje = 0;

                            // OPCIONO JER MOZE I NULL
                            con.SaveChanges();
                        }
                    }
                    popuniFooter();
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

        private void gridView_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            try
            {

                Console.WriteLine("gridView_InitNewRow: " + stavke.Rows.Count);
                var poslednjiRed = gridView.GetDataRow(stavke.Rows.Count - 1);
                var red = gridView.GetDataRow(e.RowHandle);

                if (poslednjiRed == null)
                    red["Pozicija"] = "1";
                else
                    red["Pozicija"] = Convert.ToInt32(poslednjiRed["Pozicija"]) + 1;

                redic = gridView.GetFocusedDataRow();
                redicBroj = stavke.Rows.Count;
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

        private void gridView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                Console.WriteLine("gridView_CellValueChanging");
                redic = gridView.GetFocusedDataRow();
                redicBroj = e.RowHandle;
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

        private void gridControl_Click(object sender, EventArgs e)
        {
            try
            {
                kreirajAzurirajRacun();
                proveraNavigacije();
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

        private void btnNazadJedanput_Click(object sender, EventArgs e)
        {
            try
            {
                if (Promet_ID != null)
                {
                    using (var con = new MONTESINOEntities())
                    {
                        var predracunTrenutni = Convert.ToInt32(Promet_ID.ToString().Trim());
                        var prethodni = (from x in con.Promets where x.Promet_ID < predracunTrenutni orderby x.Promet_ID descending select x).Where(a => a.VrstaDokumenta == this.VrstaDokumenta).FirstOrDefault();
                        var sledeci = (from x in con.Promets where x.Promet_ID > predracunTrenutni orderby x.Promet_ID ascending select x).Where(a => a.VrstaDokumenta == this.VrstaDokumenta).FirstOrDefault();

                        if (prethodni != null) // POPUNI SLEDECI RACUN
                        {
                            izDokumenta = true;
                            Promet_ID = prethodni.Promet_ID.ToString().Trim();
                            PopuniRacunZaglavlje(Promet_ID, "NE");
                            PopuniRacunStavke(Promet_ID);
                        }
                        //PROVERI DA LI POSTOJI SLEDECI OD TOG KAO I PRETHODNI OD TOG, UKOLIKO NE BLOKIRAJ DUGME
                        proveraNavigacije();
                    }
                }
                else
                {
                    ucitajPoslednjiRacun();
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

        private void btnNapredJedanput_Click(object sender, EventArgs e)
        {
            try
            {
                if (Promet_ID != null)
                {
                    using (var con = new MONTESINOEntities())
                    {
                        var predracunTrenutni = Convert.ToInt32(Promet_ID.ToString().Trim());
                        var prethodni = (from x in con.Promets where x.Promet_ID < predracunTrenutni orderby x.Promet_ID descending select x).Where(a => a.VrstaDokumenta == this.VrstaDokumenta).FirstOrDefault();
                        var sledeci = (from x in con.Promets where x.Promet_ID > predracunTrenutni orderby x.Promet_ID ascending select x).Where(a => a.VrstaDokumenta == this.VrstaDokumenta).FirstOrDefault();

                        if (sledeci != null) // POPUNI SLEDECI RACUN
                        {
                            izDokumenta = true;
                            Promet_ID = sledeci.Promet_ID.ToString().Trim();
                            PopuniRacunZaglavlje(Promet_ID, "NE");
                            PopuniRacunStavke(Promet_ID);
                        }
                        //PROVERI DA LI POSTOJI SLEDECI OD TOG KAO I PRETHODNI OD TOG, UKOLIKO NE BLOKIRAJ DUGME
                        proveraNavigacije();
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

        private void btnIzbrisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (Promet_ID != null)
                {
                    if (MessageBox.Show("Da li ste sigurni da želite obrisati trenutni predračun? \n Akcija je neopoziva!", "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) !=
                    DialogResult.Yes)
                        return;
                    using (var con = new MONTESINOEntities())
                    {
                        stavke.Rows.Clear();
                        izDokumenta = true;
                        var promet_id = Convert.ToInt32(Promet_ID);
                        con.Promets.Remove(con.Promets.Find(promet_id));
                        con.SaveChanges();
                        ucitajPoslednjiRacun();
                    }
                }
                else
                    ucitajPoslednjiRacun();
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

        private void btnSacuvajPromene_Click(object sender, EventArgs e)
        {
            try
            {
                kreirajAzurirajRacun();
                proveraNavigacije();
                btnSacuvajPromene.Enabled = false;
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

        private void gridControl_EnabledChanged(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("gridControl_EnabledChanged");
                kreirajAzurirajRacun();
                proveraNavigacije();
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

        private void btnPrenesi_Click(object sender, EventArgs e)
        {
            try
            {
                DataRowView rowViewPredracun = (DataRowView)cmbVezniDokument.GetSelectedDataRow();

                if (rowViewPredracun != null)
                {
                    DataRow red = rowViewPredracun.Row;
                    DataRowView rowViewKupac = (DataRowView)cmbKupac.GetSelectedDataRow();
                    DataRowView rowViewPrimalac = (DataRowView)cmbPrimalac.GetSelectedDataRow();

                    if (rowViewKupac != null || rowViewPrimalac != null) //AKO VEC POSTOJI KUPAC/PRIMILAC i RACUN_ID
                    {
                        if (MessageBox.Show("Da li ste sigurni da želite da dodate stavke sa predračuna " + cmbVezniDokument.Text + "?", "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) !=
                        DialogResult.Yes)
                            return;

                        stavke.Rows.Clear();
                        //izDokumenta = true;
                        //PopuniRacunZaglavlje(red["Predracun_ID"].ToString().Trim(), "DA");
                        kopirajStavke(red["Predracun_ID"].ToString().Trim());
                        PopuniRacunStavke(Promet_ID);
                    }
                    else // POKRECE SE PRVI PUT SAMO (Kad se napravi veza)
                    {
                        stavke.Rows.Clear();
                        //izDokumenta = true;
                        // PopuniRacunZaglavlje(red["Predracun_ID"].ToString().Trim(), "DA");
                        kopirajStavke(red["Predracun_ID"].ToString().Trim());
                        PopuniRacunStavke(Promet_ID);
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
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 500)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 500)) + "\"");
            }
        }

        private void kopirajStavke(string predracun_id) // KOPIRANJE STAVKE IZ PREDRACUNA U PROMET
        {
            try
            {
                int ID = Convert.ToInt32(predracun_id);

                using (var con = new MONTESINOEntities())
                {
                    var stavke = con.PredracunStavkes.Where(x => x.Predracun_ID == ID).ToList();
                    var poslednjaStavka = con.PrometStavkes.Where(x => x.Promet_ID.ToString().Trim() == Promet_ID).OrderByDescending(x => x.PrometStavke_ID).FirstOrDefault();
                    int poz = 0;

                    if (poslednjaStavka == null)
                        poz = 1;
                    else
                        poz = poslednjaStavka.Pozicija + 1;

                    foreach (PredracunStavke stavka in stavke)
                    {
                        PrometStavke novaStavka = new PrometStavke
                        {
                            Promet_ID = Convert.ToInt32(Promet_ID),
                            Pozicija = Convert.ToInt16(poz),
                            Napomena = stavka.Napomena,
                            Artikal_ID = stavka.Artikal_ID,
                            Sifra = stavka.Sifra,
                            Naziv = stavka.Naziv,
                            Opis = stavka.Opis,
                            JM_ID = stavka.JM_ID,
                            Kolicina = stavka.Kolicina,
                            Cena = stavka.Cena,
                            Rabat = stavka.Rabat,
                            Vrednost = stavka.Vrednost,
                            PS_ID = stavka.PS_ID,
                            CenaPDV = stavka.CenaPDV
                        };
                        con.PrometStavkes.Add(novaStavka);
                        poz++;
                    }
                    con.SaveChanges();
                    PopuniRacunStavke(Promet_ID);
                    popuniFooter();
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

        private void Racuni_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                kreirajAzurirajRacun();
                proveraNavigacije();
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

        private void btnRacun_Click(object sender, EventArgs e)
        {
            if(VrstaDokumenta == "01")
                Stampaj("Račun");
            else
                Stampaj("Odobrenje");
        }

        private void btnOtpremnica_Click(object sender, EventArgs e)
        {
            StampajOtpremnicu("Otpremnica");
        }

        private void Stampaj(string stanje)
        {
            try
            {
                kreirajAzurirajRacun();
                proveraNavigacije();
                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ispis"].ConnectionString;

                reportRacun.SetParameterValue("Predracun_ID", Promet_ID);
                reportRacun.SetParameterValue("Broj", txtBrojRacuna.Text);
                reportRacun.SetParameterValue("Stanje", stanje);
                if (cmbVezniDokument.EditValue != null && cmbVezniDokument.EditValue.ToString().Trim() != "" && cmbVezniDokument.Text.ToString().Trim() != "" && cmbVezniDokument.Text.ToString().Trim() != null && stanje != "Odobrenje")
                    reportRacun.SetParameterValue("Veza", "Narudžbenica: " + cmbVezniDokument.Text);
                else if (cmbVezniDokument.EditValue != null && cmbVezniDokument.EditValue.ToString().Trim() != "" && cmbVezniDokument.Text.ToString().Trim() != "" && cmbVezniDokument.Text.ToString().Trim() != null && stanje == "Odobrenje")
                    reportRacun.SetParameterValue("Veza", "Veza: " + cmbVezniDokument.Text);
                else
                    reportRacun.SetParameterValue("Veza", "");
                reportRacun.SetParameterValue("Slovima", MainForm.getData.Slovima(Convert.ToDecimal(txtZaPlacanje.Text), cmbValuta.Text));
                reportRacun.Dictionary.Connections[0].ConnectionString = connection;

                Stampa s = new Stampa();
                reportRacun.Preview = s.control;
                if (reportRacun.Prepare())
                {
                    reportRacun.ShowPrepared();
                    s.ShowDialog();
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

        private void StampajOtpremnicu(string stanje) // OVO JE BEZ CENE
        {
            try
            {
                kreirajAzurirajRacun();
                proveraNavigacije();
                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ispis"].ConnectionString;

                reportOtpremnica.SetParameterValue("Predracun_ID", Promet_ID);
                reportOtpremnica.SetParameterValue("Broj", txtBrojRacuna.Text);
                reportOtpremnica.SetParameterValue("Stanje", stanje);
                if (cmbVezniDokument.EditValue != null && cmbVezniDokument.EditValue.ToString().Trim() != "" && cmbVezniDokument.Text.ToString().Trim() != "" && cmbVezniDokument.Text.ToString().Trim() != null)
                    reportOtpremnica.SetParameterValue("Veza", "Narudžbenica: " + cmbVezniDokument.Text);
                else
                    reportOtpremnica.SetParameterValue("Veza", "");
                reportOtpremnica.SetParameterValue("Slovima", MainForm.getData.Slovima(Convert.ToDecimal(txtZaPlacanje.Text), cmbValuta.Text));
                reportOtpremnica.Dictionary.Connections[0].ConnectionString = connection;

                Stampa s = new Stampa();
                reportOtpremnica.Preview = s.control;
                if (reportOtpremnica.Prepare())
                {
                    reportOtpremnica.ShowPrepared();
                    s.ShowDialog();
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
        private void StampajOtpremnicuSaCenama(string stanje) // OVO JE SA CENAMA
        {
            try
            {
                kreirajAzurirajRacun();
                proveraNavigacije();
                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ispis"].ConnectionString;

                reportOtpremnicaSaCenama.SetParameterValue("Predracun_ID", Promet_ID);
                reportOtpremnicaSaCenama.SetParameterValue("Broj", txtBrojRacuna.Text);
                reportOtpremnicaSaCenama.SetParameterValue("Stanje", stanje);
                if (cmbVezniDokument.EditValue != null && cmbVezniDokument.EditValue.ToString().Trim() != "" && cmbVezniDokument.Text.ToString().Trim() != "" && cmbVezniDokument.Text.ToString().Trim() != null)
                    reportOtpremnicaSaCenama.SetParameterValue("Veza", "Narudžbenica: " + cmbVezniDokument.Text);
                else
                    reportOtpremnicaSaCenama.SetParameterValue("Veza", "");
                reportOtpremnicaSaCenama.SetParameterValue("Slovima", MainForm.getData.Slovima(Convert.ToDecimal(txtZaPlacanje.Text), cmbValuta.Text));
                reportOtpremnicaSaCenama.Dictionary.Connections[0].ConnectionString = connection;

                Stampa s = new Stampa();
                reportOtpremnicaSaCenama.Preview = s.control;
                if (reportOtpremnicaSaCenama.Prepare())
                {
                    reportOtpremnicaSaCenama.ShowPrepared();
                    s.ShowDialog();
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
        private void Racuni_Activated(object sender, EventArgs e) // MOZDA UKLJUCITI
        {
            //popuniKupce();
            //popuniPrimaoce();
            //popuniValute();
            //popuniNP();
            //popuniMesto();
            //popuniOdgovornuOsobu();
            //popuniVezneDokumente();
            //popuniStatus();
        }

        private void otpremnicaBezCena_Click(object sender, EventArgs e)
        {
            StampajOtpremnicu("Otpremnica");
        }

        private void otpremnicaSaCenama_Click(object sender, EventArgs e)
        {
            StampajOtpremnicuSaCenama("Otpremnica");
        }

        private void dateTimePickerRacun_ValueChanged(object sender, EventArgs e)
        {
            if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
            {
                btnSacuvajPromene.Enabled = true;
            }
        }

        private void dateTimePickerPDV_ValueChanged(object sender, EventArgs e)
        {
            if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
            {
                btnSacuvajPromene.Enabled = true;
            }
        }

        private void cmbValuta_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
            {
                btnSacuvajPromene.Enabled = true;
            }
        }

        private void cmbVezniDokument_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
            {
                btnSacuvajPromene.Enabled = true;
            }
        }

        private void cmbNP_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
            {
                btnSacuvajPromene.Enabled = true;
            }
        }

        private void cmbMesto_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
            {
                btnSacuvajPromene.Enabled = true;
            }
        }

        private void cmbOdgovornaOsoba_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
            {
                btnSacuvajPromene.Enabled = true;
            }
        }

        private void cmbStatus_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
            {
                btnSacuvajPromene.Enabled = true;
            }
        }

        private void txtNapomena_TextChanged(object sender, EventArgs e)
        {
            if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
            {
                btnSacuvajPromene.Enabled = true;
            }
        }

        private void btnPrvi_Click(object sender, EventArgs e)
        {
            try
            {
                using (var con = new MONTESINOEntities())
                {
                        if (MainForm.getData.GetTableRacuni(VrstaDokumenta).Rows.Count > 0) // DA LI POSTOJI PREDRACUN UOPSTE (SAMO DOK SE NE NAPRAVI PRVI RACUN)
                        {
                            var racun = con.Promets.Find(con.Promets.Where(a => a.VrstaDokumenta == this.VrstaDokumenta).Min(x => x.Promet_ID));
                            izDokumenta = true;
                            Promet_ID = racun.Promet_ID.ToString().Trim();
                            PopuniRacunZaglavlje(Promet_ID, "NE");
                            PopuniRacunStavke(Promet_ID);

                            //PROVERI DA LI POSTOJI SLEDECI OD TOG KAO I PRETHODNI OD TOG, UKOLIKO NE BLOKIRAJ DUGME
                            proveraNavigacije();
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

        private void btnPoslednji_Click(object sender, EventArgs e)
        {
            try
            {
                if (Promet_ID != null)
                {
                    using (var con = new MONTESINOEntities())
                    {
                        if (MainForm.getData.GetTableRacuni(VrstaDokumenta).Rows.Count > 0) // DA LI POSTOJI PREDRACUN UOPSTE (SAMO DOK SE NE NAPRAVI PRVI RACUN)
                        {
                            var racun = con.Promets.Find(con.Promets.Where(a => a.VrstaDokumenta == this.VrstaDokumenta).Max(x => x.Promet_ID));
                            izDokumenta = true;
                            Promet_ID = racun.Promet_ID.ToString().Trim();
                            PopuniRacunZaglavlje(Promet_ID, "NE");
                            PopuniRacunStavke(Promet_ID);

                            //PROVERI DA LI POSTOJI SLEDECI OD TOG KAO I PRETHODNI OD TOG, UKOLIKO NE BLOKIRAJ DUGME
                            proveraNavigacije();
                        }
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
        private void dbtnOtpremnica_Click(object sender, EventArgs e)
        {
            StampajOtpremnicu("Otpremnica");
        }

        private void btnOptremnicaBezCena_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StampajOtpremnicu("Otpremnica");         
        }

        private void btnOtpremnicaSaCenama_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            StampajOtpremnicuSaCenama("Otpremnica");
        }
    }
}