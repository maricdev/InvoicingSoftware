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
    public partial class Predracuni : Form
    {
        private Boolean izDokumenta = false;
        private string Predracun_ID = null;
        private DataTable stavke;
        private string nazivKolone = null;
        private RepositoryItemGridLookUpEdit cmbSifre = new RepositoryItemGridLookUpEdit();
        private RepositoryItemGridLookUpEdit cmbNaziv = new RepositoryItemGridLookUpEdit();
        private RepositoryItemGridLookUpEdit cmbJM = new RepositoryItemGridLookUpEdit();
        private RepositoryItemGridLookUpEdit cmbPS = new RepositoryItemGridLookUpEdit();
        private DataRow redic; //SLUZI ZA OSVEZAVANJE REDA U REALTIME-U KADA SE MENJA SIFRA ILI NAZIV
        private int redicBroj; //SLUZI ZA OSVEZAVANJE REDA U REALTIME-U KADA SE MENJA SIFRA ILI NAZIV

        public Predracuni()
        {
            try
            {
                //OSNOVNA INICIJALIZACIJA
                InitializeComponent();
                VazenjePredracuna();
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

                ucitajPoslednjiPredracun();
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
        private void ucitajPoslednjiPredracun()
        {
            try
            {
                if (MainForm.getData.GetTablePredracuni().Rows.Count > 0) // DA LI POSTOJI PREDRACUN UOPSTE (SAMO DOK SE NE NAPRAVI PRVI PREDRACUN)
                {
                    using (var con = new MONTESINOEntities())
                    {
                        var poslednji = con.Predracuns.Max(x => x.Predracun_ID);
                        Predracun_ID = poslednji.ToString().Trim();
                        PopuniPredracunZaglavlje(poslednji.ToString().Trim());
                        PopuniPredracunStavke(poslednji.ToString().Trim());
                        proveraNavigacije();
                    }
                }
                else
                {
                    //UCITAJ POSLEDNJI RACUN (POKRECE SE SAMO NA POCETKU)
                    popuniBlankoPredracun();
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
                    dateTimePickerDatum.Enabled = false;
                    dateTimePickerRokIsporuke.Enabled = false;
                    dateTimePickerVazenje.Enabled = false;
                    btnIzbrisiPredracun.Enabled = false;
                    btnPredracuni.Enabled = false;
                    btnPredracun.Enabled = false;
                    proveraNavigacije();
                    txtBrojPredracuna.Text = DateTime.Now.ToString("yy") + "-PON-??????";
                    MessageBox.Show("Izgleda da još niste napravili prvi predračun. \nDa bi ste omogućili formu po prvi put, morate napraviti" +
                        " predračun klikom na + (plus).", "Dobrodošli", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void cmbSifre_EditValueChanged(object sender, EventArgs e) // EVENT ZA HVATANJE INSTANT PROMENE
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

        private void cmbNaziv_EditValueChanged(object sender, EventArgs e) // EVENT ZA HVATANJE INSTANT PROMENE
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

        private void PopuniPredracunZaglavlje(string broj) //POPUNJAVANJE CELOG ZAGLAVLJA I FOOTERA PREKO PREDRACUN_ID
        {
            try
            {
                using (var con = new MONTESINOEntities())
                {
                    var predracun = con.Predracuns.FirstOrDefault(x => x.Predracun_ID.ToString().Trim() == broj.ToString().Trim());

                    txtBrojPredracuna.Text = MainForm.getData.BrojRastavi(predracun.Broj); //BROJ PREDRACUNA
                    cmbKupac.EditValue = predracun.Subjekat_ID.ToString().Trim(); //KUPAC
                    cmbPrimalac.EditValue = predracun.Primalac_ID.ToString().Trim(); //PRIMILAC
                    dateTimePickerDatum.Value = predracun.Datum; //DATUM
                    dateTimePickerRokIsporuke.Value = predracun.RokIsporuke; //ROK ISPORUKE
                    txtVazenje.Text = predracun.RokVazenja.ToString().Trim(); // TXTBOX VAZENJE

                    //OPCIONO
                    if (predracun.Ponuda_ID != null)
                        cmbVezniDokument.EditValue = predracun.Ponuda_ID.ToString().Trim(); // VEZNI DOKUMENT (PONUDA)
                    else
                    {
                        cmbVezniDokument.EditValue = null;
                        cmbVezniDokument.Reset();
                        cmbVezniDokument.ResetText();
                    }
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
                    {
                        txtUkupno.Text = predracun.Ukupno.ToString().Trim(); // UKUPNO
                        txtUkupno.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtUkupno.Text));
                    }
                    else
                        txtUkupno.Text = null;
                    if (predracun.Vrednost != null)
                    {
                        txtVrednost.Text = predracun.Vrednost.ToString().Trim(); // VREDNOST
                        txtVrednost.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtVrednost.Text));
                    }
                    else
                        txtVrednost.Text = null;
                    if (predracun.PDV != null)
                    {
                        txtPDV.Text = predracun.PDV.ToString().Trim(); // PDV
                        txtPDV.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtPDV.Text));
                    }
                    else
                        txtPDV.Text = null;
                    if (predracun.PopustBroj != null)
                    {
                        txtPopustBroj.Text = predracun.PopustBroj.ToString().Trim(); // POPUST BROJ
                        txtPopustBroj.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtPopustBroj.Text));
                    }
                    else
                        txtPopustBroj.Text = null;
                    if (predracun.PopustProcenat != null)
                    {
                        txtPopustProcenat.Text = predracun.PopustProcenat.ToString().Trim(); // POPUST PROCENAT
                        txtPopustProcenat.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtPopustProcenat.Text));
                    }
                    else
                        txtPopustProcenat.Text = null;
                    if (predracun.ZaPlacanje != null)
                    {
                        txtZaPlacanje.Text = predracun.ZaPlacanje.ToString().Trim(); // ZA PLACANJE
                        txtZaPlacanje.Text = MainForm.getData.Formatiraj(Convert.ToDecimal(txtZaPlacanje.Text));
                    }
                    else
                        txtZaPlacanje.Text = null;
                    //OPCIONO
                    proveraNavigacije();
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

        private void PopuniPredracunStavke(string broj) //POPUNJAVANJE STAVKI U TABELI PREKO PREDRACUN_ID
        {
            try
            {
                stavke = MainForm.getData.GetTablePredracuniStavke(broj);
                gridControl.DataSource = stavke;
                // stavke.DefaultView.Sort = "[Pozicija] ASC";
                //SAKRIVANJE ODREDJENIH TABELA
                gridView.Columns["PredracunStavke_ID"].Visible = false;
                gridView.Columns["Predracun_ID"].Visible = false;
                gridView.Columns["Artikal_ID"].Visible = false;
                gridView.Columns["PredracunStavke_ID"].Visible = false;
                gridView.Columns["PredracunStavke_ID"].Visible = false;
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

        private void VazenjePredracuna() //RACUNA VAZENJE PREMA DATUMU IZADVANJA I TXTBOXA
        {
            try
            {
                int Dan = Convert.ToInt16(txtVazenje.Text.ToString().Trim());
                DateTime temp;
                temp = dateTimePickerDatum.Value.AddDays(Dan);
                dateTimePickerVazenje.Value = temp;
                dateTimePickerVazenje.MinDate = dateTimePickerDatum.Value;
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
                VazenjePredracuna();
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
                VazenjePredracuna();
            if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
            {
                btnSacuvajPromene.Enabled = true;
            }
        }

        private void dateTimePickerVazenje_ValueChanged(object sender, EventArgs e) //RACUNA TEXT BOX PREMA DATUMU VAZENJA
        {
            try
            {
                int totalDays = Math.Abs(Convert.ToInt16((dateTimePickerDatum.Value - dateTimePickerVazenje.Value).TotalDays));
                txtVazenje.Text = totalDays.ToString();
                dateTimePickerVazenje.MinDate = dateTimePickerDatum.Value;
                dateTimePickerRokIsporuke.MinDate = dateTimePickerDatum.Value;
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

        private void dateTimePickerDatum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateTimePickerVazenje.MinDate = dateTimePickerDatum.Value;
                dateTimePickerRokIsporuke.MinDate = dateTimePickerDatum.Value;
                VazenjePredracuna();
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
                        MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.InnerException.ToString().Trim().Substring(0, Math.Min(ex.InnerException.ToString().Trim().Length, 350)) + "\"");
                }
            }
            catch (Exception ex)
            {
                MainForm.logger.Error("Naziv klase: " + this.GetType().Name + "\n Funkcija: " + System.Reflection.MethodBase.GetCurrentMethod().Name + "\n\"" + ex.Message.ToString().Trim().Substring(0, Math.Min(ex.Message.ToString().Trim().Length, 350)) + "\"");
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

        private void popuniBlankoPredracun() // SETOVANJE DEFAULT PODESAVANJA
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
                        dateTimePickerDatum.Value = DateTime.Today;
                        cmbOdgovornaOsoba.EditValue = tempOdgovorneOsobe.OdgovorneOsobe_ID.ToString().Trim();
                        cmbValuta.EditValue = sub["Valuta_ID"].ToString().Trim();
                        cmbMesto.EditValue = tempSubjekat.Posta_ID.ToString().Trim();
                        cmbNP.EditValue = sub["NP_ID"].ToString().Trim();
                        cmbStatus.EditValue = sub["StatusPredracun_ID"].ToString().Trim();
                        txtVazenje.Text = sub["RokVazenja"].ToString().Trim();
                        dateTimePickerRokIsporuke.Value = dateTimePickerVazenje.Value;
                    }
                }
                else
                {
                    dateTimePickerDatum.Value = DateTime.Today;
                    txtVazenje.Text = "0";
                    dateTimePickerRokIsporuke.Value = dateTimePickerVazenje.Value;
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

                txtBrojPredracuna.Text = DateTime.Now.ToString("yy") + "-PON-??????";

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

                PopuniPredracunStavke("0");
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

        private void popuniVezneDokumente()
        {
            try
            {
                cmbVezniDokument.Properties.DataSource = MainForm.getData.GetTableVezniDokument();
                cmbVezniDokument.Properties.ValueMember = "Predracun_ID";
                cmbVezniDokument.Properties.DisplayMember = "Broj";
                cmbVezniDokument.Properties.ForceInitialize();
                cmbVezniDokument.Properties.PopulateColumns();
                cmbVezniDokument.Properties.Columns["Predracun_ID"].Visible = false;
                cmbVezniDokument.Properties.Columns["Subjekat_ID"].Caption = "Kupac";
                cmbVezniDokument.Properties.Columns["Primilac_ID"].Caption = "Primilac";
                cmbVezniDokument.Properties.Columns["RokVazenja"].Caption = "Rok važenja - Dana";
                cmbVezniDokument.Properties.Columns["RokIsporuke"].Caption = "Rok isporuke";
                cmbVezniDokument.Properties.Columns["ZaPlacanje"].Caption = "Za plaćanje";
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

        private void popuniStatus()
        {
            try
            {
                cmbStatus.Properties.DataSource = MainForm.getData.GetTableStatusi("PON");
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

        public void popuniTabeluDefaultVrednostima() // popunjava sva polja sa vrednostima default
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

        private void btnPredračuni_Click(object sender, EventArgs e)
        {
            try
            {
                using (var Predracuni = new Dokumenti("PON", ""))
                {
                    Predracuni.BringToFront();
                    Predracuni.Activate();
                    var result = Predracuni.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        izDokumenta = true;
                        Predracun_ID = Predracuni.Broj;
                        PopuniPredracunZaglavlje(Predracun_ID);
                        PopuniPredracunStavke(Predracun_ID);
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

        private void btnNapraviPredracun_Click(object sender, EventArgs e)
        {
            try
            {
                cmbKupac.Enabled = true;
                cmbPrimalac.Enabled = true;
                cmbMesto.Enabled = true;
                cmbNP.Enabled = true;
                cmbOdgovornaOsoba.Enabled = true;
                cmbVezniDokument.Enabled = true;
                cmbStatus.Enabled = true;
                txtVazenje.Enabled = true;
                txtNapomena.Enabled = true;
                dateTimePickerDatum.Enabled = true;
                dateTimePickerRokIsporuke.Enabled = true;
                dateTimePickerVazenje.Enabled = true;
                btnPredracuni.Enabled = true;

                btnIzbrisiPredracun.Enabled = false;
                btnPredracun.Enabled = false;
                btnPonuda.Enabled = false;
                btnSacuvajPromene.Enabled = false;

                popuniBlankoPredracun();
                Predracun_ID = null;
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
                    view.SetColumnError(gridView.Columns["Kolicina"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (Cena == "")
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Cena"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (CenaPDV == "")
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
                if (JM == "")
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["JM_ID"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (PS == "")
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
                        var stavka = new Model.PredracunStavke()
                        {
                            Predracun_ID = Convert.ToInt32(Predracun_ID),
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
                        con.PredracunStavkes.Add(stavka);
                        con.SaveChanges();
                        red["PredracunStavke_ID"] = stavka.PredracunStavke_ID;
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
                        var stavke_id = Convert.ToInt32(red["PredracunStavke_ID"]);
                        con.PredracunStavkes.RemoveRange(con.PredracunStavkes.Where(x => x.PredracunStavke_ID == stavke_id));
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

        private void gridView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e) // SNIMANJE U BAZU IZMENJENOG REDA
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

                        var stavka_ID = Convert.ToInt32(red["PredracunStavke_ID"]);
                        var jm = con.PredracunStavkes.SingleOrDefault(x => x.PredracunStavke_ID == stavka_ID);
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
                Console.WriteLine("PREDRACUN_ID: " + Predracun_ID);
                kreirajAzurirajPredracun();
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
                if (stavke.Rows.Count > 0) // BUG
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

        private void kreirajAzurirajPredracun()
        {
            try
            {
                if (cmbKupac.EditValue != null && cmbPrimalac.EditValue != null)
                {
                    Console.WriteLine("gridControl_Click");
                    if (Predracun_ID == null) // NAPRAVI NOVI UKOLIKO PREDRACUN_ID NE POSTOJI
                    {
                        using (var con = new MONTESINOEntities())
                        {
                            int poslednji = con.Predracuns.DefaultIfEmpty().Max(x => x == null ? 0 : x.Predracun_ID);
                            int Broj = 1;
                            if (poslednji != 0)
                            {
                                var predracunObj = con.Predracuns.Find(Convert.ToInt32(poslednji));
                                Broj = Convert.ToInt32(predracunObj.Broj.ToString().Substring(5, 6)) + 1;
                            }
                            string noviBroj = DateTime.Now.ToString("yy") + "PON" + Broj.ToString().Trim().PadLeft(6, '0');
                            var predracun = new Model.Predracun()
                            {
                                Broj = noviBroj,
                                Subjekat_ID = Convert.ToInt32(cmbKupac.EditValue),
                                Primalac_ID = Convert.ToInt32(cmbPrimalac.EditValue),
                                Datum = dateTimePickerDatum.Value,
                                RokVazenja = Convert.ToInt16(txtVazenje.Text),
                                RokIsporuke = dateTimePickerRokIsporuke.Value,
                            };
                            if (cmbValuta.EditValue != null)
                                predracun.Valuta_ID = Convert.ToInt32(cmbValuta.EditValue);
                            else
                                predracun.Valuta_ID = null;
                            if (cmbNP.EditValue != null)
                                predracun.NP_ID = Convert.ToInt32(cmbNP.EditValue);
                            else
                                predracun.NP_ID = null;
                            if (cmbVezniDokument.EditValue != null)
                                predracun.Ponuda_ID = Convert.ToInt32(cmbVezniDokument.EditValue);
                            else
                                predracun.Ponuda_ID = null;
                            if (cmbMesto.EditValue != null)
                                predracun.Posta_ID = Convert.ToInt32(cmbMesto.EditValue);
                            else
                                predracun.Posta_ID = null;
                            if (cmbOdgovornaOsoba.EditValue != null)
                                predracun.OdgovorneOsobe_ID = Convert.ToInt32(cmbOdgovornaOsoba.EditValue);
                            else
                                predracun.OdgovorneOsobe_ID = null;
                            if (cmbStatus.EditValue != null)
                                predracun.Status_ID = Convert.ToInt32(cmbStatus.EditValue);
                            else
                                predracun.Status_ID = null;

                            //FOOTER
                            if (txtNapomena.Text != null && txtNapomena.Text.ToString().Trim() != "")
                                predracun.Napomena = txtNapomena.Text.ToString().Trim();
                            else
                                predracun.Napomena = null;
                            if (txtUkupno.Text != null && txtUkupno.Text.ToString().Trim() != "")
                                predracun.Ukupno = Convert.ToDecimal(txtUkupno.Text);
                            else
                                predracun.Ukupno = 0;
                            if (txtVrednost.Text != null && txtVrednost.Text.ToString().Trim() != "")
                                predracun.Vrednost = Convert.ToDecimal(txtVrednost.Text);
                            else
                                predracun.Vrednost = 0;
                            if (txtPopustBroj.Text != null && txtPopustBroj.Text.ToString().Trim() != "")
                                predracun.PopustBroj = Convert.ToDecimal(txtPopustBroj.Text);
                            else
                                predracun.PopustBroj = 0;
                            if (txtPopustProcenat.Text != null && txtPopustProcenat.Text.ToString().Trim() != "")
                                predracun.PopustProcenat = Convert.ToDecimal(txtPopustProcenat.Text);
                            else
                                predracun.PopustProcenat = 0;
                            if (txtPDV.Text != null && txtPDV.Text.ToString().Trim() != "")
                                predracun.PDV = Convert.ToDecimal(txtPDV.Text);
                            else
                                predracun.PDV = 0;
                            if (txtZaPlacanje.Text != null && txtZaPlacanje.Text.ToString().Trim() != "")
                                predracun.ZaPlacanje = Convert.ToDecimal(txtZaPlacanje.Text);
                            else
                                predracun.ZaPlacanje = 0;

                            con.Predracuns.Add(predracun);
                            con.SaveChanges();
                            Predracun_ID = predracun.Predracun_ID.ToString().Trim();
                            txtBrojPredracuna.Text = MainForm.getData.BrojRastavi(noviBroj);
                        }
                    }
                    else // AZURIRAJ POSTOJECI
                    {
                        using (var con = new MONTESINOEntities())
                        {
                            var predracun_id = Convert.ToInt32(Predracun_ID);
                            var predracun = con.Predracuns.SingleOrDefault(x => x.Predracun_ID == predracun_id);

                            predracun.Subjekat_ID = Convert.ToInt32(cmbKupac.EditValue);
                            predracun.Primalac_ID = Convert.ToInt32(cmbPrimalac.EditValue);
                            predracun.Datum = dateTimePickerDatum.Value;
                            predracun.RokVazenja = Convert.ToInt16(txtVazenje.Text);
                            predracun.RokIsporuke = dateTimePickerRokIsporuke.Value;

                            // OPCIONO JER MOZE I NULL
                            if (cmbValuta.EditValue != null)
                                predracun.Valuta_ID = Convert.ToInt32(cmbValuta.EditValue);
                            else
                                predracun.Valuta_ID = null;
                            if (cmbNP.EditValue != null)
                                predracun.NP_ID = Convert.ToInt32(cmbNP.EditValue);
                            else
                                predracun.NP_ID = null;
                            if (cmbVezniDokument.EditValue != null)
                                predracun.Ponuda_ID = Convert.ToInt32(cmbVezniDokument.EditValue);
                            else
                                predracun.Ponuda_ID = null;
                            if (cmbMesto.EditValue != null)
                                predracun.Posta_ID = Convert.ToInt32(cmbMesto.EditValue);
                            else
                                predracun.Posta_ID = null;
                            if (cmbOdgovornaOsoba.EditValue != null)
                                predracun.OdgovorneOsobe_ID = Convert.ToInt32(cmbOdgovornaOsoba.EditValue);
                            else
                                predracun.OdgovorneOsobe_ID = null;
                            if (cmbStatus.EditValue != null)
                                predracun.Status_ID = Convert.ToInt32(cmbStatus.EditValue);
                            else
                                predracun.Status_ID = null;

                            //FOOTER
                            if (txtNapomena.Text != null && txtNapomena.Text.ToString().Trim() != "")
                                predracun.Napomena = txtNapomena.Text.ToString().Trim();
                            else
                                predracun.Napomena = null;
                            if (txtUkupno.Text != null && txtUkupno.Text.ToString().Trim() != "")
                                predracun.Ukupno = Convert.ToDecimal(txtUkupno.Text);
                            else
                                predracun.Ukupno = 0;
                            if (txtVrednost.Text != null && txtVrednost.Text.ToString().Trim() != "")
                                predracun.Vrednost = Convert.ToDecimal(txtVrednost.Text);
                            else
                                predracun.Vrednost = 0;
                            if (txtPopustBroj.Text != null && txtPopustBroj.Text.ToString().Trim() != "")
                                predracun.PopustBroj = Convert.ToDecimal(txtPopustBroj.Text);
                            else
                                predracun.PopustBroj = 0;
                            if (txtPopustProcenat.Text != null && txtPopustProcenat.Text.ToString().Trim() != "")
                                predracun.PopustProcenat = Convert.ToDecimal(txtPopustProcenat.Text);
                            else
                                predracun.PopustProcenat = 0;
                            if (txtPDV.Text != null && txtPDV.Text.ToString().Trim() != "")
                                predracun.PDV = Convert.ToDecimal(txtPDV.Text);
                            else
                                predracun.PDV = 0;
                            if (txtZaPlacanje.Text != null && txtZaPlacanje.Text.ToString().Trim() != "")
                                predracun.ZaPlacanje = Convert.ToDecimal(txtZaPlacanje.Text);
                            else
                                predracun.ZaPlacanje = 0;
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

                //var redic = gridView.GetDataRow(stavke.Rows.Count);
                //stavke.NewRow();
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

                nazivKolone = e.Column.FieldName;
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
                kreirajAzurirajPredracun();
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
                if (Predracun_ID != null)
                {
                    using (var con = new MONTESINOEntities())
                    {
                        var predracunTrenutni = Convert.ToInt32(Predracun_ID.ToString().Trim());
                        var prethodni = (from x in con.Predracuns where x.Predracun_ID < predracunTrenutni orderby x.Predracun_ID descending select x).FirstOrDefault();
                        var sledeci = (from x in con.Predracuns where x.Predracun_ID > predracunTrenutni orderby x.Predracun_ID ascending select x).FirstOrDefault();

                        if (prethodni != null) // POPUNI SLEDECI RACUN
                        {
                            izDokumenta = true;
                            Predracun_ID = prethodni.Predracun_ID.ToString().Trim();
                            PopuniPredracunZaglavlje(Predracun_ID);
                            PopuniPredracunStavke(Predracun_ID);
                        }
                        //PROVERI DA LI POSTOJI SLEDECI OD TOG KAO I PRETHODNI OD TOG, UKOLIKO NE BLOKIRAJ DUGME
                        proveraNavigacije();
                    }
                }
                else
                    ucitajPoslednjiPredracun();
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
                if (Predracun_ID != null)
                {
                    using (var con = new MONTESINOEntities())
                    {
                        var predracunTrenutni = Convert.ToInt32(Predracun_ID.ToString().Trim());
                        var prethodni = (from x in con.Predracuns where x.Predracun_ID < predracunTrenutni orderby x.Predracun_ID descending select x).FirstOrDefault();
                        var sledeci = (from x in con.Predracuns where x.Predracun_ID > predracunTrenutni orderby x.Predracun_ID ascending select x).FirstOrDefault();

                        if (sledeci != null) // POPUNI SLEDECI RACUN
                        {
                            izDokumenta = true;
                            Predracun_ID = sledeci.Predracun_ID.ToString().Trim();
                            PopuniPredracunZaglavlje(Predracun_ID);
                            PopuniPredracunStavke(Predracun_ID);
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

        private void proveraNavigacije() // PROVERA DA LI POSTOJI JEDAN NAPRED I JEDAN NAZAD
        {
            try
            {
                if (Predracun_ID != null)
                {
                    btnIzbrisiPredracun.Enabled = true;
                    btnPredracun.Enabled = true;
                    btnPonuda.Enabled = true;
                    using (var con = new MONTESINOEntities())
                    {
                        var predracunTrenutni = Convert.ToInt32(Predracun_ID.ToString().Trim());
                        var prethodni = (from x in con.Predracuns where x.Predracun_ID < predracunTrenutni orderby x.Predracun_ID descending select x).FirstOrDefault();
                        var sledeci = (from x in con.Predracuns where x.Predracun_ID > predracunTrenutni orderby x.Predracun_ID ascending select x).FirstOrDefault();

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
                    btnIzbrisiPredracun.Enabled = true;
                    btnPrvi.Enabled = true;
                    btnNazadJedanput.Enabled = true;

                    btnPoslednji.Enabled = false;
                    btnNapredJedanput.Enabled = false;

                    btnPonuda.Enabled = false;
                    btnPredracun.Enabled = false;
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

        private void btnIzbrisiPredracun_Click(object sender, EventArgs e)
        {
            try
            {
                if (Predracun_ID != null)
                {
                    if (MessageBox.Show("Da li ste sigurni da želite obrisati trenutni predračun? \n Akcija je neopoziva!", "Potvrda", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) !=
                    DialogResult.Yes)
                        return;
                    using (var con = new MONTESINOEntities())
                    {
                        stavke.Rows.Clear();
                        izDokumenta = true;
                        var predracun_id = Convert.ToInt32(Predracun_ID);
                        con.Predracuns.Remove(con.Predracuns.Find(predracun_id));
                        con.SaveChanges();
                        ucitajPoslednjiPredracun();
                    }
                }
                else
                    ucitajPoslednjiPredracun();
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
                kreirajAzurirajPredracun();
                proveraNavigacije();
                Console.WriteLine("PREDRACUN_ID: " + Predracun_ID);
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
                kreirajAzurirajPredracun();
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

        private void btnPredracun_Click(object sender, EventArgs e)
        {
            Stampaj("Predračun");
        }

        private void Stampaj(string stanje)
        {
            try
            {
                kreirajAzurirajPredracun();
                proveraNavigacije();
                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["ispis"].ConnectionString;

                reportPredracun.SetParameterValue("Predracun_ID", Predracun_ID);
                reportPredracun.SetParameterValue("Broj", txtBrojPredracuna.Text);
                reportPredracun.SetParameterValue("Stanje", stanje);
                reportPredracun.SetParameterValue("Slovima", MainForm.getData.Slovima(Convert.ToDecimal(txtZaPlacanje.Text), cmbValuta.Text));
                reportPredracun.Dictionary.Connections[0].ConnectionString = connection;

                Stampa s = new Stampa();
                reportPredracun.Preview = s.control;
                if (reportPredracun.Prepare())
                {
                    reportPredracun.ShowPrepared();
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

        private void btnPonuda_Click(object sender, EventArgs e)
        {
            Stampaj("Ponuda");
        }

        private void Predracuni_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                kreirajAzurirajPredracun();
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

        private void Predracuni_Activated(object sender, EventArgs e) // MOZDA UKLJUCITI
        {
            //VazenjePredracuna();
            //popuniKupce();
            //popuniPrimaoce();
            //popuniValute();
            //popuniNP();
            //popuniMesto();
            //popuniOdgovornuOsobu();
            //popuniVezneDokumente();
            //popuniStatus();
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

        private void dateTimePickerRokIsporuke_ValueChanged(object sender, EventArgs e)
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
                    if (MainForm.getData.GetTablePredracuni().Rows.Count > 0) // DA LI POSTOJI PREDRACUN UOPSTE (SAMO DOK SE NE NAPRAVI PRVI RACUN)
                    {
                        var predracun = con.Predracuns.Find(con.Predracuns.Min(x => x.Predracun_ID));
                        izDokumenta = true;
                        Predracun_ID = predracun.Predracun_ID.ToString().Trim();
                        PopuniPredracunZaglavlje(Predracun_ID);
                        PopuniPredracunStavke(Predracun_ID);

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
                using (var con = new MONTESINOEntities())
                {
                    if (MainForm.getData.GetTablePredracuni().Rows.Count > 0) // DA LI POSTOJI PREDRACUN UOPSTE (SAMO DOK SE NE NAPRAVI PRVI RACUN)
                    {
                        var predracun = con.Predracuns.Find(con.Predracuns.Max(x => x.Predracun_ID));
                        izDokumenta = true;
                        Predracun_ID = predracun.Predracun_ID.ToString().Trim();
                        PopuniPredracunZaglavlje(Predracun_ID);
                        PopuniPredracunStavke(Predracun_ID);

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
    }
}