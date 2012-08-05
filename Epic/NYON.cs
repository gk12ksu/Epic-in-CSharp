using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * 
     * ADDITIONAL CHANGE
     * 8/1/2012    Modified by Paul Cain to fix build errors
     * 
     * This program predicts the daily organic N and humus loss, given
     * soil loss and enrichment ratio.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public class NYON
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public NYON()
        {
            // USE PARM

            double TOT = PARM.WHPN[PARM.LD1 - 1] + PARM.WHSN[PARM.LD1 - 1] + PARM.WBMN[PARM.LD1 - 1] + PARM.WLMN[PARM.LD1 - 1] + PARM.WLSN[PARM.LD1 - 1];
            PARM.YN = PARM.YEW * TOT;
            double X1 = 1.0 - PARM.YEW;
            PARM.WBMN[PARM.LD1 - 1] = PARM.WBMN[PARM.LD1 - 1] * X1;
            PARM.WHSN[PARM.LD1 - 1] = PARM.WHSN[PARM.LD1 - 1] * X1;
            PARM.WHPN[PARM.LD1 - 1] = PARM.WHPN[PARM.LD1 - 1] * X1;
            PARM.WLSN[PARM.LD1 - 1] = PARM.WLSN[PARM.LD1 - 1] * X1;
            PARM.WLMN[PARM.LD1 - 1] = PARM.WLMN[PARM.LD1 - 1] * X1;
        }
    }
}