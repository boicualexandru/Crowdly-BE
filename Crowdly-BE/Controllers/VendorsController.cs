using AutoMapper;
using Crowdly_BE.Vendors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Vendors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public async Task<ActionResult<Vendor>> CreateVendorAsync([FromForm] CreateVendorModel vendor)
        {
            var vendorId = Guid.NewGuid();

            var imageNames = UploadImages(vendorId, vendor.FormFiles);

            var createVendorModel = _mapper.Map<Services.Vendors.Models.CreateVendorModel>(vendor);
            createVendorModel.ImageUrls = imageNames;
            createVendorModel.Id = vendorId;

            var newVendor = await _vendorsService.CreateAsync(createVendorModel);

            return Ok(_mapper.Map<Vendor>(newVendor));
        }

        [HttpPut]
        [Route("{vendorId}")]
        public async Task<ActionResult> UpdateVendorAsync([FromRoute] Guid vendorId, [FromForm] UpdateVendorModel vendor)
        {
            var imageNames = UploadImages(vendorId, vendor.FormFiles);

            var updateVendorModel = _mapper.Map<Services.Vendors.Models.UpdateVendorModel>(vendor);
            updateVendorModel.Id = vendorId;
            updateVendorModel.ImageUrls = vendor.ExistingImageUrls.Concat(imageNames).ToArray();

            var removedImages = await _vendorsService.UpdateAsync(updateVendorModel);

            DeleteImages(vendorId, removedImages);

            return Ok();
        }

        private string[] UploadImages(Guid vendorId, IFormFile[] formFiles)
        {
            var imageNames = new List<string>();
            var directory = GetOrCreateVendorDirectory(vendorId);

            foreach (var formFile in formFiles)
            {
                var fileExtension = Path.GetExtension(formFile.FileName);
                var imageName = Guid.NewGuid().ToString() + fileExtension;

                string path = Path.Combine(directory, imageName);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                imageNames.Add(imageName);
            }

            return imageNames.ToArray();
        }

        private void DeleteImages(Guid vendorId, string[] imageNames)
        {
            var directory = GetOrCreateVendorDirectory(vendorId);

            foreach (var imageName in imageNames)
            {
                string path = Path.Combine(directory, imageName);
                System.IO.File.Delete(path);
            }
        }

        private string GetOrCreateVendorDirectory(Guid vendorId)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "vendors", vendorId.ToString());
            Directory.CreateDirectory(directory);

            return directory;
        }
    }
}
