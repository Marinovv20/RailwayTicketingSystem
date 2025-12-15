using System;
using System.Collections.Generic;
using System.Linq;
using RailwayTicketing.Core.Interfaces;
using RailwayTicketing.Core.Models;

namespace RailwayTicketing.Services
{
	public class ScheduleService : IScheduleService
	{
		private readonly List<RouteDefinition> _routes;

		public ScheduleService()
		{
			_routes = new List<RouteDefinition>
			{
				new RouteDefinition
				{
					Id = 1, From = "Sofia", To = "Plovdiv", DistanceKm = 150,
					DepartureTimes = new List<TimeSpan> { new TimeSpan(7, 30, 0), new TimeSpan(12, 0, 0), new TimeSpan(18, 0, 0) }
				},
				new RouteDefinition
				{
					Id = 2, From = "Burgas", To = "Sofia", DistanceKm = 380,
					DepartureTimes = new List<TimeSpan> { new TimeSpan(6, 0, 0), new TimeSpan(14, 30, 0) }
				},
				new RouteDefinition
				{
					Id = 3, From = "Varna", To = "Sofia", DistanceKm = 440,
					DepartureTimes = new List<TimeSpan> { new TimeSpan(8, 0, 0), new TimeSpan(20, 0, 0) }
				}
			};
		}

		public List<RouteDefinition> GetRoutes() => _routes;

		public RouteDefinition GetRouteById(int id) => _routes.FirstOrDefault(r => r.Id == id);
	}
}