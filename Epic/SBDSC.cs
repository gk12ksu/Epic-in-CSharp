using System;

namespace Epic
{
	public partial class Functions
	{
		public static void SBDSC (double X1, double X3, ref double F, int J, double II)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program estimates the effect of bulk density
            // on root growth and saturated conductivity as a function of texture
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
             * 8/16/2012    Modified by Paul Cain to make it part of the Functions partial class
             */
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;

            double BD1=X3+.00445*PARM.SAN[J];
            double BD2 = X3+.35+.005*PARM.SAN[J];
            double X2 = Math.Log(.01124*BD1);
            double B2 = (X2-Math.Log(8.0*BD2))/(BD1-BD2);
            double B1 = X2-B2*BD1;
            X2 = B1+B2*X1;
            if (X2<-10.0){
                F = 1.0;
                goto lbl6;
            }
            if (X2>10.0){
                F = .0001;
            }
            else{
                F = X1/(X1+Math.Exp(X2));
            }
      lbl6: if (II>2) return;
            if (PARM.ISAT==0 && PARM.SATC[J]>0.0) return;
            double XC = 100.0-PARM.CLA[J];
            PARM.SATC[J] = 12.7*XC/(XC+Math.Exp(11.45-.097*XC))+1.0;
            PARM.SATC[J] = F*PARM.SATC[J];
            return;
		}
	}
}

