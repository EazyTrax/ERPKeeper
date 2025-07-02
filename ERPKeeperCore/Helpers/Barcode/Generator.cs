using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPKeeperCore.Enterprise.Helper.Barcode
{
    public static class Generator
    {

        public static byte[] GenerateQRCodeImage(string content)
        {
            // Create QR code generator and data
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);

            // Create bitmap from QR code data
            using (BitmapByteQRCode qrCode = new BitmapByteQRCode(qrCodeData))
            {
                return qrCode.GetGraphic(20);
            }
        }

    }
}
