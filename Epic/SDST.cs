using System;

namespace Epic
{
	public partial class Functions
	{
		public static void SDST (ref double[] X, double DG, double DG1, double X1, double X2, int I, int ISL)
		{
            // EPICv0810
		    // Translated by Brian Cain
            // This program has no description

            /* ADDITIONAL CHANGE
             * 8/16/2012    Modified by Paul Cain to make it part of the Functions partial class
             *              and change some parameters types to make it compatible with a function 
             *              call in Main.
             */

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

