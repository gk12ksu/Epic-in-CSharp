using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program estimates N fixation for legumes.
     * 
     * This file has had its array indicies shifted for C#
     * This file has had its goto statements removed.
     * Last Modified On 7/11/2012
     */
    public partial class Functions
    {
        public static void NFIX()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            if (!(PARM.HUI[PARM.JJK - 1] < .15 || PARM.HUI[PARM.JJK - 1] > .75))
            {
                double SUM = 0.0;
                double TOT = 0.0;
                double ADD = 0.0;
                double RTO;
                int J, L1;
                for (J = 1; J <= PARM.NBSL; J++)
                {
                    PARM.ISL = PARM.LID[J - 1];
                    if (PARM.Z[PARM.ISL - 1] > .3)
                    {
                        L1 = PARM.LID[J - 2];
                        RTO = (.3 - PARM.Z[L1]) / (PARM.Z[PARM.ISL - 1] - PARM.Z[L1 - 1]);
                        SUM = SUM + (PARM.ST[PARM.ISL - 1] - PARM.S15[PARM.ISL - 1]) * RTO;
                        TOT = TOT + (PARM.FC[PARM.ISL - 1] - PARM.S15[PARM.ISL - 1]) * RTO;
                    }
                    else
                    {
                        SUM = SUM + PARM.ST[PARM.ISL - 1] - PARM.S15[PARM.ISL - 1];
                        TOT = TOT + PARM.FC[PARM.ISL - 1] - PARM.S15[PARM.ISL - 1];
                    }
                }

                double X1 = SUM / TOT;
                if (!(X1 <= .25))
                {

                    for (J = 1; J <= PARM.NBSL; J++)
                    {
                        PARM.ISL = PARM.LID[J - 1];
                        if (PARM.Z[PARM.ISL - 1] > PARM.RD[PARM.JJK - 1])
                        {
                            L1 = PARM.LID[J - 2];
                            RTO = (PARM.RD[PARM.JJK - 1] - PARM.Z[L1 - 1] / (PARM.Z[PARM.ISL - 1] - PARM.Z[L1 - 1]));
                            ADD = ADD + PARM.WNO3[PARM.ISL - 1] * RTO;
                        }
                        else
                        {
                            ADD = ADD + PARM.WNO3[PARM.ISL - 1];
                        }
                    }
                    double FXN = 1.5 - .005 * ADD / PARM.RD[PARM.JJK - 1];
                    if (FXN > 0.0)
                    {
                        double FXW = 1.333 * X1 - .333;
                        double FXG = (PARM.HUI[PARM.JJK - 1] - .1) * 5.0;
                        double FXS = 4.0 - 5.0 * PARM.HUI[PARM.JJK - 1];
                        double FXP = Math.Min(FXG, Math.Min(FXS, 1.0));
                        double FIXR = Math.Min(FXW, Math.Min(FXN, 1.0)) * FXP;
                        PARM.WFX = FIXR * PARM.UNO3;
                    }
                }
            }
            PARM.WFX = Math.Max(0.0, PARM.PRMT[6] * PARM.WFX + (1.0 - PARM.PRMT[6]) * PARM.UNO3);
            if (PARM.WFX > PARM.PRMT[67]) PARM.WFX = PARM.PRMT[67];
            PARM.SMM[49, PARM.MO - 1] = PARM.SMM[49, PARM.MO - 1] + PARM.WFX;
            PARM.DFX = PARM.DFX + PARM.WFX;
            PARM.UNO3 = PARM.UNO3 - PARM.WFX;


        }
    }
}