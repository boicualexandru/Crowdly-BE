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
        Task<SchedulePeriod[]> GetSchedulePeriodsByVendorIdAsync(Guid vendorId);
        Task<Period[]> GetUnavailablePeriodsByVendorIdAsync(Guid vendorId);
        Task<SchedulePeriod[]> GetSchedulePeriodsByUserIdAsync(Guid userId);
        Task<SchedulePeriod> CreateSchedulePeriodAsync(CreateSchedulePeriodModel period);
        Task<SchedulePeriod[]> CreateMultipleSchedulePeriodsAsync(CreateSchedulePeriodModel[] periods);
        Task<string[]> DeleteSchedulePeriodAsVendorAsync(Guid vendorId, Guid periodId);
        Task<string[]> DeleteSchedulePeriodAsUserAsync(Guid userId, Guid periodId);
    }
}
