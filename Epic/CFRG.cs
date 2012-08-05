using System;

namespace Epic
{
	public partial class Functions
	{
		public static double CFRG (ref double I, ref double J, ref double F, ref double RGS, ref double X, ref double JRT)
		{
			// EPICv0810
			// Translated by Brian Cain
			// Determines minimum stress factors for root and total biomass growth
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

			JRT = 0;
			if (F >= RGS) return JRT;
			RGS = Math.Max (0, F);
			J = I;
			if (RGS <= X) JRT = 1;
			return JRT;
		}
	}
}

