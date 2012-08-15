using System;

namespace Epic
{
	public class CROP
	{
		public CROP ()
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program predicts daily potential growth of total
			// plants biomass and roots
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

			double XX = .685-.209*PARM.ROSP;
			double PAR = .0005*PARM.SRAD*(1-Math.Exp (-XX*PARM.SLAI[PARM.JJK]));
			double X2 = PAR*PARM.AJWA[PARM.JJK];
			
			if (PARM.ICG == 0){
				double X1 = Math.Max((PARM.VPD-1), -0.5);
				XX = (PARM.WA[PARM.JJK]-PARM.WAVP[PARM.JJK]*X1)*PARM.CLF;
				PARM.DDM[PARM.JJK] = XX*X2;
				if(PARM.DDM[PARM.JJK] < 1*Math.Pow (10, -10)){
						PARM.DDM[PARM.JJK] = 0.0;
					return;
				}
			}
			else{
				X2 = X2 * PARM.WA[PARM.JJK];
				// There is no def for ASVP
				double VPDX = 0.0; //.67*(PARM.ASVP[TMX+273]-PARM.RHD*PARM.ASVP(PARM.TX+273.0));
				double X3 = .01*PARM.WUB[PARM.JJK]*Math.Pow (VPDX, -.5);
				double X4 = PARM.SU*X3;
				PARM.DDM[PARM.JJK] = Math.Min (X2, X4);
			}
			PARM.DM[PARM.JJK] = PARM.DM[PARM.JJK] + PARM.DDM[PARM.JJK];
		}
	}
}

