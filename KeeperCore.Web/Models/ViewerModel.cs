using DevExpress.XtraReports.Web.WebDocumentViewer;

namespace KeeperCore.Web.Models {
    public class ViewerModel {
        public string ReportUrl { get; set; }
        public WebDocumentViewerModel ViewerModelToBind { get; set; }
    }
}