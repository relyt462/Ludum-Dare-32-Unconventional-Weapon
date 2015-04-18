using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace HackTheWorld
{
	public partial class mainScreen : Form
	{
		private CommandParser parser;
		[DllImport("user32.dll")]
		static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
		[DllImport("user32.dll")]
		static extern bool ShowCaret(IntPtr hWnd);

		public mainScreen()
		{
			InitializeComponent();
			parser = new CommandParser();
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				printText(parser.ParseCommand(inputTextBox.Text));
				inputTextBox.Clear();
				e.Handled = true;
				e.SuppressKeyPress = true;
			}

			if (parser.CurState == InterfaceState.EXIT)
				this.Close();
		}

		private void mainScreen_Shown(object sender, EventArgs e)
		{
			CreateCaret(inputTextBox.Handle, IntPtr.Zero, 14, inputTextBox.Height);
			ShowCaret(inputTextBox.Handle);
		}

		private void printText(string strToPrint)
		{
			output.writeLine(strToPrint);
		}

		private void mainScreen_Activated(object sender, EventArgs e)
		{
			CreateCaret(inputTextBox.Handle, IntPtr.Zero, 14, inputTextBox.Height);
			ShowCaret(inputTextBox.Handle);
		}
	}

	internal class CommandParser
	{
		public InterfaceState CurState{ get; private set; }
		public string[] ValidCommands { get; private set; }
		private Command[] validCmds;

		public CommandParser()
		{
			CurState = InterfaceState.DEFAULT;
			validCmds = new Command[3];
			ValidCommands = new string[3];
			for(int i = 0; i < validCmds.Length; i++)
			{
				validCmds[i] = (Command) i;
				ValidCommands[i] = validCmds[i].ToString();
			}
		}

		public string ParseCommand(string cmd)
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
				ProcessCmd(validCmds[posCmds[0]], cmds,ref output);
			}
			else if (posCmds.Count == 0)
				output.Append("INVALID COMMAND TRY AGAIN");
			else
				output.Append("AMBIGUOUS COMMAND TRY AGAIN");

			return output.ToString();
		}
		
		private bool ProcessCmd(Command cmdToProcess,List<string> paramaters, ref StringBuilder output)
		{
			switch(cmdToProcess)
			{
				case Command.EXIT:
					ChangeState(InterfaceState.EXIT);
					return true;
				case Command.HELP:
					if(CurState == InterfaceState.DEFAULT)
						output.Append("HELP TEST");
					return true;
				case Command.HELL:
					tempTestMultCmds(paramaters, ref output);
					return true;
			}
			return false;
		}

		private void tempTestMultCmds(List<string> parameters, ref StringBuilder output)
		{
			if(parameters.Count == 2)
			{
				output.Append(parameters[1]);
			}
		}

		private bool ChangeState(InterfaceState newState)
		{

			//Add transitions for states where it is needed
			CurState = newState;
			return true;
		}

	}

	internal enum InterfaceState
	{
		DEFAULT = 0,
		EXIT = -1
	}

	internal enum Command
	{
		EXIT = 0, 
		HELP = 1,
		HELL = 2
	}
}
