﻿using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;


namespace ERPKeeper.Node.Reports.Accounting
{
  /// <summary>
  /// Summary description for ProfitandLoss
  /// </summary>
  public class IncomeStatement : DevExpress.XtraReports.UI.XtraReport
  {
    private DetailBand Detail;
    public DevExpress.DataAccess.ObjectBinding.ObjectDataSource IncomeAccount;
    public DevExpress.DataAccess.ObjectBinding.ObjectDataSource ExpenseAccount;
    private TopMarginBand TopMargin;
    private PageHeaderBand PageHeader;
    private BottomMarginBand BottomMargin;
    private DevExpress.DataAccess.ObjectBinding.ObjectDataSource IncomeStatementDataSource;
    private DetailReportBand IncomeDetailReport;
    private DetailBand Detail1;
    private DetailReportBand ExpenseDetailReport;
    private DetailBand Detail2;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell1;
    private XRTableCell xrTableCell2;
    private GroupHeaderBand GroupHeader1;
    private XRLabel xrLabel1;
    private GroupFooterBand GroupFooter1;
    private XRLabel xrLabel4;
    private XRTable xrTable2;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell3;
    private XRTableCell xrTableCell4;
    private GroupHeaderBand GroupHeader2;
    private XRLabel xrLabel2;
    private GroupFooterBand GroupFooter2;
    private XRLabel xrLabel3;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRLabel xrLabel7;
    private XRLabel xrLabel6;
    private XRLabel xrLabel5;
    private GroupFooterBand Profit;
    private XRLabel xrLabel8;
    private XRLabel xrLabel9;
    private PageFooterBand PageFooter;
    private XRPageInfo xrPageInfo1;
    private XRLabel xrLabel10;
    private DevExpress.XtraReports.Parameters.Parameter ProfileFullName;


    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    public IncomeStatement()
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
      this.Detail = new DevExpress.XtraReports.UI.DetailBand();
      this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
      this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
      this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
      this.ProfileFullName = new DevExpress.XtraReports.Parameters.Parameter();
      this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
      this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
      this.IncomeDetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
      this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
      this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
      this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
      this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
      this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
      this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
      this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
      this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
      this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
      this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
      this.IncomeStatementDataSource = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
      this.ExpenseDetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
      this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
      this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
      this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
      this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
      this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
      this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
      this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
      this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
      this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
      this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
      this.Profit = new DevExpress.XtraReports.UI.GroupFooterBand();
      this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
      this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
      this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
      this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
      ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.IncomeStatementDataSource)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // Detail
      // 
      this.Detail.Expanded = false;
      this.Detail.Font = new DevExpress.Drawing.DXFont("Tahoma", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
      this.Detail.HeightF = 25F;
      this.Detail.KeepTogether = true;
      this.Detail.KeepTogetherWithDetailReports = true;
      this.Detail.Name = "Detail";
      this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
      this.Detail.StylePriority.UseFont = false;
      this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // TopMargin
      // 
      this.TopMargin.HeightF = 23.91666F;
      this.TopMargin.Name = "TopMargin";
      this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
      this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // PageHeader
      // 
      this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrLabel10,
      this.xrLabel7,
      this.xrLabel6,
      this.xrLabel5});
      this.PageHeader.HeightF = 74.67502F;
      this.PageHeader.Name = "PageHeader";
      // 
      // xrLabel10
      // 
      this.xrLabel10.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Parameters.ProfileFullName]")});
      this.xrLabel10.Font = new DevExpress.Drawing.DXFont("Tahoma", 8.25F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(452.0833F, 10.00001F);
      this.xrLabel10.Name = "xrLabel10";
      this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel10.SizeF = new System.Drawing.SizeF(297.9167F, 23F);
      this.xrLabel10.StylePriority.UseFont = false;
      // 
      // ProfileFullName
      // 
      this.ProfileFullName.Description = "Profile Ful lName";
      this.ProfileFullName.Name = "ProfileFullName";
      this.ProfileFullName.Visible = false;
      // 
      // xrLabel7
      // 
      this.xrLabel7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EndDate]")});
      this.xrLabel7.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F);
      this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(122.125F, 33.00001F);
      this.xrLabel7.Name = "xrLabel7";
      this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel7.SizeF = new System.Drawing.SizeF(117.7083F, 23F);
      this.xrLabel7.StylePriority.UseFont = false;
      this.xrLabel7.StylePriority.UseTextAlignment = false;
      this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      this.xrLabel7.TextFormatString = "To {0:dd-MMM-yy}";
      // 
      // xrLabel6
      // 
      this.xrLabel6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[StartDate]")});
      this.xrLabel6.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F);
      this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0.1249949F, 33.00001F);
      this.xrLabel6.Name = "xrLabel6";
      this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel6.SizeF = new System.Drawing.SizeF(121.875F, 23F);
      this.xrLabel6.StylePriority.UseFont = false;
      this.xrLabel6.StylePriority.UseTextAlignment = false;
      this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      this.xrLabel6.TextFormatString = "From {0:dd-MMM-yy}";
      // 
      // xrLabel5
      // 
      this.xrLabel5.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0.1249949F, 10.00001F);
      this.xrLabel5.Name = "xrLabel5";
      this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel5.SizeF = new System.Drawing.SizeF(145.8333F, 23F);
      this.xrLabel5.StylePriority.UseFont = false;
      this.xrLabel5.StylePriority.UseTextAlignment = false;
      this.xrLabel5.Text = "Income Statement";
      this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      // 
      // BottomMargin
      // 
      this.BottomMargin.HeightF = 13.12497F;
      this.BottomMargin.Name = "BottomMargin";
      this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
      this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
      // 
      // IncomeDetailReport
      // 
      this.IncomeDetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
      this.Detail1,
      this.GroupHeader1,
      this.GroupFooter1});
      this.IncomeDetailReport.DataMember = "Incomes";
      this.IncomeDetailReport.DataSource = this.IncomeStatementDataSource;
      this.IncomeDetailReport.Level = 0;
      this.IncomeDetailReport.Name = "IncomeDetailReport";
      // 
      // Detail1
      // 
      this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrTable1});
      this.Detail1.HeightF = 25F;
      this.Detail1.Name = "Detail1";
      // 
      // xrTable1
      // 
      this.xrTable1.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F);
      this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(48.95833F, 0F);
      this.xrTable1.Name = "xrTable1";
      this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
      this.xrTableRow1});
      this.xrTable1.SizeF = new System.Drawing.SizeF(500F, 25F);
      this.xrTable1.StylePriority.UseFont = false;
      // 
      // xrTableRow1
      // 
      this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
      this.xrTableCell5,
      this.xrTableCell1,
      this.xrTableCell2});
      this.xrTableRow1.Name = "xrTableRow1";
      this.xrTableRow1.Weight = 11.5D;
      // 
      // xrTableCell5
      // 
      this.xrTableCell5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[No]")});
      this.xrTableCell5.Name = "xrTableCell5";
      this.xrTableCell5.StylePriority.UseTextAlignment = false;
      this.xrTableCell5.Text = "xrTableCell5";
      this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      this.xrTableCell5.Weight = 0.0847902097902098D;
      // 
      // xrTableCell1
      // 
      this.xrTableCell1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Name]")});
      this.xrTableCell1.Name = "xrTableCell1";
      this.xrTableCell1.StylePriority.UseTextAlignment = false;
      this.xrTableCell1.Text = "xrTableCell1";
      this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      this.xrTableCell1.Weight = 0.18417286220630566D;
      // 
      // xrTableCell2
      // 
      this.xrTableCell2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CurrentBalance]")});
      this.xrTableCell2.Name = "xrTableCell2";
      this.xrTableCell2.Text = "xrTableCell2";
      this.xrTableCell2.TextFormatString = "{0:n2}";
      this.xrTableCell2.Weight = 0.23540756164230667D;
      // 
      // GroupHeader1
      // 
      this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrLabel1});
      this.GroupHeader1.HeightF = 25.08333F;
      this.GroupHeader1.Name = "GroupHeader1";
      // 
      // xrLabel1
      // 
      this.xrLabel1.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.1249949F, 0F);
      this.xrLabel1.Name = "xrLabel1";
      this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel1.SizeF = new System.Drawing.SizeF(100F, 25.08333F);
      this.xrLabel1.StylePriority.UseFont = false;
      this.xrLabel1.StylePriority.UseTextAlignment = false;
      this.xrLabel1.Text = "Incomes";
      this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      // 
      // GroupFooter1
      // 
      this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrLabel4});
      this.GroupFooter1.HeightF = 40.625F;
      this.GroupFooter1.Name = "GroupFooter1";
      // 
      // xrLabel4
      // 
      this.xrLabel4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([CurrentBalance])")});
      this.xrLabel4.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(315.5907F, 0F);
      this.xrLabel4.Name = "xrLabel4";
      this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel4.SizeF = new System.Drawing.SizeF(233.4926F, 23F);
      this.xrLabel4.StylePriority.UseFont = false;
      xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
      this.xrLabel4.Summary = xrSummary1;
      this.xrLabel4.TextFormatString = "{0:n2}";
      // 
      // IncomeStatementDataSource
      // 
      this.IncomeStatementDataSource.DataSource = typeof(ERPKeeper.Node.Models.Accounting.Reports.IncomeStatement);
      this.IncomeStatementDataSource.Name = "IncomeStatementDataSource";
      // 
      // ExpenseDetailReport
      // 
      this.ExpenseDetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
      this.Detail2,
      this.GroupHeader2,
      this.GroupFooter2});
      this.ExpenseDetailReport.DataMember = "Expenses";
      this.ExpenseDetailReport.DataSource = this.IncomeStatementDataSource;
      this.ExpenseDetailReport.Level = 1;
      this.ExpenseDetailReport.Name = "ExpenseDetailReport";
      // 
      // Detail2
      // 
      this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrTable2});
      this.Detail2.HeightF = 26.04167F;
      this.Detail2.Name = "Detail2";
      // 
      // xrTable2
      // 
      this.xrTable2.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F);
      this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(48.83333F, 0F);
      this.xrTable2.Name = "xrTable2";
      this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
      this.xrTableRow2});
      this.xrTable2.SizeF = new System.Drawing.SizeF(500F, 25F);
      this.xrTable2.StylePriority.UseFont = false;
      // 
      // xrTableRow2
      // 
      this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
      this.xrTableCell6,
      this.xrTableCell3,
      this.xrTableCell4});
      this.xrTableRow2.Name = "xrTableRow2";
      this.xrTableRow2.Weight = 11.5D;
      // 
      // xrTableCell6
      // 
      this.xrTableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[No]")});
      this.xrTableCell6.Name = "xrTableCell6";
      this.xrTableCell6.StylePriority.UseTextAlignment = false;
      this.xrTableCell6.Text = "xrTableCell6";
      this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      this.xrTableCell6.Weight = 0.077272171956259073D;
      // 
      // xrTableCell3
      // 
      this.xrTableCell3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Name]")});
      this.xrTableCell3.Name = "xrTableCell3";
      this.xrTableCell3.StylePriority.UseTextAlignment = false;
      this.xrTableCell3.Text = "xrTableCell3";
      this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      this.xrTableCell3.Weight = 0.16789377933362459D;
      // 
      // xrTableCell4
      // 
      this.xrTableCell4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CurrentBalance]")});
      this.xrTableCell4.Name = "xrTableCell4";
      this.xrTableCell4.Text = "xrTableCell4";
      this.xrTableCell4.TextFormatString = "{0:n2}";
      this.xrTableCell4.Weight = 0.21457938653567493D;
      // 
      // GroupHeader2
      // 
      this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrLabel2});
      this.GroupHeader2.HeightF = 25.08333F;
      this.GroupHeader2.Name = "GroupHeader2";
      // 
      // xrLabel2
      // 
      this.xrLabel2.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0.1249949F, 0F);
      this.xrLabel2.Name = "xrLabel2";
      this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 25.08333F);
      this.xrLabel2.StylePriority.UseFont = false;
      this.xrLabel2.StylePriority.UseTextAlignment = false;
      this.xrLabel2.Text = "Expenses";
      this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      // 
      // GroupFooter2
      // 
      this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrLabel3});
      this.GroupFooter2.HeightF = 38.54167F;
      this.GroupFooter2.Name = "GroupFooter2";
      // 
      // xrLabel3
      // 
      this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([CurrentBalance])")});
      this.xrLabel3.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(315.5907F, 0F);
      this.xrLabel3.Name = "xrLabel3";
      this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel3.SizeF = new System.Drawing.SizeF(233.6176F, 23F);
      this.xrLabel3.StylePriority.UseFont = false;
      xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
      this.xrLabel3.Summary = xrSummary2;
      this.xrLabel3.TextFormatString = "{0:n2}";
      // 
      // Profit
      // 
      this.Profit.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrLabel9,
      this.xrLabel8});
      this.Profit.HeightF = 100F;
      this.Profit.Name = "Profit";
      // 
      // xrLabel9
      // 
      this.xrLabel9.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
      this.xrLabel9.Name = "xrLabel9";
      this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel9.SizeF = new System.Drawing.SizeF(100F, 23F);
      this.xrLabel9.StylePriority.UseFont = false;
      this.xrLabel9.StylePriority.UseTextAlignment = false;
      this.xrLabel9.Text = "Profit";
      this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      // 
      // xrLabel8
      // 
      this.xrLabel8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
      new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[Profit]")});
      this.xrLabel8.Font = new DevExpress.Drawing.DXFont("Tahoma", 10F, DevExpress.Drawing.DXFontStyle.Bold);
      this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(315.4657F, 0F);
      this.xrLabel8.Name = "xrLabel8";
      this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrLabel8.SizeF = new System.Drawing.SizeF(233.4925F, 23F);
      this.xrLabel8.StylePriority.UseFont = false;
      this.xrLabel8.Text = "xrLabel8";
      this.xrLabel8.TextFormatString = "{0:n2}";
      // 
      // PageFooter
      // 
      this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
      this.xrPageInfo1});
      this.PageFooter.HeightF = 36.45833F;
      this.PageFooter.Name = "PageFooter";
      // 
      // xrPageInfo1
      // 
      this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(0.1249949F, 0F);
      this.xrPageInfo1.Name = "xrPageInfo1";
      this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
      this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
      this.xrPageInfo1.SizeF = new System.Drawing.SizeF(313F, 23F);
      this.xrPageInfo1.StylePriority.UseTextAlignment = false;
      this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
      // 
      // IncomeStatement
      // 
      this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
      this.Detail,
      this.TopMargin,
      this.BottomMargin,
      this.PageHeader,
      this.IncomeDetailReport,
      this.ExpenseDetailReport,
      this.Profit,
      this.PageFooter});
      this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
      this.IncomeStatementDataSource});
      this.DataSource = this.IncomeStatementDataSource;
      this.Font = new DevExpress.Drawing.DXFont("Tahoma", 8.25F, DevExpress.Drawing.DXFontStyle.Regular, DevExpress.Drawing.DXGraphicsUnit.Point, new DevExpress.Drawing.DXFontAdditionalProperty[] {new DevExpress.Drawing.DXFontAdditionalProperty("GdiCharSet", ((byte)(0)))});
      this.Margins = new DevExpress.Drawing.DXMargins(53, 24, 24, 13);
      this.PageHeight = 1169;
      this.PageWidth = 827;
      this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
      this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
      this.ProfileFullName});
      this.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
      this.Version = "17.2";
      this.Watermark.Font = new DevExpress.Drawing.DXFont("Verdana", 36F, DevExpress.Drawing.DXFontStyle.Bold);
      this.Watermark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
      
      this.Watermark.TextTransparency = 250;
      ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.IncomeStatementDataSource)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion
  }
}