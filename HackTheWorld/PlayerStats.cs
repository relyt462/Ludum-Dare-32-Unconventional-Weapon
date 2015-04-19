using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace HackTheWorld
{
	[Serializable]
	internal class Player
	{
		public string Name { get; private set; }
		public Difficulty Diff { get; set; }
		public Alignment Align { get; private set; }
		public List<Contact> GovCont { get; private set; }
		public List<Contact> CrimCont { get; private set; }
		public List<Contact> BusCont { get; private set; }
		public Skill[] Skills { get; private set; }
		private Random rng;

		public Player(string name, Difficulty d)
		{
			Name = name;
			Diff = d;
			Align = Alignment.GREYHAT;
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
			//throw new NotImplementedException();
		}

		internal void Save()
		{
			SaveFileDialog sf = new SaveFileDialog();
			if (sf.ShowDialog() == DialogResult.OK)
			{
				IFormatter f = new BinaryFormatter();
				Stream s = new FileStream(sf.FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
				f.Serialize(s, this);
				s.Close();
			}
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
