using System;

namespace Epic
{
	public class WNDIR
	{
        private static MODPARAM PARM = MODPARAM.Instance;

		public WNDIR (double ID)
		{
			// EPICv0810
			// Translated by Emily Jordan
			
            /* ADDITIONAL CHANGE
            * 7/31/2012    Modified by Paul Cain to fix build errors
            */
						
			// THIS SUBPROGRAM SIMULATES DAILY WIND DIRECTION
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			// USE PARM
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
			double FX = Functions.AUNIF(PARM.IDG[5]); //IDG is array.
			int J;
			double G; //Needed to create G so it could be used later on. Could not find single variable "G" in the modparam file.
            int J1=0;
            double XJ1 =0;
			for(J = 1; J < 16; J++)
			{
				J1 = J - 1;
				if(PARM.DIR[PARM.MO - 1, J - 1] > FX) 
				{
					if(J == 1) G = FX/PARM.DIR[PARM.MO - 1, J -1];
					else G = (FX - PARM.DIR[PARM.MO - 1, J1 - 1]) / (PARM.DIR[PARM.MO - 1, J - 1] - PARM.DIR[PARM.MO - 1, J1 - 1]);
					XJ1 = J1;
					PARM.TH = PARM.PI2 * (G + XJ1 - .5)/16.0;
					if(PARM.TH < 0.0) PARM.TH = PARM.PI2 + PARM.TH;
					return;
				
				}
			}
			J = 16;
			if(J == 1) G = FX/PARM.DIR[PARM.MO - 1, J - 1];
			else G = (FX - PARM.DIR[PARM.MO - 1, J1 - 1]) / (PARM.DIR[PARM.MO - 1, J - 1] - PARM.DIR[PARM.MO - 1, J1 - 1]);
			 XJ1 = J1;
			PARM.TH = PARM.PI2 * (G + XJ1 - .5)/16.0;
			if(PARM.TH < 0.0) PARM.TH = PARM.PI2 + PARM.TH;
			return; 
	
		}
	}
}