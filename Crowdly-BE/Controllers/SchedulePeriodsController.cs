using AutoMapper;
using Crowdly_BE.Authorization;
using Crowdly_BE.Models.SchedulePeriods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.SchedulePeriods;
using Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Crowdly_BE.Controllers
{
    [ApiController]
    [Authorize]
    [Route("")]
    public class SchedulePeriodsController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ISchedulePeriodsService _schedulePeriodsService;
        private readonly IVendorsService _vendorsService;
        private readonly IMapper _mapper;

        public SchedulePeriodsController(IAuthorizationService authorizationService, ISchedulePeriodsService schedulePeriodsService, IVendorsService vendorsService, IMapper mapper)
        {
            _authorizationService = authorizationService;
            _schedulePeriodsService = schedulePeriodsService;
            _vendorsService = vendorsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Vendors/{vendorId}/SchedulePeriods")]
        public async Task<ActionResult<SchedulePeriod[]>> GetSchedulePeriodsByVendorIdAsync([FromRoute] Guid vendorId)
        {
            var existingVendor = await _vendorsService.GetByIdAsync(vendorId);
            if (existingVendor is null) return NotFound();

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, existingVendor, VendorOperations.Update);
            if (!authorizationResult.Succeeded) return Unauthorized();

            var periods = await _schedulePeriodsService.GetPeriodsByVendorIdAsync(vendorId);

            return Ok(_mapper.Map<SchedulePeriod[]>(periods));
        }

        [HttpGet]
        [Route("User/SchedulePeriods")]
        public async Task<ActionResult<SchedulePeriod[]>> GetUsersSchedulePeriodsAsync()
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var periods = await _schedulePeriodsService.GetPeriodsByUserIdAsync(userId);

            return Ok(_mapper.Map<SchedulePeriod[]>(periods));
        }

        [HttpDelete]
        [Route("Vendors/{vendorId}/SchedulePeriods/{periodId}")]
        public async Task<ActionResult> DeletePeriodAsVendorAsync([FromRoute] Guid vendorId, [FromRoute] Guid periodId)
        {
            var existingVendor = await _vendorsService.GetByIdAsync(vendorId);
            if (existingVendor is null) return NotFound();

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, existingVendor, VendorOperations.Update);
            if (!authorizationResult.Succeeded) return Unauthorized();

            var errorMessages = await _schedulePeriodsService.DeletePeriodAsVendorAsync(vendorId, periodId);
            if (errorMessages.Any()) return BadRequest(errorMessages);

            return Ok();
        }

        [HttpDelete]
        [Route("User/SchedulePeriods/{periodId}")]
        public async Task<ActionResult> DeletePeriodAsUserAsync([FromRoute] Guid periodId)
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var errorMessages = await _schedulePeriodsService.DeletePeriodAsUserAsync(userId, periodId);
            if (errorMessages.Any()) return BadRequest(errorMessages);

            return Ok();
        }

        [HttpPost]
        [Route("Vendors/{vendorId}/SchedulePeriods")]
        public async Task<ActionResult<SchedulePeriod>> CreatePeriodAsVendorAsync([FromRoute] Guid vendorId, CreateSchedulePeriodModel createPeriodModel)
        {
            var existingVendor = await _vendorsService.GetByIdAsync(vendorId);
            if (existingVendor is null) return NotFound();

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, existingVendor, VendorOperations.Update);
            if (!authorizationResult.Succeeded) return Unauthorized();

            var createPeriodServiceModel = _mapper.Map<Services.SchedulePeriods.Models.CreateSchedulePeriodModel>(createPeriodModel);
            createPeriodServiceModel.VendorId = vendorId;
            var createdPeriod = await _schedulePeriodsService.CreatePeriodAsync(createPeriodServiceModel);

            return Ok(_mapper.Map<SchedulePeriod>(createdPeriod));
        }

        [HttpPost]
        [Route("Vendors/{vendorId}/Book")]
        public async Task<ActionResult<SchedulePeriod>> BookPeriodAsUserAsync([FromRoute] Guid vendorId, CreateSchedulePeriodModel createPeriodModel)
        {
            var existingVendor = await _vendorsService.GetByIdAsync(vendorId);
            if (existingVendor is null) return NotFound();

            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var createPeriodServiceModel = _mapper.Map<Services.SchedulePeriods.Models.CreateSchedulePeriodModel>(createPeriodModel);
            createPeriodServiceModel.VendorId = vendorId;
            createPeriodServiceModel.BookedByUserId = userId;
            var createdPeriod = await _schedulePeriodsService.CreatePeriodAsync(createPeriodServiceModel);

            return Ok(_mapper.Map<SchedulePeriod>(createdPeriod));
        }
    }
}
