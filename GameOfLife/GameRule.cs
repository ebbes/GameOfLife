using System;
using System.Collections.Generic;

namespace gameoflife
{
	public class GameRule
	{
		private List<int> survive;
		private List<int> birth;
		
		public GameRule (int[] survive, int[] birth)
		{
			this.survive = new List<int> (survive);
			this.birth = new List<int> (birth);
		}
		
		public GameRule (string rule)
		{
			string s = rule.Split ('/') [0];
			string b = rule.Split ('/') [1];
			
			survive = new List<int> ();
			birth = new List<int> ();
			
			foreach (char i in s.ToCharArray ())
				survive.Add (int.Parse (i.ToString ()));
			
			foreach (char i in b.ToCharArray ())
				birth.Add (int.Parse (i.ToString ()));
		}
		
		public bool GetNextState (bool living, int neighbors)
		{
			return living && survive.Contains (neighbors) || !living && birth.Contains (neighbors);
		}
		
		public override string ToString ()
		{
			string s = "";
			
			foreach (int i in survive)
				s += i.ToString ();
			s += '/';
			foreach (int i in birth)
				s += i.ToString ();
			
			return s;
		}
	}
}

