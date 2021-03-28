using AutoMapper;
using Crowdly_BE.Vendors;
using Microsoft.AspNetCore.Mvc;
using Services.Vendors;
using System.Threading.Tasks;

namespace Crowdly_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorsService _vendorsService;
        private readonly IMapper _mapper;

        public VendorsController(IVendorsService vendorsService, IMapper mapper)
        {
            _vendorsService = vendorsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<Vendor[]>> GetVendorsAsync()
        {
            var vendors = await _vendorsService.GetAllAsync();

            return Ok(_mapper.Map<Vendor[]>(vendors));
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Vendor>> CreateVendorAsync(CreateVendorModel vendor)
        {
            var newVendor = await _vendorsService.CreateAsync(_mapper.Map<Services.Vendors.Models.CreateVendorModel>(vendor));

            return Ok(_mapper.Map<Vendor>(newVendor));
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateVendorAsync(Vendor vendor)
        {
            await _vendorsService.UpdateAsync(_mapper.Map<Services.Vendors.Models.Vendor>(vendor));

            return Ok();
        }
    }
}
