using System;

namespace Epic
{
	public partial class Functions
	{
		public double ADAJ (ref double[] NC, ref double JDT, ref int M, ref double I, ref double NYD)
		{
			// Epicv0810
			// Translated by Brian Cain
			// This program computes the day of the year
			// given the month and day of the month
			
			NC = new double[13]; // dimension in fortran is an array
			JDT = NC[M] + I;
			if (M > 2) JDT = JDT - NYD;
			return JDT;
		}
	}
}

