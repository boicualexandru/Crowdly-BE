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

        public VendorsService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Vendor> CreateAsync(CreateVendorModel vendor)
        {
            var dbVendor = _mapper.Map<DataAccess.Models.Vendor>(vendor);
            dbVendor.ThumbnailUrl = vendor.ImageUrls.FirstOrDefault();

            _dbContext.Vendors.Add(dbVendor);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Vendor>(dbVendor);
        }

        public async Task<Vendor[]> GetAllAsync()
        {
            var dbVendors = await _dbContext.Vendors.ToArrayAsync();
            return _mapper.Map<Vendor[]>(dbVendors);
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

            var removedImages = dbVendor.ImageUrls.Where(imageUrl => !vendor.ImageUrls.Contains(imageUrl)).ToArray();

            _mapper.Map(vendor, dbVendor);
            dbVendor.ThumbnailUrl = dbVendor.ImageUrls.FirstOrDefault();

            _dbContext.Vendors.Update(dbVendor);
            await _dbContext.SaveChangesAsync();

            return removedImages;
        }

        public async Task<string[]> DeleteByIdAsync(Guid id)
        {
            var dbVendor = _dbContext.Vendors.FirstOrDefault(v => v.Id == id);

            _dbContext.Vendors.Remove(dbVendor);
            await _dbContext.SaveChangesAsync();

            return dbVendor.ImageUrls;
        }
    }
}
