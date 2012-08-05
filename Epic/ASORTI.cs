using System;

namespace Epic
{
	public class ASORTI
	{
		public ASORTI (ref int[] NZ, ref int[] NY, ref int M)
		{
			// Epicv0810
			// Translated by Brian Cain
			// This program sorts integers into ascending order
			// using ripple sort
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			NY = new int[M];
			NZ = new int[M];
			int NB = M-1;
			int J = M;
			int I, K, MK, K1, N1;
			
			for (I = 0; I < NB; I++){
				J--;
				MK = 0;
				for (K = 0; K < J; K++){
					K1 = K+1;
					if (NZ[NY[K]] <= NZ[NY[K1]]){
						continue;	
					}
					else{
						N1 = NY[K1];
						NY[K1] = NY[K];
						NY[K] = N1;
						MK = 1;
					}
				}
				if (MK == 0) break;
			}
			return;
		}
	}
}

