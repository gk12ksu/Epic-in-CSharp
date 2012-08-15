using System;

namespace Epic
{
	public partial class Functions
	{
        public static double AUNIF(int J)
        {
            // EPICv0810
            // Translated by Brian Cain
            // Original file has no description

            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            /* ADDITIONAL CHANGE
            * 7/31/2012    Modified by Paul Cain to fix build errors
            */

            MODPARAM PARM = MODPARAM.Instance;

            double K = PARM.IX[J] / 127773;
            PARM.IX[J] = (int)(16807 * (PARM.IX[J] - K * 127773) - K * 2836);
            if (PARM.IX[J] < 0)
            {
                PARM.IX[J] = PARM.IX[J] + 2147483647;
            }
            // Return AUNIF?
            double AUNIF_ans = PARM.IX[J] * 4.656612875D - 10; //What....
            return AUNIF_ans;
        }
	}
}

