using System;

namespace Epic
{
	public class AINTRO
	{
		public AINTRO (ref double[] X, ref double[] Y, ref int N1, ref int N2)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program interpolates soil properties from layers with
			// equal thickness (Output from Dif Eq Soln of Gas Diffusion EQS) to
			// layers of unequal thickness (Input soil layers).
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

			X = new double[30];
			Y = new double[30];
			double Z1, TOT, AD1, AD2, Y2, J;
			Z1 = TOT = AD1 = AD2 = Y2 = 0;
			J = 1;
			
			int k;
			for (k = 0; k < N1; k++){
				AD1 = AD1 + X[k];
				while (J <= N2){
					L = PARM.LID[J];
					if (PARM.Z[1] > PARM.ZC[k]) break;
					Y[L] = TOT + X[k]*(PARM.Z[L] - Z1)/PARM.DZ;
					AD2 += Y[L];
					Z1 = PARM.Z[L];
					J++;
					TOT = 0;
				}
				if (J <= N1){
					TOT = TOT + X[k]*(PARM.ZC[k] - Z1)/PARM.DZ;
					Z1 = PARM.ZC[k];
				}
				else break;
			}
			Y[N1] = Y[N1] + AD1-AD2;
			return;
		}
	}
}

