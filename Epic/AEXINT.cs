using System;

namespace Epic
{
	public partial class Functions
	{
		public static void AEXINT (ref double WW, out double SUM)
		{
			
			// EPICv0810
			// Translated by Brian Cain
			// This program integrates the modified exponential EQ

            /* ADDITIONAL CHANGE
            * 8/16/2012    Modified by Paul Cain to make it part of the Functions partial class
            */
			
			double X1 = 1;
			double DX = .1;
			SUM = 0;
			double Y1 = 0;
			
			// Writing to a file here...???
			// Fortran: WRITE(KW(1),3)X1, DX, Y1
			// Print to Console?
			Console.WriteLine("{0}, {1}, {2}", X1, DX, Y1);
			
			double XY, X2, Y2;
			while (DX > (1*10^(-4))){
				XY = 0;	
				while (XY < .1){
					X2 = X1 - DX;
					if (X2 <= 0) break;
					Y2 = Math.Pow((-Math.Log (X2)),WW);
					XY = (Y2 + Y1)*DX;
					SUM = SUM + XY;
					X1 = X2;
					Y1 = Y2;
				}
				DX = DX*.5;
			}
			SUM = SUM*.5;
			return;
		}
	}
}

