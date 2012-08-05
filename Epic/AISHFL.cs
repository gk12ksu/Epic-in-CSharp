using System;

namespace Epic
{
	public class AISHFL
	{
		public AISHFL ()
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program shuffles data randomly. (BRATLEY, FOX, SCHRAGE, P.34)
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
			// do in fortran
			// DO [label] I = e1, e2, e3
			// I   Control variable   
			// e1  Begin range
			// e2  End range
			// e3  Stride
			
			int i;
			double RN, II, K;
			for (i = 20; i > 2; i--){
				II = PARM.IDG[i];
				RN = Functions.AUNIF(21); // Call function AUNIF here with this parameter
				K = i*RN+1;
				PARM.IDG[i] = PARM.IDG[(int)K];
				PARM.IDG[(int)K] = (int)II;
			}
			return;
		}
	}
}

