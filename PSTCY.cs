using System;

namespace Epic
{
    public class PSTCY
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public PSTCY()
        {
            // EPIC0810
            // Translated by Paul Cain
            // THIS SUBPROGRAM SIMULATES PESTICIDE TRANSPORT & DEGRADATION

            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            //Translator's Note: For now, I kept all of the array indices as 
            //	the same numbers they were in their original fortran files  
            //	even though C# use base 0 arrays and fortran use base 1 
            //	arrays. I did this to avoid confusion until we have a better
            // 	understanding of how the program works. 

            // USE PARM
            int[] NXP = new int[90] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,
				17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,
				38,39,40,41,42,43,4,45,46,47,48,49,50,51,52,53,54,55,56,57,58,
				59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,
				79,80,81,82,83,84,85,6,87,88,89,90};
            int[] NY = new int[5] { 1, 4, 21, 60, 90 };
            int II = NXP[90];
            double QQ = PARM.QD + PARM.SST;
            double Y2 = PARM.YSD[PARM.NDRV];
            PARM.SQB[1] = QQ;
            PARM.SYB[1] = Y2;
            for (int I = 2; I <= 5; I++)
            {
                PARM.SQB[I] = PARM.SQB[I] + QQ - PARM.VQ[NXP[NY[I]]];
                PARM.SYB[I] = PARM.SYB[I] + Y2 - PARM.VY[NXP[NY[I]]];
            }
            for (int K = 1; K <= PARM.NDP; K++)
            {
                double ADD = 0.0;
                double SUM = 0.0;
                double TOT = 0.0;
                double X3 = 0.0;
                double PQ = 0.0;
                double PY = 0.0;
                double Y1 = PARM.PSTZ[K, PARM.LD1];
                if (PARM.IGO > 0)
                {
                    if (PARM.PFOL[K] > 0.01)
                    {
                        if (PARM.RWO > 2.54)
                        {
                            //!                         COMPUTE PESTICIDE WASH OFF FROM FOLIAGE
                            double WO = PARM.PWOF[K] * PARM.PFOL[K];
                            PARM.PFOL[K] = PARM.PFOL[K] - WO;
                            Y1 = Y1 + WO;
                        }
                        //  !                   COMPUTE PESTICIDE DEGRADATION FROM FOLIAGE
                        double DGF = PARM.PFOL[K] * PARM.PHLF[K];
                        PARM.PFOL[K] = PARM.PFOL[K] - DGF;
                        PARM.SMMP[6, K, PARM.MO] = PARM.SMMP[6, K, PARM.MO] + DGF;
                        PARM.VARP[6, K] = DGF;
                    }
                    else
                    {
                        PARM.PFOL[K] = 0.0;
                    }
                }
                //!             COMPUTE PESTICIDE LOSS FROM TOP SOIL LAYER IN RUNOFF,
                //!             LATERAL SUBSURFACE FLOW, & PERCOLATION
                if (Y1 > 0.01)
                {
                    double DK = 0.0001 * PARM.PKOC[K] * PARM.WOC[PARM.LD1];
                    double X1 = PARM.PO[PARM.LD1] - PARM.S15[PARM.LD1];
                    double XX = X1 + DK;
                    double V = PARM.QD + PARM.SSF[PARM.LD1] + PARM.PKRZ[PARM.LD1];
                    if (V > 0.0)
                    {
                        double VPST = Y1 * (1.0 - Math.Pow(Math.E, (-1.0 * V) / XX));
                        double CO = Math.Min(PARM.PSOL[K], VPST / (PARM.PKRZ[PARM.LD1] + PARM.PRMT[18] * (PARM.QD + PARM.SSF[PARM.LD1])));
                        double CS = PARM.PRMT[18] * CO;
                        X3 = CO * PARM.PKRZ[PARM.LD1];
                        PQ = CS * PARM.QD;
                        PARM.SMMP[2, K, PARM.MO] = PARM.SMMP[2, K, PARM.MO] + PQ;
                        PARM.VARP[2, K] = PQ;
                        SUM = CS * PARM.SSF[PARM.LD1];
                        Y1 = Y1 - X3 - PQ - SUM;
                        //Translator's Note: These were commented out in the original Fortran program.
                        //  !                   WRITE(KW(1),3)IYR,MO,KDA,QD,SSF(LD1),Y1,VPST,CO,SUM,PQ
                        //!                     COMPUTE PESTICIDE LOSS WITH SEDIMENT
                        if (PARM.YEW > 0.0)
                        {
                            CS = DK * Y1 / XX;
                            PY = PARM.YEW * CS;
                            PARM.SMMP[5, K, PARM.MO] = PARM.SMMP[5, K, PARM.MO] + PY;
                            PARM.VARP[5, K] = PY;
                            Y1 = Y1 - PY;
                        }
                    }
                    //!                 COMPUTE PESTICIDE DEGRADATION IN TOP SOIL LAYER
                    double DGS = Y1 * PARM.PHLS[K];
                    Y1 = Y1 - DGS;
                    TOT = DGS;
                    ADD = Y1;
                }
                else
                {
                    Y1 = 0.0;
                }
                PARM.PSTZ[K, PARM.LD1] = Y1;
                //!             COMPUTE PESTICIDE MOVEMENT THRU SOIL LAYERS BY LATERAL
                //!             SUBSURFACE FLOW & PERCOLATION
                //!             COMPUTE PESTICIDE MOVEMENT THRU SOIL LAYERS BY LATERAL
                double X2 = 0.0;
                for (int L1 = 2; L1 <= PARM.NBSL; L1++)
                {
                    PARM.ISL = PARM.LID[L1];
                    Y1 = PARM.PSTZ[K, PARM.ISL];
                    Y1 = Y1 + X3;
                    X3 = 0.0;
                    if (Y1 > 0.01)
                    {
                        double V = PARM.PKRZ[PARM.ISL] + PARM.SSF[PARM.ISL];
                        if (V > 0.0)
                        {
                            double VPST = Y1 * (1.0 - Math.Pow(Math.E, (-1.0 * V) / (PARM.PO[PARM.ISL] - PARM.S15[PARM.ISL] + 0.0001 * PARM.PKOC[K] * PARM.WOC[PARM.ISL])));
                            double CO = Math.Min(PARM.PSOL[K], VPST / (PARM.PKRZ[PARM.ISL] + PARM.PRMT[18] * PARM.SSF[PARM.ISL]));
                            double CS = PARM.PRMT[18] * CO;
                            double X4 = CS * PARM.SSF[PARM.ISL];
                            if (PARM.ISL == PARM.IDR)
                            {
                                PARM.SMMP[10, K, PARM.MO] = PARM.SMMP[10, K, PARM.MO] + X4;
                                PARM.VARP[10, K] = X4;
                            }
                            SUM = SUM + X4;
                            X3 = CO * PARM.PKRZ[PARM.ISL];
                            if (L1 == PARM.NBSL)
                            {
                                X2 = X3;
                            }
                            Y1 = Y1 - X4 - X3;
                        }
                        else
                        {
                            //!                     COMPUTE PESTICIDE DEGRADATION IN SOIL LAYERS
                            double DGS = Y1 * PARM.PHLS[K];
                            Y1 = Y1 - DGS;
                            TOT = TOT + DGS;
                            ADD = ADD + Y1;
                        }
                    }
                    else
                    {
                        Y1 = 0.0;
                    }
                    PARM.PSTZ[K, PARM.ISL] = Y1;
                }
                PARM.SMMP[3, K, PARM.MO] = PARM.SMMP[3, K, PARM.MO] + X2;
                PARM.VARP[3, K] = X2;
                PARM.SMMP[4, K, PARM.MO] = PARM.SMMP[4, K, PARM.MO] + SUM;
                PARM.VARP[4, K] = SUM;
                PARM.SMMP[7, K, PARM.MO] = PARM.SMMP[7, K, PARM.MO] + TOT;
                PARM.VARP[7, K] = TOT;
                PARM.SMMP[9, K, PARM.MO] = ADD;
                PARM.VARP[9, K] = ADD;
                PARM.PLCH[K] = X2;
                PARM.SSPS[K] = SUM;
                double PQST = PQ + SUM;
                PARM.SPQ[1, K] = PQST;
                PARM.SPY[1, K] = PY;
                for (int I = 2; I <= 5; I++)
                {//DO I=2,5
                    PARM.SPQ[I, K] = PARM.SPQ[I, K] + PQST - PARM.PVQ[K, NXP[NY[I]]];
                    if (PARM.SPQ[I, K] < 0.001 || PARM.SQB[I] < 0.001)
                    {
                        PARM.SPQC[I, K] = 0.0;
                    }
                    else
                    {
                        PARM.SPQC[I, K] = 100.0 * PARM.SPQ[I, K] / PARM.SQB[I];
                    }
                    PARM.SPY[I, K] = PARM.SPY[I, K] + PY - PARM.PVY[K, NXP[NY[I]]];
                }
                for (int I = 1; I <= 5; I++)
                {
                    if (PARM.APQ[I, K, PARM.IY] <= PARM.SPQ[I, K])
                    {
                        PARM.APQ[I, K, PARM.IY] = PARM.SPQ[I, K];
                        PARM.AQB[I, K, PARM.IY] = PARM.SQB[I];
                    }
                    if (PARM.APQC[I, K, PARM.IY] < PARM.SPQC[I, K])
                    {
                        PARM.APQC[I, K, PARM.IY] = PARM.SPQC[I, K];
                    }
                    if (PARM.APY[I, K, PARM.IY] > PARM.SPY[I, K])
                    {
                        continue;
                    }
                    PARM.APY[I, K, PARM.IY] = PARM.SPY[I, K];
                    PARM.AYB[I, K, PARM.IY] = PARM.SYB[I];
                }
                PARM.PVQ[K, II] = PQST;
                PARM.PVY[K, II] = PY;
                PARM.VQ[II] = QQ;
                PARM.VY[II] = Y2;
            }
            for (int I = 90; I >= 2; I--)
            {
                int I1 = I - 1;
                NXP[I] = NXP[I1];
            }
            NXP[1] = II;
            //Translator's Note: These format do not seem to be used 
            //	anywhere in the file so I commented them out.					
            //    4 FORMAT(25I4)
            //   11 FORMAT(1X,10E13.5)
        }
    }
}

