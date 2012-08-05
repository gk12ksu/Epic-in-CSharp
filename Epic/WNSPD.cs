using System;

namespace Epic
{
	public class WNSPD
	{
		public WNSPD ()
		{
			// EPICv0810
			// Translated by Emily Jordan
			
						
			// THIS SUBPROGRAM SIMULATES MEAN DAILY WIND SPEED @ 10 M HEIGHT
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			// USE PARM
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
			double V6 = Epic.AUNIF(PARM.IDG[4]); //AUNIF is a method call, and IDG is a number array. I decremented the number to 4, as per our knowledge that c# arrays start at 0 instead of the Fortran 1
			PARM.U10 = PARM.UAVM[PARM.MO - 1] * Math.Pow((-Math.Log(V6)), UXP); //U10 is global, UAVM is a double array, so again, decrement by 1 to make it work in c#
      
			return;		
		}
	}
}