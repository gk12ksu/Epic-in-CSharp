using System;

namespace Epic
{
	public class SAJBD
	{
		public SAJBD ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program simulates the change in bulk density within
            // the plow layer caused by infiltration settling.
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double XX = PARM.RFV-PARM.QD;
            int J;
            double F;
            for (J = 0; J < PARM.NBSL; J++){
                PARM.ISL = PARM.LID[J];
                if (XX>0.0 && PARM.BDP[PARM.ISL]<PARM.BD[PARM.ISL]){
                    XX = XX*.2*(1.0+2.0*PARM.SAN[PARM.ISL]/(PARM.SAN[PARM.ISL]+Math.Exp(8.597-.075*PARM.SAN[PARM.ISL])))/Math.Pow(PARM.Z[PARM.ISL],1.6);
                    if (XX<200.0){
                        F = XX/(XX+Math.Exp(PARM.SCRP[5,0]-PARM.SCRP[5,1]*XX));
                    }
                    else{
                        F = 1.0;
                    }
                    PARM.BDP[PARM.ISL] = PARM.BDP[PARM.ISL]+F*(PARM.BD[PARM.ISL]-PARM.BDP[PARM.ISL]);
                }
                XX = PARM.PKRZ[PARM.ISL];
                if(PARM.Z[PARM.ISL]>PARM.BIG) break;
            }
            return;
		}
	}
}

