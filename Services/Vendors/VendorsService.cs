﻿using AutoMapper;
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

        public async Task CreateAsync(AddVendorModel vendor)
        {
            var dbVendor = _mapper.Map<DataAccess.Models.Vendor>(vendor);
            dbVendor.Id = Guid.NewGuid().ToString();

            _dbContext.Vendors.Add(dbVendor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Vendor[]> GetAllAsync()
        {
            var dbVendors = await _dbContext.Vendors.AsNoTracking().ToArrayAsync();
            return _mapper.Map<Vendor[]>(dbVendors);
        }

        public async Task UpdateAsync(Vendor vendor)
        {
            var dbVendor = _dbContext.Vendors.FirstOrDefault(v => v.Id == vendor.Id);
            _mapper.Map(vendor, dbVendor);

            _dbContext.Vendors.Update(dbVendor);
            await _dbContext.SaveChangesAsync();
        }
    }
}
