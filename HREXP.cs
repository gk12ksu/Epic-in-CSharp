using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program distributes daily rainfall exponentially and
     * furnishes the green and ampt subprogram rain increments of equal
     * Volume DRFV
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/6/2012
     */
    public class HREXP
    {
        public HREXP()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            double DRFV = 2.0;

            double PT = 0.0;
            double UPLM = .95;
            double QMN = .25;
            double BLM = .05;
            double R1 = Epic.ATRI(BLM, QMN, UPLM, 8);
            double RTP = R1 * PARM.RFV;
            double XK1 = R1 / 4.605;
            double XK2 = XK1 * (1.0 - R1) / R1;
            PARM.DUR = PARM.RFV / (PARM.REP * (XK1 + XK2));
            double X1 = PARM.REP * PARM.DUR;
            double XKP1 = XK1 * X1;
            double XKP2 = XK2 * X1;
            double RX = 0.0;
            PT = 0.0;

            //WRITE(KW(4),1)IYR,MO,KDA,SCN,XK1,XK2,RFV,RTP,REP,DUR,R1 Original Write Statement
            //1 FORMAT(1X,3I4,9F10.2) Original Write Statement

            double Q1;

            while (PT < RTP)
            {
                PT = PT + DRFV;
                RX = PARM.REP * (1.0 - (RTP - PT) / XKP1);
                Epic.HGASP(DRFV, PT, Q1, RX);
            }

            double A = RTP - PT + DRFV;
            PT = RTP;
            RX = PARM.REP;
            Epic.HGASP(A, PT, Q1, RX);

            while (PT < PARM.RFV)
            {
                PT = PT + DRFV;
                RX = PARM.REP * (1.0 - (PT - RTP) / XKP2);
                Epic.HGASP(A, PT, Q1, RX);
            }
            RX = 0.0;
            A = PARM.RFV - PT;
            PT = PARM.RFV;
            Epic.HGASP(A, PT, Q1, RX);

        }
    }
}