using System;

namespace Epic
{
	public partial class Functions
	{
		public static void HCNSLP (ref double CNII, ref double X1)
		{
            // EPICv0810
			// Translated by Brian Cain
            // This program adjusts the 2 condition SCS runoff curve
            // number for watershed slope and computes CN1 and CN3
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables

            /* ADDITIONAL CHANGE
             * 8/17/2012    Modified by Paul Cain to make it part of the Functions partial class
             */
			
			Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
            
            double C2 = 100.0-CNII;
            double CN3 = CNII*Math.Exp(.006729*C2);
            X1 = (CN3-CNII)*PARM.UPSQ+CNII;
            C2 = 100.0-X1;
            double CN1 = Math.Max(.4*X1,X1-20.0*C2/(C2+Math.Exp(2.533-.0636*C2)));
            double SMX = 254.0*(100.0/CN1-1.0);
            CN3 = X1*Math.Exp(.006729*C2);
            double S3 = 254.0*(100.0/CN3-1.0);
            double S2 = 254.0*(100.0/X1-1.0);
            double SUM=0.0;
            double TOT=0.0;

            int J;
			double N1, Z1 = 0.0, Z2 = 0.0;
			int ISL;
            for (J = 0; J < PARM.NBSL; J++){
                ISL = PARM.LID[J];
                if (PARM.Z[ISL] > 1.0){
                    // Removed Go To Statement here
                    int L1 = PARM.LID[J-1];
                    double RTO = (1.0-PARM.Z[L1])/(PARM.Z[ISL]-PARM.Z[L1]);
                    SUM = SUM+RTO*(PARM.FC[ISL]-PARM.S15[ISL]);
                    TOT = TOT+RTO*(PARM.PO[ISL]-PARM.S15[ISL]);
                    N1 = 100.0+PARM.SCRP[29,1]*(TOT/SUM-1.0)+.5;
                    PARM.SCRP[3,0] = 1.0-S2/SMX+PARM.SCRP[29,0];
                    PARM.SCRP[3,1] = 1.0-S3/SMX+N1;
                    Z1 = Functions.ASPLT(ref PARM.SCRP[3,0]);
                    Z2 = Functions.ASPLT(ref PARM.SCRP[3,1]);
                    Functions.ASCRV(ref PARM.SCRP[3,0], ref PARM.SCRP[3, 1], ref Z1, ref Z2);
                    return;
                }
                else{
                    SUM = SUM+PARM.FC[ISL]-PARM.S15[ISL];
                    TOT = TOT+PARM.PO[ISL]-PARM.S15[ISL];
                }
            }
            N1 = 100.0+PARM.SCRP[29,1]*(TOT/SUM-1.0)+.5;
            PARM.SCRP[3,0] = 1.0-S2/SMX+PARM.SCRP[29,0];
            PARM.SCRP[3,1] = 1.0-S3/SMX+N1;
            double Z1_n = Functions.ASPLT(ref PARM.SCRP[3,0]);
            double Z2_n = Functions.ASPLT(ref PARM.SCRP[3,1]);
            Functions.ASCRV(ref PARM.SCRP[3,0], ref PARM.SCRP[3, 1], ref Z1, ref Z2);
            return;
		}
	}
}

