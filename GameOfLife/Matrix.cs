using System;

namespace gameoflife
{
	public class BinaryMatrix : Object
	{
		public int Width { get; internal set; }
		
		public int Height { get; internal set; }
		
		private bool[,] entries;
		
		public BinaryMatrix (int width, int height) : this (width, height, 100)
		{
		}
		
		public BinaryMatrix (int width, int height, int initPercentage)
		{
			this.Width = width;
			this.Height = height;
			
			entries = new bool[height, width];
			
			InitializeMatrix (initPercentage);
		}
		
		private BinaryMatrix (int width, int height, bool[,] entries)
		{
			this.Width = width;
			this.Height = height;
			
			this.entries = entries;
		}
		
		private void InitializeMatrix (int initPercentage)
		{
			Random r = new Random (DateTime.Now.Millisecond);
			
			for (int i = 0; i < Height; i++) {
				for (int j = 0; j < Width; j++) {
					entries [i, j] = initPercentage >= r.Next (1, 100);
				}
			}
		}
		
		public bool GetElement (int Row, int Column)
		{
			return entries [Row, Column];
		}
		
		public void SetElement (int Row, int Column, bool val)
		{
			entries [Row, Column] = val;
		}
		
		public bool Equals (BinaryMatrix matrix)
		{
			if (this.Width != matrix.Width || this.Height != matrix.Height)
				return false;
			
			bool equals = true;
			
			for (int i = 0; i < this.Width; i++) {
				for (int j = 0; j < this.Height; j++) {
					equals = this.entries [i, j] == matrix.entries [i, j];
					
					if (!equals)
						return false;
				}
			}
			
			return equals;
		}
		
		public string Format (char False, char True)
		{
			string ret = "";
			
			for (int i = 0; i < Height; i++) {
				for (int j = 0; j < Width; j++) {
					if (entries [i, j])
						ret += True;
					else
						ret += False;
				}
				ret += '\n';
			}
			
			return ret;
		}
		
		public override string ToString ()
		{
			return Format ('0', '1');
		}
		
		public BinaryMatrix Clone ()
		{
			return new BinaryMatrix (this.Width, this.Height, (bool[,])this.entries.Clone ());
		}
	}
}

