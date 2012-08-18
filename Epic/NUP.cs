using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program calculates the daily N demand for optimal plant
     * growth.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public partial class Functions
    {
        public static void NUP()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            double CNT;
            if (PARM.NUPC == 0)
            {
                CNT = PARM.BN[1, PARM.JJK - 1] + PARM.BN[0, PARM.JJK - 1] * Math.Exp(-PARM.BN[3, PARM.JJK - 1] * PARM.HUI[PARM.JJK - 1]);
	        }else{
                CNT= (PARM.BN[3, PARM.JJK - 1] - PARM.BN[2, PARM.JJK - 1]) * (1.0 - PARM.HUI[PARM.JJK - 1] / (PARM.HUI[PARM.JJK - 1] + Math.Exp(PARM.BN[0, PARM.JJK - 1] - PARM.BN[1, PARM.JJK - 1] * PARM.HUI[PARM.JJK - 1]))) + PARM.BN[2, PARM.JJK - 1];
	        }
            PARM.UN2[PARM.JJK - 1] = CNT * PARM.DM[PARM.JJK - 1] * 1000.0;
            PARM.UNO3 = Math.Min(4000.0 * PARM.BN[2, PARM.JJK - 1] * PARM.DDM[PARM.JJK - 1], PARM.UN2[PARM.JJK - 1] - PARM.UN1[PARM.JJK - 1]);
            PARM.UNO3 = Math.Max(0.0, PARM.UNO3);

        }
    }
}