using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Models.Tickets
{
    public class ValidateTicketRequest
    {
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public Guid TicketId { get; set; }
    }

    public class ValidateTicketResponse
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }
    }
}
