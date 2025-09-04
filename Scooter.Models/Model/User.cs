using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterApp.Domain.Model
{
	public class User
	{
		public int Id { get; set; } // PK
		public string Name { get; set; } = string.Empty;
		public string PhoneNumber { get; set; } = string.Empty;
		public ICollection<Trip>? Trips { get; set; } // Nav
	}
}
