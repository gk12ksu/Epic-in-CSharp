using System;

namespace Epic
{
    public class WRLHUM
    {
        public WRLHUM()
        {
            //EPICv0810
            //Translated by Emily Jordan
            //This file supposedly computes max solar radiation at the earths surface.

            // The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			// USE PARM
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
			
            double Q1 = PARM.RHM - 1.0; //Q1 may be a local variable.
            double UPLM = PARM.RHM - Q1 * Math.Exp(Q1);
            double BLM = PARM.RHM * (1.0 - Math.Exp(-PARM.RHM));
            PARM.RHD = Epic.ATRI(BLM, PARM.RHM, UPLM, 7);

            return;
        }
    }
}


