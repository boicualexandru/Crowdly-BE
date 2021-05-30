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
        Task<SchedulePeriod[]> GetPeriodsByVendorIdAsync(Guid vendorId);
        Task<SchedulePeriod[]> GetPeriodsByUserIdAsync(Guid userId);
        Task<SchedulePeriod> CreatePeriodAsync(CreateSchedulePeriodModel period);
        Task<string[]> DeletePeriodAsVendorAsync(Guid vendorId, Guid periodId);
        Task<string[]> DeletePeriodAsUserAsync(Guid userId, Guid periodId);
    }
}
