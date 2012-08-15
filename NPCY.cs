using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program is the master nutrient cycling program.
     * Calls NPMIN, NYNIT, NLCH, NCNMI, and NDNIT for each soil
     * layer.
     * 
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public partial class Functions
    {
        public static void NPCY()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double STDX = 0.0;

            for (int K = 1; K <= PARM.LC; K++)
            {
                STDX = STDX + PARM.STD[K - 1];
            }
            PARM.SGMN = 0.0;
            PARM.SDN = 0.0;
            PARM.SN2 = 0.0;
            PARM.SMP = 0.0;
            PARM.SIP = 0.0;
            PARM.TSFN = 0.0;
            PARM.TSFK = 0.0;
            PARM.TSFS = 0.0;
            PARM.SVOL = 0.0;
            PARM.SNIT = 0.0;
            PARM.TRSP = 0.0;
            double SMNIM = 0.0;
            double XX = 0.0;
            PARM.SSO3[PARM.LD1 - 1] = 0.0;

            for (int J = 1; J <= PARM.NBSL; J++)
            {
                PARM.ISL = PARM.LID[J - 1];
                PARM.RSPC[PARM.ISL - 1] = 0.0;
                PARM.RNMN[PARM.ISL - 1] = 0.0;
                double X1 = PARM.ST[PARM.ISL - 1] - PARM.S15[PARM.ISL - 1];
                if (X1 < 0.0)
                {
                    PARM.SUT = .1 * Math.Pow((PARM.ST[PARM.ISL - 1] / PARM.S15[PARM.ISL - 1]), 2);
                }
                else
                {
                    PARM.SUT = Math.Min(1.0, .1 + .9 * Math.Sqrt(X1 / (PARM.FC[PARM.ISL - 1] - PARM.S15[PARM.ISL - 1])));
                }
                Epic.NPMIN();
                Epic.NKMIN();
                if (PARM.ISL != PARM.LD1)
                {
                    Epic.NLCH();
                }
                else
                {
                    double ZZ = PARM.PRMT[65] * (1.0 + .1 * PARM.RFV);
                    if (STDX + PARM.STDO > .001)
                        Epic.NFALL(ZZ);
                    if (PARM.LUN != 35 && PARM.RFV > 0.0)
                    {
                        Epic.NYNIT();
                        Epic.NYPA();
                        PARM.SMM[53, PARM.MO - 1] = PARM.SMM[53, PARM.MO - 1] + PARM.YP;
                        PARM.VAR[53] = PARM.YP;
                    }
                }
                PARM.TSFN = PARM.TSFN + PARM.SSFN;
                PARM.TSFK = PARM.TSFK + PARM.SSFK;
                PARM.TSFS = PARM.TSFS + PARM.SSST;

                if (PARM.ISL == PARM.IDR)
                {
                    PARM.SMM[52, PARM.MO - 1] = PARM.SMM[52, PARM.MO - 1] + PARM.SSFN;
                    PARM.VAR[52] = PARM.SSFN;
                }
                double Z5 = 500.0 * (PARM.Z[PARM.ISL - 1] + XX);
                if (PARM.WNH3[PARM.ISL - 1] > .01) Epic.NITVOL(ref Z5);
                if (PARM.STMP[PARM.ISL - 1] > 0.0)
                {
                    PARM.CDG = PARM.STMP[PARM.ISL - 1] / (PARM.STMP[PARM.ISL - 1] + Math.Exp(PARM.SCRP[13, 0] - PARM.SCRP[13, 1] * PARM.STMP[PARM.ISL - 1]));
                    SMNIM = SMNIM - PARM.WNO3[PARM.ISL - 1];
                    double CS;
                    Epic.NCNMI(ref Z5, ref CS);
                    SMNIM = SMNIM + PARM.WNO3[PARM.ISL - 1];
                    Epic.NPMN(ref CS);
                    PARM.SMP = PARM.SMP + PARM.WMP;
                    if (PARM.IDN > 0)
                    {
                        if (PARM.IDN == 1)
                        {
                            PARM.DZ = 1000.0 * (PARM.Z[PARM.ISL - 1] - XX);
                            Epic.NDNITAK();
                        }
                        else
                        {
                            PARM.WDN = 0.0;
                            PARM.DN2O = 0.0;
                            if (PARM.ST[PARM.ISL - 1] > PARM.FC[PARM.ISL - 1])
                                Epic.NDNIT();
                        }
                        PARM.SDN = PARM.SDN + PARM.WDN;
                        PARM.SN2 = PARM.SN2 + PARM.DN2O;
                    }
                }
                XX = PARM.Z[PARM.ISL - 1];
            }

            PARM.SMM[56, PARM.MO - 1] = PARM.SMM[56, PARM.MO - 1] + PARM.VAP;
            PARM.VAR[56] = PARM.VAP;
            PARM.SMM[80, PARM.MO - 1] = PARM.SMM[80, PARM.MO - 1] + PARM.VSK;
            PARM.VAR[80] = PARM.VSK;
            PARM.SMM[81, PARM.MO - 1] = PARM.SMM[81, PARM.MO - 1] + PARM.VSLT;
            PARM.VAR[81] = PARM.VSLT;
            PARM.SMM[86, PARM.MO - 1] = PARM.SMM[86, PARM.MO - 1] + SMNIM;
            PARM.VAR[86] = SMNIM;


        }
    }
}