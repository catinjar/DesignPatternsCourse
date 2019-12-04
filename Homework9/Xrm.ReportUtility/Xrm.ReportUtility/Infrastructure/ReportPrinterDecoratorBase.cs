using System;
using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Infrastructure
{
    // С помощью декораторов контролируем вывод отчета
    public abstract class ReportPrinterDecoratorBase : IReportPrinter
    {
        public Report Report => m_reportPrinter.Report;

        protected IReportPrinter m_reportPrinter;

        public ReportPrinterDecoratorBase(IReportPrinter reportPrinter)
        {
            m_reportPrinter = reportPrinter;
        }

        public void Print()
        {
            OnBeforePrint();
            m_reportPrinter.Print();
            OnAfterPrint();
        }

        protected virtual void OnBeforePrint() { }
        protected virtual void OnAfterPrint() { }
    }

    // Например, печатаем предупреждения
    // Такой логике явно не место в основном классе
    public class ReportPrinterWarningDecorator : ReportPrinterDecoratorBase
    {
        public ReportPrinterWarningDecorator(IReportPrinter reportPrinter) : base(reportPrinter) { }

        protected override void OnBeforePrint()
        {
            var config = m_reportPrinter.Report.Config;

            if (config.WithData && (!config.WithIndex || !config.WithTotalVolume || !config.WithTotalWeight))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("WARNING");
                Console.ResetColor();
            }
        }
    }
}
