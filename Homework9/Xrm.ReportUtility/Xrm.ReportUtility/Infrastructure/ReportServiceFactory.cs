using System;
using Xrm.ReportUtility.Interfaces;
using Xrm.ReportUtility.Services;

namespace Xrm.ReportUtility.Factories
{
    // Добавляем Factory для разных сервисов, чтобы изолировать логику их создания
    // Кажется, что код тут достаточно простой и проблем с добавлением пары новых сервисов возникнуть не должно
    public static class ReportServiceFactory
    {
        public static IReportService GetReportService(string[] args)
        {
            var filename = args[0];

            if (filename.EndsWith(".txt"))
            {
                return new TxtReportService(args);
            }

            if (filename.EndsWith(".csv"))
            {
                return new CsvReportService(args);
            }

            if (filename.EndsWith(".xlsx"))
            {
                return new XlsxReportService(args);
            }

            throw new NotSupportedException("this extension not supported");
        }
    }
}
