using System;
using System.Collections.Generic;
using System.Net.Sockets;
using RailwayTicketing.Core.Interfaces;
using RailwayTicketing.Core.Models;
using RailwayTicketing.Services;

namespace RailwayTicketing.App
{
	class Program
	{
		private static IUserService _userService;
		private static IPricingService _pricingService;

		static void Main(string[] args)
		{
			_userService = new UserService();
			_pricingService = new PricingService();

			RunApplication();
		}

		static void RunApplication()
		{
			Console.WriteLine("--- RAILWAY SYSTEM SETUP ---");
			Console.Write("Enter your Name to create a profile: ");
			string name = Console.ReadLine();
			_userService.CreateProfile(name, "Default Address");

			bool running = true;
			while (running)
			{
				var currentUser = _userService.GetCurrentUser();
				Console.WriteLine($"\n--- Welcome {currentUser.Name} ---");
				Console.WriteLine("1. Search & Book Ticket");
				Console.WriteLine("2. View Reservation History");
				Console.WriteLine("3. Cancel a Reservation");
				Console.WriteLine("4. Exit");
				Console.Write("Select an option: ");

				string input = Console.ReadLine();
				try
				{
					switch (input)
					{
						case "1":
							HandleBooking();
							break;
						case "2":
							HandleHistory();
							break;
						case "3":
							HandleCancellation();
							break;
						case "4":
							running = false;
							break;
						default:
							Console.WriteLine("Invalid option.");
							break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error: {ex.Message}");
				}
			}
		}

		static void HandleBooking()
		{
			Console.WriteLine("\n--- NEW BOOKING ---");
			Console.Write("Destination: ");
			string dest = Console.ReadLine();

			Console.Write("Distance (km): ");
			double dist = double.Parse(Console.ReadLine());

			Console.Write("Hour of Travel (0-23): ");
			int hour = int.Parse(Console.ReadLine());
			DateTime travelDate = DateTime.Today.AddHours(hour);

			Console.Write("Trip Type (0: OneWay, 1: RoundTrip): ");
			int typeInt = int.Parse(Console.ReadLine());

			Console.Write("RailCard (0: None, 1: Over60, 2: Family): ");
			int cardInt = int.Parse(Console.ReadLine());

			Console.Write("Promo Code (Optional): ");
			string promo = Console.ReadLine();

			var trip = new Trip
			{
				Destination = dest,
				DistanceKm = dist,
				TravelDate = travelDate,
				Type = (TicketType)typeInt,
				PromoCode = promo
			};

			var passenger = new Passenger
			{
				Name = _userService.GetCurrentUser().Name,
				Age = 30, 
				RailCard = (RailCardType)cardInt
			};

			double finalPrice = _pricingService.CalculateFinalPrice(trip, passenger);

			Console.WriteLine($"\nCalculated Price: ${finalPrice}");
			Console.Write("Confirm Booking? (y/n): ");

			if (Console.ReadLine().ToLower() == "y")
			{
				var reservation = new Reservation
				{
					ReservationId = new Random().Next(1000, 9999).ToString(),
					TripDetails = trip,
					PassengerDetails = passenger,
					FinalPrice = finalPrice,
					IsCancelled = false
				};

				_userService.AddReservation(reservation);
				Console.WriteLine("Booking Confirmed!");
			}
		}

		static void HandleHistory()
		{
			Console.WriteLine("\n--- RESERVATION HISTORY ---");
			List<Reservation> history = _userService.GetHistory();

			if (history.Count == 0)
			{
				Console.WriteLine("No reservations found.");
			}
			else
			{
				foreach (var res in history)
				{
					string status = res.IsCancelled ? "[CANCELLED]" : "[ACTIVE]";
					Console.WriteLine($"{status} ID: {res.ReservationId} | To: {res.TripDetails.Destination} | ${res.FinalPrice}");
				}
			}
		}

		static void HandleCancellation()
		{
			Console.WriteLine("\n--- CANCEL RESERVATION ---");
			Console.Write("Enter Reservation ID: ");
			string id = Console.ReadLine();

			bool success = _userService.CancelReservation(id);

			if (success)
				Console.WriteLine("Reservation cancelled successfully.");
			else
				Console.WriteLine("Reservation ID not found.");
		}
	}
}