using System;
using System.Linq;
using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Infrastructure
{
    public class ReportPrinter : IReportPrinter
    {
        private string m_headerRow;
        private string m_rowTemplate;

        public Report Report { get; }

        public ReportPrinter(Report report, string headerRow, string rowTemplate)
        {
            Report = report;

            m_headerRow = headerRow;
            m_rowTemplate = rowTemplate;
        }

        public void Print()
        {
            if (Report.Config.WithData && Report.Data != null && Report.Data.Any())
            {
                Console.WriteLine(m_headerRow);

                for (var i = 0; i < Report.Data.Length; i++)
                {
                    var dataRow = Report.Data[i];
                    Console.WriteLine(m_rowTemplate,
                        i + 1,
                        dataRow.Name,
                        dataRow.Volume,
                        dataRow.Weight,
                        dataRow.Cost,
                        dataRow.Count,
                        dataRow.Volume * dataRow.Count,
                        dataRow.Weight * dataRow.Count);
                }

                Console.WriteLine();
            }

            if (Report.Rows != null && Report.Rows.Any())
            {
                Console.WriteLine("Итого:");
                foreach (var reportRow in Report.Rows)
                {
                    Console.WriteLine(string.Format("  {0,-20}\t{1}", reportRow.Name, reportRow.Value));
                }
            }
        }
    }
}
