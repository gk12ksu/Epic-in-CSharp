using System;

namespace Epic
{
	public class HGASP
	{
		public HGASP (ref double A, ref double PT, ref double Q1, ref double RX)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program solves the green and ampt infiltration EQ
            // assuming F1 is incremented by total rain during DT
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double F1 = PT-PARM.QD;
            double X1 = PARM.SATK;

            if (PARM.STMP[PARM.LID[1]] <- 1.0){
                X1 = .01*X1;
            }

            double ZI = X1*(PARM.SCN/F1+1.0);
            if (RX>ZI){
                Q1 = A*(RX-ZI)/RX;
            }
            else{
                Q1 = 0.0;
            }
            PARM.QD = PARM.QD+Q1;
            return;
		}
	}
}

