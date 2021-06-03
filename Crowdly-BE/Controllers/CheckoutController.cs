using Crowdly_BE.Models.Checkout;
using Crowdly_BE.Models.SchedulePeriods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.SchedulePeriods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using AutoMapper;

namespace Crowdly_BE.Controllers
{
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ISchedulePeriodsService _schedulePeriodsService;
        private readonly IMapper _mapper;

        public CheckoutController(IAuthorizationService authorizationService, ISchedulePeriodsService schedulePeriodsService, IMapper mapper)
        {
            _authorizationService = authorizationService;
            _schedulePeriodsService = schedulePeriodsService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<SchedulePeriod[]>> ConfirmCheckout(ConfirmCheckoutModel checkoutModel)
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var periods = checkoutModel.Items.Select(period => new Services.SchedulePeriods.Models.CreateSchedulePeriodModel()
            {
                VendorId = period.VendorId,
                StartDate = period.StartDate,
                EndDate = period.EndDate,
                BookedByUserId = userId
            }).ToArray();

            var createdPeriods = await _schedulePeriodsService.CreateMultipleSchedulePeriodsAsync(periods);

            return Ok(_mapper.Map<SchedulePeriod[]>(createdPeriods));
        }
    }
}
