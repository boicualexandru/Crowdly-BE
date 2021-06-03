using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Models.Checkout
{
    public class ConfirmCheckoutModel
    {
        public ConfirmCheckoutItem[] Items { get; set; }
    }

    public class ConfirmCheckoutItem
    {
        [Required]
        public Guid VendorId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
