using System;
using RailwayTicketing.Core.Interfaces;
using RailwayTicketing.Core.Models;

namespace RailwayTicketing.Services
{
	public class PricingService : IPricingService
	{
		private const double BaseRatePerKm = 0.50;

		public double CalculateFinalPrice(Trip trip, Passenger passenger)
		{
			double price = trip.DistanceKm * BaseRatePerKm;

			if (trip.Type == TicketType.RoundTrip)
			{
				price *= 2.0;
			}

			if (IsSaverTime(trip.TravelDate))
			{
				price *= 0.95; 
			}

			price = ApplyRailCardDiscount(price, passenger);

			if (!string.IsNullOrEmpty(trip.PromoCode))
			{
				if (trip.PromoCode == "SUMMER") price -= 10.0;     
				else if (trip.PromoCode == "STUDENT") price *= 0.85; 
			}

			return Math.Round(price, 2);
		}

		private bool IsSaverTime(DateTime time)
		{
			TimeSpan t = time.TimeOfDay;
			bool midDaySaver = t >= new TimeSpan(9, 30, 0) && t <= new TimeSpan(16, 0, 0);
			bool eveningSaver = t > new TimeSpan(19, 30, 0);

			return midDaySaver || eveningSaver;
		}

		private double ApplyRailCardDiscount(double currentPrice, Passenger p)
		{
			if (p.RailCard == RailCardType.Over60)
			{
				return currentPrice * 0.66;
			}

			if (p.RailCard == RailCardType.Family && p.Age < 16)
			{
				return currentPrice * 0.50;
			}

			if (p.Age < 16)
			{
				return currentPrice * 0.90;
			}

			return currentPrice;
		}
	}
}