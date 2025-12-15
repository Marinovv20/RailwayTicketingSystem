using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayTicketing.Core.Models
{
    public class UserProfile
    {
		public string? Id { get; set; }
		public string? Name { get; set; }
		public string? Address { get; set; }
		public int Age { get; set; }
		public RailCardType RailCard { get; set; }
		public List<Reservation> ReservationHistory { get; set; } = new List<Reservation>();
	}
}
