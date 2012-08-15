using System;

namespace Epic
{
	public class HFURD
	{
		public HFURD ()
		{
            // EPICv0810
			// Translated by Brian Cain 
            // This program computes the storage volume of furrow dikes
            // given dike interval and height, ridge height, and slope.

            // The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double DH = PARM.DHT*.001;
            double H2 = 2.0*DH;
            double X1 = .001*PARM.DKHL;
            double TW = PARM.RGIN-X1;
            double BW = Math.Max(TW-4.0*X1,.1*TW);
            double DI = PARM.DKIN-X1;
            double D2 = DH*(1.0-2.0*PARM.UPS);
            double D3 = DH-PARM.UPS*(DI-H2);
            X1 = (TW-BW)/DH;
            double TW2 = BW+D2*X1;
            double TW3 = BW+D3*X1;
            double A2 = .5*D2*(TW2+BW);
            double A3 = .5*D3*(TW3+BW);
            double XX = DH/PARM.UPS;
            double ZZ = DI-H2;
            X1 = PARM.FDSF/(PARM.RGIN*PARM.DKIN);

            if (XX > ZZ){
                PARM.DV = X1*(A2*DH+.5*(A2+A3)*(DI-4.0*DH)+A3*D3);
            }
            else{
                PARM.DV = X1*A2*(DH+.5*(XX-H2));
            }
            return;
		}
	}
}

