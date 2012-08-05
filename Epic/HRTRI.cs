using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program distributes daily rainfall from a triangle and
     * furnishes the green and ampt subprogram rain increments of equal
     * Volume DRFV
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/6/2012
     */
    public class HRTRI
    {
        public HRTRI()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double DRFV = 2.5;
            double PT = 0.0;
            PARM.DUR = 2.0 * PARM.RFV / PARM.REP;
            double UPLM = .95 * PARM.DUR;
            double QMN = .25 * PARM.DUR;
            double BLM = .05 * PARM.DUR;
            double R1 = Epic.ATRI(BLM,QMN,UPLM,8);
            double RTP = .5 * R1 * PARM.REP;
            double B2 = PARM.REP / R1;
            double RX = 0.0;
            double T = -100.0;
            double Q1;

            while (T < R1)
            {
                PT = PT + DRFV;
                T = Math.Sqrt(2.0 * PT / B2);
                RX = B2 * T;
                Epic.HGAWY(DRFV, PT, Q1, RX);
            }

            T = R1;
            double A = RTP - PT + DRFV;
            PT = RTP;
            RX = PARM.REP;
            Epic.HGAWY(A, PT, Q1, RX);
            B2 = PARM.REP / (PARM.DUR - R1);
            double PX = PARM.REP + R1 * B2;
            double BB = PX * PX;

            while (PT < PARM.RFV)
            {
                PT = PT + DRFV;
                T = (PX - Math.Sqrt(BB - 2.0 * B2 * (PT - RTP + R1 * (PARM.REP + .5 * B2 * R1)))) / B2;
                RX = PARM.REP - B2 * (T - R1);
                Epic.HGAWY(DRFV, PT, Q1, RX);
            }

            T = PARM.DUR;
            RX = 0.0;
            A = PARM.RFV - PT;
            PT = PARM.RFV;
            Epic.HGAWY(A, PT, Q1, RX);

            //WRITE(KW(1),13)IYR,MO,KDA,SCN,RFV,PT,QD,REP,DUR,R1 Original Write Statement
            //13 FORMAT(1X,3I4,10F10.2) Original Format Statement
        }
    }
}