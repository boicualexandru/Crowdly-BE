using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.SchedulePeriods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SchedulePeriods
{
    public class SchedulePeriodsService : ISchedulePeriodsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SchedulePeriodsService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SchedulePeriod> CreatePeriodAsync(CreateSchedulePeriodModel period)
        {
            var dbPeriod = _mapper.Map<DataAccess.Models.SchedulePeriod>(period);
            dbPeriod.StartDate = period.StartDate.Date;
            dbPeriod.EndDate = period.EndDate.Date;
            dbPeriod.CreatedAt = DateTime.Now;

            _dbContext.SchedulePeriods.Add(dbPeriod);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SchedulePeriod>(dbPeriod);
        }

        public async Task<string[]> DeletePeriodAsUserAsync(Guid userId, Guid periodId)
        {
            var dbPeriod = await _dbContext.SchedulePeriods.FirstOrDefaultAsync(period => period.Id == periodId && period.BookedByUserId == userId);

            if (dbPeriod is null) return new string[] {"Not found."};

            _dbContext.SchedulePeriods.Remove(dbPeriod);
            await _dbContext.SaveChangesAsync();

            return new string[0];
        }

        public async Task<string[]> DeletePeriodAsVendorAsync(Guid vendorId, Guid periodId)
        {
            var dbPeriod = await _dbContext.SchedulePeriods.FirstOrDefaultAsync(period => period.Id == periodId && period.VendorId == vendorId);

            if (dbPeriod is null) return new string[] { "Not found." };

            _dbContext.SchedulePeriods.Remove(dbPeriod);
            await _dbContext.SaveChangesAsync();

            return new string[0];
        }

        public async Task<SchedulePeriod[]> GetPeriodsByUserIdAsync(Guid userId)
        {
            var dbPeriods = await _dbContext.SchedulePeriods.Where(period => period.BookedByUserId == userId).ToArrayAsync();

            return _mapper.Map<SchedulePeriod[]>(dbPeriods);
        }

        public async Task<SchedulePeriod[]> GetPeriodsByVendorIdAsync(Guid vendorId)
        {
            var dbPeriods = await _dbContext.SchedulePeriods.Where(period => period.VendorId == vendorId).ToArrayAsync();

            return _mapper.Map<SchedulePeriod[]>(dbPeriods);
        }
    }
}
