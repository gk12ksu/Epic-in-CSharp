using System;

namespace Epic
{
	public partial class Functions
	{
		public static double CAHU (int J, double K, double BASE, double NHS)
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program accumulates heat units for use in CPTHU
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
            * 8/16/2012    Modified by Paul Cain to make it part of the Functions partial class
            *              and made the function return a double to satisfy a call to it in Main.
            */
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
			double CAHU = 0;
			PARM.MO = 1;
			
			
			for (PARM.JDA = J; PARM.JDA > K; PARM.JDA++){
				Functions.AXMON(ref PARM.JDA, ref PARM.MO);
				if(PARM.JDHU <= 366){
					//Functions.WHRL();
					if (PARM.HRLT < PARM.WDRM && NHS == 0){
						continue;	
					}
				}
				double TA = Functions.ARALT(PARM.TAV, XX);
				double TGX = TA - BASE;
				if (TGX > 0){
					CAHU = CAHU+TGX;	
				}
			}
			return CAHU;
		}
	}
}

