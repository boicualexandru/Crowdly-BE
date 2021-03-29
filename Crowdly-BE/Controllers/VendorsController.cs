﻿using AutoMapper;
using Crowdly_BE.Vendors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Vendors;
using System;
using System.Collections.Generic;
using System.IO;
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
            var imageNames = UploadImages(vendor.FormFiles);

            // TODO: change service model to accept multiple image urls
            var createVendorModel = _mapper.Map<Services.Vendors.Models.CreateVendorModel>(vendor);
            createVendorModel.ImageUrl = imageNames[0];
            var newVendor = await _vendorsService.CreateAsync(createVendorModel);

            return Ok(_mapper.Map<Vendor>(newVendor));
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult> UpdateVendorAsync(Vendor vendor)
        {
            await _vendorsService.UpdateAsync(_mapper.Map<Services.Vendors.Models.Vendor>(vendor));

            return Ok();
        }

        private string[] UploadImages(IFormFile[] formFiles)
        {
            var imageNames = new List<string>();

            foreach(var formFile in formFiles)
            {
                var fileExtension = Path.GetExtension(formFile.FileName);
                var imageName = Guid.NewGuid().ToString() + fileExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageName);

                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    formFile.CopyTo(stream);
                }

                imageNames.Add(imageName);
            }

            return imageNames.ToArray();
        }
    }
}
