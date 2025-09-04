using ScooterApp.Domain.Model;
using ScooterApp.Domain.Enum;

namespace CP2025_09_04.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
			Scooter scooter = new()
			{
				Brand = "Voi",
				BatteryCapacity = 100,
				Status = Status.Available
			};
		}
    }
}
