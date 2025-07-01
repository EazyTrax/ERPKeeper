using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Taxes;
using ERPKeeperCore.Enterprise.Models.Transactions;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;


namespace ERPKeeperCore.Enterprise.Models.Logistic
{
    public partial class Shipment
    {
        [Key]
        public Guid Id { get; set; }
        public string? LotNo { get; set; }
        public String? Person { get; set; }
        public String? Note { get; set; }
        public String? TrackingNo { get; set; }
        public DateTime Date { get; set; }


        public Guid TransactionId { get; set; }


        public Guid ShipmentProviderId { get; set; }
        [ForeignKey("ShipmentProviderId")]
        public virtual LogisticProvider ShipmentProvider { get; set; }


        public Guid? ShipmentAddesssId { get; set; }
        [ForeignKey("ShipmentAddesssId")]
        public virtual Profiles.ProfileAddress? ShipmentAddesss { get; set; }

        public byte[] GeneratePDF()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("SHIPMENT LABEL")
                        .SemiBold().FontSize(24).FontColor(Colors.Blue.Medium)
                        .AlignCenter();

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            // Shipment Info Section
                            x.Item().Text("Shipment Information").FontSize(16).SemiBold().FontColor(Colors.Grey.Darken2);
                            x.Item().PaddingTop(10).Row(row =>
                            {
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text($"Date: {Date:yyyy-MM-dd}");
                                    col.Item().Text($"Tracking No: {TrackingNo ?? "N/A"}");
                                    col.Item().Text($"Lot No: {LotNo ?? "N/A"}");
                                });
                                row.RelativeItem().Column(col =>
                                {
                                    col.Item().Text($"Provider: {ShipmentProvider?.Name ?? "N/A"}");
                                    col.Item().Text($"Person: {Person ?? "N/A"}");
                                });
                            });

                            // Shipping Address Section
                            x.Item().PaddingTop(20).Text("Shipping Address").FontSize(16).SemiBold().FontColor(Colors.Grey.Darken2);
                            x.Item().PaddingTop(10).Border(1).Padding(10).Column(col =>
                            {
                                if (ShipmentAddesss != null)
                                {
                                    col.Item().Text($"{ShipmentAddesss.Name ?? ""}").SemiBold();
                                    col.Item().Text($"{ShipmentAddesss.AddressLine ?? ""}");
                                    col.Item().Text($"Phone: {ShipmentAddesss.PhoneNumber ?? "N/A"}");
                                }
                                else
                                {
                                    col.Item().Text("No shipping address specified").FontColor(Colors.Red.Medium);
                                }
                            });

                            // Notes Section
                            if (!string.IsNullOrEmpty(Note))
                            {
                                x.Item().PaddingTop(20).Text("Notes").FontSize(16).SemiBold().FontColor(Colors.Grey.Darken2);
                                x.Item().PaddingTop(10).Border(1).Padding(10).Text(Note);
                            }

                            // Barcode/QR Code placeholder for tracking
                            if (!string.IsNullOrEmpty(TrackingNo))
                            {
                                x.Item().PaddingTop(20).AlignCenter().Column(col =>
                                {
                                    col.Item().Text("Tracking Code").FontSize(14).SemiBold();
                                    col.Item().PaddingTop(5).Border(1).Padding(20).Text(TrackingNo).FontSize(16).FontFamily("Courier New");
                                });
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated on ");
                            x.Span($"{DateTime.Now:yyyy-MM-dd HH:mm}").SemiBold();
                        });
                });
            });

            return document.GeneratePdf();
        }
    }

}
