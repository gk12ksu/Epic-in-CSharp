using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program estimates daily loss of NO3 by denitrification.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/8/2012
     */
    public class NCONT
    {
        public NCONT()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            double SM4 = 0.0;
            PARM.TEK=0.0;
            PARM.TFK = 0.0;
            PARM.TSK = 0.0;
            PARM.TNO2 = 0.0;
            PARM.TNO3 = 0.0;
            PARM.TNH3 = 0.0;
            PARM.TAP = 0.0;
            PARM.TP = 0.0;
            PARM.TMP = 0.0;
            PARM.TRSD = 0.0;
            PARM.TFOP = 0.0;
            PARM.TWN = 0.0;
            PARM.TOP = 0.0;
            PARM.ZLS = 0.0;
            PARM.ZLM = 0.0;
            PARM.ZLSL = 0.0;
            PARM.ZLSC = 0.0;
            PARM.ZLMC = 0.0;
            PARM.ZLSLC = 0.0;
            PARM.ZLSLNC = 0.0;
            PARM.ZBMC = 0.0;
            PARM.ZHSC = 0.0;
            PARM.ZHPC = 0.0;
            PARM.ZLSN = 0.0;
            PARM.ZLMN = 0.0;
            PARM.ZBMN = 0.0;
            PARM.ZHSN = 0.0;
            PARM.ZHPN = 0.0;
            PARM.TNOR = 0.0;

            for (int J = 1; J < PARM.NBSL; J++) 
            {
                int LL = PARM.LID[J - 1];
                PARM.TNO2 = PARM.TNO2 + PARM.WNO2[LL - 1];
                PARM.TNO3 = PARM.TNO3 + PARM.WNO3[LL - 1];
                PARM.TNH3 = PARM.TNH3 + PARM.WNH3[LL - 1];
                PARM.TAP = PARM.TAP + PARM.AP[LL - 1];
                PARM.TOP = PARM.TOP + PARM.OP[LL - 1];
                PARM.TMP = PARM.TMP + PARM.PMN[LL - 1];
                PARM.TP = PARM.TP + PARM.WP[LL - 1];
                PARM.TRSD = PARM.TRSD + PARM.RSD[LL - 1];
                PARM.SW = PARM.SW + PARM.ST[LL - 1];
                PARM.TFOP = PARM.TFOP + PARM.FOP[LL - 1];
                PARM.TEK = PARM.TEK + PARM.EXCK[LL - 1];
                PARM.TFK = PARM.TFK + PARM.FIXK[LL - 1];
                PARM.TSK = PARM.TSK + PARM.SOLK[LL - 1];
                PARM.ZLS = PARM.ZLS + PARM.WLS[LL - 1];
                PARM.ZLM = PARM.ZLM + PARM.WLM[LL - 1];
                PARM.ZLSL = PARM.ZLSL + PARM.WLSL[LL - 1];
                PARM.ZLSC = PARM.ZLSC + PARM.WLSC[LL - 1];
                PARM.ZLMC = PARM.ZLMC + PARM.WLMC[LL - 1];
                PARM.ZLSLC = PARM.ZLSLC + PARM.WLSLC[LL - 1];
                PARM.ZLSLNC = PARM.ZLSLNC + PARM.WLSLNC[LL - 1];
                PARM.ZBMC = PARM.ZBMC + PARM.WBMC[LL - 1];
                PARM.ZHSC = PARM.ZHSC + PARM.WHSC[LL - 1];
                PARM.ZHPC = PARM.ZHPC + PARM.WHPC[LL - 1];
                PARM.WOC[LL - 1] = PARM.WBMC[LL - 1] + PARM.WHPC[LL - 1] + PARM.WHSC[LL - 1] + PARM.WLMC[LL - 1] + PARM.WLSC[LL - 1];
                PARM.ZLSN = PARM.ZLSN + PARM.WLSN[LL - 1];
                PARM.ZLMN = PARM.ZLMN + PARM.WLMN[LL - 1];
                PARM.ZBMN = PARM.ZBMN + PARM.WBMN[LL - 1];
                PARM.ZHSN = PARM.ZHSN + PARM.WHSN[LL - 1];
                PARM.ZHPN = PARM.ZHPN + PARM.WHPN[LL - 1];
                PARM.WON[LL - 1] = PARM.WBMN[LL - 1] + PARM.WHPN[LL - 1] + PARM.WHSN[LL - 1] + PARM.WLMN[LL - 1] + PARM.WLSN[LL - 1];
            }

            PARM.TWN = PARM.ZLSN + PARM.ZLMN + PARM.ZBMN + PARM.ZHSN + PARM.ZHPN;
            PARM.TOC = PARM.ZLSC + PARM.ZLMC + PARM.ZBMC + PARM.ZHSC + PARM.ZHPC;

/* Commented out code in original source
!     WRITE(KW(1),125)IY,MO1,KDA,ZLSC,ZLMC,ZBMC,ZHSC,ZHPC,TOC
!     X1=UP1(1)+UP1(2)
!     SUM=TAP+TOP+TMP+TP+TFOP+STDP+STDOP+X1
!     BAL=BTP-YP-QAP-VAP-YLP+VAR(62)+VAR(63)-SUM
!     SM4=SM4+BAL
!     BAL=BTP-SEDN-SOLN-PRKN-CYLN+TFO+FNIT-SUM
!     IF(ABS(BAL)<.001)GO TO 1
!     WRITE(KW(1),125)IY,MO1,KDA,TAP,TOP,TMP,TP,TFOP,STDP,STDOP,X1,SUM
!     WRITE(KW(1),125)IY,MO1,KDA,BTP,YP,QAP,VAP,VAR(62),VAR(63),YLP,BAL,
!    1SM4
!   1 BTP=SUM
!     YLP=0.
!     DF=BTC-YOC-VBC-QBC-RSPC(LL)+TFOC+RSDC-TOC
!     WRITE(KW(1),125)IY,MO1,KDA,BTC,YOC,VBC,QBC,RSPC(LL),TFOC,RSDC,TOC
!     BTC=TOC
*/
            double ADD = 0.0, TOT = 0.0;
            for (int I = 1; I < PARM.LC; I++)
            {
                ADD = ADD + PARM.UN1[I - 1];
                TOT = TOT + PARM.STDN[I - 1];
            }

            PARM.TYN1 = PARM.TYN - PARM.TYN1;
            double SUM = PARM.TNO2 + PARM.TNO3 + PARM.TNH3 + PARM.TWN + TOT + PARM.STDON + ADD;
            double BAL = PARM.BTNX + PARM.RNO3 - PARM.VAR[42] - PARM.VAR[43] - PARM.VAR[44] - PARM.VAR[45] - PARM.VAR[48] - PARM.VAR[51] + PARM.VAR[58] + PARM.VAR[59] + PARM.VAR[60] + PARM.DFX - PARM.VAR[97] - PARM.TYN1 - SUM;
            SM4 = SM4 + BAL;

            if (Math.Abs(BAL) > .1)
            {
                //This program writes to the file KW(1) from the original source
                //file.Write(" !!!!! " + PARM.IY + "" + PARM.MO1 + "" + PARM.KDA  + "" + PARM.TNO2  + "" + PARM.TNO3  + "" + PARM.TNH3  + "" + PARM.TWN  + "" + TOT  + "" + PARM.STDON  + "" + ADD  + "" + PARM.BTNX  + "" + SUM + "\n");
                //file.Write(" !!!!! " + PARM.IY + "" + PARM.MO1 + "" + PARM.KDA + "" + PARM.RNO3 + "" + PARM.VAR[42] + "" + PARM.VAR[43] + "" + PARM.VAR[44] + "" + PARM.VAR[45] + "" + PARM.VAR[48] + "" + PARM.VAR[51] + "" + PARM.VAR[58] + "" + PARM.VAR(60) + "" + PARM.VAR[60] + "" + PARM.DFX + "" + PARM.VAR[97] + "" + PARM.TYN1 + "" + BAL + "" + SM4 + "\n");
            }

            PARM.BTNX = SUM;
            PARM.TYN1 = PARM.TYN;

            return;
/* Commented out code in original source
!     X1=UK1(1)+UK1(2)
!     SUM=TSK+TEK+TFK+STDK+STDOK+X1
!     BAL=BTK-YK-QK-SSK-PRKK-CYLK+FSK-SUM
!     WRITE(KW(1),125)IY,MO1,KDA,TSK,TEK,TFK,STDK,STDOK,X1,SUM
!     WRITE(KW(1),125)IY,MO1,KDA,QK,YK,SSK,PRKK,FSK,CYLK,BAL
!     BTK=SUM
*/
        }
    }
}