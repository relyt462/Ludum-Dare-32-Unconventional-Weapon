using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;
namespace HackTheWorld
{
	internal class CommandParser
	{
		public string[] ValidCommands { get; private set; }
		private Command[] validCmds;

		public CommandParser(Command[] validCommands)
		{
			validCmds = validCommands.ToArray();
			ValidCommands = new string[validCmds.Length];
			for(int i = 0; i < validCommands.Length; ++i)
			{
				ValidCommands[i] = validCmds[i].ToString();
			}
		}

		public Command ParseCommand(string cmd)
		{
			cmd = cmd.ToUpper();
			List<int> posCmds = new List<int>();
			List<string> cmds = cmd.Split(' ').ToList();
			StringBuilder output = new StringBuilder();
			if (ValidCommands.Contains(cmds[0]))
			{
				posCmds.Add(Array.IndexOf(ValidCommands, cmds[0]));
			}
			else
			{
				Regex cmdSearcher = new Regex("^" + cmds[0] + ".*$");
				for (int i = 0; i < ValidCommands.Length; i++)
				{
					if (cmdSearcher.IsMatch(ValidCommands[i]))
						posCmds.Add(i);
				}
			}


			if (posCmds.Count == 1)
			{
				return validCmds[posCmds[0]];
			}
			else if (posCmds.Count == 0)
				throw new CommandException("INVALID COMMAND TRY AGAIN");
			else
				throw new CommandException("AMBIGUOUS COMMAND TRY AGAIN");
		}

	}
	[Serializable]
	public class CommandException : Exception
	{
		public CommandException() { }
		public CommandException(string message) : base(message) { }
		public CommandException(string message, Exception inner) : base(message, inner) { }
		protected CommandException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
