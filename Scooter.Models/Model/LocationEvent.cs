using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterApp.Domain.Model
{
	public class LocationEvent
	{
		public int Id { get; set; } // PK
		public DateTime Time {  get; set; }
		public double XCoordinate { get; set; }
		public double YCoordinate { get; set; }
		public int ScooterId { get; set; } // FK
		public Scooter? Scooter { get; set; } // Nav
	}
}
