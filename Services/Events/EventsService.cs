using AutoMapper;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services.Common.Models;
using Services.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events
{
    public class EventsService : IEventsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        private const int PAGE_SIZE = 5;

        public EventsService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Event> CreateAsync(CreateEventModel createEventModel)
        {
            var dbEvent = _mapper.Map<DataAccess.Models.Event>(createEventModel);

            _dbContext.Events.Add(dbEvent);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Event>(dbEvent);
        }

        public async Task<string[]> DeleteByIdAsync(Guid id)
        {
            var dbEvent = _dbContext.Events.FirstOrDefault(v => v.Id == id);

            _dbContext.Events.Remove(dbEvent);
            await _dbContext.SaveChangesAsync();

            return dbEvent.Images;
        }

        public async Task<DataPage<Event>> GetAllAsync(EventsFilters filters)
        {
            var dbEventsQuery = _dbContext.Events.AsQueryable();

            if (filters.CityId.HasValue)
                dbEventsQuery = dbEventsQuery.Where(ev => ev.CityId == filters.CityId.Value);

            if (filters.Category != EventCategoryType.None)
            {
                var category = _mapper.Map<DataAccess.Models.EventCategoryType>(filters.Category);
                dbEventsQuery = dbEventsQuery.Where(ev => ev.Category == category);
            }

            if (filters.PriceMin.HasValue)
                dbEventsQuery = dbEventsQuery.Where(ev => ev.Price >= filters.PriceMin.Value);
            if (filters.PriceMax.HasValue)
                dbEventsQuery = dbEventsQuery.Where(ev => ev.Price <= filters.PriceMax.Value);

            dbEventsQuery = dbEventsQuery.Where(ev => ev.StartDateTime >= (filters.AfterDateTime ?? DateTime.Now).Date);

            dbEventsQuery = dbEventsQuery.OrderBy(ev => ev.StartDateTime);

            if (filters.Skip > 0)
                dbEventsQuery = dbEventsQuery.Skip(filters.Skip.Value);

            var data = await dbEventsQuery.Take(PAGE_SIZE + 1).ToArrayAsync();

            var page = new DataPage<Event>()
            {
                Data = _mapper.Map<Event[]>(data.Take(PAGE_SIZE)),
                HasMore = data.Length > PAGE_SIZE
            };

            return page;
        }

        public async Task<EventDetails> GetByIdAsync(Guid id)
        {
            var dbEvent = await _dbContext.Events.FirstOrDefaultAsync(v => v.Id == id);
            return _mapper.Map<EventDetails>(dbEvent);
        }

        public async Task<Event[]> GetByUser(Guid userId)
        {
            var dbEvents = await _dbContext.Events.Where(v => v.CreatedByUserId == userId).ToArrayAsync();
            return _mapper.Map<Event[]>(dbEvents);
        }

        public async Task<string[]> UpdateAsync(UpdateEventModel updateEventModel)
        {
            var dbEvent = _dbContext.Events.FirstOrDefault(v => v.Id == updateEventModel.Id);

            var removedImages = dbEvent.Images.Where(image => !updateEventModel.Images.Contains(image)).ToArray();

            _mapper.Map(updateEventModel, dbEvent);

            _dbContext.Events.Update(dbEvent);
            await _dbContext.SaveChangesAsync();

            return removedImages;
        }
    }
}
