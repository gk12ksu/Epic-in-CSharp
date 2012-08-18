using System;

namespace Epic
{
	public partial class Functions
	{
		public static void ASORT1 (ref double[] X, ref int[] NX, ref int M)
		{
			// Epicv0810
			// Translated by Brian Cain
			// This program sorts real numbers into ascending order
			// using ripple sort (This is really bad, maybe we can optimize this?
			// I'm pretty sure this is no better than bubble sort. It has a
			// worst case performance of O(n^2). Best case O(n) )

            /* ADDITIONAL CHANGE
            * 8/17/2012    Modified by Paul Cain to make it part of the Functions partial class
            *               and also change the X parameter from int[] to double[] because the 
            *               only two calls that use this method pass it a double[] for the X
            *               parameters.
            */
			
			X = new double[200];
			NX = new int[200];
			int NB = M - 1;
			int J = M;
			
			int i, k;
			int MK, N1, KP1;
			for (i = 0; i < NB; i++){
				J--;
				MK = 0;
				for (k = 0; k < J; k++){
					KP1 = k + 1;
					if (X[NX[k]] >= X[NX[KP1]]){
						// CYCLE
						continue;
					}
					else{
						N1 = NX[KP1];
						NX[KP1] = NX[k];
						NX[k] = N1;
						MK = 1;	
					}
					
				}
				if (MK == 0) break;
			}
			return;
		}
	}
}
