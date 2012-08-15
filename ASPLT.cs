using System;

namespace Epic
{
	public partial class Functions
	{
		public static double ASPLT (ref double XX)
		{
			// Epicv0810
			// Translated by Brian Cain
			// This program splits dual purpose real input variables
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
			
			// According to fortran documentation and a quick google search,
			// this is the C# equivalent to aint(): 
			// http://stackoverflow.com/questions/247143/matching-fortran-rounding-in-c-sharp
			double ASPLT_ans = Math.Round (XX, MidpointRounding.AwayFromZero);
			XX = XX - ASPLT_ans;
			return XX;
		}
	}
}

