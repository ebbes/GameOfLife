using System;
using System.Windows.Forms;

namespace gameoflife
{
	public static class Program
	{
		public static void Main ()
		{
			Application.EnableVisualStyles ();
			Application.Run (new MainForm ());
		}
	}
}

