using System;

namespace Epic
{
	public partial class Functions
	{
		public static double AXMON (ref double JDX, ref double MOX)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program determines the month, given the day of the year
			
			// I feel like this could be optimized for C#...
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			if (JDX > PARM.NC[2]){
				double M = MOX;
				for (MOX = M; MOX < 12; MOX++){
					int M1 = (int)MOX+1;
					double NDA = PARM.NC[M1]-PARM.NYD;
					if (JDX <= NDA) return NDA;
				}
			}
			MOX = 1;
			return 0.0;
		}
	}
}

