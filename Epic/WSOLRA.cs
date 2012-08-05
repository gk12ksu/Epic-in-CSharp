using System;
	  
namespace Epic  
{
		
	public class WSOLRA		
	{			
		public WSOLRA ()			
		{
				//EPICv0810
				//Translated by Emily Jordan
				//This pogram doesn't have a description
			
				// The fortran file uses global variables, refer to MODPARAM.cs for
				// a list of all global variables
			
				double RX = PARM.RAMX-PARM.RM;
				SRAD = PARM.RM + PARM.WX[2] * PARM.RX/4; //Standard subtract 1 fromt he array to make it fight into the c# frame.
				if(PARM.STRAD <= 0) PARM.SRAD = .05 * PARM.RAMX;
			
				return;
		
			
		}	
	}	
}

