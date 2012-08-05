using System;

namespace Epic
{
	public class SWTN
	{
		public SWTN ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program has no description
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            int J;
            for (J = 0; J < PARM.NBSL; J++){
                PARM.ISL = PARM.LID[J];
                if (PARM.Z[PARM.ISL] >= .15) goto lbl1;
            }
            PARM.ISL = PARM.LID[PARM.NBSL];
      lbl1: double XX = Math.Log10(PARM.S15[PARM.ISL]);
            double X1 = Math.Max(.1, PARM.ST[PARM.ISL]);
            PARM.WTN = Math.Max(5.0,Math.Pow(10.0,(3.1761-1.6576*((Math.Log10(X1)-XX)/(Math.Log10(PARM.FC[PARM.ISL])-XX)))));
            return;
		}
	}
}

