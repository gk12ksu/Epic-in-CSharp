using System;

namespace Epic
{
	public class HRFEI
	{
		public HRFEI ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program estimates the usle rainfall energy factor, given daily
            // rainfall.
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double ALMN = .02083;
            double AJP = 1.0-Math.Exp(-125.0/(PARM.RFV+5.0));
			int temp = 4;
            PARM.AL5 = Functions.ATRI(ref ALMN, ref PARM.WI[PARM.NWI,PARM.MO],ref AJP, ref temp); 
            double X1 = -2.0*Math.Log(1.0-PARM.AL5);
            double PR = 2.0*PARM.RFV*PARM.AL5+.001;
            if (PARM.REP < Math.Pow(10, -5)) PARM.REP = X1*PARM.RFV+.001;
            PARM.DUR = Math.Min(24.0,4.605/X1);
            PARM.EI = Math.Max(0.0,PARM.RFV*(12.1+8.9*(Math.Log10(PARM.REP)-.4343))*PR/1000.0);
            PR = X1;
            return;
		}
	}
}

