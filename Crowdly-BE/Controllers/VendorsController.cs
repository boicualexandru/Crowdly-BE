using AutoMapper;
using Crowdly_BE.Authorization;
using Crowdly_BE.Vendors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Vendors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Crowdly_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendorsController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IVendorsService _vendorsService;
        private readonly IMapper _mapper;

        public VendorsController(IAuthorizationService authorizationService, IVendorsService vendorsService, IMapper mapper)
        {
            _authorizationService = authorizationService;
            _vendorsService = vendorsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<Vendor[]>> GetVendorsAsync([FromQuery]VendorsFilters filters)
        {
            var vendors = await _vendorsService.GetAllAsync(_mapper.Map<Services.Vendors.Models.VendorsFilters>(filters));

            return Ok(_mapper.Map<DataPage<Vendor>>(vendors));
        }

        [Authorize]
        [HttpGet("Editable")]
        public async Task<ActionResult<Vendor[]>> GetEditableVendorsAsync()
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var vendors = await _vendorsService.GetByUser(userId);

            return Ok(_mapper.Map<Vendor[]>(vendors));
        }

        [Authorize]
        [HttpGet]
        [Route("{vendorId}")]
        public async Task<ActionResult<VendorDetails>> GetVendorAsync([FromRoute]Guid vendorId)
        {
            var vendor = await _vendorsService.GetByIdAsync(vendorId);

            return Ok(await ConvertToVendorResponseAsync(vendor));
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Vendor>> CreateVendorAsync([FromForm] CreateVendorModel vendor)
        {
            var vendorId = Guid.NewGuid();

            var imageNames = UploadImages(vendorId, vendor.FormFiles);

            var createVendorModel = _mapper.Map<Services.Vendors.Models.CreateVendorModel>(vendor);
            createVendorModel.Images = imageNames;
            createVendorModel.Id = vendorId;
            createVendorModel.CreatedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var newVendor = await _vendorsService.CreateAsync(createVendorModel);

            return Ok(_mapper.Map<Vendor>(newVendor));
        }

        [Authorize]
        [HttpPut]
        [Route("{vendorId}")]
        public async Task<ActionResult> UpdateVendorAsync([FromRoute] Guid vendorId, [FromForm] UpdateVendorModel vendor)
        {
            var existingVendor = await _vendorsService.GetByIdAsync(vendorId);
            if (existingVendor is null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, existingVendor, VendorOperations.Update);

            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }

            var imageNames = UploadImages(vendorId, vendor.FormFiles);

            var updateVendorModel = _mapper.Map<Services.Vendors.Models.UpdateVendorModel>(vendor);
            updateVendorModel.Id = vendorId;
            updateVendorModel.Images = (vendor.ExistingImages ?? new string[0]).Concat(imageNames).ToArray();

            var removedImages = await _vendorsService.UpdateAsync(updateVendorModel);

            DeleteImages(vendorId, removedImages);

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("{vendorId}")]
        public async Task<ActionResult> DeleteVendorAsync([FromRoute] Guid vendorId)
        {
            var existingVendor = await _vendorsService.GetByIdAsync(vendorId);
            if (existingVendor is null)
            {
                return NotFound();
            }

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, existingVendor, VendorOperations.Delete);

            if (!authorizationResult.Succeeded)
            {
                return Unauthorized();
            }

            var removedImages = await _vendorsService.DeleteByIdAsync(vendorId);

            DeleteImages(vendorId, removedImages);

            return Ok();
        }

        private string[] UploadImages(Guid vendorId, IFormFile[] formFiles)
        {
            if (formFiles is null) return new string[0];

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

        private async Task<VendorDetails> ConvertToVendorResponseAsync(Services.Vendors.Models.VendorDetails vendor)
        {
            var vendorResponse = _mapper.Map<VendorDetails>(vendor);
            vendorResponse.IsEditable = false;

            if (User is null) return vendorResponse;

            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var vendorByUser = await _vendorsService.GetByUser(userId);

            vendorResponse.IsEditable = vendorByUser.Select(v => v.Id).Contains(vendor.Id);
            return vendorResponse;
        }
    }
}
