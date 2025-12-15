using RailwayTicketing.Core.Models;
using System.Collections.Generic;

namespace RailwayTicketing.Core.Interfaces
{
	public interface IScheduleService
	{
		List<RouteDefinition> GetRoutes();
		RouteDefinition GetRouteById(int id);
	}
}