using NUnit.Framework;
using System;
using System.Linq; 
using RailwayTicketing.Core.Interfaces;
using RailwayTicketing.Core.Models;
using RailwayTicketing.Services;

namespace RailwayTicketing.Tests
{
	[TestFixture]
	public class SystemTests
	{
		private IPricingService _pricingService;
		private IUserService _userService;

		[SetUp]
		public void Setup()
		{
			_pricingService = new PricingService();
			_userService = new UserService();
		}

		[Test]
		public void CalculatePrice_SaverTime_AppliesDiscount()
		{
			var trip = new Trip
			{
				DistanceKm = 100,
				TravelDate = DateTime.Today.AddHours(10), 
				Type = TicketType.OneWay
			};
			var passenger = new Passenger { Age = 30, RailCard = RailCardType.None };

			double price = _pricingService.CalculateFinalPrice(trip, passenger);

			Assert.AreEqual(47.50, price, 0.01);
		}

		[Test]
		public void CalculatePrice_SeniorCitizen_Applies34Percent()
		{
			var trip = new Trip
			{
				DistanceKm = 100,
				TravelDate = DateTime.Today.AddHours(8), 
				Type = TicketType.OneWay
			};
			var passenger = new Passenger { Age = 65, RailCard = RailCardType.Over60 };

			double price = _pricingService.CalculateFinalPrice(trip, passenger);

			Assert.AreEqual(33.00, price, 0.01);
		}

		[Test]
		public void CalculatePrice_RoundTrip_DoublesPrice()
		{
			var trip = new Trip
			{
				DistanceKm = 100,
				TravelDate = DateTime.Today.AddHours(8), 
				Type = TicketType.RoundTrip
			};
			var passenger = new Passenger { Age = 30, RailCard = RailCardType.None };

			double price = _pricingService.CalculateFinalPrice(trip, passenger);

			Assert.AreEqual(100.00, price, 0.01);
		}

		[Test]
		public void UserService_CanCreateAndCancelReservation()
		{
			_userService.CreateProfile("TestUser", "Address", 30, RailCardType.None);

			var res = new Reservation { ReservationId = "123", IsCancelled = false };
			_userService.AddReservation(res);

			Assert.AreEqual(1, _userService.GetHistory().Count);

			bool cancelled = _userService.CancelReservation("123");

			Assert.IsTrue(cancelled);
			Assert.IsTrue(_userService.GetHistory()[0].IsCancelled);
		}

		[Test]
		public void UserService_ModifyReservation_UpdatesPrice()
		{
			_userService.CreateProfile("TestUser", "Address", 30, RailCardType.None);

			var trip = new Trip
			{
				DistanceKm = 100,
				TravelDate = DateTime.Today.AddHours(10), 
				Type = TicketType.OneWay
			};

			double initialPrice = _pricingService.CalculateFinalPrice(trip, new Passenger { Age = 30 });

			var res = new Reservation
			{
				ReservationId = "999",
				TripDetails = trip,
				PassengerDetails = new Passenger { Age = 30, RailCard = RailCardType.None },
				FinalPrice = initialPrice,
				IsCancelled = false
			};

			_userService.AddReservation(res);

			DateTime newDate = DateTime.Today.AddHours(8);
			bool modified = _userService.ModifyReservation("999", newDate, _pricingService);

			Assert.IsTrue(modified);

			var updatedRes = _userService.GetHistory().First(r => r.ReservationId == "999");

			Assert.AreEqual(50.00, updatedRes.FinalPrice, 0.01);
			Assert.AreEqual(newDate, updatedRes.TripDetails.TravelDate);
		}
	}
}