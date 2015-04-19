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
		private FiniteStateMachine fsm;
		[DllImport("user32.dll")]
		static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);
		[DllImport("user32.dll")]
		static extern bool ShowCaret(IntPtr hWnd);

		public mainScreen()
		{
			InitializeComponent();
			fsm = new FiniteStateMachine(ref output);
		}

		private void textBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				switch (fsm.ProcessCommand(inputTextBox.Text))
				{
					case -1:
						this.Close();
						break;
					default:
						break;

				}
				inputTextBox.Clear();
				e.Handled = true;
				e.SuppressKeyPress = true;
			}

			
		}

		private void mainScreen_Shown(object sender, EventArgs e)
		{
			CreateCaret(inputTextBox.Handle, IntPtr.Zero, 14, inputTextBox.Height);
			ShowCaret(inputTextBox.Handle);
		}

		private void mainScreen_Activated(object sender, EventArgs e)
		{
			CreateCaret(inputTextBox.Handle, IntPtr.Zero, 14, inputTextBox.Height);
			ShowCaret(inputTextBox.Handle);
		}
	}


	internal enum InterfaceState
	{
		DEFAULT = 0,
		EXIT = -1,
		START = 15
	}

	internal enum Command
	{
		NEW,
		CONTINUE,
		EXIT,
		HELP,
		OPTION,
		FONT,
		SIZE,
		YES,
		NO,
		STATISTICS,
		VIEW,
		SHOW,
		EASY,
		MEDIUM,
		HARD,
		SAVE
	}


}
