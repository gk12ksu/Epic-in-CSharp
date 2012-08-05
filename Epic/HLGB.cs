using System;

namespace Application
{
	public class HLGB
	{
		public HLGB (ref double Q, ref double EV, ref double O, ref double RG, ref double VLGE, ref double VLGB, ref double WW, ref double[] KW, ref int MSO)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program checks the lagoon water balance at the end
            // of the simulation
            
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            KW = new double[MSO+3];
            //WRITE(KW(1),'(T10,A)')'LAGOON WATER BALANCE'
            double DF = VLGB+Q-EV-O-VLGE-RG+WW;
            double PER = 200.0*DF/(VLGB+VLGE);
            //WRITE(KW(1),3)DF,VLGB,Q,EV,O,PARM.VLGE,RG,WW
            VLGB = VLGE;
            return;
            //3 FORMAT(8E16.6)
		}
	}
}

