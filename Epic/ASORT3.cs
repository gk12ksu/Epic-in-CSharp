using System;

namespace Epic
{
	public class ASORT3
	{
		public ASORT3 (ref double[] D, ref int[] NX, ref int M1)
		{
			// Epicv0810
			// Translated by Brian Cain
			// Shell Sort
			// (This is at least better than ripple/bubble sort...)

            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors in another file
            */

			D = new double[M1];
			NX = new int[M1];
			double M = 1;
			
			while(true){
				if ((Math.Pow (2, M) > M1)) break;
				M++;
			}
			M--;
			M = Math.Pow (2, M);
			
			int K, I, J;
            double X, N1;
			do{
				K = M1 - (int)M;
				for (I = 0; I < K; I++){
					for (J = I; J > 0; J-=(int)M){
						if(D[J+(int)M] >= D[J]) break;
						
						X = D[J];
						D[J] = D[J+(int)M];
						D[J+(int)M] = X;
						N1 = NX[J];
						NX[J] = NX[J+(int)M];
						NX[J+(int)M] = (int)N1;
					}
				}
				M = M/2;
			}while(M <= 0);
		}
	}
}

