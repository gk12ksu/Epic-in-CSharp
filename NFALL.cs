using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program simulates the conversion of standing dead crop
     * residue to flat residue.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public partial class Functions
    {
        public static void NFALL(ref double ZZ)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double SUM = 0.0;
            double TOT = 0.0;
            double ZS;
            for (int K = 1; K <= PARM.LC; K++)
            {
                if (PARM.STD[K - 1] < .001)
                    continue;
                double X1 = ZZ * PARM.STD[K - 1];
                PARM.STD[K - 1] = PARM.STD[K - 1] - X1;
                SUM = SUM + X1;
                double X2 = ZZ * PARM.STDN[K - 1];
                PARM.STDN[K - 1] = PARM.STDN[K - 1] - X2;
                TOT = TOT + X2;
                ZS = ZZ * PARM.STDP;
                PARM.FOP[PARM.LD1 - 1] = PARM.FOP[PARM.LD1 - 1] + ZS;
                PARM.STDP = PARM.STDP - ZS;
                ZS = ZZ * PARM.STDK;
                PARM.SOLK[PARM.LD1 - 1] = PARM.SOLK[PARM.LD1 - 1] + ZS;
                PARM.STDK = Math.Max(1.0E-5, PARM.STDK - ZS);
                ZS = ZZ * PARM.STDL;
                PARM.STDL = PARM.STDL - ZS;
            }

            ZZ = Math.Min(1.0, ZZ * 10.0);
            ZS = ZZ * PARM.STDO;
            PARM.STDO = Math.Max(1.0E-5, PARM.STDO - ZS);
            SUM = SUM + ZS;
            ZS = ZZ * PARM.STDON;
            TOT = TOT + ZS;

            Functions.NCNSTD(SUM, TOT, 0);

            PARM.STDON = Math.Max(1.0E-5, PARM.STDON - ZS);
            ZS = ZZ * PARM.STDOP;
            PARM.FOP[PARM.LD1 - 1] = PARM.FOP[PARM.LD1 - 1] + ZS;
            PARM.STDOP = Math.Max(1.0E-5, PARM.STDOP - ZS);
            ZS = ZZ * PARM.STDOK;
            PARM.SOLK[PARM.LD1 - 1] = PARM.SOLK[PARM.LD1 - 1] + ZS;
            PARM.STDOK = Math.Max(1.0E-5, PARM.STDOK - ZS);

        }
    }
}