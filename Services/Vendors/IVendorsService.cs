using Services.Vendors.Models;
using System.Threading.Tasks;

namespace Services.Vendors
{
    public interface IVendorsService
    {
        Task<Vendor[]> GetAllAsync();

        Task CreateAsync(AddVendorModel vendor);

        Task UpdateAsync(Vendor vendor);
    }
}
