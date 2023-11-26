
using ERPKeeper.Node.Models.Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using ERPKeeper.Node.DAL.Company;
using ERPKeeper.Node.Models.Accounting;
using ERPKeeper.Node.Models.Transactions;
using System.Data.Entity;
using ERPKeeper.Node.Models.Accounting.Enums;
using ERPKeeper.Node.Models.Documents;
using DevExpress.Pdf;
using System.IO;

namespace ERPKeeper.Node.DAL.Documents
{

    public class Documents : ERPNodeDalRepository
    {
        public Documents(Organization organization) : base(organization)
        {

        }

        public List<Document> GetAll() => erpNodeDBContext.Documents.ToList();
        public IQueryable<Document> GetQuery() => erpNodeDBContext.Documents;
        public Document Find(Guid id) => erpNodeDBContext.Documents.Find(id);
        public Document CreateNew(Document template)
        {
            template.Uid = Guid.NewGuid();
            erpNodeDBContext.Documents.Add(template);
            this.SaveChanges();

            return template;
        }

        public void Delete(Guid id)
        {
            var ExistFile = this.erpNodeDBContext.Documents.Find(id);
            this.erpNodeDBContext.Documents.Remove(ExistFile);
            this.SaveChanges();
        }


        public Document ExtractPage(Document sourceDocument, int fromPageNo)
        {
            using (PdfDocumentProcessor source = new PdfDocumentProcessor())
            {
                Stream streamDocumentData = new MemoryStream(sourceDocument.Content);
                source.LoadDocument(streamDocumentData);

                using (PdfDocumentProcessor target = new PdfDocumentProcessor())
                {
                    target.CreateEmptyDocument();

                    for (int i = fromPageNo; i < source.Document.Pages.Count; i++)
                    {
                        target.Document.Pages.Add(source.Document.Pages[i]);
                    }

                    MemoryStream targetMemoryStream = new MemoryStream();
                    target.SaveDocument(targetMemoryStream);

                    var targetDocument = new Document()
                    {
                        Uid = Guid.NewGuid(),
                        FileName = $"REM-{sourceDocument.FileName}",
                        Name = $"REM-{sourceDocument.Name}",
                        ContentType = sourceDocument.ContentType,
                        TransactionType = sourceDocument.TransactionType,
                        DocumentDate = sourceDocument.DocumentDate,
                        RecordTime = DateTime.Now,
                        PageCount = target.Document.Pages.Count,
                        Creator = sourceDocument.Creator,
                        Content = targetMemoryStream.ToArray(),
                        Size = targetMemoryStream.Length
                    };

                    this.Create(targetDocument);
                    this.SaveChanges();
                    return targetDocument;
                }
            }


        }

        public void Matching()
        {
            organization.ErpNodeDBContext.Documents.Where(d => d.ReferenceGuid == null).ToList()
                  .ForEach(d =>
                  {

                      var com = organization.ErpNodeDBContext
                      .Commercials
                      .Where(c => c.Reference.ToLower() == d.Name.ToLower() || c.Name == d.ObjectName)
                      .FirstOrDefault();

                      if (com != null)
                      {
                          Console.WriteLine($"Mapping {com.Name} => {d.Name}");
                          d.ReferenceGuid = com?.Uid;
                      }
                  });
            this.SaveChanges();
        }

        public void ReOrder()
        {
            int i = 0;
            erpNodeDBContext.Documents
                .OrderBy(d => d.DocumentDate)
                .ToList()
                .ForEach(d => d.No = ++i);
            organization.SaveChanges();

        }


        public void RemovePage(Document pdfDocument, int page)
        {
            using (PdfDocumentProcessor source = new PdfDocumentProcessor())
            {
                Stream streamDocumentData = new MemoryStream(pdfDocument.Content);
                source.LoadDocument(streamDocumentData);
                source.DeletePage(page);
                MemoryStream ms = new MemoryStream();
                source.SaveDocument(ms);

                pdfDocument.Content = ms.ToArray();
                pdfDocument.Size = pdfDocument.Content.Length;
                pdfDocument.PageCount = source.Document.Pages.Count;
            }
        }
        public Document RemovePages(Document document, int fromPageNo)
        {
            using (PdfDocumentProcessor source = new PdfDocumentProcessor())
            {
                Stream streamDocumentData = new MemoryStream(document.Content);
                source.LoadDocument(streamDocumentData);

                var sourcePagesCount = source.Document.Pages.Count;

                for (int i = sourcePagesCount; i > fromPageNo; i--)
                {
                    source.DeletePage(i);
                }

                MemoryStream ms = new MemoryStream();
                source.SaveDocument(ms);

                document.Content = ms.ToArray();
                document.Size = document.Content.Length;
                document.PageCount = source.Document.Pages.Count;
            }

            this.SaveChanges();
            return document;
        }

        public void Create(Document document)
        {
            erpNodeDBContext.Documents.Add(document);
        }
    }
}