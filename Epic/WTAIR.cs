using System;

namespace Epic
{
	public class WTAIR
	{
		public WTAIR ()
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program has no description
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			// USE PARM
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
			PARM.TMX = PARM.TXXM+PARM.TXSD*PARM.WX[0];
			PARM.TMN = PARM.TMNM+PARM.TNSD*PARM.WX[1];
			if (PARM.TMN>PARM.TMX) PARM.TMN = PARM.TMX-.2*Math.Abs(PARM.TMX);
			return;
		}
	}
}

