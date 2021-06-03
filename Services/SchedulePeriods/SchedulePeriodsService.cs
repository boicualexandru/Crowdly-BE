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

        public async Task<SchedulePeriod> CreateSchedulePeriodAsync(CreateSchedulePeriodModel period)
        {
            var now = DateTime.Now;
            var dbPeriod = ConvertToDbSchedulePeriod(period, now);

            _dbContext.SchedulePeriods.Add(dbPeriod);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SchedulePeriod>(dbPeriod);
        }

        public async Task<SchedulePeriod[]> CreateMultipleSchedulePeriodsAsync(CreateSchedulePeriodModel[] periods)
        {
            var now = DateTime.Now;
            var dbPeriods = periods.Select(period => ConvertToDbSchedulePeriod(period, now)).ToArray();

            _dbContext.SchedulePeriods.AddRange(dbPeriods);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<SchedulePeriod[]>(dbPeriods);
        }

        public async Task<string[]> DeleteSchedulePeriodAsUserAsync(Guid userId, Guid periodId)
        {
            var dbPeriod = await _dbContext.SchedulePeriods.FirstOrDefaultAsync(period => period.Id == periodId && period.BookedByUserId == userId);

            if (dbPeriod is null) return new string[] {"Not found."};

            _dbContext.SchedulePeriods.Remove(dbPeriod);
            await _dbContext.SaveChangesAsync();

            return new string[0];
        }

        public async Task<string[]> DeleteSchedulePeriodAsVendorAsync(Guid vendorId, Guid periodId)
        {
            var dbPeriod = await _dbContext.SchedulePeriods.FirstOrDefaultAsync(period => period.Id == periodId && period.VendorId == vendorId);

            if (dbPeriod is null) return new string[] { "Not found." };

            _dbContext.SchedulePeriods.Remove(dbPeriod);
            await _dbContext.SaveChangesAsync();

            return new string[0];
        }

        public async Task<SchedulePeriod[]> GetSchedulePeriodsByUserIdAsync(Guid userId)
        {
            var dbPeriods = await _dbContext.SchedulePeriods
                .Where(period => period.BookedByUserId == userId)
                .OrderByDescending(period => period.StartDate)
                .ToArrayAsync();

            return _mapper.Map<SchedulePeriod[]>(dbPeriods);
        }

        public async Task<SchedulePeriod[]> GetSchedulePeriodsByVendorIdAsync(Guid vendorId)
        {
            var dbPeriods = await _dbContext.SchedulePeriods
                .Where(period => period.VendorId == vendorId)
                .OrderByDescending(period => period.StartDate)
                .ToArrayAsync();

            return _mapper.Map<SchedulePeriod[]>(dbPeriods);
        }

        public async Task<Period[]> GetUnavailablePeriodsByVendorIdAsync(Guid vendorId)
        {
            var dbPeriods = await _dbContext.SchedulePeriods
                .Where(period => period.VendorId == vendorId)
                .OrderBy(period => period.StartDate)
                .Select(period => new Period { StartDate = period.StartDate, EndDate = period.EndDate })
                .ToArrayAsync();

            return dbPeriods;
        }

        private DataAccess.Models.SchedulePeriod ConvertToDbSchedulePeriod(CreateSchedulePeriodModel period, DateTime createdAt)
        {
            var dbPeriod = _mapper.Map<DataAccess.Models.SchedulePeriod>(period);
            dbPeriod.StartDate = period.StartDate.Date;
            dbPeriod.EndDate = period.EndDate.Date;
            dbPeriod.CreatedAt = createdAt;

            return dbPeriod;
        }
    }
}
