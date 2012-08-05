using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program computes actual N plant uptake from each
     * layer (Uptake = minimum of plant demand and soil supply).
     * 
     * This file has had its array indicies shifted for C#
     * GO TO statements removed
     * Last Modified On 7/8/2012
     */
    public class NAJN
    {
        public NAJN(ref double[] UU, ref double[] AN, ref double DMD, ref double SUPL, ref double AJF, ref double IAJ)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            double SUM, X2;
            SUM = 0.0;

            if (IAJ == 0)
            {
                X2 = DMD / (SUPL + Math.Pow(10, -20));
                if (!(X2 > 1.0 || DMD < 0.0))
                {
                    for (int J = 1; J < PARM.LRD; J++)
                    {
                        int K = PARM.LID[J - 1];
                        UU[K - 1] = UU[K - 1] * X2;
                        SUM = SUM + UU[K - 1];
                    }
                    SUPL = SUM;
                    return;
                }
            }

            X2 = AJF * (DMD - SUPL);
            double X21 = X2;
            for (int J = 1; J < PARM.LRD; J++)
            {
                int K = PARM.LID[J - 1];
                double XX = UU[K - 1] + X2;
                double X1 = AN[K - 1] - .001 * PARM.PRMT[26] * PARM.WT[K - 1];
                if (XX < X1)
                {
                    UU[K - 1] = XX;
                    SUPL = SUPL + X21;
                    return;
                }
                if (X1 > 0.0)
                {
                    X2 = X2 - X1 + UU[K - 1];
                    UU[K - 1] = X1;
                    SUM = SUM + UU[K - 1];
                }
                else
                {
                    UU[K - 1] = 0.0;
                }
            }
            SUPL = SUM;
            return;

        }
    }
}