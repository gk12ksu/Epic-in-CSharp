using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program estimates daily NO3 leaching by percolation and
     * lateral subsurface flow for all layers except the surface layer.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public partial class Functions
    {
        public static void NLCH()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            PARM.AP[PARM.ISL - 1] = PARM.AP[PARM.ISL - 1] + PARM.VAP;
            PARM.WNO3[PARM.ISL - 1] = PARM.WNO3[PARM.ISL - 1] + PARM.VMN;
            PARM.SOLK[PARM.ISL - 1] = PARM.SOLK[PARM.ISL - 1] + PARM.VSK;
            PARM.WSLT[PARM.ISL - 1] = PARM.WSLT[PARM.ISL - 1] + PARM.VSLT;
            PARM.VMN = 0.0;
            PARM.VAP = 0.0;
            PARM.VSK = 0.0;
            PARM.VSLT = 0.0;
            PARM.SSO3[PARM.ISL - 1] = 0.0;
            PARM.SSFN = 0.0;
            PARM.SSFK = 0.0;
            PARM.SSST = 0.0;
            double V = PARM.PKRZ[PARM.ISL - 1] + PARM.SSF[PARM.ISL - 1] + 1.0E-10;
            if (V < 1.0E-5)
                return;
            double X2 = 1.0 - Math.Exp(-V / (PARM.STFR[PARM.ISL - 1] * PARM.PO[PARM.ISL - 1]));
            double X1 = PARM.WNO3[PARM.ISL - 1] - .001 * PARM.WT[PARM.ISL - 1] * PARM.PRMT[26];

            if (X1 > 0.0)
            {
                double VNO3 = X1 * X2;
                double VV = VNO3 / V;
                PARM.WNO3[PARM.ISL - 1] = PARM.WNO3[PARM.ISL - 1] - VNO3;
                PARM.SSO3[PARM.ISL - 1] = VV * PARM.PKRZ[PARM.ISL - 1];
                PARM.SSFN = VV * PARM.SSF[PARM.ISL - 1];
                PARM.VMN = PARM.SSO3[PARM.ISL - 1];
            }
            if (PARM.SOLK[PARM.ISL - 1] > 0.0)
            {
                double X3 = PARM.SOLK[PARM.ISL - 1] * X2;
                PARM.SOLK[PARM.ISL - 1] = PARM.SOLK[PARM.ISL - 1] - X3;
                PARM.VSK = X3 * PARM.PKRZ[PARM.ISL - 1] / V;
                PARM.SSFK = X3 - PARM.VSK;
            }
            if (PARM.WSLT[PARM.ISL - 1] > 1.0E-5)
            {
                double X3 = PARM.WSLT[PARM.ISL - 1] * X2;
                PARM.WSLT[PARM.ISL - 1] = PARM.WSLT[PARM.ISL - 1] - X3;
                PARM.VSLT = X3 * PARM.PKRZ[PARM.ISL - 1] / V;
                PARM.SSST = X3 - PARM.VSLT;
            }
            if (PARM.AP[PARM.ISL - 1] < 1.0E-5)
                return;
            double XX = Math.Min(.75, PARM.PKRZ[PARM.ISL - 1] / PARM.WT[PARM.ISL - 1]);
            PARM.VAP = XX * PARM.AP[PARM.ISL - 1];
            PARM.AP[PARM.ISL - 1] = PARM.AP[PARM.ISL - 1] - PARM.VAP;

        }
    }
}