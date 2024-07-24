﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemName.Models
{
    public class TO
    {
        [Key]
        public int customerID { get; set; } //gets and sets 
        
        public int? PatronId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public int? PostCode { get; set; }

        public string? Suburb { get; set; }

        public string? State { get; set; }
        public string? CardNumber { get; set; }
        public string? CardOwner { get; set; }
        public string? Expiry { get; set; }

        public int? CVV { get; set; }

    }
}
