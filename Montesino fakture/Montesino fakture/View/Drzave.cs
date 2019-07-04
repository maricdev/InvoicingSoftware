using DevExpress.XtraGrid.Views.Grid;
using Montesino_fakture.Model;
using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace Montesino_fakture.View
{
    public partial class Drzave : Form
    {
        private DataTable table = new DataTable();

        public Drzave()
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
                table = MainForm.getData.GetTableDrzava();
                gridControl.DataSource = table;
                // table.DefaultView.Sort = "[Kod] ASC";

                //SAKRIVANJE ODREDJENIH TABELA
                gridView.Columns["Drzava_ID"].Visible = false;
                //SAKRIVANJE ODREDJENIH TABELA

                //MENJANJE NAZIVA KOLONA
                gridView.Columns["Naziv"].Caption = "Naziv";
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
                        var ps_id = Convert.ToInt32(red["Drzava_ID"]);
                        var ps = con.Drzavas.SingleOrDefault(x => x.Drzava_ID == ps_id);
                        ps.Naziv = red["Naziv"].ToString().Trim();
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

        private void gridView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;

                var Naziv = gridView.GetFocusedRowCellValue("Naziv").ToString().Trim();
                Boolean napravi = true;

                if (Naziv == "" || Naziv.Length > 255)
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Naziv"], "[MAX: 255 KARAKTERA]: Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (gridView.IsNewItemRow(e.RowHandle) && napravi == true) //DODAVANJE
                {
                    var red = gridView.GetDataRow(e.RowHandle);

                    using (var con = new MONTESINOEntities())
                    {
                        var ps = new Model.Drzava()
                        {
                            Naziv = red["Naziv"].ToString().Trim(),
                        };
                        con.Drzavas.Add(ps);
                        con.SaveChanges();
                        red["Drzava_ID"] = ps.Drzava_ID;
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
                        var ps_id = Convert.ToInt32(red["Drzava_ID"]);
                        con.Drzavas.RemoveRange(con.Drzavas.Where(x => x.Drzava_ID == ps_id));
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