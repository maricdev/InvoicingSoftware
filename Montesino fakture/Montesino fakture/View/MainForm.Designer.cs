namespace Montesino_fakture.View
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.barManagerGornji = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btnSifarnikSubjekata = new DevExpress.XtraBars.BarButtonItem();
            this.btnSifarnikIdenata = new DevExpress.XtraBars.BarButtonItem();
            this.btnPredracuni = new DevExpress.XtraBars.BarButtonItem();
            this.btnRacuni = new DevExpress.XtraBars.BarButtonItem();
            this.btnOdobrenje = new DevExpress.XtraBars.BarButtonItem();
            this.btnPodesavanje = new DevExpress.XtraBars.BarButtonItem();
            this.menuPodesavanje = new DevExpress.XtraBars.PopupMenu(this.components);
            this.btnJM = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnPS = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnPosta = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnDrzave = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnNaciniPlacanja = new DevExpress.XtraBars.BarButtonItem();
            this.btnValute = new DevExpress.XtraBars.BarButtonItem();
            this.btnStatus = new DevExpress.XtraBars.BarButtonItem();
            this.btnOdgovorneOsobe = new DevExpress.XtraBars.BarButtonItem();
            this.btnFirma = new DevExpress.XtraBars.BarButtonItem();
            this.btnAutor = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnRacun = new DevExpress.XtraBars.BarLargeButtonItem();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerGornji)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuPodesavanje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManagerGornji
            // 
            this.barManagerGornji.AllowCustomization = false;
            this.barManagerGornji.AllowMoveBarOnToolbar = false;
            this.barManagerGornji.AllowQuickCustomization = false;
            this.barManagerGornji.AllowShowToolbarsPopup = false;
            this.barManagerGornji.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManagerGornji.DockControls.Add(this.barDockControlTop);
            this.barManagerGornji.DockControls.Add(this.barDockControlBottom);
            this.barManagerGornji.DockControls.Add(this.barDockControlLeft);
            this.barManagerGornji.DockControls.Add(this.barDockControlRight);
            this.barManagerGornji.DockWindowTabFont = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.barManagerGornji.Form = this;
            this.barManagerGornji.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnRacun,
            this.btnPS,
            this.btnJM,
            this.btnPosta,
            this.btnDrzave,
            this.btnSifarnikSubjekata,
            this.btnSifarnikIdenata,
            this.btnPredracuni,
            this.btnRacuni,
            this.btnPodesavanje,
            this.btnNaciniPlacanja,
            this.btnValute,
            this.btnOdgovorneOsobe,
            this.btnFirma,
            this.btnStatus,
            this.btnAutor,
            this.btnOdobrenje});
            this.barManagerGornji.MaxItemId = 26;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.btnSifarnikSubjekata, "Šifarnik subjekata", true, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnSifarnikIdenata, "", false, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnPredracuni, "", true, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnRacuni, "", true, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnOdobrenje, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnPodesavanje, "", false, false, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.AutoPopupMode = DevExpress.XtraBars.BarAutoPopupMode.None;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // btnSifarnikSubjekata
            // 
            this.btnSifarnikSubjekata.AllowRightClickInMenu = false;
            this.btnSifarnikSubjekata.Border = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            resources.ApplyResources(this.btnSifarnikSubjekata, "btnSifarnikSubjekata");
            this.btnSifarnikSubjekata.ContentHorizontalAlignment = DevExpress.XtraBars.BarItemContentAlignment.Center;
            this.btnSifarnikSubjekata.Id = 12;
            this.btnSifarnikSubjekata.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSifarnikSubjekata.ImageOptions.SvgImage")));
            this.btnSifarnikSubjekata.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("btnSifarnikSubjekata.ItemAppearance.Normal.Font")));
            this.btnSifarnikSubjekata.ItemAppearance.Normal.Options.UseFont = true;
            this.btnSifarnikSubjekata.ItemAppearance.Pressed.Font = ((System.Drawing.Font)(resources.GetObject("btnSifarnikSubjekata.ItemAppearance.Pressed.Font")));
            this.btnSifarnikSubjekata.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnSifarnikSubjekata.Name = "btnSifarnikSubjekata";
            this.btnSifarnikSubjekata.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSifarnikSubjekata_ItemClick);
            // 
            // btnSifarnikIdenata
            // 
            resources.ApplyResources(this.btnSifarnikIdenata, "btnSifarnikIdenata");
            this.btnSifarnikIdenata.ContentHorizontalAlignment = DevExpress.XtraBars.BarItemContentAlignment.Center;
            this.btnSifarnikIdenata.Id = 13;
            this.btnSifarnikIdenata.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSifarnikIdenata.ImageOptions.SvgImage")));
            this.btnSifarnikIdenata.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("btnSifarnikIdenata.ItemAppearance.Normal.Font")));
            this.btnSifarnikIdenata.ItemAppearance.Normal.Options.UseFont = true;
            this.btnSifarnikIdenata.ItemAppearance.Pressed.Font = ((System.Drawing.Font)(resources.GetObject("btnSifarnikIdenata.ItemAppearance.Pressed.Font")));
            this.btnSifarnikIdenata.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnSifarnikIdenata.Name = "btnSifarnikIdenata";
            this.btnSifarnikIdenata.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSifarnikIdenata_ItemClick);
            // 
            // btnPredracuni
            // 
            resources.ApplyResources(this.btnPredracuni, "btnPredracuni");
            this.btnPredracuni.ContentHorizontalAlignment = DevExpress.XtraBars.BarItemContentAlignment.Center;
            this.btnPredracuni.Id = 14;
            this.btnPredracuni.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPredracuni.ImageOptions.SvgImage")));
            this.btnPredracuni.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("btnPredracuni.ItemAppearance.Normal.Font")));
            this.btnPredracuni.ItemAppearance.Normal.Options.UseFont = true;
            this.btnPredracuni.ItemAppearance.Pressed.Font = ((System.Drawing.Font)(resources.GetObject("btnPredracuni.ItemAppearance.Pressed.Font")));
            this.btnPredracuni.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnPredracuni.Name = "btnPredracuni";
            this.btnPredracuni.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPredracuni_ItemClick);
            // 
            // btnRacuni
            // 
            resources.ApplyResources(this.btnRacuni, "btnRacuni");
            this.btnRacuni.ContentHorizontalAlignment = DevExpress.XtraBars.BarItemContentAlignment.Center;
            this.btnRacuni.Id = 15;
            this.btnRacuni.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRacuni.ImageOptions.SvgImage")));
            this.btnRacuni.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("btnRacuni.ItemAppearance.Normal.Font")));
            this.btnRacuni.ItemAppearance.Normal.Options.UseFont = true;
            this.btnRacuni.ItemAppearance.Pressed.Font = ((System.Drawing.Font)(resources.GetObject("btnRacuni.ItemAppearance.Pressed.Font")));
            this.btnRacuni.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnRacuni.Name = "btnRacuni";
            this.btnRacuni.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRacuni_ItemClick);
            // 
            // btnOdobrenje
            // 
            resources.ApplyResources(this.btnOdobrenje, "btnOdobrenje");
            this.btnOdobrenje.ContentHorizontalAlignment = DevExpress.XtraBars.BarItemContentAlignment.Center;
            this.btnOdobrenje.Id = 25;
            this.btnOdobrenje.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOdobrenje.ImageOptions.Image")));
            this.btnOdobrenje.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnOdobrenje.ImageOptions.LargeImage")));
            this.btnOdobrenje.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("btnOdobrenje.ItemAppearance.Normal.Font")));
            this.btnOdobrenje.ItemAppearance.Normal.Options.UseFont = true;
            this.btnOdobrenje.ItemAppearance.Pressed.Font = ((System.Drawing.Font)(resources.GetObject("btnOdobrenje.ItemAppearance.Pressed.Font")));
            this.btnOdobrenje.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnOdobrenje.Name = "btnOdobrenje";
            this.btnOdobrenje.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOdobrenje_ItemClick);
            // 
            // btnPodesavanje
            // 
            this.btnPodesavanje.ActAsDropDown = true;
            this.btnPodesavanje.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.CheckDropDown;
            resources.ApplyResources(this.btnPodesavanje, "btnPodesavanje");
            this.btnPodesavanje.ContentHorizontalAlignment = DevExpress.XtraBars.BarItemContentAlignment.Center;
            this.btnPodesavanje.DropDownControl = this.menuPodesavanje;
            this.btnPodesavanje.Id = 16;
            this.btnPodesavanje.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPodesavanje.ImageOptions.SvgImage")));
            this.btnPodesavanje.ItemAppearance.Disabled.Font = ((System.Drawing.Font)(resources.GetObject("btnPodesavanje.ItemAppearance.Disabled.Font")));
            this.btnPodesavanje.ItemAppearance.Disabled.Options.UseFont = true;
            this.btnPodesavanje.ItemAppearance.Normal.Font = ((System.Drawing.Font)(resources.GetObject("btnPodesavanje.ItemAppearance.Normal.Font")));
            this.btnPodesavanje.ItemAppearance.Normal.Options.UseFont = true;
            this.btnPodesavanje.ItemAppearance.Pressed.Font = ((System.Drawing.Font)(resources.GetObject("btnPodesavanje.ItemAppearance.Pressed.Font")));
            this.btnPodesavanje.ItemAppearance.Pressed.Options.UseFont = true;
            this.btnPodesavanje.Name = "btnPodesavanje";
            // 
            // menuPodesavanje
            // 
            this.menuPodesavanje.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnJM),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPS),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnPosta),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDrzave),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnNaciniPlacanja),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnValute),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnStatus),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnOdgovorneOsobe),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnFirma),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnAutor)});
            this.menuPodesavanje.Manager = this.barManagerGornji;
            this.menuPodesavanje.MenuDrawMode = DevExpress.XtraBars.MenuDrawMode.SmallImagesText;
            this.menuPodesavanje.Name = "menuPodesavanje";
            this.menuPodesavanje.OptionsMultiColumn.LargeImages = DevExpress.Utils.DefaultBoolean.True;
            this.menuPodesavanje.ShowCaption = true;
            // 
            // btnJM
            // 
            resources.ApplyResources(this.btnJM, "btnJM");
            this.btnJM.Id = 7;
            this.btnJM.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnJM.ImageOptions.SvgImage")));
            this.btnJM.Name = "btnJM";
            this.btnJM.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnJM_ItemClick);
            // 
            // btnPS
            // 
            resources.ApplyResources(this.btnPS, "btnPS");
            this.btnPS.Id = 6;
            this.btnPS.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPS.ImageOptions.SvgImage")));
            this.btnPS.Name = "btnPS";
            this.btnPS.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPS_ItemClick);
            // 
            // btnPosta
            // 
            resources.ApplyResources(this.btnPosta, "btnPosta");
            this.btnPosta.Id = 8;
            this.btnPosta.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnPosta.ImageOptions.SvgImage")));
            this.btnPosta.Name = "btnPosta";
            this.btnPosta.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnPosta_ItemClick);
            // 
            // btnDrzave
            // 
            resources.ApplyResources(this.btnDrzave, "btnDrzave");
            this.btnDrzave.Id = 9;
            this.btnDrzave.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnDrzave.ImageOptions.SvgImage")));
            this.btnDrzave.Name = "btnDrzave";
            this.btnDrzave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDrzave_ItemClick);
            // 
            // btnNaciniPlacanja
            // 
            resources.ApplyResources(this.btnNaciniPlacanja, "btnNaciniPlacanja");
            this.btnNaciniPlacanja.Id = 19;
            this.btnNaciniPlacanja.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnNaciniPlacanja.ImageOptions.SvgImage")));
            this.btnNaciniPlacanja.Name = "btnNaciniPlacanja";
            this.btnNaciniPlacanja.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNaciniPlacanja_ItemClick);
            // 
            // btnValute
            // 
            resources.ApplyResources(this.btnValute, "btnValute");
            this.btnValute.Id = 20;
            this.btnValute.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnValute.ImageOptions.SvgImage")));
            this.btnValute.Name = "btnValute";
            this.btnValute.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnValute_ItemClick);
            // 
            // btnStatus
            // 
            resources.ApplyResources(this.btnStatus, "btnStatus");
            this.btnStatus.Id = 23;
            this.btnStatus.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnStatus.ImageOptions.SvgImage")));
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnStatus_ItemClick);
            // 
            // btnOdgovorneOsobe
            // 
            resources.ApplyResources(this.btnOdgovorneOsobe, "btnOdgovorneOsobe");
            this.btnOdgovorneOsobe.Id = 21;
            this.btnOdgovorneOsobe.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnOdgovorneOsobe.ImageOptions.SvgImage")));
            this.btnOdgovorneOsobe.Name = "btnOdgovorneOsobe";
            this.btnOdgovorneOsobe.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOdgovorneOsobe_ItemClick);
            // 
            // btnFirma
            // 
            resources.ApplyResources(this.btnFirma, "btnFirma");
            this.btnFirma.Id = 22;
            this.btnFirma.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnFirma.ImageOptions.SvgImage")));
            this.btnFirma.Name = "btnFirma";
            this.btnFirma.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnFirma_ItemClick);
            // 
            // btnAutor
            // 
            resources.ApplyResources(this.btnAutor, "btnAutor");
            this.btnAutor.Id = 24;
            this.btnAutor.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAutor.ImageOptions.SvgImage")));
            this.btnAutor.Name = "btnAutor";
            this.btnAutor.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAutor_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            this.barDockControlTop.Manager = this.barManagerGornji;
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            this.barDockControlBottom.Manager = this.barManagerGornji;
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            this.barDockControlLeft.Manager = this.barManagerGornji;
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            this.barDockControlRight.Manager = this.barManagerGornji;
            // 
            // btnRacun
            // 
            resources.ApplyResources(this.btnRacun, "btnRacun");
            this.btnRacun.Id = 4;
            this.btnRacun.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnRacun.ImageOptions.SvgImage")));
            this.btnRacun.Name = "btnRacun";
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.AllowDragDrop = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabbedMdiManager1.AppearancePage.Header.Font = ((System.Drawing.Font)(resources.GetObject("xtraTabbedMdiManager1.AppearancePage.Header.Font")));
            this.xtraTabbedMdiManager1.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabbedMdiManager1.AppearancePage.HeaderActive.Font = ((System.Drawing.Font)(resources.GetObject("xtraTabbedMdiManager1.AppearancePage.HeaderActive.Font")));
            this.xtraTabbedMdiManager1.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabbedMdiManager1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.xtraTabbedMdiManager1.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.xtraTabbedMdiManager1.CloseTabOnMiddleClick = DevExpress.XtraTabbedMdi.CloseTabOnMiddleClick.OnMouseUp;
            this.xtraTabbedMdiManager1.FloatOnDoubleClick = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabbedMdiManager1.FloatOnDrag = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabbedMdiManager1.HeaderButtons = DevExpress.XtraTab.TabButtons.None;
            this.xtraTabbedMdiManager1.HeaderButtonsShowMode = DevExpress.XtraTab.TabButtonShowMode.Always;
            this.xtraTabbedMdiManager1.MdiParent = this;
            this.xtraTabbedMdiManager1.ShowFloatingDropHint = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabbedMdiManager1.ShowHeaderFocus = DevExpress.Utils.DefaultBoolean.True;
            this.xtraTabbedMdiManager1.UseFormIconAsPageImage = DevExpress.Utils.DefaultBoolean.False;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManagerGornji)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuPodesavanje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagerGornji;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarLargeButtonItem btnRacun;
        private DevExpress.XtraBars.PopupMenu menuPodesavanje;
        private DevExpress.XtraBars.BarLargeButtonItem btnPS;
        private DevExpress.XtraBars.BarLargeButtonItem btnJM;
        private DevExpress.XtraBars.BarLargeButtonItem btnPosta;
        private DevExpress.XtraBars.BarLargeButtonItem btnDrzave;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.BarButtonItem btnSifarnikSubjekata;
        private DevExpress.XtraBars.BarButtonItem btnSifarnikIdenata;
        private DevExpress.XtraBars.BarButtonItem btnPredracuni;
        private DevExpress.XtraBars.BarButtonItem btnRacuni;
        private DevExpress.XtraBars.BarButtonItem btnPodesavanje;
        private DevExpress.XtraBars.BarButtonItem btnNaciniPlacanja;
        private DevExpress.XtraBars.BarButtonItem btnValute;
        private DevExpress.XtraBars.BarButtonItem btnOdgovorneOsobe;
        private DevExpress.XtraBars.BarButtonItem btnFirma;
        private DevExpress.XtraBars.BarButtonItem btnStatus;
        private DevExpress.XtraBars.BarButtonItem btnAutor;
        private DevExpress.XtraBars.BarButtonItem btnOdobrenje;
    }
}