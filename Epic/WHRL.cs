using System;

namespace Epic
{
	public partial class Functions
	{
		public static void WHRL()
		{
			// EPICv0810
			// Translated by Emily Jordan
			
						
			// THIS SUBPROGRAM COMPUTES DAY LENGTH
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
            * 8/16/2012    Modified by Paul Cain to make it part of the Functions partial class
            */
			
			// USE PARM
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			double XI = PARM.JDA;
			double SD = .4102 * Math.Sin((XI-80.25)/PARM.PIT);
			double CH = -PARM.YTN1 * Math.Tan(SD);
		
			double H; //I think H is a local variable, so I made it here.
		
			if(CH >= 1.0) H = 0.0;
			else
			{
				if(CH <= -1.0) H = 3.1416;
				else H = Math.Acos(CH);
		
			}
		
		PARM.HRLT = 7.72 * H;
		PARM.HR1 = PARM.HRLT - PARM.HR0;
		PARM.HR0 = PARM.HRLT;
		return;
	
		}
	}
}
