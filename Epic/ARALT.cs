using System;

namespace Epic
{
	public class ARALT
	{
		public ARALT (ref double[] X, ref double DRV)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program interpolates monthly values of daylight
			// hours and maximum solar radiation to provide daily values
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			X = new double[12];
			int M1 = PARM.MO + 1;
			double N2 = PARM.NC[M1];
			if (PARM.MO == 2){
				N2 = N2 - PARM.NYD;	
			}
			double N1 = PARM.NC[PARM.MO];
			double X1 = PARM.JDA - N1;
			double X2 = N2 - N1;
			double RTO = X1 / X2;
			if (M1 == 13){ 
				M1 = 1;
			}
			double XX = X[M1] - X[PARM.MO];
			DRV = XX/X2;
			double ARALT_var = XX*RTO+X[PARM.MO];
			return; // return ALRAT?
		}
	}
}

