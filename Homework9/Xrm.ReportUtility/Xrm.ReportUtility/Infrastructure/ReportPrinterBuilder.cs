using System.Linq;
using System.Text;
using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Infrastructure
{
    // С помощью билдера определяем форму отчета; см. Program.cs
    public class ReportPrinterBuilder
    {
        private Report m_report;
        private bool m_withData;

        private StringBuilder m_headerRow = new StringBuilder();
        private StringBuilder m_rowTemplate = new StringBuilder();

        public ReportPrinterBuilder(Report report)
        {
            m_report = report;
            m_withData = report.Config.WithData && report.Data != null && report.Data.Any();
        }

        public IReportPrinter Create()
        {
            return new ReportPrinter(m_report, m_headerRow.ToString(), m_rowTemplate.ToString());
        }

        public ReportPrinterBuilder AddName()
        {
            if (!m_withData)
                return this;

            Append("Наименование\t", "{1,12}\t");

            return this;
        }

        public ReportPrinterBuilder AddPackageVolume()
        {
            if (!m_withData)
                return this;

            Append("Объём упаковки\t", "{2,14}\t");

            return this;
        }

        public ReportPrinterBuilder AddMass()
        {
            if (!m_withData)
                return this;

            Append("Масса упаковки\t", "{3,14}\t");

            return this;
        }

        public ReportPrinterBuilder AddCost()
        {
            if (!m_withData)
                return this;

            Append("Стоимость\t", "{4,9}\t");

            return this;
        }

        public ReportPrinterBuilder AddCount()
        {
            if (!m_withData)
                return this;

            Append("Количество\t", "{5,10}\t");

            return this;
        }

        public ReportPrinterBuilder AddIndex()
        {
            if (!m_withData || !m_report.Config.WithIndex)
                return this;

            Append("№\t", "{0}\t");

            return this;
        }

        public ReportPrinterBuilder AddTotalVolume()
        {
            if (!m_withData || !m_report.Config.WithTotalVolume)
                return this;

            Append("Суммарный объём\t", "{6,15}\t");

            return this;
        }

        public ReportPrinterBuilder AddTotalWeight()
        {
            if (!m_withData || !m_report.Config.WithTotalWeight)
                return this;

            Append("Суммарный вес\t", "{7,13}\t");

            return this;
        }

        private void Append(string headerRow, string rowTemplate)
        {
            m_headerRow.Append(headerRow);
            m_rowTemplate.Append(rowTemplate);
        }
    }
}
