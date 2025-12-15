using System;
using System.Collections.Generic;
using System.Linq;
using RailwayTicketing.Core.Interfaces;
using RailwayTicketing.Core.Models;
using RailwayTicketing.Services;

namespace RailwayTicketing.App
{
	class Program
	{
		private static IUserService _userService;
		private static IPricingService _pricingService;
		private static IScheduleService _scheduleService; 

		static void Main(string[] args)
		{
			_userService = new UserService();
			_pricingService = new PricingService();
			_scheduleService = new ScheduleService(); 

			RunApplication();
		}

		static void RunApplication()
		{
			Console.WriteLine("--- BULGARIAN RAILWAY SYSTEM SETUP ---");

			Console.Write("Enter your Name: ");
			string name = Console.ReadLine();
			Console.Write("Enter your Age: ");
			int age = int.Parse(Console.ReadLine());

			Console.WriteLine("Select RailCard (0: None, 1: Over60, 2: Family): ");
			int cardInt = int.Parse(Console.ReadLine());

			_userService.CreateProfile(name, "Sofia Center", age, (RailCardType)cardInt);

			bool running = true;
			while (running)
			{
				var currentUser = _userService.GetCurrentUser();
				Console.WriteLine($"\n--- Welcome {currentUser.Name} ---");
				Console.WriteLine("1. Search Trains & Book (Routes Available)");
				Console.WriteLine("2. View Reservation History");
				Console.WriteLine("3. Modify Reservation (Date/Time)"); 
				Console.WriteLine("4. Cancel a Reservation");
				Console.WriteLine("5. Exit");
				Console.Write("Select an option: ");

				string input = Console.ReadLine();
				try
				{
					switch (input)
					{
						case "1": HandleSearchAndBooking(); break;
						case "2": HandleHistory(); break;
						case "3": HandleModification(); break;
						case "4": HandleCancellation(); break;
						case "5": running = false; break;
						default: Console.WriteLine("Invalid option."); break;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error: {ex.Message}");
				}
			}
		}

		static void HandleSearchAndBooking()
		{
			Console.WriteLine("\n--- SEARCH TRAINS (Bulgarian Routes) ---");
			var routes = _scheduleService.GetRoutes();

			foreach (var r in routes)
			{
				Console.WriteLine($"Route #{r.Id}: {r.From} -> {r.To} ({r.DistanceKm} km)");
				Console.Write("   Departures: ");
				foreach (var t in r.DepartureTimes) Console.Write(t.ToString(@"hh\:mm") + " | ");
				Console.WriteLine("\n");
			}

			Console.Write("Enter Route ID to book: ");
			int routeId = int.Parse(Console.ReadLine());
			var selectedRoute = _scheduleService.GetRouteById(routeId);

			if (selectedRoute == null) { Console.WriteLine("Invalid Route."); return; }

			Console.Write("Enter specific Hour of departure (e.g. 7 for 07:30): ");
			int hour = int.Parse(Console.ReadLine());

			DateTime travelDate = DateTime.Today.AddHours(hour).AddMinutes(30);

			Console.Write("Trip Type (0: OneWay, 1: RoundTrip): ");
			int typeInt = int.Parse(Console.ReadLine());

			var currentUser = _userService.GetCurrentUser();
			Console.WriteLine($"Loading Profile for {currentUser.Name} (Age: {currentUser.Age})...");

			var passenger = new Passenger
			{
				Name = currentUser.Name,
				Age = currentUser.Age,
				RailCard = currentUser.RailCard
			};

			var trip = new Trip
			{
				StartCity = selectedRoute.From,
				Destination = selectedRoute.To,
				DistanceKm = selectedRoute.DistanceKm,
				TravelDate = travelDate,
				Type = (TicketType)typeInt
			};

			double finalPrice = _pricingService.CalculateFinalPrice(trip, passenger);

			Console.WriteLine($"\n--- TICKET SUMMARY ---");
			Console.WriteLine($"{trip.StartCity} -> {trip.Destination}");
			Console.WriteLine($"Time: {trip.TravelDate:HH:mm} | Type: {trip.Type}");
			Console.WriteLine($"Passenger: {passenger.Name} | Card: {passenger.RailCard}");
			Console.WriteLine($"Total Price: ${finalPrice:F2}");

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

		static void HandleModification()
		{
			Console.WriteLine("\n--- MODIFY RESERVATION ---");
			Console.Write("Enter Reservation ID: ");
			string id = Console.ReadLine();

			var history = _userService.GetHistory();
			var res = history.FirstOrDefault(r => r.ReservationId == id);

			if (res == null || res.IsCancelled)
			{
				Console.WriteLine("Error: Reservation not found or already cancelled.");
				return;
			}

			var route = _scheduleService.GetRoutes()
				.FirstOrDefault(r => r.From == res.TripDetails.StartCity && r.To == res.TripDetails.Destination);

			if (route == null)
			{
				Console.WriteLine("Error: Original route schedule not found.");
				return;
			}

			Console.WriteLine($"\nSelect new time for {route.From} -> {route.To}:");
			for (int i = 0; i < route.DepartureTimes.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {route.DepartureTimes[i]:hh\\:mm}");
			}

			Console.Write("Select an option number: ");
			if (int.TryParse(Console.ReadLine(), out int timeIndex) && timeIndex > 0 && timeIndex <= route.DepartureTimes.Count)
			{
				TimeSpan newTime = route.DepartureTimes[timeIndex - 1];

				DateTime originalDate = res.TripDetails.TravelDate.Date; 
				DateTime newDateTime = originalDate.Add(newTime);

				bool success = _userService.ModifyReservation(id, newDateTime, _pricingService);

				if (success)
				{
					var updatedRes = _userService.GetHistory().First(r => r.ReservationId == id);
					Console.WriteLine($"\nSuccess! New Time: {updatedRes.TripDetails.TravelDate:HH:mm}");
					Console.WriteLine($"New Price: ${updatedRes.FinalPrice:F2} (Price updated based on new time)");
				}
				else
				{
					Console.WriteLine("Update failed.");
				}
			}
			else
			{
				Console.WriteLine("Invalid selection.");
			}
		}

		static void HandleHistory()
		{
			Console.WriteLine("\n--- RESERVATION HISTORY ---");
			List<Reservation> history = _userService.GetHistory();
			foreach (var res in history)
			{
				string status = res.IsCancelled ? "[CANCELLED]" : "[ACTIVE]";
				Console.WriteLine($"{status} ID: {res.ReservationId} | {res.TripDetails.StartCity}->{res.TripDetails.Destination} | Time: {res.TripDetails.TravelDate:HH:mm} | ${res.FinalPrice:F2}");
			}
		}

		static void HandleCancellation()
		{
			Console.WriteLine("\n--- CANCEL RESERVATION ---");
			Console.Write("Enter Reservation ID: ");
			string id = Console.ReadLine();
			bool success = _userService.CancelReservation(id);
			Console.WriteLine(success ? "Cancelled successfully." : "ID not found.");
		}
	}
}