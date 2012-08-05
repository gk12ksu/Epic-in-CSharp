using System;

namespace Epic
{
	public class HSNOM
	{
		public HSNOM ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program predicts daily snow melt when the average air
            // temperature exceeds 0 degrees C.
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double X2 = Math.Min(PARM.DST0,PARM.STMP[PARM.LID[2]]);
            double X1 = Math.Sqrt(PARM.TMX*PARM.SRAD);
            double SNPKT = .3333*(2.0*X2+PARM.TX);
            double F = PARM.TSNO/(PARM.TSNO+Math.Exp(PARM.SCRP[15,0]-PARM.SCRP[15,1]*PARM.TSNO));
            PARM.SML = Math.Max(0.0,X1*(1.52+.54*F*SNPKT));
            PARM.SML = Math.Min(PARM.SML,PARM.SNO);
            PARM.SNO = PARM.SNO-PARM.SML;
            if (PARM.REP < Math.Pow(10, -5)) PARM.REP = .042*PARM.SML;

            PARM.PR = .042;
            PARM.DUR = 24.0;
            return;
		}
	}
}

