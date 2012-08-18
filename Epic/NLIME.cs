using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program applies lime when the sum of the soil lime requirement
     * and accumulated lime requirement caused by N fertilizer excess
     * 4 t/ha
     * 
     * 
     * This file has had its array indicies shifted for C#
     * This file has had its gotos statements removed.
     * Last Modified On 7/10/2012
     */
    public partial class Functions
    {
        public static void NPMIN()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double SMFN = 0.0;
            double OC = 0.0;
            double TOT = 0.0;
            double XZ = 0.0;
            double ZZ = 0.0;
            double XX = 0.0;
            double XY, X1, RTO, W2, W3;
            int J;
            bool skip = false;
            for (J = 1; J <= PARM.NBSL; J++)
            {
                PARM.ISL = PARM.LID[J - 1];
                if (PARM.Z[PARM.ISL - 1] > PARM.BIG)
                {
                    int L1 = PARM.LID[J - 2];
                    W3 = PARM.Z[PARM.ISL - 1] - PARM.Z[L1 - 1];
                    W2 = PARM.BIG - PARM.Z[L1 - 1];
                    RTO = W2 * PARM.WT[PARM.ISL - 1] / W3;
                    X1 = .1 * PARM.WOC[PARM.ISL - 1] / PARM.WT[PARM.ISL - 1];
                    OC = OC + RTO * X1;
                    TOT = TOT + RTO * PARM.PH[PARM.ISL - 1];
                    ZZ = ZZ + RTO * PARM.SMB[PARM.ISL - 1];
                    XZ = XZ + RTO * PARM.CEC[PARM.ISL - 1];
                    XX = XX + RTO;
                    skip = true;
                }
                else
                {
                    XY = PARM.WT[PARM.ISL - 1];
                    OC = OC + .1 * PARM.WOC[PARM.ISL - 1];
                    XZ = XZ + PARM.CEC[PARM.ISL - 1] * XY;
                    TOT = TOT + PARM.PH[PARM.ISL - 1] * XY;
                    ZZ = ZZ + PARM.SMB[PARM.ISL - 1] * XY;
                    XX = XX + XY;
                }
            }
            if (skip == false)
            {
                J = PARM.NBSL;
                PARM.ISL = PARM.LID[PARM.NBSL - 1];
            }

            XZ = XZ / XX;
            OC = OC / XX;
            TOT = TOT / XX;
            ZZ = ZZ / XX;
            XY = .001 * XX;
            X1 = PARM.SMY[59] + PARM.SMY[60];
            double DSB = .036 * (PARM.SMY[49] + X1 - SMFN) / XX;
            SMFN = X1;
            double BS = 100.0 / XZ;
            TOT = TOT - .05 * DSB * BS;

            double ALSX, BSA, DBS;
            Epic.NLIMA(ZZ, DSB, BS, TOT, ref ALSX, OC, ref BSA);
            skip = false;
            double ALN, PHN;

            if (TOT > 6.5)
            {
                PARM.TLA = 0.0;
                ALN = ALSX;
                PHN = TOT;
                BSA = ZZ;
                skip = true;
            }
            if (PARM.IDSP == 4 && !skip)
            {
                double EAL = .01 * ALSX * XZ;
                PARM.TLA = EAL * XY;
                if (PARM.TLA < 1.0)
                {
                    PARM.TLA = 0.0;
                    ALN = ALSX;
                    PHN = TOT;
                    BSA = ZZ;
                    skip = true;
                }
                else
                {
                    TOT = 5.4;
                    Epic.NLIMA(ZZ, -EAL, BS, TOT, ref ALSX, OC, ref BSA);
                    ALN = ALSX;
                    PHN = TOT;
                    BSA = ZZ;
                    skip = true;
                }
            }
            if (!skip)
            {
                DBS = Math.Min((6.5 - TOT) / .023, 90.0 - BSA);
                ALN = 0.0;
                PARM.TLA = DBS * XY / BS;
            }
            if (PARM.TLA > 2.0 && !skip)
            {
                RTO = 2.0 / PARM.TLA;
                PARM.TLA = 2.0;
            }
            else
            {
                if (TOT > 5.0)
                {
                    PARM.TLA = 0.0;
                    ALN = ALSX;
                    PHN = TOT;
                    BSA = ZZ;
                    skip = true;
                }
                else
                {
                    RTO = 1.0;
                }
            }
            if (!skip)
            {
                PHN = (6.5 - TOT) * RTO + TOT;
                DBS = Math.Min((PHN - TOT) / .023, 90.0 - BSA);
                BSA = (BSA + DBS) / BS;
            }


            for (int K = 1; K <= J; K++)
            {
                PARM.ISL = PARM.LID[K - 1];
                TOT = PARM.SMB[PARM.ISL - 1];
                XZ = PARM.PH[PARM.ISL - 1];
                ALSX = PARM.ALS[PARM.ISL - 1];
                PARM.SMB[PARM.ISL - 1] = BSA;
                PARM.PH[PARM.ISL - 1] = PHN;
                PARM.ALS[PARM.ISL - 1] = ALN;
            }
            if (J == PARM.NBSL)
                return;

            PARM.ISL = PARM.LID[J - 1];
            double W1 = PARM.Z[PARM.ISL - 1] - PARM.BIG;
            PARM.SMB[PARM.ISL - 1] = (W1 * TOT + W2 * PARM.SMB[PARM.ISL - 1]) / W3;
            PARM.PH[PARM.ISL - 1] = (W1 * XZ + W2 * PARM.PH[PARM.ISL - 1]) / W3;
            PARM.ALS[PARM.ISL - 1] = Math.Max(.001, (W1 * ALSX + W2 * PARM.ALS[PARM.ISL - 1]) / W3);

        }
    }
}