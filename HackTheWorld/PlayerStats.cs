using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackTheWorld
{
	internal class PlayerStats
	{
		public string Name { get; private set; }
		public Alignment Align { get; private set; }
		public Contact[] GovCont { get; private set; }
		public Contact[] CrimCont { get; private set; }
		public Contact[] BusCont { get; private set; }

	}

	internal class Contact
	{
		public string ContactName { get; private set; }
	}

	internal class Skill
	{
		public string SkillName { get; private set; }
		public int SkillRank { get; private set; }
	}

	internal enum Alignment
	{
		WHITEHAT,
		BLACKHAT,
		GREYHAT
	}
}
