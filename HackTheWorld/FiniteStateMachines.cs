using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;
namespace HackTheWorld
{
	class FiniteStateMachine
	{
		private State currentState;
		private ConsoleStyleLabel outputLabel;
		private Command[] defCommands;
		private CommandParser defParser;
		public FiniteStateMachine(ref ConsoleStyleLabel label)
		{
			outputLabel = label;
			defCommands = new Command[] { Command.OPTION, Command.FONT, Command.SIZE, Command.EXIT };
			defParser = new CommandParser(defCommands);
			currentState = new StartScreen(ref outputLabel);
		}

		public int ProcessCommand(string cmd)
		{
			try
			{
				switch (defParser.ParseCommand(cmd))
				{
					case Command.EXIT:
						if (cmd.Length == 1)
							return 0;
						return -1;
					case Command.OPTION:
						StringBuilder output = new StringBuilder();
						return options(cmd, ref output);
					default:
						return 0;
				}
			}
			catch (CommandException)
			{
				return Convert.ToInt32(currentState.ProcessCommand(cmd, ref currentState));
			}

		}

		private int options(string cmd, ref StringBuilder output)
		{
			List<string> parameters = cmd.Split(' ').ToList();
			if (parameters.Count == 4 && defCommands.Contains(defParser.ParseCommand(parameters[1])) && defCommands.Contains(defParser.ParseCommand(parameters[2])))
			{
				switch (defParser.ParseCommand(parameters[1]))
				{
					case Command.FONT:
						{
							switch (defParser.ParseCommand(parameters[2]))
							{
								case Command.SIZE:
									outputLabel.ChangeFont(new Font(outputLabel.Font.FontFamily, float.Parse(parameters[3])));
									outputLabel.Clear();
									return 1;
							}
						} break;
				}
			}
			return 0;
		}
	}

	abstract class State
	{
		private Command[] validCommands;
		private CommandParser parser;
		private ConsoleStyleLabel outputLabel;
		public abstract void EnterState();
		public abstract bool ProcessCommand(string cmd, ref State s);
		public abstract void ExitState();
	}

	internal class StartScreen : State
	{
		private ConsoleStyleLabel outputLabel;
		private CommandParser parser;
		private Command[] validCommands;

		public StartScreen(ref ConsoleStyleLabel label)
		{
			outputLabel = label;
			validCommands = new Command[] {Command.NEW, Command.CONTINUE, Command.HELP};
			parser = new CommandParser(validCommands);
			EnterState();
		}

		public override bool ProcessCommand(string command, ref State s)
		{
			Command cmd;
			try
			{
				 cmd = parser.ParseCommand(command);
			}
			catch(CommandException e)
			{
				outputLabel.writeLine(e.Message);
				return false;
			}

			switch(cmd)
			{
				case Command.NEW:
					ExitState();
					s = new NewGame(ref outputLabel);
					break;
				case Command.CONTINUE:
					ExitState();
					s = new MainGame(ref outputLabel, Player.Load());
					break;
				case Command.HELP:
					displayHelp();
					break;
			}
			return false;
		}
		public override void EnterState()
		{
			showStartMessage();
		}
		public override void ExitState()
		{

		}

		private void showStartMessage()
		{
			string[] lines = new String[]{"HACK THE WORLD!",
										"Made by Tyler Whittin",
										"For Ludum Dare 32-An Unconventional Weapon", 
										"SELECT AN OPTION FROM BELOW:",
										"NEW GAME", 
										"CONTINUE",
										"HELP",
										"EXIT"};
			const int screenWidth = 55;
			StringBuilder output = new StringBuilder();
			for(int i = 0; i < 3;i++)
			{
				output.Append(' ', screenWidth - lines[i].Length);
				output.Insert(screenWidth * i + (screenWidth - lines[i].Length) / 2, lines[i]);
				output.Append("\n");
			}
			output.Append('-', screenWidth);
			output.Append("\n");
			output.AppendLine(lines[3]);
			output.Append("\n");
			for (int i = 4; i < lines.Length; i++)
			{
				output.AppendLine(lines[i]);
			}
			outputLabel.writeLine(output.ToString());
		}
		private void displayHelp()
		{
			outputLabel.writeLine("\nIn order to enter select an option type in one of the above options");
		}

	}

	internal class NewGame : State
	{
		private Command[] validCommands;
		private CommandParser parser;
		private ConsoleStyleLabel outputLabel;
		private string name;
		public NewGame(ref ConsoleStyleLabel label)
		{
			outputLabel = label;
			validCommands = new Command[] {Command.YES, Command.NO};
			parser = new CommandParser(validCommands);
			EnterState();
		}
		
		public override void EnterState()
		{
			StringBuilder output = new StringBuilder();
			output.Append("ENTER A NAME: ");
			outputLabel.writeLine(output.ToString());
		}

		public override bool ProcessCommand(string cmd, ref State s)
		{
			if(name == null)
			{
				name = cmd;
				StringBuilder output = new StringBuilder();
				output.AppendFormat("IS {0} CORRECT? (Y/N)", name);
				outputLabel.writeLine(output.ToString());
				return true;
			}
			else
			{
				Command c;
				try
				{
					c = parser.ParseCommand(cmd);
				}catch(CommandException e)
				{
					outputLabel.writeLine(e.Message);
					return false;
				}

				switch(c)
				{
					case Command.YES:
						s = new ChooseDiff(ref outputLabel, name);
						break;
					case Command.NO:
						EnterState();
						name = null;
						break;
				}
				return false;
			}


		}
		public override void ExitState()
		{
			throw new NotImplementedException();
		}
	}

	internal class ChooseDiff : State
	{
		private Command[] validCommands;
		private CommandParser parser;
		private ConsoleStyleLabel outputLabel;
		private string name;
		public ChooseDiff(ref ConsoleStyleLabel label, string name)
		{
			this.name = name;
			outputLabel = label;
			validCommands = new Command[] { Command.EASY, Command.MEDIUM, Command.HARD};
			parser = new CommandParser(validCommands);
			EnterState();
		}

		public override void EnterState()
		{
			StringBuilder output = new StringBuilder();
			output.AppendLine("CHOOSE YOUR DIFFICULTY: ");
			output.AppendLine("EASY - SCRIPT KIDDIE");
			output.AppendLine("MEDIUM - ALT MEDIUM");
			output.AppendLine("HARD - L33T H@XOR");
			outputLabel.writeLine(output.ToString());
		}

		public override bool ProcessCommand(string cmd, ref State s)
		{
			Difficulty diff;
			if(Regex.IsMatch("SCRIPT KIDDIE", "^" + cmd + ".*$",RegexOptions.Multiline | RegexOptions.IgnoreCase))
			{
				diff = Difficulty.EASY;
				s = new MainGame(ref outputLabel, new Player(name, diff));
			}
			else if (Regex.IsMatch("ALT MEDIUM", "^" + cmd + ".*$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
			{
				diff = Difficulty.MEDIUM;
				s = new MainGame(ref outputLabel, new Player(name, diff));
			}
			else if (Regex.IsMatch("L33t H@XOR", "^" + cmd + ".*$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
			{
				diff = Difficulty.HARD;
				s = new MainGame(ref outputLabel, new Player(name, diff));
			}
			else
			{
				Command c;
				try
				{
					c = parser.ParseCommand(cmd);
				}catch(CommandException e)
				{
					outputLabel.writeLine(e.Message);
					return false;
				}

				switch(c)
				{
					case Command.EASY:
						diff = Difficulty.EASY;
						s = new MainGame(ref outputLabel, new Player(name, diff));
						break;
					case Command.MEDIUM:
						diff = Difficulty.MEDIUM;
						s = new MainGame(ref outputLabel, new Player(name, diff));
						break;
					case Command.HARD:
						diff = Difficulty.HARD;
						s = new MainGame(ref outputLabel, new Player(name, diff));
						break;
				}
				return false;
			}
			return false;
		}
		public override void ExitState()
		{
			throw new NotImplementedException();
		}
	}
	internal enum Difficulty
	{
		EASY,
		MEDIUM,
		HARD
	}


	internal class MainGame : State
	{
		private Command[] validCommands;
		private CommandParser parser;
		private ConsoleStyleLabel outputLabel;
		private Player player;
		
		public MainGame(ref ConsoleStyleLabel label, Player p)
		{
			outputLabel = label;
			validCommands = new Command[] {Command.STATISTICS, Command.VIEW, Command.HELP, Command.SAVE};
			parser = new CommandParser(validCommands);
			player = p;
			EnterState();
		}
		
		public override void EnterState()
		{
			outputLabel.writeLine("Entered Main game");
		}

		public override bool ProcessCommand(string cmd, ref State s)
		{
			Command c;
			try
			{
				c = parser.ParseCommand(cmd);
			}
			catch (CommandException e)
			{
				outputLabel.writeLine(e.Message);
				return false;
			}

			switch (c)
			{
				case Command.SAVE:
					player.Save();
					break;
				case Command.VIEW:
					outputLabel.writeLine(player.ToString());
					break;
				case Command.HELP:
					displayHelp();
					break;
			}
			return false;
		}

		private void displayHelp()
		{
			throw new NotImplementedException();
		}
		public override void ExitState()
		{
			throw new NotImplementedException();
		}
	}
}
