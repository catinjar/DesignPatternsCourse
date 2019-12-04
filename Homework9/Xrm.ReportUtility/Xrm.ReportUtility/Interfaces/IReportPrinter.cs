using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Interfaces
{
    // Используем для реализации декораторов
    public interface IReportPrinter
    {
        Report Report { get; }
        void Print();
    }
}
