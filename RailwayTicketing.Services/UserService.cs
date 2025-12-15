using System;
using System.Collections.Generic;
using System.Linq;
using RailwayTicketing.Core.Interfaces;
using RailwayTicketing.Core.Models;

namespace RailwayTicketing.Services
{
	public class UserService : IUserService
	{
		private List<UserProfile> _users = new List<UserProfile>();
		private UserProfile _currentUser;

		public void CreateProfile(string name, string address, int age, RailCardType cardType)
		{
			var user = new UserProfile
			{
				Id = Guid.NewGuid().ToString().Substring(0, 5),
				Name = name,
				Address = address,
				Age = age,
				RailCard = cardType
			};
			_users.Add(user);
			_currentUser = user;
		}

		public UserProfile GetCurrentUser() => _currentUser;

		public void AddReservation(Reservation reservation)
		{
			if (_currentUser != null) _currentUser.ReservationHistory.Add(reservation);
		}

		public bool CancelReservation(string reservationId)
		{
			var res = _currentUser?.ReservationHistory.FirstOrDefault(r => r.ReservationId == reservationId);
			if (res != null)
			{
				res.IsCancelled = true;
				return true;
			}
			return false;
		}

		public bool ModifyReservation(string reservationId, DateTime newDate, IPricingService pricingService)
		{
			var res = _currentUser?.ReservationHistory.FirstOrDefault(r => r.ReservationId == reservationId);
			if (res == null || res.IsCancelled) return false;

			res.TripDetails.TravelDate = newDate;

			double newPrice = pricingService.CalculateFinalPrice(res.TripDetails, res.PassengerDetails);

			res.FinalPrice = newPrice;
			return true;
		}

		public List<Reservation> GetHistory()
		{
			return _currentUser?.ReservationHistory ?? new List<Reservation>();
		}
	}
}