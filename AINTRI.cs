using System;

namespace Epic
{
	public class AINTRI
	{
		public AINTRI (ref double[] X, ref double[] Y, ref int N1, ref int N2)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program interpolates soil properties from layers
			// with un equal thickness to layers of equal thickness used
			// in differential equations of gas diffusion
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
		    Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

			X = new double[15];
			Y = new double[N2];
			double ZZ = 0, Z1 = 0, TOT = 0;
			int J = 1;
			
			int k;
			for (k = 0; k < N1; k++){
				int L = PARM.LID[k]; //What is this in Fortran???
				//int L = 1;
				while (J <= N2){
					if (PARM.ZC[J] > PARM.Z[L]) break;
					Y[J] = TOT+X[L]*(PARM.ZC[J]-ZZ)/(PARM.Z[L]-Z1);
					ZZ = PARM.ZC[J];
					J++;
					TOT = 0;
				}
				if (J > N2) break;
				TOT = TOT+X[L]*(PARM.Z[L] - ZZ)/(PARM.Z[L] - Z1);
				Z1 = PARM.Z[L];
				ZZ = PARM.Z[L];
			}
			Y[J] = (int)TOT;
			return;
		}
	}
}

