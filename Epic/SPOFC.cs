using System;

namespace Epic
{
	public class SPOFC
	{
		public SPOFC (ref int I)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program has no description
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double X1 = .9*PARM.PO[I];
            if (PARM.FC[I] < X1) return;
            double X2 = PARM.FC[I]-PARM.S15[I];
            PARM.FC[I] = X1;
            PARM.S15[I] = PARM.FC[I]-X2;
            if (PARM.S15[I] <= 0.0) PARM.S15[I] = .01*PARM.FC[I];
		}
	}
}

