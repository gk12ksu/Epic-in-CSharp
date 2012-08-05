using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program simulates mineralization of P using parpan eqs.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/11/2012
     */
    public class NPMN
    {
        public NPMN(ref double CS)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double HMP = PARM.DHN[PARM.ISL - 1] * PARM.WP[PARM.ISL - 1] / (PARM.WHSN[PARM.ISL - 1] + PARM.WHPN[PARM.ISL - 1]);
            double TKG = PARM.RSD[PARM.ISL - 1] * 1000.0;
            double R4 = .58 * TKG;
            double X1 = Math.Max(1.0, PARM.FOP[PARM.ISL - 1] + PARM.AP[PARM.ISL - 1]);
            double CPR = Math.Min(2000.0, R4 / X1);
            double CPRF = Math.Exp(-.693 * (CPR - 200.0) / 200.0);
            double DECR = Math.Max(.01, .05 * CPRF * CS);
            double RMP = DECR * PARM.FOP[PARM.ISL - 1];
            if (PARM.FOP[PARM.ISL - 1] > 1.0E-5)
            {
                PARM.FOP[PARM.ISL - 1] = PARM.FOP[PARM.ISL - 1] - RMP;
            }
            else
            {
                RMP = PARM.FOP[PARM.ISL - 1];
                PARM.FOP[PARM.ISL - 1] = 0.0;
            }
            PARM.WMP = .8 * RMP + HMP;
            X1 = PARM.AP[PARM.ISL - 1] + PARM.WMP;
            if (X1 < 0.0)
            {
                PARM.WMP = PARM.AP[PARM.ISL - 1];
                HMP = PARM.WMP - .8 * RMP;
                PARM.AP[PARM.ISL - 1] = 1.0E-5;
            }
            else
            {
                PARM.AP[PARM.ISL - 1] = PARM.AP[PARM.ISL - 1] + PARM.WMP;
            }
            PARM.WP[PARM.ISL - 1] = PARM.WP[PARM.ISL - 1] - HMP + .2 * RMP;


        }
    }
}