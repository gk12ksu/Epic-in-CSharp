using System;

namespace Epic
{
	public class HTR55
	{
		public HTR55 ()
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program estimates peak runoff rates using the SCS TR55
            // extended method
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double[] PIAF = new double[18] {0.0,0.1,0.2,0.3,0.35,0.4,0.45,0.5,0.55,0.6,0.65,0.7,0.75,0.8,0.85,0.9,0.95,1.0};

            double XOX;
            if (PARM.QP > .35){
                XOX = (PARM.QP-.35)/.05+5.0;
            }
            else{
                XOX = PARM.QP/.1+1.0;
            }
            int INT = (int)XOX;
            int INT1 = INT+1;
            double RTO = (PARM.QP-PIAF[INT])/(PIAF[INT1]-PIAF[INT]);
            double X1 = Math.Log(PARM.TC);
            double Y1 = Functions.HQP(ref X1, ref PARM.CQP, ref PARM.ITYP, ref INT);

            double Y;
            if (INT < 17){
                double Y2 = Functions.HQP(ref X1, ref PARM.CQP, ref PARM.ITYP, ref INT1);
                Y = Y1+(Y2-Y1)*RTO;
                Y = Math.Exp(Y);
            }
            else{
                Y = Math.Exp(Y1)*(1.0-RTO);
            }
            PARM.QP = Y*PARM.RFV;
            return;
		}
	}
}

