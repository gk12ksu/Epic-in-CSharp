using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program simulates mineralization and immobilization of N
     * and C using equations taken from century.
     * 
     * This file has had its array indicies shifted for C#
     * This file has had its goto statements removed
     * Last Modified On 7/8/2012
     */
    public partial class Functions
    {
        public static void NCNMI(ref double Z5, ref double CS)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double XZ = PARM.WNO3[PARM.ISL - 1] + PARM.WNH3[PARM.ISL - 1];
            //!     AD1=PARM.WLSN(ISL)+PARM.WLMN(ISL)+WBMN(ISL)+WHSN(ISL)+WHPN(ISL)+XZ Commented out in original source

            double RLR = Math.Min(.8, PARM.WLSL[PARM.ISL - 1] / (PARM.WLS[PARM.ISL - 1] + Math.Pow(10, -5)));
            double RHS = PARM.PRMT[46];
            double RHP = PARM.PRMT[47];
            double XHN = PARM.WHSN[PARM.ISL - 1] + PARM.WHPN[PARM.ISL - 1];
            double OX;
            if (PARM.IOX > 0)
            {
                double Y1 = .1 * PARM.WOC[PARM.ISL - 1] / PARM.WT[PARM.ISL - 1];
                double Y2 = 2.11 + .0375 * PARM.CLA[PARM.ISL - 1];
                double Y3 = 100.0 * Y1 / Y2;
                OX = Y3 / (Y3 + Math.Exp(PARM.SCRP[23, 0] - PARM.SCRP[23, 1] * Y3));
            }
            else
            {
                OX = 1.0 - PARM.PRMT[52] * Z5 / (Z5 + Math.Exp(PARM.SCRP[19, 0] - PARM.SCRP[19, 1] * Z5));
            }

            double X1 = Math.Exp(PARM.PRMT[51] * (PARM.BD[PARM.ISL - 1] - PARM.BDP[PARM.ISL - 1]));
            PARM.SMS[3, PARM.ISL - 1] = PARM.SMS[3, PARM.ISL - 1] + X1;
            CS = Math.Min(10.0, Math.Sqrt(PARM.CDG * PARM.SUT) * PARM.PRMT[19] * OX * X1);
            PARM.SMS[10, PARM.ISL - 1] = PARM.SMS[10, PARM.ISL - 1] + 1.0;
            PARM.SMS[0, PARM.ISL - 1] = PARM.SMS[0, PARM.ISL - 1] + PARM.SUT;
            double APCO2 = .55;
            double ASCO2 = .60;
            double BMNC, A1CO2, ABCO2, RBM, RLM, RLS, XBM, HSNC, HPNC; ;

            if (PARM.ISL == PARM.LD1)
            {
                CS = CS * PARM.PRMT[50];
                ABCO2 = .55;
                A1CO2 = .55;
                RBM = .0164;
                RLM = .0405;
                RLS = .0107;
                HPNC = .1;
                XBM = 1.0;
                //COMPUTE N/C RATIOS
                X1 = .1 * (PARM.WLMN[PARM.LD1 - 1] + PARM.WLSN[PARM.LD1 - 1]) / (PARM.RSD[PARM.LD1 - 1] + Math.Pow(10, -5));
                if (X1 > 2.0)
                {
                    BMNC = .1;
                }
                else if (X1 > .01)
                {
                    BMNC = 1.0 / (20.05 - 5.0251 * X1);
                }
                else
                {
                    BMNC = .05;
                }
                HSNC = BMNC / (5.0 * BMNC + 1.0);
            }
            else
            {

                ABCO2 = .17 + .0068 * PARM.SAN[PARM.ISL - 1];
                A1CO2 = .55;
                RBM = .02;
                RLM = .0507;
                RLS = .0132;
                XBM = .25 + .0075 * PARM.SAN[PARM.ISL - 1];
                X1 = 1000.0 * (PARM.WNH3[PARM.ISL - 1] + PARM.WNO3[PARM.ISL - 1]) / PARM.WT[PARM.ISL - 1];

                if (X1 > 7.15)
                {
                    BMNC = .33;
                    HSNC = .083;
                    HPNC = .143;
                }
                else
                {
                    BMNC = 1.0 / (15.0 - 1.678 * X1);
                    HSNC = 1.0 / (20.0 - 1.119 * X1);
                    HPNC = 1.0 / (10.0 - .42 * X1);
                }
            }
            double ABP = .003 + .00032 * PARM.CLA[PARM.ISL - 1];
            PARM.SMS[2, PARM.ISL - 1] = PARM.SMS[2, PARM.ISL - 1] + CS;
            double ASP = Math.Max(.001, PARM.PRMT[44] - .00009 * PARM.CLA[PARM.ISL - 1]);
            //POTENTIAL TRANSFORMATIONS STRUCTURAL LITTER
            X1 = RLS * CS * Math.Exp(-3.0 * RLR);
            double TLSCP = X1 * PARM.WLSC[PARM.ISL - 1];
            double TLSLCP = TLSCP * RLR;
            double TLSLNCP = TLSCP * (1.0 - RLR);
            double TLSNP = X1 * PARM.WLSN[PARM.ISL - 1];
            //POTENTIAL TRANSFORMATIONS METABOLIC LITTER
            X1 = RLM * CS;
            double TLMCP = PARM.WLMC[PARM.ISL - 1] * X1;
            double TLMNP = PARM.WLMN[PARM.ISL - 1] * X1;
            //POTENTIAL TRANSFORMATIONS MICROBIAL BIOMASS
            X1 = RBM * CS * XBM;
            double TBMCP = PARM.WBMC[PARM.ISL - 1] * X1;
            double TBMNP = PARM.WBMN[PARM.ISL - 1] * X1;
            //POTENTIAL TRANSFORMATIONS SLOW HUMUS
            X1 = RHS * CS;
            double THSCP = PARM.WHSC[PARM.ISL - 1] * X1;
            double THSNP = PARM.WHSN[PARM.ISL - 1] * X1;
            //POTENTIAL TRANSFORMATIONS PASSIVE HUMUS
            X1 = CS * RHP;
            double THPCP = PARM.WHPC[PARM.ISL - 1] * X1;
            double THPNP = PARM.WHPN[PARM.ISL - 1] * X1;
            //ESTIMATE N DEMAND
            double A1 = 1.0 - A1CO2;
            double ASX = 1.0 - ASCO2 - ASP;
            double APX = 1.0 - APCO2;
            double PN1 = TLSLNCP * A1 * BMNC;
            double PN2 = .7 * TLSLCP * HSNC;
            double PN3 = TLMCP * A1 * BMNC;
            double PN5 = TBMCP * ABP * HPNC;
            double PN6 = TBMCP * (1.0 - ABP - ABCO2) * HSNC;
            double PN7 = THSCP * ASX * BMNC;
            double PN8 = THSCP * ASP * HPNC;
            double PN9 = THPCP * APX * BMNC;
            //COMPARE SUPPLY AND DEMAND FOR N
            double SUM = 0.0;
            double CPN1 = 0.0;
            double CPN2 = 0.0;
            double CPN3 = 0.0;
            double CPN4 = 0.0;
            double CPN5 = 0.0;
            X1 = PN1 + PN2;

            if (TLSNP < X1)
            {
                CPN1 = X1 - TLSNP;
            }
            else
            {
                SUM = SUM + TLSNP - X1;
            }

            if (TLMNP < PN3)
            {
                CPN2 = PN3 - TLMNP;
            }
            else
            {
                SUM = SUM + TLMNP - PN3;
            }

            X1 = PN5 + PN6;

            if (TBMNP < X1)
            {
                CPN3 = X1 - TBMNP;
            }
            else
            {
                SUM = SUM + TBMNP - X1;
            }

            X1 = PN7 + PN8;

            if (THSNP < X1)
            {
                CPN4 = X1 - THSNP;
            }
            else
            {
                SUM = SUM + THSNP - X1;
            }

            if (THPNP < PN9)
            {
                CPN5 = PN9 - THPNP;
            }
            else
            {
                SUM = SUM + THPNP - PN9;
            }

            //WNH3(ISL)=WNH3(ISL)+SUM Commented out in the original source
            double WMIN = Math.Max(Math.Pow(10, -5), PARM.WNO3[PARM.ISL - 1] + SUM);
            double DMDN = CPN1 + CPN2 + CPN3 + CPN4 + CPN5;
            double X3 = 1.0;
            //REDUCE DEMAND IF SUPPLY LIMITS
            if (WMIN < DMDN) X3 = WMIN / DMDN;
            PARM.SMS[4, PARM.ISL - 1] = PARM.SMS[4, PARM.ISL - 1] + X3;
            //ACTUAL TRANSFORMATIONS
            double TLSCA = TLSCP * X3;
            double TLSLCA = TLSLCP * X3;
            double TLSLNCA = TLSLNCP * X3;
            double TLSNA = TLSNP * X3;
            double TLMCA = TLMCP * X3;
            double TLMNA = TLMNP * X3;
            double TBMCA = TBMCP * X3;
            double TBMNA = TBMNP * X3;
            double THSCA = THSCP * X3;
            double THSNA = THSNP * X3;
            double THPCA = THPCP * X3;
            double THPNA = THPNP * X3;
            //DMDN=DMDN*X3 Commented out in the original source
            PARM.SGMN = PARM.SGMN + SUM;
            PARM.RNMN[PARM.ISL - 1] = SUM - DMDN;
            //UPDATE
            if (PARM.RNMN[PARM.ISL - 1] > 0.0)
            {
                PARM.WNH3[PARM.ISL - 1] = PARM.WNH3[PARM.ISL - 1] + PARM.RNMN[PARM.ISL - 1];
                //WNO3(ISL)=WNO3(ISL)+RNMN(ISL) Commented out in the original source
            }
            else
            {
                X1 = PARM.WNO3[PARM.ISL - 1] + PARM.RNMN[PARM.ISL - 1];
                if (X1 < 0.0)
                {
                    PARM.RNMN[PARM.ISL - 1] = -PARM.WNO3[PARM.ISL - 1];
                    PARM.WNO3[PARM.ISL - 1] = Math.Pow(10, -10);
                }
                else
                {
                    PARM.WNO3[PARM.ISL - 1] = X1;
                }
            }
            double DF1 = TLSNA;
            double DF2 = TLMNA;
            PARM.SNMN = PARM.SNMN + PARM.RNMN[PARM.ISL - 1];
            PARM.SMS[8, PARM.ISL - 1] = PARM.SMS[8, PARM.ISL - 1] + PARM.RNMN[PARM.ISL - 1];
            PARM.WLSC[PARM.ISL - 1] = Math.Max(Math.Pow(10, -10), PARM.WLSC[PARM.ISL - 1] - TLSCA);
            PARM.WLSLC[PARM.ISL - 1] = Math.Max(Math.Pow(10, -10), PARM.WLSLC[PARM.ISL - 1] - TLSLCA);
            PARM.WLSLNC[PARM.ISL - 1] = Math.Max(Math.Pow(10, -10), PARM.WLSC[PARM.ISL - 1] - PARM.WLSLC[PARM.ISL - 1]);
            PARM.WLMC[PARM.ISL - 1] = Math.Max(Math.Pow(10, -10), PARM.WLMC[PARM.ISL - 1] - TLMCA);
            PARM.WLM[PARM.ISL - 1] = Math.Max(Math.Pow(10, -10), PARM.WLM[PARM.ISL - 1] - TLMCA / .42);
            PARM.WLSL[PARM.ISL - 1] = Math.Max(Math.Pow(10, -10), PARM.WLSL[PARM.ISL - 1] - TLSLCA / .42);
            PARM.WLS[PARM.ISL - 1] = Math.Max(Math.Pow(10, -10), PARM.WLS[PARM.ISL - 1] - TLSCA / .42);
            X3 = APX * THPCA + ASX * THSCA + A1 * (TLMCA + TLSLNCA);
            PARM.WBMC[PARM.ISL - 1] = PARM.WBMC[PARM.ISL - 1] - TBMCA + X3;
            double DF3 = TBMNA - BMNC * X3;
            X1 = .7 * TLSLCA + TBMCA * (1.0 - ABP - ABCO2);
            PARM.WHSC[PARM.ISL - 1] = PARM.WHSC[PARM.ISL - 1] - THSCA + X1;
            double DF4 = THSNA - HSNC * X1;
            X1 = THSCA * ASP + TBMCA * ABP;
            PARM.WHPC[PARM.ISL - 1] = PARM.WHPC[PARM.ISL - 1] - THPCA + X1;
            double DF5 = THPNA - HPNC * X1;
            double DF6 = XZ - PARM.WNO3[PARM.ISL - 1] - PARM.WNH3[PARM.ISL - 1];
            PARM.SMS[9, PARM.ISL - 1] = PARM.SMS[9, PARM.ISL] - DF6;
            double ADD = DF1 + DF2 + DF3 + DF4 + DF5 + DF6;
            double ADF1 = Math.Abs(DF1);
            double ADF2 = Math.Abs(DF2);
            double ADF3 = Math.Abs(DF3);
            double ADF4 = Math.Abs(DF4);
            double ADF5 = Math.Abs(DF5);
            double TOT = ADF1 + ADF2 + ADF3 + ADF4 + ADF5;
            double XX = ADD / (TOT + Math.Pow(10, -10));
            PARM.WLSN[PARM.ISL - 1] = Math.Max(.001, PARM.WLSN[PARM.ISL - 1] - DF1 + XX * ADF1);
            PARM.WLMN[PARM.ISL - 1] = Math.Max(.001, PARM.WLMN[PARM.ISL - 1] - DF2 + XX * ADF2);
            PARM.WBMN[PARM.ISL - 1] = PARM.WBMN[PARM.ISL - 1] - DF3 + XX * ADF3;
            PARM.WHSN[PARM.ISL - 1] = PARM.WHSN[PARM.ISL - 1] - DF4 + XX * ADF4;
            PARM.WHPN[PARM.ISL - 1] = PARM.WHPN[PARM.ISL - 1] - DF5 + XX * ADF5;
            PARM.RSPC[PARM.ISL - 1] = .3 * TLSLCA + A1CO2 * (TLSLNCA + TLMCA) + ABCO2 * TBMCA + ASCO2 * THSCA + APCO2 * THPCA;
            PARM.SMM[73, PARM.MO - 1] = PARM.SMM[73, PARM.MO - 1] + PARM.RSPC[PARM.ISL - 1];
            PARM.SMS[7, PARM.ISL - 1] = PARM.SMS[7, PARM.ISL - 1] + PARM.RSPC[PARM.ISL - 1];
            PARM.TRSP = PARM.TRSP + PARM.RSPC[PARM.ISL - 1];
            PARM.VAR[73] = PARM.VAR[73] + PARM.RSPC[PARM.ISL - 1];
            PARM.RSD[PARM.ISL - 1] = .001 * (PARM.WLS[PARM.ISL - 1] + PARM.WLM[PARM.ISL - 1]);
            PARM.DHN[PARM.ISL - 1] = XHN - PARM.WHSN[PARM.ISL - 1] - PARM.WHPN[PARM.ISL - 1];

            /* Commented out in the original source
            !     XZ=WNO3(ISL)+WNH3(ISL)
            !     AD2=WLSN(ISL)+WLMN(ISL)+WBMN(ISL)+WHSN(ISL)+WHPN(ISL)+XZ
            !     DF=AD2-AD1
            !     IF(ABS(DF)>.001)WRITE(KW(1),1)IY,MO,KDA,AD1,AD2,DF
            !   1 FORMAT(1X,'NCNMI',3I4,3E16.6)     
            */
        }
    }
}