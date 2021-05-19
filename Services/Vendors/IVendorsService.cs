using Services.Vendors.Models;
using System;
using System.Threading.Tasks;

namespace Services.Vendors
{
    public interface IVendorsService
    {
        Task<Vendor[]> GetAllAsync();
        Task<VendorDetails> GetByIdAsync(Guid id);
        Task<Vendor> CreateAsync(CreateVendorModel vendor);
        Task<string[]> UpdateAsync(UpdateVendorModel vendor);
        Task<string[]> DeleteByIdAsync(Guid id);
    }
}
