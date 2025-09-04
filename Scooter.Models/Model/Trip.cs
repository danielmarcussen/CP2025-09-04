using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterApp.Domain.Model
{
	public class Trip
	{
		public int Id { get; set; } // PK
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public double DistanceKm { get; set; }
		public double CostNOK { get; set; }
		public int UserId { get; set; } // FK
		public User? User { get; set; } // Nav
		public int ScooterId { get; set; } // FK
		public Scooter? Scooter { get; set; } // Nav
	}
}
