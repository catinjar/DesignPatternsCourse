using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Services
{
    // Конкретный Adapter; см. IReportService
    public class XlsxReportService : ReportServiceBase
    {
        public XlsxReportService(string[] args) : base(args) { }

        protected override DataRow[] GetDataRows(string text)
        {
            return new DataRow[0];
        }
    }
}