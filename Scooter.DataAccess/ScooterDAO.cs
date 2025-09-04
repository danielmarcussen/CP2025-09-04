using ScooterApp.Domain.Model;
using ScooterApp.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ScooterApp.DataAccess
{
	public class ScooterDAO(ScooterDbContext db) : IScooterDAO
	{
		private readonly ScooterDbContext _db = db;
		public void RebuildDatabase()
		{
			_db.Database.EnsureDeleted();
			_db.Database.Migrate();
		}
		// C
		public int CreateScooter(Scooter scooter)
		{
			_db.Scooter.Add(scooter);
			_db.SaveChanges();
			return scooter.Id;
		}
		// R
		public List<Scooter> ReadScootersAvailableWithBatteryAbove20()
		{
			return [.. _db.Scooter
				.Where(s => s.Status == Status.Available && s.BatteryCapacity > 20)
			];
		} // ✅ test written
		public List<Trip> ReadUserIdTripsOrderByStartTime(int userId)
		{
			// get every trip for a given user id, order by the trip's start
			return [.. _db.Trip
				.Where(t => t.UserId == userId)
				.OrderBy(t => t.StartTime)
			];
		}
		public List<Trip> ReadUserNameTripsOrderByStartTime(string name)
		{
			// get every trip for a given user name, order by the trip's start
			return [.. _db.Trip
				.Include(t => t.User)
				.Where(t => t.User!.Name == name)
				.OrderBy(t => t.StartTime)
			];
		}
		public double AveragePricePerKmAllTrips()
		{
			return _db.Trip
				.Average(t => t.CostNOK / t.DistanceKm);
		}
		public User? UserMostTrips()
		{
			int userId = _db.Trip
				.GroupBy(t => t.UserId) // g.Key
				.OrderByDescending(g => g.Count())
				.Select(g => g.Key)
				.FirstOrDefault();

			return userId == 0 ? null : _db.User.Find(userId);
		}
		public TripsDTO GetTripsStatistics()
		{
			return !_db.Trip.Any() 
				?
			new TripsDTO
			{
				TripCount = 0,
				LongestTrip = 0,
				ShortestTrip = 0,
				AverageDistance = 0
			}
			:
			new TripsDTO
			{
				TripCount = _db.Trip.Count(),
				LongestTrip = _db.Trip.Max(t => t.DistanceKm),
				ShortestTrip = _db.Trip.Min(t => t.DistanceKm),
				AverageDistance = _db.Trip.Average(t => t.DistanceKm)
			};
		}
		public double CoordinateDistance(double x, double y)
		{
			// TODO d = √[(x₂ - x₁)² + (y₂ - y₁)²]
			return -1D;
		}
		// U
		// D
	}
}
