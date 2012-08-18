using System;

namespace Epic
{
	public partial class Functions
	{
        /* ADDITIONAL CHANGE
         * 8/16/2012    Modified by Paul Cain to make it part of the 
         *              Functions partial class and overload it to 
         *              work with integers as well as doubles
         */

		public static void AISPL (ref double II, ref double JJ)
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

        public static void AISPL(ref int II, ref int JJ)
        {
            //Convert the integers to doubles, run AISPL, 
            //  and then convert them back to integers
            double dII = (double)II;
            double dJJ = (double)JJ;
            AISPL(ref dII, ref dJJ);
            II = (int)dII;
            JJ = (int)dJJ;
        }
	}
}

