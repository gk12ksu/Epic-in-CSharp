using System;

namespace Epic
{
	public class SOLT
	{
		public SOLT ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program estimates daily average temp at the center
            // of each soil layer
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double XLAG = .8;
            double XLG1 = 1.0-XLAG;
            double F = PARM.ABD/(PARM.ABD+686.0*Math.Exp(-5.63*PARM.ABD));
            double DP = 1.0+2.5*F;
            double WW = .356-.144*PARM.ABD;
            double B = Math.Log(.5/DP);
            double WC = .001*PARM.SW/(WW*PARM.Z[PARM.LID[PARM.NBSL]]);
            F = Math.Exp(B*Math.Pow(((1.0-WC)/(1.0+WC)),2));
            double DD = F*DP;
            double X2 = PARM.TX+.5*(PARM.TMX-PARM.TMN)*(PARM.ST0-14.0)/20.0;
            double X3 = (1.0-PARM.BCV)*X2+PARM.BCV*PARM.STMP[PARM.LID[1]];
            PARM.DST0 = .5*(X2+X3);
            double ZZ = 2.0*DD;
            double XX = 0.0;
            double X1 = PARM.AVT-PARM.DST0;
			int J;
			double ZD;
            for (J = 1; J < PARM.NBSL; J++){
                PARM.ISL = PARM.LID[J];
                ZD = (XX+PARM.Z[PARM.ISL])/ZZ;
                F = ZD/(ZD+Math.Exp(-.8669-2.0775*ZD));
                PARM.STMP[PARM.ISL] = XLAG*PARM.STMP[PARM.ISL]+XLG1*(F*X1+PARM.DST0);
                PARM.SEV[PARM.ISL] = 0.0;
                XX = PARM.Z[PARM.ISL];
            }
            return;
		}
	}
}

