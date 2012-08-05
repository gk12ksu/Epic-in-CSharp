using System;

namespace Epic
{
	public class ASORT1
	{
		public ASORT1 (ref int[] X, ref int[] NX, ref int M)
		{
			// Epicv0810
			// Translated by Brian Cain
			// This program sorts real numbers into ascending order
			// using ripple sort (This is really bad, maybe we can optimize this?
			// I'm pretty sure this is no better than bubble sort. It has a
			// worst case performance of O(n^2). Best case O(n) )
			
			X = new int[200];
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
