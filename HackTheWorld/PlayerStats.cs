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
		public List<Contact> GovCont { get; private set; }
		public List<Contact> CrimCont { get; private set; }
		public List<Contact> BusCont { get; private set; }
		public Skill[] Skills { get; private set; }
		private Random rng;

		public PlayerStats(string name, string Alignment)
		{
			Name = name;
			Align = (Alignment)Enum.Parse(typeof(Alignment), Alignment);
			GovCont = new List<Contact>();
			CrimCont = new List<Contact>();
			BusCont = new List<Contact>();
			initSkills();
			rng = new Random();
		}

		public bool testSkill(int targetNum, string skill)
		{
			int skillIndex = Array.IndexOf(Skills, skill);
			if(skillIndex < 0)
			{
				throw new SkillNotFoundException("ERROR: " + skill + " Not found");
			}
			int roll = 0;
			for (int i = 0; i < 3; i++ )
			{
				roll += rng.Next(1,7);
			}
			if(roll + Skills[skillIndex].SkillRank > targetNum)
			{
				return true;
			}

			return false;
		}

		private void initSkills()
		{
			throw new NotImplementedException();
		}
	}

	[Serializable]
	public class SkillNotFoundException : Exception
	{
		public SkillNotFoundException() { }
		public SkillNotFoundException(string message) : base(message) { }
		public SkillNotFoundException(string message, Exception inner) : base(message, inner) { }
		protected SkillNotFoundException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}

	internal class Contact
	{
		public string ContactName { get; private set; }
		public int ContractRank { get; private set; }
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
