using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program developed by Armen Kemanian estimates daily
     * denitrification and N2O losses of soil NO3.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public class NDNITAK
    {
        public NDNITAK()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            PARM.WDN = 0.0;
	        PARM.DN2O = 0.0;
            //COMPUTE WATER FACTOR
	        double WFP = PARM.ST[PARM.ISL - 1] / PARM.PO[PARM.ISL - 1];
	        double AIRV = Math.Max(0.0, (PARM.PO[PARM.ISL - 1] - PARM.ST[PARM.ISL - 1]) / PARM.DZ);
	        double X1 = 0.90 + .001 * PARM.CLA[PARM.ISL - 1];
	        double X2 = (1.0001 - AIRV) / X1;
	        if (X2 < .8)
                return;
	        double H2O_F = 1.0 / (1.0 + Math.Pow(X2, -60));
            //COMPUTE NITRATE FACTOR
	        double ONO3_C = Math.Max(Math.Pow(10, -5), 1000.0 * PARM.WNO3[PARM.ISL - 1] / PARM.WT[PARM.ISL - 1]);
 	        double ONO3_F = ONO3_C / (ONO3_C + 60.0);
            //COMPUTE RESPIRATION FACTOR
	        double X3 = 1000.0 * PARM.RSPC[PARM.ISL - 1] / PARM.WT[PARM.ISL - 1];
	        double C_F = Math.Min(1.0, X3 / 50.0);
	        double D_F = ONO3_F * H2O_F * C_F;
	        double DNITMX = 32.0;
	        PARM.WDN = D_F * DNITMX * PARM.WT[PARM.ISL - 1] / 1000.0;
	        if (PARM.WDN > PARM.WNO3[PARM.ISL - 1]) 
                PARM.WDN = PARM.WNO3[PARM.ISL - 1];
	        //compute N2O as a fraction of WDN
            PARM.DN2O = ONO3_F * (1.0 - Math.Sqrt(H2O_F)) * (1.0 - Math.Pow(C_F, .25)) * PARM.WDN;
            PARM.WNO3[PARM.ISL - 1] = PARM.WNO3[PARM.ISL - 1] - PARM.WDN;
        }
    }
}