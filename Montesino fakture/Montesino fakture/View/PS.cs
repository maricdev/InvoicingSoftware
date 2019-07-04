using DevExpress.XtraGrid.Views.Grid;
using Montesino_fakture.Model;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace Montesino_fakture.View
{
    public partial class PS : Form
    {
        private DataTable table = new DataTable();

        public PS()
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
                table = MainForm.getData.GetTablePS();
                gridControl.DataSource = table;
                // table.DefaultView.Sort = "[Kod] ASC";

                //SAKRIVANJE ODREDJENIH TABELA
                gridView.Columns["PS_ID"].Visible = false;
                //SAKRIVANJE ODREDJENIH TABELA

                //MENJANJE NAZIVA KOLONA
                gridView.Columns["Kod"].Caption = "Kod";
                gridView.Columns["Naziv"].Caption = "Naziv";
                gridView.Columns["Vrednost"].Caption = "Vrednost %";
                gridView.Columns["Vrednost"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
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

        private void gridView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                if (gridView.IsNewItemRow(e.RowHandle) == false) //IZMENA
                {
                    using (var con = new MONTESINOEntities())
                    {
                        var red = gridView.GetFocusedDataRow();
                        var formatiranBroj = MainForm.getData.Formatiraj(Convert.ToDecimal(red["Vrednost"].ToString().Trim()));
                        red["Vrednost"] = formatiranBroj;

                        var ps_id = Convert.ToInt32(red["PS_ID"]);
                        var ps = con.Porezs.SingleOrDefault(x => x.PS_ID == ps_id);

                        ps.Kod = red["Kod"].ToString().Trim();
                        ps.Naziv = red["Naziv"].ToString().Trim();
                        ps.Vrednost = Convert.ToDecimal(red["Vrednost"]);
                        con.SaveChanges();
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

        public static string Formatiraj(double broj)
        {
            return string.Format("{0:0.00}", broj);
        }

        private void gridView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                var Kod = gridView.GetFocusedRowCellValue("Kod").ToString().Trim();
                var Naziv = gridView.GetFocusedRowCellValue("Naziv").ToString().Trim();
                var Vrednost = gridView.GetFocusedRowCellValue("Vrednost").ToString().Trim();
                Boolean napravi = true;

                if (Kod == "" || Kod.Length > 3)
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Kod"], "[MAX: 3 KARAKTERA]: Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (Naziv == "" || Naziv.Length > 255)
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Naziv"], "[MAX: 255 KARAKTERA]: Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (Vrednost == "")
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Vrednost"], "Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (gridView.IsNewItemRow(e.RowHandle) && napravi == true) //DODAVANJE
                {
                    using (var con = new MONTESINOEntities())
                    {
                        var red = gridView.GetDataRow(e.RowHandle);
                        var formatiranBroj = MainForm.getData.Formatiraj(Convert.ToDecimal(red["Vrednost"].ToString().Trim()));
                        red["Vrednost"] = formatiranBroj;
                        var ps = new Model.Porez()
                        {
                            Kod = red["Kod"].ToString().Trim(),
                            Naziv = red["Naziv"].ToString().Trim(),
                            Vrednost = Convert.ToDecimal(red["Vrednost"])
                        };
                        con.Porezs.Add(ps);
                        con.SaveChanges();
                        red["PS_ID"] = ps.PS_ID;
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

        private void gridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                var red = gridView.GetFocusedDataRow();
                if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.Control && red != null)
                {
                    if (MessageBox.Show("Da li ste sigurni da želite obrisati izabrani red?", "Potvrda", MessageBoxButtons.YesNo) !=
                      DialogResult.Yes)
                        return;
                    using (var con = new MONTESINOEntities())
                    {
                        var ps_id = Convert.ToInt32(red["PS_ID"]);
                        con.Porezs.RemoveRange(con.Porezs.Where(x => x.PS_ID == ps_id));
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
    }
}