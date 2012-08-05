using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program computes P flux between the labile, active mineral
     * and stable mineral P pools.
     * 
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public class NPMIN
    {
        public NPMIN()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            //!S5 = .1 * SUT * Math.Exp(.115 * PARM.STMP[PARM.ISL - 1] - 2.88);

            double RTO = Math.Min(.8, PARM.PSP[PARM.ISL - 1] / (1.0 - PARM.PSP[PARM.ISL - 1]));
            double RMN = PARM.PRMT[76] * (PARM.AP[PARM.ISL - 1] - PARM.PMN[PARM.ISL - 1] * RTO);
            double X1 = 4.0 * PARM.PMN[PARM.ISL - 1] - PARM.OP[PARM.ISL - 1];
            double ROC;
            if (X1 > 500.0)
                ROC = Math.Pow(10.0,(Math.Log10(PARM.BPC[PARM.ISL - 1]) + Math.Log10(X1)));
            else
                ROC = PARM.BPC[PARM.ISL - 1] * X1;

            ROC = PARM.PRMT[77] * ROC;
            PARM.OP[PARM.ISL - 1] = PARM.OP[PARM.ISL - 1] + ROC;
            PARM.PMN[PARM.ISL - 1] = PARM.PMN[PARM.ISL - 1] - ROC + RMN;
            PARM.AP[PARM.ISL - 1] = PARM.AP[PARM.ISL - 1] - RMN;

        }
    }
}