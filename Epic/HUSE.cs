using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program is the master water and nutrient use program.
     * Calls HSWU and NUPPO for each soil layer.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/7/2012
     */
    public class HUSE
    {
        public HUSE()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            PARM.LRD = 1;
            int IAR = 0;
            PARM.UX = 0.0;
            PARM.SEP = 0.0;
            double TOT = 0.0;
            double CPWU = 1.0;
            double RGS = 1.0;

            for (int J = 1; J < PARM.NBSL; J++)
            {
                PARM.ISL = PARM.LID[J - 1];
                if (PARM.Z[PARM.ISL - 1] < 1.0) goto lbl5;
                if (IAR > 0) goto lbl6;
                IAR = 1;
                double X3 = 1.0 - PARM.SEP;
                double X4 = PARM.Z[PARM.ISL - 1] - PARM.SEP;
                double RTO = X3 / X4;

                if (PARM.WTBL <= PARM.Z[PARM.ISL - 1])
                {
                    double X1 = PARM.PO[PARM.ISL - 1] * (PARM.Z[PARM.ISL - 1] - PARM.WTBL) / X4;
                    double X2 = PARM.ST[PARM.ISL - 1] - X1;
                    if (PARM.WTBL > 1.0)
                    {
                        PARM.SAT = PARM.SAT + X2 * X3 / (PARM.WTBL - PARM.SEP);
                    }
                    else
                    {
                        PARM.SAT = PARM.SAT + X2 + PARM.PO[PARM.ISL - 1] * (1.0 - PARM.WTBL) / X4;
                    }
                    goto lbl4;
                }
                PARM.SAT = PARM.SAT + RTO * PARM.ST[PARM.ISL - 1];
                lbl4: TOT = TOT + RTO * PARM.PO[PARM.ISL - 1];
                goto lbl6;
                lbl5: PARM.SAT = PARM.SAT + PARM.ST[PARM.ISL - 1];
                TOT = TOT + PARM.PO[PARM.ISL - 1];
                PARM.SEP = PARM.Z[PARM.ISL - 1];
                lbl6: if (PARM.LRD > 1) continue;
                if (PARM.RD[PARM.JJK - 1] > PARM.Z[PARM.ISL - 1])
                {
                    PARM.GX = PARM.Z[PARM.ISL - 1];
                }else{
                    PARM.GX = PARM.RD[PARM.JJK - 1];
                    PARM.LRD = J;
                }
                Epic.HSWU(CPWU, RGS);
                PARM.SU = PARM.SU + PARM.U[PARM.ISL - 1];
            }
            if (PARM.LRD == 0) PARM.LRD = PARM.NBSL;
            if (PARM.RZSW > PARM.PAW)
            {
                double RTO = Math.Min(1.0, PARM.SAT / TOT);
                double F = 100.0 * (RTO - PARM.CAF[PARM.JJK - 1]) / (1.0001 - PARM.CAF[PARM.JJK - 1]);
                if (F > 0.0)
                {
                    PARM.SAT = 1.0 - F / (F + Math.Exp(PARM.SCRP[6, 0] - PARM.SCRP[6, 1] * F));
                    return;
                }
            }
            else
            {
                PARM.SAT = 1.0;
            }



        }
    }
}