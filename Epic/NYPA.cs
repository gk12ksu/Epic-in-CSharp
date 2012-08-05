using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program predicts daily P loss, given soil loss and
     * enrichment ratio.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public class NYPA
    {
        public NYPA()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            PARM.YP = PARM.YEW * PARM.WP[PARM.LD1 - 1];
            PARM.WP[PARM.LD1 - 1] = PARM.WP[PARM.LD1 - 1] - PARM.YP;
            double X2 = PARM.AP[PARM.LD1 - 1];
            double YAP = PARM.YEW * X2;
            X2 = X2 - YAP;
            double X1 = PARM.EXCK[PARM.LD1 - 1] * PARM.YEW;
            PARM.EXCK[PARM.LD1 - 1] = PARM.EXCK[PARM.LD1 - 1] - X1;
            double X3 = PARM.FIXK[PARM.LD1 - 1] * PARM.YEW;
            PARM.FIXK[PARM.LD1 - 1] = PARM.FIXK[PARM.LD1 - 1] - X3;
            PARM.VAR[77] = X1 + X3;
            PARM.SMM[77, PARM.MO - 1] = PARM.SMM[77, PARM.MO - 1] + X1 + X3;
            double V = PARM.QD + PARM.PKRZ[PARM.LD1 - 1];
            X1 = Math.Max(5.0 * V, PARM.WT[PARM.LD1 - 1] * PARM.PRMT[7]);
            if (PARM.QD > 0.0)
            {
                if (PARM.LBP > 0)
                {
                    double RTO = Math.Pow((10.0 * PARM.WP[PARM.LD1 - 1] / PARM.WT[PARM.LD1 - 1]), PARM.PRMT[33]);
                    PARM.QAP = Math.Min(.5 * X2, X2 * PARM.QD * RTO / X1);
                }else{
                    PARM.QAP = X2 * PARM.QD / X1;
                }
                X2 = X2 - PARM.QAP;
            }
            PARM.VAP = Math.Min(.5 * X2, X2 * PARM.PKRZ[PARM.LD1 - 1] / X1);
            PARM.AP[PARM.LD1 - 1] = X2 - PARM.VAP;
            double YMP = PARM.PMN[PARM.LD1 - 1] * PARM.YEW;
            PARM.PMN[PARM.LD1 - 1] = PARM.PMN[PARM.LD1 - 1] - YMP;
            PARM.YP = PARM.YP + YMP + YAP;

        }
    }
}