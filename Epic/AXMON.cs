using System;

namespace Epic
{
	public partial class Functions
	{
        /* ADDITIONAL CHANGE
           * 8/16/2012    Modified by Paul Cain by changing some of the data 
           *              types to make it compatible with 
           *              a call to it in Main.
           */

		public static void AXMON (ref int JDX, ref int MOX)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program determines the month, given the day of the year
			
			// I feel like this could be optimized for C#...
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			if (JDX > PARM.NC[2]){
				int M = MOX;
				for (MOX = M; MOX < 12; MOX++){
					int M1 = MOX+1;
					int NDA = PARM.NC[M1]-PARM.NYD;
					if (JDX <= NDA) return;
				}
			}
			MOX = 1;
			return;
		}
	}
}

