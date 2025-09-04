using Moq;
using ScooterApp.DataAccess;
using ScooterApp.Domain.Enum;
using ScooterApp.Domain.Model;

namespace ScooterApp.Test
{
	public class ScooterLogicTests
	{
		private ScooterDbContext _db;
		private ScooterDAO _scooterDAO;
		private readonly Mock<IScooterDAO> _mockScooterDAO;
		public ScooterLogicTests()
		{
			_mockScooterDAO = new(MockBehavior.Strict);
		}

		[SetUp]
		public void Setup()
		{
			_db = new ScooterDbContext();
			_scooterDAO = new(_db);
			_scooterDAO.RebuildDatabase();

			User userWithMostTrips = new()
			{
				Name = "Dávid Keira",
				PhoneNumber = "47654321"
			};

			// setup data
			List<Scooter> scooters =
			[
				new Scooter
				{
					Brand = "Voi",
					BatteryCapacity = 20,
					Status = Status.Available,
					Trips = [
						new Trip()
						{
							StartTime = new DateTime(2025, 9, 2, 10, 0, 0, DateTimeKind.Utc),
							EndTime = new DateTime(2025, 9, 2, 10, 30, 0, DateTimeKind.Utc),
							DistanceKm = 10,
							CostNOK = 5,
							User = new()
							{
								Name = "Hilaria Wayland",
								PhoneNumber = "98765432",
							}
						},
						new Trip()
						{
							StartTime = new DateTime(2025, 9, 3, 11, 20, 0, DateTimeKind.Utc),
							EndTime = new DateTime(2025, 9, 3, 11, 50, 0, DateTimeKind.Utc),
							DistanceKm = 5,
							CostNOK = 15,
							User = userWithMostTrips
						}
					]
				},
				new Scooter
				{
					Brand = "Tier",
					BatteryCapacity = 80,
					Status = Status.Available,
					Trips = [
						new Trip()
						{
							StartTime = new DateTime(2025, 9, 4, 9, 0, 0, DateTimeKind.Utc),
							EndTime = new DateTime(2025, 9, 4, 9, 25, 0, DateTimeKind.Utc),
							DistanceKm = 3.2,
							CostNOK = 12,
							User = new User
							{
								Name = "Lilli Íñigo",
								PhoneNumber = "92345678"
							}
						},
						new Trip()
						{
							StartTime = new DateTime(2025, 9, 5, 14, 10, 0, DateTimeKind.Utc),
							EndTime = new DateTime(2025, 9, 5, 14, 45, 0, DateTimeKind.Utc),
							DistanceKm = 5.1,
							CostNOK = 18,
							User = userWithMostTrips
						}
					]
				}
			];

			// cascade insert
			foreach (Scooter s in scooters)
			{
				_db.Scooter.Add(s);
			}
			_db.SaveChanges();
		}

		[TearDown]
		public void TearDown()
		{
			_db.Dispose();
		}
		[Test]
		public void ReadScootersAvailableWithBatteryAbove20_OneScooter_DataCorrect()
		{
			List<Scooter> res = _scooterDAO.ReadScootersAvailableWithBatteryAbove20();

			Assert.That(res.Count, Is.EqualTo(1));

			Scooter scooter = res[0];

			// scooter props
			Assert.That(scooter.Brand, Is.EqualTo("Tier"));
			Assert.That(scooter.BatteryCapacity, Is.EqualTo(80));
			Assert.That(scooter.Status, Is.EqualTo(Status.Available));

			// trips count
			Assert.That(scooter.Trips, Is.Not.Null);
			Assert.That(scooter.Trips!.Count, Is.EqualTo(2));

			// first trip details
			Trip trip1 = scooter.Trips.ElementAt(0);
			Assert.That(trip1.DistanceKm, Is.EqualTo(3.2));
			Assert.That(trip1.CostNOK, Is.EqualTo(12));
			Assert.That(trip1.User!.Name, Is.EqualTo("Lilli Íñigo"));
			Assert.That(trip1.User.PhoneNumber, Is.EqualTo("92345678"));

			// second trip details
			Trip trip2 = scooter.Trips.ElementAt(1);
			Assert.That(trip2.DistanceKm, Is.EqualTo(5.1));
			Assert.That(trip2.CostNOK, Is.EqualTo(18));
			Assert.That(trip2.User!.Name, Is.EqualTo("Dávid Keira"));
			Assert.That(trip2.User.PhoneNumber, Is.EqualTo("47654321"));
		}
		[Test]
		public void ReadUserIdTripsOrderByStartTime_OneUser_CorrectName()
		{
			List<Trip> res = _scooterDAO.ReadUserNameTripsOrderByStartTime("Dávid Keira");

			Assert.That(res.Count, Is.EqualTo(2));

			Assert.That(res[0].StartTime, Is.LessThan(res[1].StartTime));

			foreach (Trip t in res)
			{
				Assert.That(t.User, Is.Not.Null);
				Assert.That(t.User.Name, Is.EqualTo("Dávid Keira"));
				Assert.That(t.User.PhoneNumber, Is.EqualTo("47654321"));
			}

			// trip details
			Assert.That(res[0].StartTime, Is.EqualTo(new DateTime(2025, 9, 3, 11, 20, 0, DateTimeKind.Utc)));
			Assert.That(res[1].StartTime, Is.EqualTo(new DateTime(2025, 9, 5, 14, 10, 0, DateTimeKind.Utc)));
		}
		[Test]
		public void AveragePricePerKmAllTrips_CalculatesCorrectly()
		{
			double res = _scooterDAO.AveragePricePerKmAllTrips();

			double expected = 
				(
					5.0 / 10.0 
					+ 15.0 / 5.0 
					+ 12.0 / 3.2 
					+ 18.0 / 5.1
				) 
				/ 4.0;

			Assert.That(res, Is.EqualTo(expected));
		}
		[Test]
		public void UserMostTrips_ReturnsCorrectUser()
		{
			User? res = _scooterDAO.UserMostTrips();

			Assert.That(res, Is.Not.Null);
			Assert.That(res.Name, Is.EqualTo("Dávid Keira"));
			Assert.That(res.PhoneNumber, Is.EqualTo("47654321"));
		}

		// mocking
		[Test]
		public void ReadScootersAvailableWithBatteryAbove20_MockScooter_DataCorrect()
		{
			Scooter expectedScooter = new()
			{
				Brand = "Tier",
				BatteryCapacity = 80,
				Status = Status.Available
			};

			_mockScooterDAO.Setup(s => s.ReadScootersAvailableWithBatteryAbove20())
				.Returns([expectedScooter]);

			List<Scooter> res = _mockScooterDAO.Object.ReadScootersAvailableWithBatteryAbove20();

			Assert.That(res, Is.Not.Null);
			Assert.That(res.Count, Is.EqualTo(1));

			Scooter scooter = res[0];
			Assert.That(scooter.Brand, Is.EqualTo("Tier"));
			Assert.That(scooter.BatteryCapacity, Is.EqualTo(80));
			Assert.That(scooter.Status, Is.EqualTo(Status.Available));
		}

		// aggr
		[Test]
		public void GetTripsStats_AggregatesCorrect()
		{
			TripsDTO stats = _scooterDAO.GetTripsStatistics();

			Assert.That(stats, Is.Not.Null);

			// 10, 5, 3.2, 5.1
			int expectedCount = 4;
			double expectedLongest = 10.0;
			double expectedShortest = 3.2;
			double expectedAverage = (10 + 5 + 3.2 + 5.1) / 4.0;

			Assert.That(stats.TripCount, Is.EqualTo(expectedCount));
			Assert.That(stats.LongestTrip, Is.EqualTo(expectedLongest).Within(0.0001));
			Assert.That(stats.ShortestTrip, Is.EqualTo(expectedShortest).Within(0.0001));
			Assert.That(stats.AverageDistance, Is.EqualTo(expectedAverage).Within(0.0001));
		}

		//locationevent

	}
}
