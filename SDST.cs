using System;

namespace Epic
{
	public class SDST
	{
		public SDST (ref double[] X, ref double DG, ref double DG1, ref double X1, ref double X2, ref int I, ref int ISL)
		{
            // EPICv0810
		    // Translated by Brian Cain
            // This program has no description

            X = new double[ISL];
			double RTO;
            if (X[I] > 0.0) return;
            if (I == 1){
                X[0] = X1;
                return;
            }
            else{
                double XX = X2*DG;
                if (XX > 10.0){
                    RTO = .0001;
                }
                else{
	                RTO = DG*Math.Exp(-XX)/DG1;
                }
	            X[I] = X[I-1]*RTO;
            }
            return;
		}
	}
}

