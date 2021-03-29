using Services.Vendors.Models;
using System.Threading.Tasks;

namespace Services.Vendors
{
    public interface IVendorsService
    {
        Task<Vendor[]> GetAllAsync();

        Task<Vendor> CreateAsync(CreateVendorModel vendor);

        Task<string[]> UpdateAsync(UpdateVendorModel vendor);
    }
}
