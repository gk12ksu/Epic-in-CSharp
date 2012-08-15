using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program estimates upward NO3 movement caused by soil evaporation
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public partial class Functions
    {
        public static void NEVN()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            for (int J = 1; J <= PARM.NBSL; J++)
            {
                PARM.ISL = PARM.LID[J - 1];
            }
            if (PARM.NEV == 1)
                return;
            double TOT = 0.0;
            for (int J = PARM.NEV; J >= 2; J--)
            {
                PARM.ISL = PARM.LID[J - 1];
                double X1 = PARM.WNO3[PARM.ISL - 1] - .001 * PARM.WT[PARM.ISL - 1] * PARM.PRMT[26];
                if (X1 <= .01)
                    continue;
	            double X2 = 1.0 - Math.Exp(-PARM.PRMT[61] * PARM.SEV[PARM.ISL - 1] / (PARM.STFR[PARM.ISL - 1] * PARM.PO[PARM.ISL - 1]));
	            double XX = X1 * X2;
                TOT = TOT + XX;
                PARM.WNO3[PARM.ISL - 1] = PARM.WNO3[PARM.ISL - 1] - XX;
            }
            PARM.WNO3[PARM.LD1 - 1] = PARM.WNO3[PARM.LD1 - 1] + TOT;
        }
    }
}