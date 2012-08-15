using System;

namespace Epic
{
	public partial class Functions
	{
		public static double AICL ()
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program computes the day of the month, given the month
			// and the day of the year

            //Modified by Paul Cain on 7/30/2012 to fix build errors.

			Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            PARM.KDA = PARM.JDA - PARM.NC[PARM.MO];
			if (PARM.MO>2) PARM.KDA = PARM.KDA+PARM.NYD;
			return PARM.KDA;
		}
	}
}

