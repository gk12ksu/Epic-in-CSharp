using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program applies N and P fertilizer at specified dates,
     * rates, and depth.
     * 
     * 
     * This file has had its array indicies shifted for C#
     * This file had its gotos removed
     * Last Modified On 7/10/2012
     */
    public partial class Functions
    {
        public static void NFERT(ref int IRC, ref int JFT)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            if (PARM.ANA[PARM.JJK - 1] >= PARM.FNMX[PARM.JJK - 1])
                return;
            double X1 = 0.0;
            int I = PARM.LD1;
            double ZFT = PARM.TLD[JFT - 1];
            double X3 = 0.0;
            bool skip = false;
            switch (IRC)
            {
                case 1:
                    PARM.KF = PARM.LFT[PARM.IRO - 1, PARM.KT - 1];
                    X1 = PARM.WFA[PARM.IRO - 1, PARM.KT - 1];
                    break;
                case 2:
                    PARM.KF = PARM.IDFT[0];
                    X1 = PARM.FNP;
                    break;
                case 3:
                    if (PARM.IRR != 4)
                    {
                        PARM.KF = PARM.IDFT[0];
                        X1 = PARM.FNP;
                    }
                    else
                    {
                        PARM.KF = PARM.IDFT[1];
                    }
                    break;
                case 4:
                    PARM.KF = PARM.IDFT[1];
                    X3 = PARM.BFT - PARM.TNOR;
                    skip = true;
                    X3 = Math.Min(X3, PARM.FNMX[PARM.JJK - 1] - PARM.ANA[PARM.JJK - 1]);
                    X1 = X3 / PARM.FN[PARM.KF - 1];
                    break;
                case 5:
                    PARM.KF = PARM.IDFP;
                    X1 = PARM.APMU;
                    X3 = PARM.APMU * PARM.FN[PARM.KF - 1];
                    skip = true;
                    break;
            }
            if (skip == false)
            {
                for (int J = 1; J <= PARM.NBSL; J++)
                {
                    I = PARM.LID[J - 1];
                    if (ZFT < PARM.Z[I - 1])
                        break;
                }
                if (X1 > 0.0)
                {
                    if (PARM.FN[PARM.KF - 1] > 0.0)
                    {
                        X3 = X1 * PARM.FN[PARM.KF - 1];
                        X3 = Math.Min(X3, PARM.FNMX[PARM.JJK - 1] - PARM.ANA[PARM.JJK - 1]);
                        X1 = X3 / PARM.FN[PARM.KF - 1];
                    }
                    else
                    {
                        X3 = 0.0;
                    }
                }
                else
                {
                    X1 = Math.Max(PARM.PRMT[27] * PARM.UNA[PARM.JJK - 1] - PARM.TNOR, 0.0);
                    X3 = X1;
                    if (PARM.FN[PARM.KF - 1] < 1.0E-10)
                        return;
                    X3 = Math.Min(X3, PARM.FNMX[PARM.JJK - 1] - PARM.ANA[PARM.JJK - 1]);
                    X1 = X3 / PARM.FN[PARM.KF - 1];
                }
            }
            double X2 = X1 * PARM.FP[PARM.KF - 1];
            double X4 = X3 * PARM.FNH3[PARM.KF - 1];
            double X5 = X1 * PARM.FNO[PARM.KF - 1];
            double X6 = X1 * PARM.FPO[PARM.KF - 1];
            double X7 = X3 - X4;
            double X8 = X1 * PARM.FOC[PARM.KF - 1];
            PARM.SMM[64, PARM.MO - 1] = PARM.SMM[64, PARM.MO - 1] + X8;
            double XX;
            if (X8 > .1)
            {
                double RLN = .175 * X8 / (X3 + X5);
                double X10 = .85 - .018 * RLN;
                if (X10 < .01)
                    X10 = .01;
                else
                    if (X10 > .7) X10 = .7;

                XX = X8 * X10;
                PARM.WLMC[I - 1] = PARM.WLMC[I - 1] + XX;
                double YY = X1 * X10;
                PARM.WLM[I - 1] = PARM.WLM[I - 1] + YY;
                double ZZ = X5 * X10;
                PARM.WLMN[I - 1] = PARM.WLMN[I - 1] + ZZ;
                PARM.WLSN[I - 1] = PARM.WLSN[I - 1] + X5 - ZZ;
                double XZ = X8 - XX;
                PARM.WLSC[I - 1] = PARM.WLSC[I - 1] + XZ;
                PARM.WLSLC[I - 1] = PARM.WLSLC[I - 1] + XZ * .175;
                PARM.WLSLNC[I - 1] = PARM.WLSC[I - 1] - PARM.WLSLC[I - 1];
                double YZ = X1 - YY;
                PARM.WLS[I - 1] = PARM.WLS[I - 1] + YZ;
                PARM.WLSL[I - 1] = PARM.WLSL[I - 1] + YZ * .175;
            }

            double X9 = X1 * PARM.FK[PARM.KF - 1];
            double X11 = X1 * PARM.FSLT[PARM.KF - 1];
            PARM.WNH3[I - 1] = PARM.WNH3[I - 1] + X4;
            PARM.AP[I - 1] = PARM.AP[I - 1] + X2;
            PARM.WP[I - 1] = PARM.WP[I - 1] + X6;
            PARM.SMM[58, PARM.MO - 1] = PARM.SMM[58, PARM.MO - 1] + X5;
            PARM.VAR[58] = X5;
            PARM.SMM[61, PARM.MO - 1] = PARM.SMM[61, PARM.MO - 1] + X6;
            PARM.VAR[61] = X6;
            PARM.SMM[60, PARM.MO - 1] = PARM.SMM[60, PARM.MO - 1] + X4;
            PARM.VAR[60] = X4;
            PARM.SMM[62, PARM.MO - 1] = PARM.SMM[62, PARM.MO - 1] + X2;
            PARM.VAR[62] = X2;
            PARM.WNO3[I - 1] = PARM.WNO3[I - 1] + X7;
            PARM.ANA[PARM.JJK - 1] = PARM.ANA[PARM.JJK - 1] + X3;
            PARM.SMM[59, PARM.MO - 1] = PARM.SMM[59, PARM.MO - 1] + X7;
            PARM.VAR[59] = PARM.VAR[59] + X7;
            PARM.SOLK[I - 1] = PARM.SOLK[I - 1] + X9;
            PARM.SMM[63, PARM.MO - 1] = PARM.SMM[63, PARM.MO - 1] + X9;
            PARM.VAR[63] = X9;
            PARM.WSLT[I - 1] = PARM.WSLT[I - 1] + X11;
            PARM.SMM[71, PARM.MO - 1] = PARM.SMM[71, PARM.MO - 1] + X11;
            PARM.VAR[71] = X11;
            int N1 = Math.Max(1, PARM.NCP[PARM.JJK - 1]);
            PARM.FRTN[N1 - 1, PARM.JJK - 1] = PARM.FRTN[N1 - 1, PARM.JJK - 1] + X3 + X5;
            PARM.FRTP[N1 - 1, PARM.JJK - 1] = PARM.FRTP[N1 - 1, PARM.JJK - 1] + X2 + X6;
            XX = X1 * PARM.FCST[PARM.KF - 1];
            PARM.COST = PARM.COST + XX;
            PARM.SMM[95, PARM.MO - 1] = PARM.SMM[95, PARM.MO - 1] + PARM.FCEM[PARM.KF - 1] * X1;

            if (IRC == 3)
            {
                double Y1 = PARM.COTL[JFT - 1];
                double Y2 = Y1 - PARM.COOP[JFT - 1];
                PARM.COST = PARM.COST + Y1;
                PARM.CSFX = PARM.CSFX + Y2;
            }

            if (PARM.KFL[19] > 0)
            {
                //These two write statements write to file KW(20)
                //WRITE(KW(20),18)IYR,MO,KDA,FTNM(KF),KDC(JJK),KDF(KF),IHC(JFT),NBE(JFT),NBT(JFT),XX,XX,X1
                if (IRC == 3)
                {
                    //WRITE(KW(20),50)IYR,MO,KDA,TIL(JFT),KDC(JJK),IHC(JFT),NBE(JFT),NBT(JFT),Y1,Y2,FULU(JFT)
                }
            }

            if (PARM.NOP > 0) //This writes to file KW(1)
            {
                //WRITE(KW(1),90)IYR,MO,KDA,FTNM(KF),X1,ZFT,X3,X4,X5,X2,X6,X9,XHSM,XX
            }
            PARM.NFA = 0;

        }
    }
}