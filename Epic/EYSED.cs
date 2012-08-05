using System;

namespace Epic
{

    public class EYSED
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public EYSED()
        {
            //     EPIC0810
            //     Translated by Heath Yates 

            /* ADDITIONAL CHANGE
            * 8/1/2012    Modified by Paul Cain to fix build errors
            */

            //     THIS SUBPROGRAM PREDICTS DAILY SOIL LOSS CAUSED BY WATER EROSION
            //     AND ESTIMATES THE NUTRIENT ENRICHMENT RATIO.
            //     USE PARM
            new ERSC();
            new EYCC();
            double CVX = PARM.CVF;
            double X1;
            double B;
            double RXM;
            double RLFX;
            double YI;
            double SUM;
            double T2;
            double QQ;
            double B1;
            double B2;
            double XX;

            if (PARM.ICF == 0) CVX = PARM.SLR;
            double F = 1.0; //Had difficulty finding this. Not sure if this in Modparam.cs or not. 
            XX = PARM.STMP[PARM.LID[2]];
            if (XX <= 0.0)
            {
                XX = 273.0 + XX;
                F = XX / (XX + Math.Exp(PARM.SCRP[18, 1] - PARM.SCRP[18, 2] * XX));
            }
            XX = CVX * PARM.USL * F;
            double YLM = 100.0 * PARM.RFV;
            if (PARM.RFV > 12.7)
            {
                PARM.YSD[3] = Math.Min(YLM, PARM.EI * XX * 1.292);
                PARM.SMM[29, PARM.MO] = PARM.SMM[29, PARM.MO] + PARM.CVF * PARM.EI;
                PARM.SMM[28, PARM.MO] = PARM.SMM[28, PARM.MO] + PARM.EI;
                X1 = PARM.SLR * PARM.EI;
                PARM.SMM[37, PARM.MO] = PARM.SMM[37, PARM.MO] + X1;
                PARM.YSD[7] = Math.Min(YLM, X1 * PARM.RSLK);
                if (PARM.YSD[7] < 1 * Math.Pow(10, -10)) goto two;
                B = PARM.BETA;
                RXM = B / (1.0 + B);
                RLFX = Math.Pow(PARM.UPSX, RXM);
                PARM.SMM[90, PARM.MO] = PARM.SMM[90, PARM.MO] + RLFX * PARM.RSF * PARM.EI;
                PARM.SMM[91, PARM.MO] = PARM.SMM[91, PARM.MO] + PARM.REK * PARM.EI;
                YI = Math.Min(YLM, .5 * X1 * PARM.RSK);
                SUM = PARM.PSZ[1] * PARM.SAN[PARM.LD1];
                SUM = SUM + PARM.PSZ[2] * PARM.SIL[PARM.LD1];
                SUM = SUM + PARM.PSZ[3] * PARM.CLA[PARM.LD1];
                SUM = .01 * PARM.PRMT[71] * SUM / (PARM.QPR + 1.E - 5);
                T2 = PARM.PRMT[72] * PARM.QPR * PARM.UPS;
                if (T2 > YI)
                {
                    PARM.YSD[8] = 1.5 * X1 * RLFX * PARM.RSK;
                    PARM.RUSM[1] = PARM.RUSM[1] + PARM.YSD[8];
                }
                else
                {
                    PARM.YSD[8] = Math.Max(0.0, YI + SUM * (T2 - YI));
                    PARM.RUSM[2] = PARM.RUSM[2] + PARM.YSD[8];
                    PARM.RUSM[3] = PARM.RUSM[3] + YI;
                }
            }
        two: if (PARM.QD < 1.0) return;
            PARM.REP = PARM.REP * Math.Pow((PARM.QD / PARM.RWO), 0.1);
            PARM.YSD[2] = Math.Min(YLM, (0.646 * PARM.EI + 0.45 * PARM.QD * Math.Pow(PARM.QP, 0.3333) * XX));
            QQ = PARM.QD * PARM.QP;
            PARM.YSD[4] = Math.Min(YLM, PARM.YSW * Math.Pow(QQ, 0.65) * XX);
            PARM.YSD[1] = Math.Min(YLM, 2.5 * Math.Sqrt(QQ) * XX);
            PARM.YSD[6] = Math.Min(YLM, PARM.BUS[1] * Math.Pow(PARM.QD, PARM.BUS[2]) * Math.Pow(PARM.QP, PARM.BUS[3]) * XX);
            PARM.CX[PARM.MO] = PARM.CX[PARM.MO] + 1.0;
            PARM.TAL[PARM.MO] = PARM.TAL[PARM.MO] + PARM.AL5;
            PARM.YSD[5] = Math.Min(YLM, PARM.WSA1 * Math.Pow(QQ, .56) * XX);
            PARM.DR = Math.Sqrt(PARM.QP / PARM.REP);
            double CY = 0.1 * PARM.YSD[4] / PARM.QD + (1 * Math.Pow(10, -1));
            if (PARM.IERT > 0)
            {
                PARM.ER = .78 * Math.Pow(CY, (-.2468));
            }
            else
            {
                double DR1 = 1.0 / PARM.DR;
                B2 = -1 * (Math.Log10(DR1) / 2.699);
                B1 = 1.0 / Math.Pow(0.1, B2);
                PARM.ER = Math.Max(1.0, B1 * Math.Pow(CY + (1 * Math.Pow(10, -4)), B2));
                PARM.ER = Math.Min(PARM.ER, 3.0);
            }
            PARM.SMM[58, PARM.MO] = PARM.SMM[58, PARM.MO] + PARM.ER;
            return;
        }
    }
}

