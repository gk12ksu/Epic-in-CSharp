using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program computes K flux between the water soluble,
     * exchangeable, & fixed pools
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public partial class Functions
    {
        public static void NKMIN()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double RSE = (PARM.SOLK[PARM.ISL - 1] - PARM.EXCK[PARM.ISL - 1] * PARM.EQKS[PARM.ISL - 1]) * PARM.PRMT[28];
            double REF = (PARM.EXCK[PARM.ISL - 1] - PARM.FIXK[PARM.ISL - 1] * PARM.EQKE[PARM.ISL - 1]) * PARM.PRMT[21];
            PARM.SOLK[PARM.ISL - 1] = Math.Max(.0001, PARM.SOLK[PARM.ISL - 1] - RSE);
            PARM.EXCK[PARM.ISL - 1] = Math.Max(.0001, PARM.EXCK[PARM.ISL - 1] + RSE - REF);
            PARM.FIXK[PARM.ISL - 1] = PARM.FIXK[PARM.ISL - 1] + REF;
        }
    }
}