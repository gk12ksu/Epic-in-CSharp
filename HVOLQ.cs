using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program predicts daily runoff volume and peak runoff rate
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/7/2012
     */
    public partial class Functions
    {
        public static void HVOLQ()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double SUM = 0.0;
            double ADD = 0.0;

            if (PARM.LUN == 35)
            {
                PARM.SCN = 25400.0 / PARM.CN0 - 254.0;
                PARM.CN = PARM.CN0;
                goto lbl20;
            }

            if (PARM.NVCN > 0) goto lbl25;
            double XX = 0.0;
            int J;
            double ZZ;
            
            for (int JJ = 1; JJ <= PARM.NBSL; JJ++)
            {
                J = PARM.LID[JJ - 1];
                
                if (PARM.Z[J - 1] > 1.0)
                    goto lbl3;
                ZZ = (PARM.Z[J - 1] - XX) / PARM.Z[J - 1];
                SUM = SUM + (PARM.ST[J - 1] - PARM.S15[J - 1]) * ZZ / (PARM.FC[J - 1] - PARM.S15[J - 1]);
                ADD = ADD + ZZ;
                XX = PARM.Z[J - 1];
            }
  
            goto lbl11;
            lbl3: ZZ = 1.0 - XX;
            SUM = SUM + (PARM.ST[J - 1] - PARM.S15[J - 1]) * ZZ / (PARM.FC[J - 1] - PARM.S15[J - 1]);
            ADD = ADD + ZZ;
            goto lbl11;

            lbl25: if (PARM.NVCN > 1) goto lbl19;
            int L1 = 0, L;
            for (int JJ = 1; JJ <= PARM.NBSL; JJ++)
            {
                L = PARM.LID[JJ - 1];
                if (PARM.Z[L - 1] > 1.0) goto lbl26;
                SUM = SUM + PARM.ST[L - 1] - PARM.S15[L - 1];
                ADD = ADD + PARM.FC[L - 1] - PARM.S15[L - 1];
                L1 = L;
            }
            goto lbl11;
            lbl26: double RTO = (1.0 - PARM.Z[L1 - 1]) / (PARM.Z[L - 1] - PARM.Z[L1 - 1]);
            SUM = SUM + (PARM.ST[L - 1] - PARM.S15[L - 1]) * RTO;
            ADD = ADD + (PARM.FC[L - 1] - PARM.S15[L - 1]) * RTO;

            lbl11: SUM = SUM / ADD;

            if (SUM > 0.0) goto lbl13;

            lbl31: PARM.SCN = PARM.SMX * Math.Pow((1.0 - SUM), 2);
            goto lbl14;
            lbl13: SUM = SUM * 100.0;
            PARM.SCN = PARM.SMX * (1.0 - SUM / (SUM + Math.Exp(PARM.SCRP[3, 0] - PARM.SCRP[3, 1] * SUM)));
            lbl14: PARM.SCN = (PARM.SCN - PARM.SMX) * Math.Exp(PARM.PRMT[74] * (1.0 - PARM.RSD[PARM.LD1 - 1])) + PARM.SMX;
            if (PARM.STMP[PARM.LID[1] - 1] < -1.0) PARM.SCN = PARM.SCN * PARM.PRMT[64];
            if (PARM.ICV > 0) PARM.SCN = PARM.SCN * Math.Sqrt(1.0 - PARM.FCV); 
            PARM.CN = 25400.0 / (PARM.SCN+254.0);
            if (PARM.ISCN == 0)
            {
                double UPLM = Math.Min(99.5, PARM.CN + 5.0);
                double BLM = Math.Max(1.0, PARM.CN - 5.0);
                PARM.CN = Epic.ATRI(BLM,PARM.CN,UPLM,8);
            }
            PARM.SCN = 25400.0 / PARM.CN - 254.0;
            if (PARM.SCN > 3.0) goto lbl20;
            PARM.SCN = 3.0; 
	        PARM.CN = 25400.0 / (PARM.SCN + 254.0);
            goto lbl20;
            lbl19: if (PARM.NVCN > 2) goto lbl28;

            for (int JJ = 1; JJ <= PARM.NBSL; JJ++)
            {
                PARM.ISL = PARM.LID[JJ - 1];
                SUM = SUM + PARM.ST[PARM.ISL - 1] - PARM.S15[PARM.ISL - 1];
                ADD = ADD + PARM.FC[PARM.ISL - 1] - PARM.S15[PARM.ISL - 1];
                if(PARM.Z[PARM.ISL - 1] > 1.0) break;
            }
            SUM = SUM / ADD;
            if (SUM < 0.0) goto lbl31;
            RTO = Math.Min(.98, SUM);
            PARM.SCN = PARM.SMX * (1.0 - RTO);
            goto lbl14;
            lbl28: if (PARM.NVCN > 3) goto lbl33;
            lbl33: PARM.SCN = Math.Max(3.0, PARM.SCI);
            goto lbl14;
            lbl20: double BB = .2 * PARM.SCN;
            double TOT = 100.0;
            int I;
            for (I = 1; I <= 9; I++)
            {
                TOT = TOT - 5.0;
                if (PARM.CN > TOT) break;
            }
            PARM.CNDS[I - 1] = PARM.CNDS[I - 1] + 1.0;
            RTO = Math.Min(1.0, PARM.SCN / PARM.SMX);
            PARM.CRKF = PARM.PRMT[16] * PARM.RFV * RTO;
            PARM.RFV = PARM.RFV - PARM.CRKF;
            if (PARM.RWO < Math.Pow(10, -5)) goto lbl10;

            switch (PARM.INFL)
            {
                case 1:
                    double X1 = PARM.RWO - BB;
                    if (X1 <= 0.0) goto lbl10;
                    PARM.QD = X1 * X1 / (PARM.RWO + .8 * PARM.SCN);
                    break;
                case 2:
                case 3: Epic.HREXP();
                    break;
                case 4: Epic.HRUNF();
                    break;
            }

            if (PARM.IFD > 0)
            {
                if (PARM.DHT > .01 && PARM.RGIN > .01)
                {
                    Epic.HFURD();
                    double X1 = Math.Max(0.0, PARM.ST[PARM.LD1 - 1] - PARM.PO[PARM.LD1 - 1]);
                    //if (PARM.NOP > 0) WRITE(KW(1),17)IYR,MO,KDA,DHT,RHTT,QD,DV,X1,XHSM Original Write Statement
                    if (PARM.QD <= PARM.DV - X1)
                    {
                        PARM.QD = 0.0;
                        goto lbl10;
                    }
                }
                PARM.DHT = 0.0;
                if (PARM.IDRL == 0.0 && PARM.CHT[PARM.JJK - 1] < 1.0) PARM.DHT = PARM.DKHL;
            }

            if (PARM.ITYP == 0)
            {
                double X2 = PARM.QD / PARM.DUR;
                if (X2 > 1.0)
                {
                    X2 = Math.Pow(X2, .25);
                }else{
                    X2 = 1.0;
                }
                double X4 = Math.Min(PARM.UPSL / 360.0, PARM.TCS / X2);
                PARM.TC = X4 + PARM.TCC / X2;
                PARM.ALTC = 1.0 - Math.Exp(-PARM.TC * PARM.PR);
	            double X1 = PARM.ALTC * PARM.QD;
                PARM.QP = X1 / PARM.TC;
	            PARM.QPR = X1 / X4;
                goto lbl10;
            }

            PARM.TC = PARM.TCC + PARM.TCS / Math.Sqrt(PARM.RWO);
            PARM.QP = BB / PARM.RWO;
            Epic.HTR55();
            lbl10: if (PARM.KFL[3] > 0) {/*WRITE(KW(4),27)IYR,MO,KDA,CN,RWO,QD,TC,QP,DUR,ALTC,AL5 Original Write Statement*/}

        }
    }
}