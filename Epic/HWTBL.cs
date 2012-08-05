using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program simulates water table dynamics as a function
     * of rain and evap.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/7/2012
     */
    public class HWTBL
    {
        public HWTBL()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double RTO = 100.0 * PARM.GWST / PARM.GWMX;
            PARM.WTBL = PARM.WTMX + (PARM.WTMN - PARM.WTMX) * RTO / (RTO + Math.Exp(PARM.SCRP[18,0] - PARM.SCRP[18,1] * RTO));
            double SUM = 0.0;
            double TOT = 0.0;
            double XX = 0.0;

            if (PARM.WTBL <= PARM.Z[PARM.LID[PARM.NBSL - 1] - 1])
            {
                double NN = 0.0;

                for (int K = 1; K < PARM.NBSL; K++)
                {
                    PARM.ISL = PARM.LID[K - 1];
                    SUM = SUM + PARM.ST[PARM.ISL - 1];

                    if (PARM.WTBL <= PARM.Z[PARM.ISL - 1])
                    {
                        if (NN > 0)
                        {
                            PARM.ST[PARM.ISL - 1] = PARM.PO[PARM.ISL - 1];
                        }else{
                            NN = 1;
                            if (PARM.ST[PARM.ISL - 1] > PARM.FC[PARM.ISL - 1])
                                PARM.ST[PARM.ISL - 1] = PARM.FC[PARM.ISL - 1];
                            PARM.ST[PARM.ISL - 1] = (PARM.ST[PARM.ISL - 1] * (PARM.WTBL - XX) + PARM.PO[PARM.ISL - 1] * (PARM.Z[PARM.ISL - 1] - PARM.WTBL)) / (PARM.Z[PARM.ISL - 1] - XX);
                        }
                    }
                    TOT = TOT + PARM.ST[PARM.ISL - 1];
                    XX = PARM.Z[PARM.ISL - 1];
                }
            }
            XX = TOT - SUM;
            PARM.QIN[PARM.MO - 1] = PARM.QIN[PARM.MO - 1] + XX;
            PARM.SMM[19, PARM.MO - 1] = PARM.SMM[19, PARM.MO - 1] + XX;
            PARM.VAR[19] = XX;
        }
    }
}