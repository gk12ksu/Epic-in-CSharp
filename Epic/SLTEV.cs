using System;

namespace Epic
{
	public class SLTEV
	{
		public SLTEV ()
		{
            // EPICv0810
			// Translated by Brian Cain
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            if (PARM.NEV == 1) return;
            int J = PARM.NEV;
            double SUM = 0.0;
			double XX, X1;
			int L;
            for (J = PARM.NEV; J > 2; J--){
                L = PARM.LID[J];
                X1 = PARM.WSLT[L];
                if(X1 <= 1.0E-5) continue;
                XX = Math.Min(.05*X1,PARM.SEV[L]*X1/(PARM.ST[L]+PARM.SEV[L]));
                SUM = SUM+XX;
                PARM.WSLT[L] = PARM.WSLT[L]-XX;
            }
            PARM.WSLT[PARM.LD1] = PARM.WSLT[PARM.LD1]+SUM;
            return;
		}
	}
}

