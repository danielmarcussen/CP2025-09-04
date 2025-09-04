using ScooterApp.Domain.Enum;

namespace ScooterApp.Domain.Model
{
	public class Scooter
	{
		public int Id { get; set; } // PK
		public string Brand { get; set; } = string.Empty;
		public int BatteryCapacity { get; set; }
		public Status? Status { get; set; }
		public ICollection<Trip>? Trips { get; set; } // Nav
	}
}
