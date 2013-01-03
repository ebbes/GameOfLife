using System;

namespace gameoflife
{
	public class GameOfLife
	{
		public int Width { get { return matrix.Width; } }
		
		public int Height { get { return matrix.Height; } }
		
		public int Generations { get; private set; }
		
		public bool Active { get; private set; }
		
		private BinaryMatrix matrix;
		private GameRule rules;
		
		public GameOfLife (int width, int height, int initPercentage, GameRule rule)
		{
			matrix = new BinaryMatrix (width, height, initPercentage);
			Generations = 0;
			rules = rule;
			Active = true;
		}
		
		public override string ToString ()
		{
			string s = Generations.ToString () + " Generations";
			
			if (!Active)
				s += " (Inactive)";
			
			s += " - " + rules.ToString ();
			return s;
		}
		
		public string MatrixToString ()
		{
			return matrix.Format (' ', '*');
		}
		
		public string MatrixToString (char dead, char alive)
		{
			return matrix.Format (dead, alive);
		}
		
		public bool IsCellAlive (int row, int col)
		{
			return matrix.GetElement (row, col);
		}
		
		public void ToggleCellState (int row, int col)
		{
			matrix.SetElement (row, col, !matrix.GetElement (row, col));
			Active = true;
		}
		
		private int GetNeighborCount (BinaryMatrix m, int row, int col)
		{
			int neighbors = 0;
			
			//Top left, top, top right, left, right, bottom left, bottom, bottom right
			if (row > 0 && col > 0 && m.GetElement (row - 1, col - 1))
				neighbors++;
			if (row > 0 && m.GetElement (row - 1, col))
				neighbors++;
			if (row > 0 && col < m.Width - 1 && m.GetElement (row - 1, col + 1))
				neighbors++;
			
			if (col > 0 && m.GetElement (row, col - 1))
				neighbors++;
			if (col < m.Width - 1 && m.GetElement (row, col + 1))
				neighbors++;
			
			if (row < m.Height - 1 && col > 0 && m.GetElement (row + 1, col - 1))
				neighbors++;
			if (row < m.Height - 1 && m.GetElement (row + 1, col))
				neighbors++;
			if (row < m.Height - 1 && col < m.Width - 1 && m.GetElement (row + 1, col + 1))
				neighbors++;
			
			return neighbors;
		}
		
		public bool AdvanceGeneration ()
		{
			if (!Active)
				return false;
			BinaryMatrix lastGeneration = matrix.Clone ();
			
			for (int i = 0; i < Height; i++) {
				for (int j = 0; j < Width; j++) {
					int neighbors = GetNeighborCount (lastGeneration, i, j);
					
					bool current = lastGeneration.GetElement (i, j);
					
					matrix.SetElement (i, j, rules.GetNextState (current, neighbors));
				}
			}
			
			Generations++;
			
			Active = !matrix.Equals (lastGeneration);
			return Active;
		}
	}
}

