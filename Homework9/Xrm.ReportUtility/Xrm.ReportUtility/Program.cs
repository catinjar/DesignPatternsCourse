using System;
using Xrm.ReportUtility.Factories;
using Xrm.ReportUtility.Infrastructure;

namespace Xrm.ReportUtility
{
    public static class Program
    {
        // "Files/table.txt" -data -weightSum -costSum -withIndex -withTotalVolume
        public static void Main(string[] args)
        {
            var service = ReportServiceFactory.GetReportService(args); // Используем Factory, см. ReportServiceFactory.cs
            var report = service.CreateReport();

            // Реализовываем Builder для вывода таблиц вместо обычной функции
            // Кода стало побольше, но он аккуратнее, причем мы теперь можем быстро менять порядок/набор столбцов, которые хотим вывести
            // Логика, отвечающая за наличие/отсутствие столбцов скрыта внутри билдера
            var reportPrinter = new ReportPrinterBuilder(report)
                .AddIndex() // Добавляем столбцы
                .AddName()
                .AddPackageVolume()
                .AddMass()
                .AddCost()
                .AddCount()
                .AddTotalVolume()
                .AddTotalWeight()
                .Create(); // Все добавили, создаем принтер

            reportPrinter = new ReportPrinterWarningDecorator(reportPrinter); // Будем выводить с предупреждением

            reportPrinter.Print();

            Console.WriteLine("");
            Console.WriteLine("Press enter...");
            Console.ReadLine();
        }
    }
}