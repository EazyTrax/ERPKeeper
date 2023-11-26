using DevExpress.Pdf;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ERPKeeper.WebFrontEnd.Areas.Documents.Models
{
    public class PdfPageModel
    {
        PdfDocumentProcessor _documentProcessor;

        public PdfPageModel(PdfDocumentProcessor documentProcessor)
        {
            this._documentProcessor = documentProcessor;
        }

        public PdfDocumentProcessor DocumentProcessor
        {
            get
            {
                return _documentProcessor;
            }
        }

        public int PageNumber
        {
            get;
            set;
        }

        public byte[] GetPageImageBytes()
        {
            using (Bitmap bitmap = DocumentProcessor.CreateBitmap(PageNumber, 1000))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }
    }
}