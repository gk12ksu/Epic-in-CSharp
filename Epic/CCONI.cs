using System;

namespace Epic
{
	public class CCONI
	{
		public CCONI ()
		{
			// EPICv0810
			// Translated by Brian Cain
			// This program does an initial partition each gas conc in air
			// into gas concentration in air and liquid phase and then
			// calculates the total gas concentration (G/M3 soil)
			// It is an approximation and should be calculated only once at the
			// beginning of each simulation - RCI - 7/19/09
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
			int J;
			for (J = 0; J < PARM.NBCL; J++){
				PARM.CLO2[J] = PARM.CGO2[J]/PARM.HKPO[J];
				PARM.CLCO2[J] = PARM.CGCO2[J]/PARM.HKPC[J];
				PARM.CLN2O[J] = PARM.CGN2O[J]/PARM.HKPN[J];	
				PARM.AO2C[J] = PARM.CGO2[J]*PARM.AFP[J]+PARM.CLO2[J]*PARM.VWC[J];
				PARM.ACO2C[J] = PARM.CGCO2[J]*PARM.AFP[J]+PARM.CLCO2[J]*PARM.VWC[J];
				PARM.AN2OC[J] = PARM.CGN2O[J]*PARM.AFP[J]+PARM.CLN2O[J]*PARM.VWC[J];
			}
		}
	}
}

