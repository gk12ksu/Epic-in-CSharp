using System;

namespace Epic
{
	public class HRUNF
	{
		public HRUNF ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program distributes daily rainfall uniformly and
            // furnishes the green and ampt subprogram rain increments of equal
            // volume = DRFV
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double DRFV = 5.0;
            double PT = 0.0;
            //WRITE(KW(1),1)PARM.SCN,PARM.RFV,PARM.REP,PARM.DUR

            while (PT < PARM.RFV){
                PT = PT + DRFV;
                Epic.HGASP(DRFV, PT, Q1, PARM.REP);
                //WRITE(KW(1),1)PT,PARM.QD
            }
            double A = PARM.RFV-PT+DRFV;
            PT = PARM.RFV;
            Epic.HGASP(A,PT,Q1,PARM.REP);
            //WRITE(KW(1),1)PT,PARM.QD
            //WRITE(KW(1),13)IYR,MO,KDA,PARM.SCN,PARM.RFV,PT,PARM.QD,PARM.REP,PARM.DUR
            return;
            //1 FORMAT(1X,10E13.5)
            //13 FORMAT(1X,3I4,10F10.2)
		}
	}
}

