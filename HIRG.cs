using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program is used to simulate automatic or user specified
     * irrigation applications. Computes the amount of irrigation water
     * needed to bring the root zone water content to field capacity, for
     * automatic option. User specified amount is applied for manual option.
     * Erosion and runoff are estimated.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/7/2012
     */
    public class HIRG
    {
        public HIRG(ref double AIR, ref double EFD, ref double ZX, ref int JRT, ref int IRX, ref double IRY)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            if (PARM.VIRT >= PARM.VIMX || PARM.NII < PARM.IRI)
            {
	            AIR = 0.0;
                JRT = 1;
                return;
            }
            double X3 = PARM.COIR * AIR;
            int N1 = Math.Max(1, PARM.NCP[PARM.JJK - 1]);
	        int I = 1;
            double X4 = AIR;      
            AIR = AIR * EFD;
            double YX = 0.0;
            double X1, XX;
            if (IRY == 1)
            {
                if (PARM.IAC > 0) goto lbl5;
                X1 = AIR;
                XX = (PARM.PAW - PARM.RZSW) / (1.0 - PARM.EFI);
            }else{
                X1 = 10000.0;
                if (PARM.IAC > 0){
                    XX = 10000.0;
                }else{
                    XX = (PARM.PAW - PARM.RZSW) / (1.0 - PARM.EFI);
                }
            }
            X4 = Math.Min(Math.Min(X1, PARM.VIMX - PARM.VIRT), Math.Min(XX, PARM.ARMX));
            AIR = X4 * EFD;
            X3 = PARM.COIR * X4;
            if (AIR > PARM.ARMN) goto lbl5;
            AIR = 0.0;
            goto lbl10;
            lbl5: PARM.NII = 0;
            double QXM = AIR * PARM.EFI;
            double X2;
            if (PARM.IRR != 5)
            {
                if (QXM < Math.Pow(10, -5)) goto lbl7;
                double QPX = QXM / 24.0;
                if (PARM.IRR != 1)
                {
                    PARM.CVF = 1.0;
                    if (PARM.RHTT < Math.Pow(10, -10) || PARM.RGIN < Math.Pow(10, -5)) goto lbl3;
                    X1 = 1000.0 * PARM.RGIN / PARM.RHTT;
                    QPX = 2.778 * Math.Pow(10, -6) * QPX * PARM.RGIN * PARM.WSA / PARM.FW;
                    double DX = Math.Pow((2.0 * QPX / (PARM.SX * X1 * Math.Pow((1.0 / (4.0 + 16.0 / (X1 * X1))), .3333))), .375);
                    X2 = DX * X1;
                    double PX = 2.0 * Math.Sqrt(DX * DX + .25 * X2 * X2);
                    double AX = .5 * DX * X2;
                    double VX = Math.Pow((AX / PX), .6667) * PARM.SX;
                    double CY = PARM.PRMT[35] * Math.Pow(VX, PARM.PRMT[30]);
                    YX = 10.0 * QXM * CY * PARM.EK;
                    goto lbl1;
                }
                Epic.EYCC();
                lbl3: YX = 2.5 * PARM.CVF * PARM.USL * Math.Sqrt(QPX * QXM);
                goto lbl1;
            }

            for (int J = 0; J < PARM.NBSL; J++)
            {
                I = PARM.LID[J - 1];
                if (ZX < PARM.Z[I - 1]) break;
            }
            PARM.ST[I - 1] = PARM.ST[I - 1] + AIR;
            lbl1: PARM.QD = PARM.QD + QXM;
            PARM.YERO = PARM.YERO + YX;
            PARM.YSD[PARM.NDRV - 1] = PARM.YSD[PARM.NDRV - 1] + YX;
            lbl7: X1 = PARM.RZSW - PARM.PAW;
            PARM.VSLT = .01 * AIR * PARM.CSLT;
            PARM.SMM[68, PARM.MO - 1] = PARM.SMM[68, PARM.MO - 1] + PARM.VSLT;
            PARM.VAR[68] = PARM.VSLT;
	        if (PARM.KG[PARM.JJK - 1] > 0 || PARM.JPL[PARM.JJK - 1] > 0) PARM.XHSM = PARM.HU[PARM.JJK - 1] / PARM.PHU[PARM.JJK - 1, PARM.IHU[PARM.JJK - 1] - 1];
            if (PARM.NOP > 0) {} //WRITE(KW(1),9)IYR,MO,KDA,AIR,WS,WTN,X1,BIR,QXM,YX,XHSM,X3 Original Write Statement
            PARM.VIRT = PARM.VIRT + X4;
            PARM.COST = PARM.COST + X3;
            if (PARM.VIRT > 0.0)
            {
                if (IRY == 0)
                {
                    X1 = PARM.COTL[IRX - 1];
                    X2 = X1 - PARM.COOP[IRX - 1];
                    PARM.COST = PARM.COST + X1;
                    PARM.CSFX = PARM.CSFX + X2;
                }
                if (PARM.KFL[19] > 0)
                {
                    //WRITE(KW(20),14)IYR,MO,KDA,TIL(IRX),KDC(JJK),IHC(IRX),NBE(IRX),X3,X3,AIR Original Write Statement
                    if (IRY == 0) { } //WRITE(KW(20),50)IYR,MO,KDA,TIL(IRX),KDC(JJK),IHC(IRX),NBE(IRX),NBT(IRX),X1,X2,FULU(IRX) Original Write Statement
                }
            }
            PARM.SMM[18, PARM.MO - 1] = PARM.SMM[18, PARM.MO - 1] + X4;
            PARM.VAR[18] = X4;
	        PARM.SMM[91, PARM.MO - 1] = PARM.SMM[91, PARM.MO - 1] + PARM.FULU[IRX - 1];
            double XEF = X4 - AIR;
            PARM.SMM[83, PARM.MO - 1] = PARM.SMM[83, PARM.MO - 1] + XEF;
            PARM.VAR[83] = XEF;
            AIR = AIR - QXM;
	        PARM.VIR[N1 - 1, PARM.JJK - 1] = PARM.VIR[N1 - 1, PARM.JJK - 1] + X4;
            PARM.VIL[N1 - 1, PARM.JJK - 1] = PARM.VIL[N1 - 1, PARM.JJK - 1] + XEF;
	        PARM.AFN = AIR * PARM.CNO3I;
            PARM.ANA[PARM.JJK - 1] = PARM.ANA[PARM.JJK - 1] + PARM.AFN;
            PARM.WNO3[I - 1] = PARM.WNO3[I - 1] + PARM.AFN;
            PARM.SMM[59, PARM.MO - 1] = PARM.SMM[59, PARM.MO - 1] + PARM.AFN;
            PARM.VAR[59] = PARM.VAR[59] + PARM.AFN;
            if (PARM.NOP > 0 && PARM.AFN > 0.0) {} //WRITE(KW(1),133)IYR,MO,KDA,AFN,AIR,XHSM Original Write Statement
            lbl10: JRT = 0;
            if (PARM.IRR == 5)
            {
	            PARM.AWC = PARM.AWC + AIR;
	            AIR = 0.0;
            }


        }
    }
}
