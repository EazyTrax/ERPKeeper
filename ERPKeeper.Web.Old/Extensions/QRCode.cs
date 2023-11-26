using DevExpress.BarCodes;
using System;
using System.Drawing;
using System.Linq;

namespace ERPKeeper.WebFrontEnd.Extensions
{
    public class QRCode
    {
        public void QR(string Type)
        {
            Guid id = Guid.NewGuid();
            var barCode = new BarCode();

            System.IO.Stream imageStream;
            imageStream = new System.IO.MemoryStream();
            barCode.Symbology = Symbology.QRCode;

            barCode.CodeBinaryData = System.Text.Encoding.UTF8.GetBytes(Type + id.ToString("D"));
            barCode.ImageWidth = 70;
            barCode.Options.QRCode.CompactionMode = QRCodeCompactionMode.Byte;
            barCode.Options.QRCode.ErrorLevel = QRCodeErrorLevel.M;
            barCode.ImageHeight = 70;
            barCode.ForeColor = ColorTranslator.FromHtml("#3b5998");
            barCode.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);

            imageStream.Position = 0;
            Image img = Image.FromStream(imageStream);

            int cropWidth = 250;
            int cropHeight = 110;

            Bitmap newImage = new Bitmap(cropWidth, cropHeight, img.PixelFormat);

            using (Graphics g = Graphics.FromImage(newImage))
            {
                // fill target image with white color
                g.FillRectangle(Brushes.White, 0, 0, cropWidth, cropHeight);


                g.DrawImage(img, 5, 5, 100, 100);


                Font fontBold = new Font("Arial", 9, FontStyle.Bold);
                PointF TypedrawingPoint = new PointF(110, 10);
                g.DrawString(Type.ToUpper(), fontBold, Brushes.Black, TypedrawingPoint);

                Font font = new Font("Arial", 9);
                PointF drawingPoint = new PointF(110, 35);

                g.DrawString(id.ToString("D").Replace("-", Environment.NewLine), font, Brushes.Black, drawingPoint);

            }

            newImage.Save(@"c:\temp\" + Type + "_" + id.ToString() + @".png");
        }
    }
}