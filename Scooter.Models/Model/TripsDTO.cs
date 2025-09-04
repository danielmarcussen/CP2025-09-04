using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterApp.Domain.Model
{
	public class TripsDTO
	{
		public int TripCount { get; set; }
		public double LongestTrip { get; set; }
		public double ShortestTrip { get; set; }
		public double AverageDistance { get; set; }
	}
}
