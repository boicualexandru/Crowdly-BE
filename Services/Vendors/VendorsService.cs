using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.Vendors.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Vendors
{
    public class VendorsService : IVendorsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        private const int PAGE_SIZE = 5;

        public VendorsService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Vendor> CreateAsync(CreateVendorModel vendor)
        {
            var dbVendor = _mapper.Map<DataAccess.Models.Vendor>(vendor);
            dbVendor.Thumbnail = vendor.Images.FirstOrDefault();

            _dbContext.Vendors.Add(dbVendor);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Vendor>(dbVendor);
        }

        public async Task<DataPage<Vendor>> GetAllAsync(VendorsFilters filters)
        {
            var dbVendorsQuery = _dbContext.Vendors.AsQueryable();

            if (!String.IsNullOrWhiteSpace(filters.City))
                dbVendorsQuery = dbVendorsQuery.Where(vendor => vendor.City == filters.City);

            if (filters.Category != VendorCategoryType.None)
            {
                var category = _mapper.Map<DataAccess.Models.VendorCategoryType>(filters.Category);
                dbVendorsQuery = dbVendorsQuery.Where(vendor => vendor.Category == category);
            }

            if (filters.PriceMin.HasValue)
                dbVendorsQuery = dbVendorsQuery.Where(vendor => vendor.Price >= filters.PriceMin.Value);
            if (filters.PriceMax.HasValue)
                dbVendorsQuery = dbVendorsQuery.Where(vendor => vendor.Price <= filters.PriceMax.Value);

            if (filters.Skip > 0)
                dbVendorsQuery = dbVendorsQuery.Skip(filters.Skip.Value);

            var data = await dbVendorsQuery.Take(PAGE_SIZE + 1).ToArrayAsync();

            var page = new DataPage<Vendor>()
            {
                Data = _mapper.Map<Vendor[]>(data.Take(PAGE_SIZE)),
                HasMore = data.Length > PAGE_SIZE
            };

            return page;
        }

        public async Task<VendorDetails> GetByIdAsync(Guid id)
        {
            var vendor = await _dbContext.Vendors.FirstOrDefaultAsync(v => v.Id == id);
            return _mapper.Map<VendorDetails>(vendor);
        }

        public async Task<Vendor[]> GetByUser(string userId)
        {
            var dbVendors = await _dbContext.Vendors.Where(v => v.CreatedByUserId == userId).ToArrayAsync();
            return _mapper.Map<Vendor[]>(dbVendors);
        }

        public async Task<string[]> UpdateAsync(UpdateVendorModel vendor)
        {
            var dbVendor = _dbContext.Vendors.FirstOrDefault(v => v.Id == vendor.Id);

            var removedImages = dbVendor.Images.Where(image => !vendor.Images.Contains(image)).ToArray();

            _mapper.Map(vendor, dbVendor);
            dbVendor.Thumbnail = dbVendor.Images.FirstOrDefault();

            _dbContext.Vendors.Update(dbVendor);
            await _dbContext.SaveChangesAsync();

            return removedImages;
        }

        public async Task<string[]> DeleteByIdAsync(Guid id)
        {
            var dbVendor = _dbContext.Vendors.FirstOrDefault(v => v.Id == id);

            _dbContext.Vendors.Remove(dbVendor);
            await _dbContext.SaveChangesAsync();

            return dbVendor.Images;
        }
    }
}
