using RailwayTicketing.Core.Models;

namespace RailwayTicketing.Core.Interfaces
{
    public interface IPricingService
    {
		double CalculateFinalPrice(Trip trip, Passenger passenger);
	}
}
