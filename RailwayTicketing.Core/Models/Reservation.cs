using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayTicketing.Core.Models
{
    public class Reservation
    {
		public string? ReservationId { get; set; }
		public Trip? TripDetails { get; set; }
		public Passenger? PassengerDetails { get; set; }
		public double FinalPrice { get; set; }
		public bool IsCancelled { get; set; }
	}
}
