using AutoMapper;
using Crowdly_BE.Authorization;
using Crowdly_BE.Models.Common;
using Crowdly_BE.Models.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Crowdly_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IEventsService _eventsService;
        private readonly IMapper _mapper;

        public EventsController(IAuthorizationService authorizationService, IEventsService eventsService, IMapper mapper)
        {
            _authorizationService = authorizationService;
            _eventsService = eventsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<Event[]>> GetEventsAsync([FromQuery] EventsFilters filters)
        {
            var events = await _eventsService.GetAllAsync(_mapper.Map<Services.Events.Models.EventsFilters>(filters));

            return Ok(_mapper.Map<DataPage<Event>>(events));
        }

        [Authorize]
        [HttpGet("Editable")]
        public async Task<ActionResult<Event[]>> GetEditableEventsAsync()
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var events = await _eventsService.GetByUser(userId);

            return Ok(_mapper.Map<Event[]>(events));
        }

        [Authorize]
        [HttpGet]
        [Route("{eventId}")]
        public async Task<ActionResult<EventDetails>> GetEventAsync([FromRoute] Guid eventId)
        {
            var eventModel = await _eventsService.GetByIdAsync(eventId);

            return Ok(await ConvertToEventResponseAsync(eventModel));
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Event>> CreateEventAsync([FromForm] CreateEventModel eventModel)
        {
            var eventId = Guid.NewGuid();

            var imageNames = UploadImages(eventId, eventModel.FormFiles);

            var createEventModel = _mapper.Map<Services.Events.Models.CreateEventModel>(eventModel);
            createEventModel.Images = imageNames;
            createEventModel.Id = eventId;
            createEventModel.CreatedByUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var newEvent = await _eventsService.CreateAsync(createEventModel);

            return Ok(_mapper.Map<Event>(newEvent));
        }

        [Authorize]
        [HttpPut]
        [Route("{eventId}")]
        public async Task<ActionResult> UpdateEventAsync([FromRoute] Guid eventId, [FromForm] UpdateEventModel eventModel)
        { 
            var existingEvent = await _eventsService.GetByIdAsync(eventId);
            if (existingEvent is null) return NotFound();

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, existingEvent, EventOperations.Update);
            if (!authorizationResult.Succeeded) return Unauthorized();

            var imageNames = UploadImages(eventId, eventModel.FormFiles);

            var updateEventModel = _mapper.Map<Services.Events.Models.UpdateEventModel>(eventModel);
            updateEventModel.Id = eventId;
            updateEventModel.Images = (eventModel.ExistingImages ?? new string[0]).Concat(imageNames).ToArray();

            var removedImages = await _eventsService.UpdateAsync(updateEventModel);

            DeleteImages(eventId, removedImages);

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        [Route("{eventId}")]
        public async Task<ActionResult> DeleteEventAsync([FromRoute] Guid eventId)
        {
            var existingEvent = await _eventsService.GetByIdAsync(eventId);
            if (existingEvent is null) return NotFound();

            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, existingEvent, EventOperations.Delete);

            if (!authorizationResult.Succeeded) return Unauthorized();

            var removedImages = await _eventsService.DeleteByIdAsync(eventId);

            DeleteImages(eventId, removedImages);

            return Ok();
        }

        private string[] UploadImages(Guid eventId, IFormFile[] formFiles)
        {
            if (formFiles is null) return new string[0];

            var imageNames = new List<string>();
            var directory = GetOrCreateEventDirectory(eventId);

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

        private void DeleteImages(Guid eventId, string[] imageNames)
        {
            var directory = GetOrCreateEventDirectory(eventId);

            foreach (var imageName in imageNames)
            {
                string path = Path.Combine(directory, imageName);
                System.IO.File.Delete(path);
            }
        }

        private string GetOrCreateEventDirectory(Guid eventId)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "events", eventId.ToString());
            Directory.CreateDirectory(directory);

            return directory;
        }

        private async Task<EventDetails> ConvertToEventResponseAsync(Services.Events.Models.EventDetails eventModel)
        {
            var eventResponse = _mapper.Map<EventDetails>(eventModel);
            eventResponse.IsEditable = false;

            if (User is null) return eventResponse;

            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var eventByUser = await _eventsService.GetByUser(userId);

            eventResponse.IsEditable = eventByUser.Select(v => v.Id).Contains(eventModel.Id);
            return eventResponse;
        }
    }
}
