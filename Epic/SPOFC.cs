using System;

namespace Epic
{
	public partial class Functions
	{
		public static void SPOFC (ref int I)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program has no description
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
           * 8/16/2012    Modified by Paul Cain to make it part of the Functions partial class
           */
			
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

