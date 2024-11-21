using System;
using DevExpress.XtraReports.UI;

namespace ERPKeeperCore.Enterprise.Report
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Create an instance of your XtraReport1 class
                XtraReport1 report = new XtraReport1();

                // Optionally set report parameters (if any)
                // report.Parameters["ParameterName"].Value = "Value";

                // Export the report to a PDF file
                string outputPath = @"GeneratedReport.pdf";
                report.ExportToPdf(outputPath);

                Console.WriteLine($"Report successfully exported to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
