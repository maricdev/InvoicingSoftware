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
    public partial class Poste : Form
    {
        private DataTable table = new DataTable();
        private RepositoryItemGridLookUpEdit cmbDrzava = new RepositoryItemGridLookUpEdit();

        public Poste()
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
                table = MainForm.getData.GetTablePoste();
                table.Columns.Add("Drzava_Naziv");

                gridControl.DataSource = table;
                // table.DefaultView.Sort = "[Kod] ASC";

                //SAKRIVANJE ODREDJENIH TABELA
                gridView.Columns["Posta_ID"].Visible = false;
                gridView.Columns["Drzava_ID"].Visible = false;
                //SAKRIVANJE ODREDJENIH TABELA

                //MENJANJE NAZIVA KOLONA
                gridView.Columns["Broj"].Caption = "Broj";
                gridView.Columns["Naziv"].Caption = "Mesto";
                gridView.Columns["Drzava_Naziv"].Caption = "Država";
                //MENJANJE NAZIVA KOLONA

                NapraviSpisakDrzava();
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

        public void NapraviSpisakDrzava()
        {
            try
            {
                popuniDefaultDrzave();
                cmbDrzava.DataSource = MainForm.getData.GetTableDrzava();
                cmbDrzava.ValueMember = "Drzava_ID";
                cmbDrzava.DisplayMember = "Naziv";
                cmbDrzava.PopulateViewColumns();
                cmbDrzava.View.Columns.ColumnByFieldName("Drzava_ID").Visible = false;

                gridControl.RepositoryItems.Add(cmbDrzava);
                gridView.Columns["Drzava_Naziv"].ColumnEdit = cmbDrzava;
                cmbDrzava.NullText = "Izaberite državu";
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

        public void popuniDefaultDrzave()
        {
            try
            {
                int brojacReda = 0;
                foreach (DataRow redPoste in table.Rows)
                    gridView.SetRowCellValue(brojacReda++, "Drzava_Naziv", redPoste["Drzava_ID"]);
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
                        red["Drzava_ID"] = red["Drzava_Naziv"].ToString().Trim();
                        var ps_id = Convert.ToInt32(red["Posta_ID"]);
                        var ps = con.Postas.SingleOrDefault(x => x.Posta_ID == ps_id);
                        ps.Naziv = red["Naziv"].ToString().Trim();
                        ps.Broj = red["Broj"].ToString().Trim();
                        ps.Drzava_ID = Convert.ToInt32(red["Drzava_ID"]);
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

                var Broj = gridView.GetFocusedRowCellValue("Broj").ToString().Trim();
                var Naziv = gridView.GetFocusedRowCellValue("Naziv").ToString().Trim();
                var Drzava_Naziv = gridView.GetFocusedRowCellValue("Drzava_Naziv").ToString().Trim();
                Boolean napravi = true;

                if (Broj == "" || Broj.Length > 13)
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Broj"], "[MAX: 13 KARAKTERA]: Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (Naziv == "" || Naziv.Length > 255)
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Naziv"], "[MAX: 255 KARAKTERA]: Polje ne sme biti prazno!");
                    napravi = false;
                }
                if (Drzava_Naziv == "")
                {
                    e.Valid = false;
                    view.SetColumnError(gridView.Columns["Drzava_Naziv"], "Polje ne sme biti prazno, morate izabrati državu!");
                    napravi = false;
                }
                if (gridView.IsNewItemRow(e.RowHandle) && napravi == true) //DODAVANJE
                {
                    using (var con = new MONTESINOEntities())
                    {
                        var red = gridView.GetDataRow(e.RowHandle);
                        red["Drzava_ID"] = red["Drzava_Naziv"].ToString().Trim();
                        var ps = new Model.Posta()
                        {
                            Broj = red["Broj"].ToString().Trim(),
                            Naziv = red["Naziv"].ToString().Trim(),
                            Drzava_ID = Convert.ToInt32(red["Drzava_ID"])
                        };
                        con.Postas.Add(ps);
                        con.SaveChanges();
                        red["Posta_ID"] = ps.Posta_ID;
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
                        var ps_id = Convert.ToInt32(red["Posta_ID"]);
                        con.Postas.RemoveRange(con.Postas.Where(x => x.Posta_ID == ps_id));
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

        private void Poste_Activated(object sender, EventArgs e)
        {
            try
            {
                NapraviSpisakDrzava();
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