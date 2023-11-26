using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace KeeperCore.ERPNode.Reports.Accounting
{
  public class AccountGroupLedgers : DevExpress.XtraReports.UI.XtraReport
  {
    private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    private DevExpress.XtraReports.Parameters.Parameter AccountName;
    private TopMarginBand topMarginBand1;
    private BottomMarginBand bottomMarginBand1;
    private XRPageInfo xrPageInfo2;
    private ReportHeaderBand reportHeaderBand1;
    private GroupHeaderBand groupHeaderBand1;
    private GroupHeaderBand groupHeaderBand2;
    private DetailBand detailBand1;
    private XRTable xrTable3;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRTableCell xrTableCell8;
    private GroupFooterBand groupFooterBand1;
    private GroupFooterBand groupFooterBand2;
    private XRPanel xrPanel2;
    private XRLabel xrLabel4;
    private XRLabel xrLabel6;
    private XRControlStyle Title;
    private XRControlStyle GroupCaption3;
    private XRControlStyle GroupData3;
    private XRControlStyle DetailCaption3;
    private XRControlStyle DetailData3;
    private XRControlStyle DetailData3_Odd;
    private XRControlStyle DetailCaptionBackground3;
    private XRControlStyle TotalCaption3;
    private XRControlStyle TotalData3;
    private XRControlStyle TotalBackground3;
    private XRControlStyle GrandTotalCaption3;
    private XRControlStyle GrandTotalData3;
    private XRControlStyle GrandTotalBackground3;
    private XRControlStyle PageInfo;
    private XRTableCell xrTableCell10;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell12;
    private XRTableCell xrTableCell13;
    private XRTableCell xrTableCell11;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public AccountGroupLedgers()
    {
      InitializeComponent();
      //
      // TODO: Add constructor logic here
      //
    }

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

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
      DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
      this.AccountName = new DevExpress.XtraReports.Parameters.Parameter();
      this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
      this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
      this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
      this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
      this.reportHeaderBand1 = new DevExpress.XtraReports.UI.ReportHeaderBand();
      this.groupHeaderBand1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
      this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
      this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
      this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
      this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
      this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
      this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
      this.groupHeaderBand2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
      this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
      this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
      this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
      this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
      this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
      this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
      this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
      this.groupFooterBand1 = new DevExpress.XtraReports.UI.GroupFooterBand();
      this.groupFooterBand2 = new DevExpress.XtraReports.UI.GroupFooterBand();
      this.xrPanel2 = new DevExpress.XtraReports.UI.XRPanel();
      this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
      this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
      this.GroupCaption3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.GroupData3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.DetailCaption3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.DetailData3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.DetailData3_Odd = new DevExpress.XtraReports.UI.XRControlStyle();
      this.DetailCaptionBackground3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.TotalCaption3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.TotalData3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.TotalBackground3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.GrandTotalCaption3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.GrandTotalData3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.GrandTotalBackground3 = new DevExpress.XtraReports.UI.XRControlStyle();
      this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
      this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
      ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // AccountName
      // 
      this.AccountName.Description = "Account DisplayName";
      this.AccountName.Name = "AccountName";
      this.AccountName.Visible = false;
      // 
      // objectDataSource1
      // 
      this.objectDataSource1.DataSource = typeof(KeeperCore.ERPNode.Models.Accounting.Ledger);
      this.objectDataSource1.Name = "objectDataSource1";
      // 
      // topMarginBand1
      // 
      this.topMarginBand1.HeightF = 63.58334F;
      this.topMarginBand1.Name = "topMarginBand1";
      // 
      // bottomMarginBand1
      // 
      this.bottomMarginBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrPageInfo2});
      this.bottomMarginBand1.HeightF = 35.35004F;
      this.bottomMarginBand1.Name = "bottomMarginBand1";
      // 
      // xrPageInfo2
      // 
      this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(362.0001F, 0F);
      this.xrPageInfo2.Name = "xrPageInfo2";
      this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrPageInfo2.SizeF = new System.Drawing.SizeF(338F, 23F);
      this.xrPageInfo2.StyleName = "PageInfo";
      this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      this.xrPageInfo2.TextFormatString = "Page {0} of {1}";
      // 
      // reportHeaderBand1
      // 
      this.reportHeaderBand1.HeightF = 26.66667F;
      this.reportHeaderBand1.Name = "reportHeaderBand1";
      // 
      // groupHeaderBand1
      // 
      this.groupHeaderBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrTable1});
      this.groupHeaderBand1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
      new DevExpress.XtraReports.UI.GroupField("transactionNo", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
      this.groupHeaderBand1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
      this.groupHeaderBand1.HeightF = 25F;
      this.groupHeaderBand1.Level = 1;
      this.groupHeaderBand1.Name = "groupHeaderBand1";
      // 
      // xrTable1
      // 
      this.xrTable1.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
      this.xrTable1.Name = "xrTable1";
      this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
      this.xrTableRow1});
      this.xrTable1.SizeF = new System.Drawing.SizeF(700.0001F, 25F);
      this.xrTable1.StylePriority.UseFont = false;
      // 
      // xrTableRow1
      // 
      this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
      this.xrTableCell11,
      this.xrTableCell1,
      this.xrTableCell12,
      this.xrTableCell13});
      this.xrTableRow1.Name = "xrTableRow1";
      this.xrTableRow1.Weight = 1D;
      // 
      // xrTableCell11
      // 
      this.xrTableCell11.BackColor = System.Drawing.Color.White;
      this.xrTableCell11.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Name]")});
      this.xrTableCell11.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrTableCell11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.xrTableCell11.Name = "xrTableCell11";
      this.xrTableCell11.StylePriority.UseBackColor = false;
      this.xrTableCell11.StylePriority.UseFont = false;
      this.xrTableCell11.StylePriority.UseForeColor = false;
      this.xrTableCell11.StylePriority.UseTextAlignment = false;
      this.xrTableCell11.Text = "xrTableCell11";
      this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      this.xrTableCell11.TextFormatString = "{0}";
      this.xrTableCell11.Weight = 0.2671685331517536D;
      // 
      // xrTableCell1
      // 
      this.xrTableCell1.BackColor = System.Drawing.Color.White;
      this.xrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProfileName]")});
      this.xrTableCell1.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrTableCell1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.xrTableCell1.Name = "xrTableCell1";
      this.xrTableCell1.StylePriority.UseBackColor = false;
      this.xrTableCell1.StylePriority.UseFont = false;
      this.xrTableCell1.StylePriority.UseForeColor = false;
      this.xrTableCell1.StylePriority.UseTextAlignment = false;
      this.xrTableCell1.Text = "xrTableCell1";
      this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      this.xrTableCell1.Weight = 0.39529128254141221D;
      // 
      // xrTableCell12
      // 
      this.xrTableCell12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.xrTableCell12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.xrTableCell12.Name = "xrTableCell12";
      this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
      this.xrTableCell12.StylePriority.UseBackColor = false;
      this.xrTableCell12.StylePriority.UseFont = false;
      this.xrTableCell12.StylePriority.UseForeColor = false;
      this.xrTableCell12.StylePriority.UsePadding = false;
      this.xrTableCell12.StylePriority.UseTextAlignment = false;
      this.xrTableCell12.Text = "Debit";
      this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      this.xrTableCell12.Weight = 0.20139528676193008D;
      // 
      // xrTableCell13
      // 
      this.xrTableCell13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.xrTableCell13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
      this.xrTableCell13.Name = "xrTableCell13";
      this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
      this.xrTableCell13.StylePriority.UseBackColor = false;
      this.xrTableCell13.StylePriority.UseForeColor = false;
      this.xrTableCell13.StylePriority.UsePadding = false;
      this.xrTableCell13.StylePriority.UseTextAlignment = false;
      this.xrTableCell13.Text = "Credit";
      this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      this.xrTableCell13.Weight = 0.212986701552766D;
      // 
      // groupHeaderBand2
      // 
      this.groupHeaderBand2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
      this.groupHeaderBand2.HeightF = 4.250018F;
      this.groupHeaderBand2.Level = 2;
      this.groupHeaderBand2.Name = "groupHeaderBand2";
      // 
      // detailBand1
      // 
      this.detailBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrTable3});
      this.detailBand1.HeightF = 25F;
      this.detailBand1.KeepTogether = true;
      this.detailBand1.Name = "detailBand1";
      this.detailBand1.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
      new DevExpress.XtraReports.UI.GroupField("Credit", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending),
      new DevExpress.XtraReports.UI.GroupField("Debit", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
      // 
      // xrTable3
      // 
      this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
      this.xrTable3.Name = "xrTable3";
      this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
      this.xrTableRow3});
      this.xrTable3.SizeF = new System.Drawing.SizeF(700F, 25F);
      // 
      // xrTableRow3
      // 
      this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
      this.xrTableCell6,
      this.xrTableCell2,
      this.xrTableCell10,
      this.xrTableCell7,
      this.xrTableCell8});
      this.xrTableRow3.Name = "xrTableRow3";
      this.xrTableRow3.Weight = 11.5D;
      // 
      // xrTableCell6
      // 
      this.xrTableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TrnDate]")});
      this.xrTableCell6.Name = "xrTableCell6";
      this.xrTableCell6.StyleName = "DetailData3";
      this.xrTableCell6.Text = "xrTableCell6";
      this.xrTableCell6.TextFormatString = "{0:dd-MMM-yyyy}";
      this.xrTableCell6.Weight = 0.19392813335115045D;
      // 
      // xrTableCell10
      // 
      this.xrTableCell10.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ChartOfAccountName]")});
      this.xrTableCell10.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F);
      this.xrTableCell10.Name = "xrTableCell10";
      this.xrTableCell10.StylePriority.UseFont = false;
      this.xrTableCell10.StylePriority.UseTextAlignment = false;
      this.xrTableCell10.Text = "xrTableCell10";
      this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      this.xrTableCell10.Weight = 0.32696459980251447D;
      // 
      // xrTableCell7
      // 
      this.xrTableCell7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Debit]")});
      this.xrTableCell7.Name = "xrTableCell7";
      this.xrTableCell7.StyleName = "DetailData3";
      this.xrTableCell7.Text = "xrTableCell7";
      this.xrTableCell7.TextFormatString = "{0:n2}";
      this.xrTableCell7.Weight = 0.187024231005188D;
      // 
      // xrTableCell8
      // 
      this.xrTableCell8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Credit]")});
      this.xrTableCell8.Name = "xrTableCell8";
      this.xrTableCell8.StyleName = "DetailData3";
      this.xrTableCell8.Text = "xrTableCell8";
      this.xrTableCell8.TextFormatString = "{0:n2}";
      this.xrTableCell8.Weight = 0.197788173185617D;
      // 
      // groupFooterBand1
      // 
      this.groupFooterBand1.GroupUnion = DevExpress.XtraReports.UI.GroupFooterUnion.WithLastDetail;
      this.groupFooterBand1.HeightF = 6F;
      this.groupFooterBand1.Name = "groupFooterBand1";
      // 
      // groupFooterBand2
      // 
      this.groupFooterBand2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrPanel2});
      this.groupFooterBand2.HeightF = 26.10833F;
      this.groupFooterBand2.Level = 1;
      this.groupFooterBand2.Name = "groupFooterBand2";
      // 
      // xrPanel2
      // 
      this.xrPanel2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrLabel4,
      this.xrLabel6});
      this.xrPanel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
      this.xrPanel2.Name = "xrPanel2";
      this.xrPanel2.SizeF = new System.Drawing.SizeF(700F, 26.10833F);
      this.xrPanel2.StyleName = "TotalBackground3";
      // 
      // xrLabel4
      // 
      this.xrLabel4.CanGrow = false;
      this.xrLabel4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([Debit])")});
      this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(430.6314F, 0.1083374F);
      this.xrLabel4.Name = "xrLabel4";
      this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
      this.xrLabel4.SizeF = new System.Drawing.SizeF(130.9168F, 16F);
      this.xrLabel4.StyleName = "TotalData3";
      this.xrLabel4.StylePriority.UsePadding = false;
      xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
      this.xrLabel4.Summary = xrSummary1;
      this.xrLabel4.Text = "xrLabel4";
      this.xrLabel4.TextFormatString = "{0:n2}";
      this.xrLabel4.WordWrap = false;
      // 
      // xrLabel6
      // 
      this.xrLabel6.CanGrow = false;
      this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([Credit])")});
      this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(561.5483F, 0.1083374F);
      this.xrLabel6.Name = "xrLabel6";
      this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
      this.xrLabel6.SizeF = new System.Drawing.SizeF(138.4518F, 16F);
      this.xrLabel6.StyleName = "TotalData3";
      this.xrLabel6.StylePriority.UsePadding = false;
      xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
      this.xrLabel6.Summary = xrSummary2;
      this.xrLabel6.Text = "xrLabel6";
      this.xrLabel6.TextFormatString = "{0:n2}";
      this.xrLabel6.WordWrap = false;
      // 
      // Title
      // 
      this.Title.BackColor = System.Drawing.Color.Transparent;
      this.Title.BorderColor = System.Drawing.Color.Black;
      this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.Title.BorderWidth = 1F;
      this.Title.Font = new DevExpress.Drawing.DXFont("Tahoma", 14F);
      this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
      this.Title.Name = "Title";
      // 
      // GroupCaption3
      // 
      this.GroupCaption3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
      this.GroupCaption3.BorderColor = System.Drawing.Color.White;
      this.GroupCaption3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
      this.GroupCaption3.BorderWidth = 2F;
      this.GroupCaption3.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.GroupCaption3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
      this.GroupCaption3.Name = "GroupCaption3";
      this.GroupCaption3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
      this.GroupCaption3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // GroupData3
      // 
      this.GroupData3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
      this.GroupData3.BorderColor = System.Drawing.Color.White;
      this.GroupData3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
      this.GroupData3.BorderWidth = 2F;
      this.GroupData3.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.GroupData3.ForeColor = System.Drawing.Color.White;
      this.GroupData3.Name = "GroupData3";
      this.GroupData3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
      this.GroupData3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // DetailCaption3
      // 
      this.DetailCaption3.BackColor = System.Drawing.Color.Transparent;
      this.DetailCaption3.BorderColor = System.Drawing.Color.Transparent;
      this.DetailCaption3.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.DetailCaption3.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.DetailCaption3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
      this.DetailCaption3.Name = "DetailCaption3";
      this.DetailCaption3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
      this.DetailCaption3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // DetailData3
      // 
      this.DetailData3.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F);
      this.DetailData3.ForeColor = System.Drawing.Color.Black;
      this.DetailData3.Name = "DetailData3";
      this.DetailData3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
      this.DetailData3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // DetailData3_Odd
      // 
      this.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
      this.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent;
      this.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.DetailData3_Odd.BorderWidth = 1F;
      this.DetailData3_Odd.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F);
      this.DetailData3_Odd.ForeColor = System.Drawing.Color.Black;
      this.DetailData3_Odd.Name = "DetailData3_Odd";
      this.DetailData3_Odd.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
      this.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // DetailCaptionBackground3
      // 
      this.DetailCaptionBackground3.BackColor = System.Drawing.Color.Transparent;
      this.DetailCaptionBackground3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(206)))), ((int)(((byte)(206)))));
      this.DetailCaptionBackground3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
      this.DetailCaptionBackground3.BorderWidth = 2F;
      this.DetailCaptionBackground3.Name = "DetailCaptionBackground3";
      // 
      // TotalCaption3
      // 
      this.TotalCaption3.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.TotalCaption3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(147)))), ((int)(((byte)(147)))));
      this.TotalCaption3.Name = "TotalCaption3";
      this.TotalCaption3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
      this.TotalCaption3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // TotalData3
      // 
      this.TotalData3.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.TotalData3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
      this.TotalData3.Name = "TotalData3";
      this.TotalData3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 6, 0, 0, 100F);
      this.TotalData3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // TotalBackground3
      // 
      this.TotalBackground3.Name = "TotalBackground3";
      // 
      // GrandTotalCaption3
      // 
      this.GrandTotalCaption3.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.GrandTotalCaption3.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.GrandTotalCaption3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
      this.GrandTotalCaption3.Name = "GrandTotalCaption3";
      this.GrandTotalCaption3.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
      this.GrandTotalCaption3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // GrandTotalData3
      // 
      this.GrandTotalData3.Borders = DevExpress.XtraPrinting.BorderSide.None;
      this.GrandTotalData3.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.GrandTotalData3.ForeColor = System.Drawing.Color.White;
      this.GrandTotalData3.Name = "GrandTotalData3";
      this.GrandTotalData3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 6, 0, 0, 100F);
      this.GrandTotalData3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      // 
      // GrandTotalBackground3
      // 
      this.GrandTotalBackground3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(111)))), ((int)(((byte)(111)))));
      this.GrandTotalBackground3.BorderColor = System.Drawing.Color.White;
      this.GrandTotalBackground3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
      this.GrandTotalBackground3.BorderWidth = 2F;
      this.GrandTotalBackground3.Name = "GrandTotalBackground3";
      // 
      // PageInfo
      // 
      this.PageInfo.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F, DevExpress.Drawing.DXFontStyle.Bold);
      this.PageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
      this.PageInfo.Name = "PageInfo";
      this.PageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      // 
      // xrTableCell2
      // 
      this.xrTableCell2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[accountItem].[Code]")});
      this.xrTableCell2.Font = new DevExpress.Drawing.DXFont("Tahoma", 9F);
      this.xrTableCell2.Name = "xrTableCell2";
      this.xrTableCell2.StylePriority.UseFont = false;
      this.xrTableCell2.StylePriority.UseTextAlignment = false;
      this.xrTableCell2.Text = "xrTableCell2";
      this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
      this.xrTableCell2.Weight = 0.0942948492093148D;
      // 
      // AccountGroupLedgers
      // 
      this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
      this.topMarginBand1,
      this.bottomMarginBand1,
      this.reportHeaderBand1,
      this.groupHeaderBand1,
      this.groupHeaderBand2,
      this.detailBand1,
      this.groupFooterBand1,
      this.groupFooterBand2});
      this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
      this.objectDataSource1});
      this.DataSource = this.objectDataSource1;
      this.Margins = new DevExpress.Drawing.DXMargins(100, 50, 64, 35);
      this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
      this.AccountName});
      this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
      this.Title,
      this.GroupCaption3,
      this.GroupData3,
      this.DetailCaption3,
      this.DetailData3,
      this.DetailData3_Odd,
      this.DetailCaptionBackground3,
      this.TotalCaption3,
      this.TotalData3,
      this.TotalBackground3,
      this.GrandTotalCaption3,
      this.GrandTotalData3,
      this.GrandTotalBackground3,
      this.PageInfo});
      this.Version = "17.2";
      ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
  }
}