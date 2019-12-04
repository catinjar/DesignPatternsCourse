using Xrm.ReportUtility.Models;

namespace Xrm.ReportUtility.Interfaces
{
    // Здесь паттерн Adapter
    // Это интерфейс для конкретных адаптеров, инкапсулирующих логику разных API для работы с форматами данных
    public interface IReportService
    {
        Report CreateReport();
    }
}
