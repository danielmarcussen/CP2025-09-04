using ScooterApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterApp.DataAccess
{
	public interface IScooterDAO
	{
		// C
		// R
		List<Scooter> ReadScootersAvailableWithBatteryAbove20();
		/*List<Trip> ReadUserIdTripsOrderByStartTime(int userId);
		List<Trip> ReadUserNameTripsOrderByStartTime(string name);
		double AveragePricePerKmAllTrips();
		User? UserMostTrips();*/
		// U
		// D
	}
}

