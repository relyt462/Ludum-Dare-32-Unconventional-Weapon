using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackTheWorld
{
	internal abstract class Job
	{
		public string JobName { get; private set; }
		public int hoursPerDay { get; private set; }
		public int payPerDay { get; private set; }
		public int rquiredDaysPerWeek { get; private set; }
	}
	
	internal class GroceryCashier : Job
	{

	}
}
