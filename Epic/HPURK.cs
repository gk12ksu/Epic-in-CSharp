using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program is the master percolation component. It manages the routing process.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/6/2012
     */
    public class HPURK
    {
        public HPURK()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            double ADD = 0.0;

            PARM.SEP = PARM.RFV - PARM.QD;

            for (int KK = 1; KK < PARM.NBSL; KK++)
            {
                PARM.ISL = PARM.LID[KK - 1];
                PARM.ST[PARM.ISL - 1] = PARM.ST[PARM.ISL - 1] + PARM.SEP;
                if(PARM.WTBL <= PARM.Z[PARM.ISL - 1])
                {
                    PARM.SSF[PARM.ISL - 1] = 0.0;
                    PARM.PKRZ[PARM.ISL - 1] = 0.0;
                    PARM.SEP = 0.0;
                    continue;
                }
                Epic.HPERC();
                PARM.ST[PARM.ISL - 1] = PARM.ST[PARM.ISL - 1] - PARM.SEP - PARM.SST;
                PARM.SSF[PARM.ISL - 1] = PARM.SST;
                if(PARM.ISL == PARM.IDR)
                {
                    PARM.SMM[17, PARM.MO - 1] = PARM.SMM[17, PARM.MO - 1] + PARM.SST;
                    PARM.VAR[17] = PARM.SST;
                }
                PARM.PKRZ[PARM.ISL] = PARM.SEP;
                ADD = ADD + PARM.SST;
            }
            PARM.SST = ADD;
            int K = PARM.NBSL;
            int L1;
            for (; K < 2; K--)
            {
                PARM.ISL = PARM.LID[K - 1];
                L1 = PARM.LID[K - 2];
                double XX = PARM.ST[PARM.ISL - 1] - PARM.PO[PARM.ISL - 1];

                if (XX > 0.0)
                {
                    PARM.ST[L1 - 1] = PARM.ST[L1 - 1] + XX;
                    PARM.PKRZ[L1 - 1] = Math.Max(0.0, PARM.PKRZ[L1 - 1] - XX);
                    PARM.ST[PARM.ISL - 1] = PARM.PO[PARM.ISL - 1];
                }
                
                XX = PARM.ST[PARM.ISL - 1] - PARM.FC[PARM.ISL - 1];
                if (XX <= 0.0)
                    continue;
	            double WP1 = Math.Log10(PARM.S15[L1 - 1]);
                double FC1 = Math.Log10(PARM.FC[L1 - 1]);

                double T1;
                if (PARM.ST[L1 - 1] > .01)
                {
	                T1 = Math.Pow(10.0, (3.1761 - 1.6576 * ((Math.Log10(PARM.ST[L1 - 1]) - WP1)/(FC1-WP1))));
	                if(T1 < 33.0)
                        continue;
	            }else{
	                T1 = 1500.0;
                }
                
                double WP2 = Math.Log10(PARM.S15[PARM.ISL - 1]);
                double FC2 = Math.Log10(PARM.FC[PARM.ISL - 1]);
                double T2 = Math.Pow(10.0, (3.1761 - 1.6576 * (Math.Log10(PARM.ST[PARM.ISL - 1]) - WP2) / (FC2 - WP2)));

                if (T1 < T2)
                    continue;

	            double X1 = XX * Math.Min(.5,(T1-T2)/T1);
                PARM.ST[L1 - 1] = PARM.ST[L1 - 1] + X1;
                PARM.PKRZ[L1 - 1] = PARM.PKRZ[L1 - 1] - X1;
                PARM.ST[PARM.ISL - 1] = PARM.ST[PARM.ISL - 1] - X1;
            }

            if (PARM.PKRZ[L1 - 1] < 0.0)
                PARM.PKRZ[L1 - 1] = 0.0;

        }
    }
}