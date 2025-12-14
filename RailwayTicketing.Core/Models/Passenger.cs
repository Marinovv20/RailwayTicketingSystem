using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailwayTicketing.Core.Models
{
    public class Passenger
    {
		public string? Name { get; set; }
		public int Age { get; set; }
		public RailCardType RailCard { get; set; }
	}
}
