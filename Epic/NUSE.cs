using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program calculates the daily potential soil supply of P
     * for each layer.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/11/2012
     */
    public class NUSE
    {
        public NUSE()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double XX=1.5*PARM.UPP/PARM.RW[PARM.JJK-1];
            for (int J = 1; J < PARM.LRD; J++)
            {
                PARM.ISL = PARM.LID[J - 1];
                PARM.UN[PARM.ISL - 1] = Math.Max(0.0, (PARM.WNO3[PARM.ISL - 1] - .001 * PARM.PRMT[26] * PARM.WT[PARM.ISL - 1]) * PARM.U[PARM.ISL - 1] / (PARM.ST[PARM.ISL - 1] + .001));
                PARM.SUN = PARM.SUN + PARM.UN[PARM.ISL - 1];
                PARM.UK[PARM.ISL - 1] = PARM.SOLK[PARM.ISL - 1] * PARM.U[PARM.ISL - 1] / (PARM.ST[PARM.ISL - 1] + .001);
                PARM.SUK = PARM.SUK + PARM.UK[PARM.ISL - 1];
                double F = 1000.0 * PARM.AP[PARM.ISL - 1] / PARM.WT[PARM.ISL - 1];
                if (F > 30.0)
                {
                    F = 1.0;
                }
                else
                {
                    F = F / (F + Math.Exp(8.0065 - .3604 * F));
                }
                PARM.UP[PARM.ISL - 1] = XX * F * PARM.RWT[PARM.ISL - 1, PARM.JJK - 1];
                if (PARM.UP[PARM.ISL - 1] >= PARM.AP[PARM.ISL - 1])
                    PARM.UP[PARM.ISL - 1] = .9 * PARM.AP[PARM.ISL - 1];
                PARM.SUP = PARM.SUP + PARM.UP[PARM.ISL - 1];
            }

        }
    }
}