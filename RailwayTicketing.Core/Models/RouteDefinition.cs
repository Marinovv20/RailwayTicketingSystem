using System;
using System.Collections.Generic;

namespace RailwayTicketing.Core.Models
{
	public class RouteDefinition
	{
		public int Id { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public double DistanceKm { get; set; }
		public List<TimeSpan> DepartureTimes { get; set; } 
	}
}