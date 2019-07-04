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
    public partial class Status : Form
    {
        public Status()
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
                DataTable table = MainForm.getData.GetTableStatusi("SVI");
                gridControl.DataSource = table;
                // table.DefaultView.Sort = "[Kod] ASC";

                //SAKRIVANJE ODREDJENIH TABELA
                gridView.Columns["Status_ID"].Visible = false;
                //SAKRIVANJE ODREDJENIH TABELA

                RepositoryItemCheckEdit ri = new RepositoryItemCheckEdit();
                ri.ValueChecked = "1";
                ri.ValueUnchecked = "0";
                gridView.Columns["jePredracun"].ColumnEdit = ri;
                gridView.Columns["jeRacun"].ColumnEdit = ri;

                //MENJANJE NAZIVA KOLONA
                gridView.Columns["Naziv"].Caption = "Naziv statusa";
                gridView.Columns["jePredracun"].Caption = "Uključi status za predračun";
                gridView.Columns["jeRacun"].Caption = "Uključi status za račun";
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
                    var red = gridView.GetFocusedDataRow();

                    if (red["jePredracun"].ToString().Trim() == "")
                        red["jePredracun"] = "0";
                    if (red["jeRacun"].ToString().Trim() == "")
                        red["jeRacun"] = "0";

                    using (var con = new MONTESINOEntities())
                    {
                        var jm_id = Convert.ToInt32(red["Status_ID"]);
                        var jm = con.Status.SingleOrDefault(x => x.Status_ID == jm_id);
                        jm.Naziv = red["Naziv"].ToString().Trim();
                        jm.jePredracun = red["jePredracun"].ToString().Trim();
                        jm.jeRacun = red["jeRacun"].ToString().Trim();

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
                var Status_ID = gridView.GetFocusedRowCellValue("Status_ID").ToString().Trim();
                var jePredracun = gridView.GetFocusedRowCellValue("jePredracun").ToString().Trim();
                var jeRacun = gridView.GetFocusedRowCellValue("jeRacun").ToString().Trim();
                Boolean napravi = true;

                if (Naziv == "" || Naziv.Length > 255)
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Naziv"], "[MAX: 255 KARAKTERA]: Polje ne sme biti prazno!");
                    napravi = false;
                }

                if (MainForm.getData.GetTablePodesavanja().Rows.Count > 0)
                {
                    DataRow sub = MainForm.getData.GetTablePodesavanja().Rows[0];
                    Boolean aktivanPredracun = false;
                    Boolean aktivanRacun = false;
                    if (Status_ID.ToString().Trim() == sub["StatusPredracun_ID"].ToString().Trim())
                        aktivanPredracun = true;
                    if (Status_ID.ToString().Trim() == sub["StatusRacun_ID"].ToString().Trim())
                        aktivanRacun = true;

                    if (jePredracun.ToString().Trim() == "0" && aktivanPredracun == true)
                    {
                        e.Valid = false;
                        view.SetColumnError(gridView.Columns["jePredracun"], "Status mora biti aktivan jer je postavljen kao podrazumevana vrednost. \n Ukoliko želite da promenite stanje trenutnog statusa, morate prvo promeniti podrazumevanu vrednost u podacima firme.");
                        napravi = false;
                    }
                    if (jeRacun.ToString().Trim() == "0" && aktivanRacun == true)
                    {
                        e.Valid = false;
                        view.SetColumnError(gridView.Columns["jeRacun"], "Status mora biti aktivan jer je postavljen kao podrazumevana vrednost. \n Ukoliko želite da promenite stanje trenutnog statusa, morate prvo promeniti podrazumevanu vrednost u podacima firme.");
                        napravi = false;
                    }
                }

                if (gridView.IsNewItemRow(e.RowHandle) && napravi == true) //DODAVANJE
                {
                    var red = gridView.GetDataRow(e.RowHandle);

                    if (red["jePredracun"].ToString().Trim() == "")
                        red["jePredracun"] = "0";
                    if (red["jeRacun"].ToString().Trim() == "")
                        red["jeRacun"] = "0";

                    using (var con = new MONTESINOEntities())
                    {
                        var jm = new Model.Status()
                        {
                            Naziv = red["Naziv"].ToString().Trim(),
                            jePredracun = red["jePredracun"].ToString().Trim(),
                            jeRacun = red["jePredracun"].ToString().Trim()
                        };
                        con.Status.Add(jm);
                        con.SaveChanges();
                        red["Status_ID"] = jm.Status_ID;
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
                        var jm_id = Convert.ToInt32(red["Status_ID"]);
                        con.Status.RemoveRange(con.Status.Where(x => x.Status_ID == jm_id));
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