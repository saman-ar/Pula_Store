using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.StateAndCity
{
	public class State
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}

	public class City
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string StateId { get; set; }
	}

}
