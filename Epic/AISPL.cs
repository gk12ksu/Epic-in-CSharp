using System;

namespace Epic
{
	public class AISPL
	{
		public AISPL (ref double II, ref double JJ)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program shuffles data randomly. (BRATLEY, FOX, SCHRAGE, P.34)
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            // Not sure why this is using globals...none are referenced
			double XX = II;
			XX = XX*.1;
			JJ = XX;
			II = II-JJ*10;
			return;
		}
	}
}

