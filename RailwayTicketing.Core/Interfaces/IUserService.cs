using RailwayTicketing.Core.Interfaces;
using RailwayTicketing.Core.Models;

public interface IUserService
{
	void CreateProfile(string name, string address, int age, RailCardType cardType);
	UserProfile GetCurrentUser();
	void AddReservation(Reservation reservation);
	bool CancelReservation(string reservationId);
	bool ModifyReservation(string reservationId, DateTime newDate, IPricingService pricingService);
	List<Reservation> GetHistory();
}