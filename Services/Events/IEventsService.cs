using Services.Common.Models;
using Services.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Events
{
    public interface IEventsService
    {
        Task<DataPage<Event>> GetAllAsync(EventsFilters filters);
        Task<Event[]> GetByUser(Guid userId);
        Task<EventDetails> GetByIdAsync(Guid id);
        Task<Event> CreateAsync(CreateEventModel createEventModel);
        Task<string[]> UpdateAsync(UpdateEventModel updateEventModel);
        Task<string[]> DeleteByIdAsync(Guid id);
    }
}
