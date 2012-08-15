using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program estimates upward soluble P movement caused by soil evaporation
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public partial class Functions
    {
        public static void NEVP()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            if (PARM.NEV==1)
                return;
            double TOT = 0.0;

            for (int J = PARM.NEV; J >= 2; J--)
            {
                PARM.ISL = PARM.LID[J - 1];
                if (PARM.AP[PARM.ISL - 1] < .001)
                    continue;
                double XX = PARM.AP[PARM.ISL - 1] * Math.Min(.75, PARM.PRMT[42] * PARM.SEV[PARM.ISL - 1] / PARM.WT[PARM.ISL - 1]);
                TOT = TOT + XX;
                PARM.AP[PARM.ISL - 1] = PARM.AP[PARM.ISL - 1] - XX;
            }
            PARM.AP[PARM.LD1 - 1] = PARM.AP[PARM.LD1 - 1] + TOT;

        }
    }
}