using Services.SchedulePeriods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SchedulePeriods
{
    public interface ISchedulePeriodsService
    {
        Task<VendorSchedulePeriod[]> GetSchedulePeriodsByVendorIdAsync(Guid vendorId);
        Task<Period[]> GetUnavailablePeriodsByVendorIdAsync(Guid vendorId);
        Task<UserSchedulePeriod[]> GetSchedulePeriodsByUserIdAsync(Guid userId);
        Task<Guid> CreateSchedulePeriodAsync(CreateSchedulePeriodModel period);
        Task<Guid[]> CreateMultipleSchedulePeriodsAsync(CreateSchedulePeriodModel[] periods);
        Task<string[]> DeleteSchedulePeriodAsVendorAsync(Guid vendorId, Guid periodId);
        Task<string[]> DeleteSchedulePeriodAsUserAsync(Guid userId, Guid periodId);
    }
}
