using RailwayTicketing.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayTicketing.Core.Interfaces
{
    public interface IUserService
    {
		void CreateProfile(string name, string address);
		UserProfile GetCurrentUser();
		void AddReservation(Reservation reservation);
		bool CancelReservation(string reservationId);
		List<Reservation> GetHistory();
	}
}
