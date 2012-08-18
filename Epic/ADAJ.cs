using System;

namespace Epic
{
	public partial class Functions
	{
        /* ADDITIONAL CHANGE
           * 8/16/2012    Modified by Paul Cain by changing some of the data 
           *              types and overloading it to make it compatible with 
           *              a call to it in Main.
           */

		public static void ADAJ (ref int[] NC, ref double JDT, int M, double I, double NYD)
		{
			// Epicv0810
			// Translated by Brian Cain
			// This program computes the day of the year
			// given the month and day of the month
			
			NC = new int[13]; // dimension in fortran is an array
			JDT = NC[M] + I;
			if (M > 2) JDT = JDT - NYD;
		}

        public static void ADAJ(ref int[] NC, ref int JDT, int M, double I, double NYD)
        {
            double dJDT = (double)JDT;
            ADAJ(ref NC,ref dJDT,M,I,NYD);
            JDT = (int)dJDT;
        }
    }
}

