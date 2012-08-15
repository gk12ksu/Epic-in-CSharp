using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program estimates daily loss of NO3 by denitrification.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/8/2012
     */
    public class NDNIT
    {
        public NDNIT()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double RTO = 100.0 * (PARM.ST[PARM.ISL - 1] - PARM.FC[PARM.ISL - 1]) / (PARM.PO[PARM.ISL - 1] - PARM.FC[PARM.ISL - 1]);
            double F = RTO / (RTO + Math.Exp(PARM.SCRP[24, 0] - PARM.SCRP[24, 1] * RTO));
	        double X1 = Math.Min(PARM.PRMT[3], 1.0 - Math.Exp(-PARM.CDG * PARM.WOC[PARM.ISL - 1] / PARM.WT[PARM.ISL - 1]));
            PARM.WDN = PARM.WNO3[PARM.ISL - 1] * X1 * F;
	        if (PARM.WDN > PARM.WNO3[PARM.ISL - 1]) PARM.WDN = PARM.WNO3[PARM.ISL - 1];
            PARM.WNO3[PARM.ISL - 1] = PARM.WNO3[PARM.ISL - 1] - PARM.WDN;
        }
    }
}