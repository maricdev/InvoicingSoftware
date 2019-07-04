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
    public partial class Artikli : Form
    {
        public Artikli()
        {
            try
            {
                InitializeComponent();
                ucitajTabelu();
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
        public void ucitajTabelu()
        {
            try
            {
                DataTable table = MainForm.getData.GetTableArtikli("");
                gridControl.DataSource = table;
                table.DefaultView.Sort = "[Naziv] ASC";

                //CHECKBOX SA TABELE
                RepositoryItemCheckEdit riAktivan = new RepositoryItemCheckEdit();
                riAktivan.ValueChecked = "1";
                riAktivan.ValueUnchecked = "0";
                gridView.Columns["Aktivan"].ColumnEdit = riAktivan;

                RepositoryItemCheckEdit riArtikal = new RepositoryItemCheckEdit();
                riArtikal.ValueChecked = "A";
                riArtikal.ValueUnchecked = "U";
                gridView.Columns["Artikal"].ColumnEdit = riArtikal;

                RepositoryItemCheckEdit riUsluga = new RepositoryItemCheckEdit();
                riUsluga.ValueChecked = "U";
                riUsluga.ValueUnchecked = "A";
                gridView.Columns["Usluga"].ColumnEdit = riUsluga;
                //CHECKBOX SA TABELE

                //SAKRIVANJE ODREDJENIH TABELA
                gridView.Columns["Artikal_ID"].Visible = false;
                gridView.Columns["JM_ID"].Visible = false;
                gridView.Columns["PS_ID"].Visible = false;
                //SAKRIVANJE ODREDJENIH TABELA

                //MENJANJE NAZIVA KOLONA
                gridView.Columns["Aktivan"].Caption = "Aktivan";
                gridView.Columns["Artikal"].Caption = "Artikal";
                gridView.Columns["Usluga"].Caption = "Usluga";
                gridView.Columns["Sifra"].Caption = "Sifra";
                gridView.Columns["Naziv"].Caption = "Naziv";
                gridView.Columns["Opis"].Caption = "Opis";
                gridView.Columns["Cena"].Caption = "Cena";
                gridView.Columns["JM_Kod"].Caption = "JM";
                gridView.Columns["PS_Naziv"].Caption = "Poreska stopa";

                gridView.Columns["Cena"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
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
        private void btnNapravi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) // KREIRANJE NOVOG IDENTA
        {
            try
            {
                using (var kreirajArtikal = new InterakcijaArtikal())
                {
                    kreirajArtikal.BringToFront();
                    kreirajArtikal.Activate();
                    var result = kreirajArtikal.ShowDialog();
                    if (result == DialogResult.OK)
                        ucitajTabelu();
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

        private void btnIzmeni_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) // DUGME IZMENA
        {
            try
            {
                izmeniIdent();
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
        private void izmeniIdent() //FUNCKIJA IZMENI IDENT
        {
            try
            {
                if (gridView.SelectedRowsCount > 0 && gridView.GetFocusedRowCellValue("Artikal_ID") != null)
                {
                    var Artikal_ID = gridView.GetFocusedRowCellValue("Artikal_ID");
                    var Aktivan = gridView.GetFocusedRowCellValue("Aktivan");
                    var Artikal = gridView.GetFocusedRowCellValue("Artikal");
                    var Usluga = gridView.GetFocusedRowCellValue("Usluga");
                    var Sifra = gridView.GetFocusedRowCellValue("Sifra");
                    var Naziv = gridView.GetFocusedRowCellValue("Naziv");
                    var Opis = gridView.GetFocusedRowCellValue("Opis");
                    var Cena = gridView.GetFocusedRowCellValue("Cena");
                    var JM_ID = gridView.GetFocusedRowCellValue("JM_ID");
                    var JM = gridView.GetFocusedRowCellValue("JM_Kod");
                    var PS_ID = gridView.GetFocusedRowCellValue("PS_ID");
                    var PoreskaStopa = gridView.GetFocusedRowCellValue("PS_Naziv");

                    Artikal art = new Artikal()
                    {
                        Artikal_ID = Convert.ToInt32(gridView.GetFocusedRowCellValue("Artikal_ID")),
                        Aktivan = gridView.GetFocusedRowCellValue("Aktivan").ToString(),
                        Vrsta = gridView.GetFocusedRowCellValue("Artikal").ToString(),
                        Sifra = gridView.GetFocusedRowCellValue("Sifra").ToString(),
                        Naziv = gridView.GetFocusedRowCellValue("Naziv").ToString(),
                        Opis = gridView.GetFocusedRowCellValue("Opis").ToString(),
                        Cena = Convert.ToDecimal(gridView.GetFocusedRowCellValue("Cena")),
                        JM_ID = Convert.ToInt32(gridView.GetFocusedRowCellValue("JM_ID")),
                        PS_ID = Convert.ToInt32(gridView.GetFocusedRowCellValue("PS_ID"))
                    };

                    using (var izmeniArtikal = new InterakcijaArtikal(art))
                    {
                        izmeniArtikal.BringToFront();
                        izmeniArtikal.Activate();
                        var result = izmeniArtikal.ShowDialog();
                        if (result == DialogResult.OK)
                            ucitajTabelu();
                    }
                }
                else
                    MessageBox.Show("Morate prvo da izaberete ident iz tabele!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnIzbrisi_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) // DUGME BRISANJE
        {
            try
            {
                BrisanjeIdenta();
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
        private void BrisanjeIdenta() // FUNKCIJA ZA BRISANJE IDENTA
        {
            try
            {
                if (gridView.SelectedRowsCount > 0)
                {
                    var Artikal_ID = gridView.GetFocusedRowCellValue("Artikal_ID");
                    var Naziv = gridView.GetFocusedRowCellValue("Naziv");

                    if (Artikal_ID != null)
                    {
                        DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite obrisati \"" + Naziv + "\" iz šifarnika idenata?", "Potvrda", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            using (var con = new MONTESINOEntities())
                            {
                                con.Artikals.RemoveRange(con.Artikals.Where(x => x.Artikal_ID.ToString().Trim() == Artikal_ID.ToString().Trim()));
                                con.SaveChanges();
                                ucitajTabelu();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Morate prvo da izabere ident iz tabele!", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void gridView_DoubleClick(object sender, EventArgs e) // IZMENA NA DVOKLIK
        {
            try
            {
                izmeniIdent();
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

        private void gridView_KeyDown(object sender, KeyEventArgs e) //BRISANJE NA CTL + DEL
        {
            try
            {
                var red = gridView.GetFocusedDataRow();
                if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control && red != null)
                {
                    BrisanjeIdenta();
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

        private void gridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;
                Meni.Show(view.GridControl, e.Point);
            }
        }

        private void menuIzmeni_Click_1(object sender, EventArgs e)
        {
            try
            {
                izmeniIdent();
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

        private void menuIzbrisi_Click_1(object sender, EventArgs e)
        {
            try
            {
                BrisanjeIdenta();
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