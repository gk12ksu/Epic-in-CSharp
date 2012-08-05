using System;

namespace Epic
{
	public class HPERC
	{
		public HPERC ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program computes percolation and lateral subsurface flow
            // from a soil layer when field capacity is exceeded
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            PARM.SEP = 0.0;
            PARM.SST = 0.0;
            double AVW = PARM.ST[PARM.ISL]-PARM.FC[PARM.ISL];
            if (AVW < Math.Pow(10, -5)) return;

            double X1 = 24.0/(PARM.PO[PARM.ISL]-PARM.FC[PARM.ISL]);
            double X2 = PARM.HCL[PARM.ISL]*X1;
            double ZZ = X1*PARM.SATC[PARM.ISL];
            double XZ = X2+ZZ;

            double X3;
            if (XZ > 20.0){
                X3 = AVW;
            }
            else{
                X3 = AVW*(1.0-Math.Exp(-XZ));
            }
            PARM.SEP = X3/(1.0+X2/ZZ);
            PARM.SST = X3-PARM.SEP;

            if (PARM.ISL != PARM.IDR) PARM.SST = PARM.SST * PARM.RFTT;
            return;
		}
	}
}

