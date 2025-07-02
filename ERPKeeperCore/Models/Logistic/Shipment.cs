using ERPKeeperCore.Enterprise.Models.Accounting.Enums;
using ERPKeeperCore.Enterprise.Models.Customers.Enums;
using ERPKeeperCore.Enterprise.Models.Enums;
using ERPKeeperCore.Enterprise.Models.Profiles;
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

        public byte[] GeneratePDF(ProfileAddress senderAddress)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(4, 6, Unit.Inch);
                    page.Margin(5, Unit.Millimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content()
                        .PaddingVertical(2, Unit.Millimetre) // Reduced from 1 centimetre to 2 millimetres
                        .Column(x =>
                        {


                            // Sender Address Section
                            x.Item().PaddingTop(2).Text("ผู้ส่ง:Sender").FontSize(16).SemiBold().FontColor(Colors.Grey.Darken2); // Reduced from 5 to 2
                            x.Item().PaddingTop(2).Border(1).Padding(5).Column(col => // Reduced from 5 to 2
                            {
                                if (senderAddress != null)
                                {
                                    col.Item().Text($"{senderAddress.Profile.Name ?? ""}").SemiBold();
                                    col.Item().Text($"{senderAddress.Title ?? ""}").SemiBold();
                                    col.Item().Text($"{senderAddress.AddressLine ?? ""}");
                                    col.Item().Text($"{senderAddress.PhoneNumber ?? "N/A"}");
                                }
                                else
                                {
                                    col.Item().Text("No Sender Address specified").FontColor(Colors.Red.Medium);
                                }
                            });


                            // Shipping Address Section
                            x.Item().PaddingTop(2).Text("ผู้รับ:Receiver").FontSize(16).SemiBold().FontColor(Colors.Grey.Darken2); // Reduced from 5 to 2
                            x.Item().PaddingTop(2).Border(1).Padding(5).MinHeight(120).Column(col => // Reduced MinHeight from 150 to 120 and PaddingTop from 5 to 2
                            {

                                if (ShipmentAddesss != null)
                                {
                                    col.Item().Text($"{ShipmentAddesss.Profile.Name ?? ""}").SemiBold();
                                    col.Item().Text($"{ShipmentAddesss.Title ?? ""}").SemiBold();
                                    col.Item().Text($"{ShipmentAddesss.AddressLine ?? ""}");
                                    col.Item().Text($"{ShipmentAddesss.PhoneNumber ?? "N/A"}");
                                    col.Item().Text($"{Person ?? "N/A"}").SemiBold();
                                }
                                else
                                {
                                    col.Item().Text("No shipping address specified").FontColor(Colors.Red.Medium);
                                }
                            });

                            // Notes Section
                            if (!string.IsNullOrEmpty(Note))
                            {
                                x.Item().PaddingTop(2).Text("Notes").FontSize(16).SemiBold().FontColor(Colors.Grey.Darken2); // Reduced from 5 to 2
                                x.Item().PaddingTop(2).Border(1).Padding(5).Text(Note); // Reduced from 5 to 2
                            }




                            // Barcode/QR Code placeholder for tracking
                            if (!string.IsNullOrEmpty(TrackingNo))
                            {
                                x.Item().PaddingTop(2).AlignCenter().Column(col => // Reduced from 5 to 2
                                {
                                    col.Item().Text("Tracking Code").FontSize(14).SemiBold();
                                    col.Item().PaddingTop(2).Border(1).Padding(15).Text(TrackingNo).FontSize(16).FontFamily("Courier New"); // Reduced PaddingTop from 5 to 2 and Padding from 20 to 15
                                });
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Row(row =>
                        {
                            row.RelativeItem().Column(col =>
                            {
                                col.Item().Text($"Lot No: {LotNo ?? "N/A"}");
                                col.Item().Text($"{DateTime.Now:yyyy-MM-dd HH:mm}").SemiBold();
                            });
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}