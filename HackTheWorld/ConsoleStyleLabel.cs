using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
namespace HackTheWorld
{
	class ConsoleStyleLabel : Label
	{
		public int numLines { get; private set; }
		public int maxLines { get; private set; }
		
		public ConsoleStyleLabel() : base()
		{
			numLines = 0;
			maxLines = 5;
		}

		public ConsoleStyleLabel(int maxLines) : base()
		{
			numLines = 0;
			this.maxLines = maxLines;
		}

		public void writeLine(string input)
		{
			this.Text += input + Environment.NewLine.ToString();
			numLines += Regex.Matches(input,"^.*$",RegexOptions.Multiline).Count;
			while(numLines > maxLines)
			{
				deleteTopLine();
			}
		}

		private void deleteTopLine()
		{
			var t = this.Text.Split(Environment.NewLine.ToCharArray(),StringSplitOptions.RemoveEmptyEntries).Skip(1);
			this.Text = string.Join(Environment.NewLine, t);
			this.Text += Environment.NewLine.ToString();
			numLines = t.Count();
		}

		public void writeLine(char input)
		{
			writeLine(input.ToString());
		}

		public void writeLine(int input)
		{
			writeLine(input.ToString());
		}

		public void writeLine(double input)
		{
			writeLine(input.ToString());
		}
		public void writeLine(float input)
		{
			writeLine(input.ToString());
		}
		public void writeLine(short input)
		{
			writeLine(input.ToString());
		}
		public void writeLine(long input)
		{
			writeLine(input.ToString());
		}

	}
}
