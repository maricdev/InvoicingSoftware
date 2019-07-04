namespace Montesino_fakture.View
{
    partial class Artikli
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Artikli));
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.btnNapravi = new DevExpress.XtraBars.BarButtonItem();
            this.btnIzmeni = new DevExpress.XtraBars.BarButtonItem();
            this.btnIzbrisi = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnNapraviStari = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnIzmeniStari = new DevExpress.XtraBars.BarLargeButtonItem();
            this.btnIzbrisiStari = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barMdiChildrenListItem1 = new DevExpress.XtraBars.BarMdiChildrenListItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.bar5 = new DevExpress.XtraBars.Bar();
            this.Meni = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuIzmeni = new System.Windows.Forms.ToolStripMenuItem();
            this.menuIzbrisi = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.Meni.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl
            // 
            this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl.Location = new System.Drawing.Point(0, -1);
            this.gridControl.LookAndFeel.SkinName = "Office 2013 Light Gray";
            this.gridControl.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(1156, 598);
            this.gridControl.TabIndex = 22;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsBehavior.ReadOnly = true;
            this.gridView.OptionsCustomization.AllowColumnMoving = false;
            this.gridView.OptionsCustomization.AllowFilter = false;
            this.gridView.OptionsCustomization.AllowGroup = false;
            this.gridView.OptionsCustomization.AllowMergedGrouping = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gridView.OptionsFilter.AllowFilterEditor = false;
            this.gridView.OptionsFind.AlwaysVisible = true;
            this.gridView.OptionsFind.Behavior = DevExpress.XtraEditors.FindPanelBehavior.Filter;
            this.gridView.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.FindClick;
            this.gridView.OptionsFind.FindNullPrompt = "Pretraga idenata...";
            this.gridView.OptionsFind.HighlightFindResults = false;
            this.gridView.OptionsFind.ShowCloseButton = false;
            this.gridView.OptionsFind.ShowSearchNavButtons = false;
            this.gridView.OptionsLayout.Columns.AddNewColumns = false;
            this.gridView.OptionsLayout.Columns.RemoveOldColumns = false;
            this.gridView.OptionsMenu.EnableColumnMenu = false;
            this.gridView.OptionsMenu.EnableFooterMenu = false;
            this.gridView.OptionsMenu.EnableGroupPanelMenu = false;
            this.gridView.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.False;
            this.gridView.OptionsMenu.ShowAutoFilterRowItem = false;
            this.gridView.OptionsNavigation.AutoFocusNewRow = true;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.gridView_PopupMenuShowing);
            this.gridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView_KeyDown);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar1.FloatLocation = new System.Drawing.Point(78, 794);
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.AutoPopupMode = DevExpress.XtraBars.BarAutoPopupMode.None;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar2.FloatLocation = new System.Drawing.Point(78, 794);
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.AutoPopupMode = DevExpress.XtraBars.BarAutoPopupMode.None;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Tools";
            // 
            // barManager1
            // 
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowMdiChildButtons = false;
            this.barManager1.AllowMoveBarOnToolbar = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.CloseButtonAffectAllTabs = false;
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnNapraviStari,
            this.btnIzmeniStari,
            this.btnIzbrisiStari,
            this.barMdiChildrenListItem1,
            this.barButtonItem1,
            this.btnNapravi,
            this.btnIzmeni,
            this.btnIzbrisi});
            this.barManager1.MaxItemId = 8;
            this.barManager1.PopupMenuAlignment = DevExpress.XtraBars.PopupMenuAlignment.Left;
            // 
            // bar3
            // 
            this.bar3.BarAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bar3.BarAppearance.Normal.Options.UseFont = true;
            this.bar3.BarAppearance.Pressed.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bar3.BarAppearance.Pressed.Options.UseFont = true;
            this.bar3.BarName = "Tools";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.FloatLocation = new System.Drawing.Point(78, 794);
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.Caption | DevExpress.XtraBars.BarLinkUserDefines.PaintStyle))), this.btnNapravi, "Napravi ident", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnIzmeni, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btnIzbrisi, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.AutoPopupMode = DevExpress.XtraBars.BarAutoPopupMode.None;
            this.bar3.OptionsBar.DisableClose = true;
            this.bar3.OptionsBar.DisableCustomization = true;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Tools";
            // 
            // btnNapravi
            // 
            this.btnNapravi.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.btnNapravi.Caption = "Napravi ident";
            this.btnNapravi.Id = 5;
            this.btnNapravi.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnNapravi.ImageOptions.SvgImage")));
            this.btnNapravi.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnNapravi.ItemAppearance.Normal.Options.UseFont = true;
            this.btnNapravi.Name = "btnNapravi";
            this.btnNapravi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNapravi_ItemClick);
            // 
            // btnIzmeni
            // 
            this.btnIzmeni.Caption = "Izmeni ident";
            this.btnIzmeni.Id = 6;
            this.btnIzmeni.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnIzmeni.ImageOptions.SvgImage")));
            this.btnIzmeni.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzmeni.ItemAppearance.Normal.Options.UseFont = true;
            this.btnIzmeni.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzmeni.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.btnIzmeni.Name = "btnIzmeni";
            this.btnIzmeni.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnIzmeni_ItemClick);
            // 
            // btnIzbrisi
            // 
            this.btnIzbrisi.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.btnIzbrisi.Caption = "Izbriši ident";
            this.btnIzbrisi.Id = 7;
            this.btnIzbrisi.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnIzbrisi.ImageOptions.SvgImage")));
            this.btnIzbrisi.ItemAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzbrisi.ItemAppearance.Normal.Options.UseFont = true;
            this.btnIzbrisi.ItemInMenuAppearance.Normal.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnIzbrisi.ItemInMenuAppearance.Normal.Options.UseFont = true;
            this.btnIzbrisi.Name = "btnIzbrisi";
            this.btnIzbrisi.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnIzbrisi_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1155, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 603);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1155, 39);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 603);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1155, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 603);
            // 
            // btnNapraviStari
            // 
            this.btnNapraviStari.Caption = "Napravi ident";
            this.btnNapraviStari.Id = 0;
            this.btnNapraviStari.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnNapraviStari.ImageOptions.SvgImage")));
            this.btnNapraviStari.Name = "btnNapraviStari";
            this.btnNapraviStari.Size = new System.Drawing.Size(100, 0);
            this.btnNapraviStari.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNapravi_ItemClick);
            // 
            // btnIzmeniStari
            // 
            this.btnIzmeniStari.Caption = "Izmeni ident";
            this.btnIzmeniStari.Id = 1;
            this.btnIzmeniStari.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnIzmeniStari.ImageOptions.SvgImage")));
            this.btnIzmeniStari.Name = "btnIzmeniStari";
            this.btnIzmeniStari.Size = new System.Drawing.Size(100, 0);
            this.btnIzmeniStari.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnIzmeni_ItemClick);
            // 
            // btnIzbrisiStari
            // 
            this.btnIzbrisiStari.Caption = "Izbriši ident";
            this.btnIzbrisiStari.Id = 2;
            this.btnIzbrisiStari.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnIzbrisiStari.ImageOptions.SvgImage")));
            this.btnIzbrisiStari.Name = "btnIzbrisiStari";
            this.btnIzbrisiStari.Size = new System.Drawing.Size(100, 0);
            this.btnIzbrisiStari.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnIzbrisi_ItemClick);
            // 
            // barMdiChildrenListItem1
            // 
            this.barMdiChildrenListItem1.Caption = "barMdiChildrenListItem1";
            this.barMdiChildrenListItem1.Id = 3;
            this.barMdiChildrenListItem1.Name = "barMdiChildrenListItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 4;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // bar4
            // 
            this.bar4.BarName = "Custom 3";
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar4.OptionsBar.MultiLine = true;
            this.bar4.OptionsBar.UseWholeRow = true;
            this.bar4.Text = "Custom 3";
            // 
            // bar5
            // 
            this.bar5.BarName = "Custom 4";
            this.bar5.DockCol = 0;
            this.bar5.DockRow = 1;
            this.bar5.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar5.Text = "Custom 4";
            // 
            // Meni
            // 
            this.Meni.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Meni.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuIzmeni,
            this.menuIzbrisi});
            this.Meni.Name = "contextMenuStrip1";
            this.Meni.Size = new System.Drawing.Size(211, 80);
            // 
            // menuIzmeni
            // 
            this.menuIzmeni.Name = "menuIzmeni";
            this.menuIzmeni.Size = new System.Drawing.Size(210, 24);
            this.menuIzmeni.Text = "Izmeni ident";
            this.menuIzmeni.Click += new System.EventHandler(this.menuIzmeni_Click_1);
            // 
            // menuIzbrisi
            // 
            this.menuIzbrisi.Name = "menuIzbrisi";
            this.menuIzbrisi.Size = new System.Drawing.Size(210, 24);
            this.menuIzbrisi.Text = "Izbriši ident";
            this.menuIzbrisi.Click += new System.EventHandler(this.menuIzbrisi_Click_1);
            // 
            // Artikli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 642);
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "Artikli";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Šifarnik idenata";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.Meni.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarLargeButtonItem btnNapraviStari;
        private DevExpress.XtraBars.BarLargeButtonItem btnIzmeniStari;
        private DevExpress.XtraBars.BarLargeButtonItem btnIzbrisiStari;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem btnNapravi;
        private DevExpress.XtraBars.BarMdiChildrenListItem barMdiChildrenListItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem btnIzmeni;
        private DevExpress.XtraBars.BarButtonItem btnIzbrisi;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.Bar bar5;
        private System.Windows.Forms.ContextMenuStrip Meni;
        private System.Windows.Forms.ToolStripMenuItem menuIzmeni;
        private System.Windows.Forms.ToolStripMenuItem menuIzbrisi;
    }
}