using System;

namespace Epic
{
	public partial class Functions
	{
		public static double ASVP (double TK)
		{
			// Epicv0810
			// Translated by Brian Cain
			// This program computes the saturation vapor pressure
			// given temperature

            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors in another file
            */
			
			// ASVP is not global variable...so return?
			
			double ASVP_ans = .1*(Math.Exp (54.879-5.029*Math.Log (TK) - 6790.5/TK));
			return ASVP_ans;
			
		}
	}
}

