using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayTicketing.Core.Models
{
    public class Trip
    {
		public string? StartCity { get; set; }
		public string? Destination { get; set; }
		public double DistanceKm { get; set; }
		public DateTime TravelDate { get; set; } 
		public TicketType Type { get; set; }
		public string? PromoCode { get; set; } 
	}
}
