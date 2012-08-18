using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program calculates the daily K demand for optimal plant
     * growth.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public partial class Functions
    {
        public static void NUK()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double CKT = PARM.BK[0, PARM.JJK - 1] + PARM.HUI[PARM.JJK - 1] * (PARM.BK[1, PARM.JJK - 1] + PARM.HUI[PARM.JJK - 1] * PARM.BK[3, PARM.JJK - 1]);
            PARM.UK2[PARM.JJK - 1] = CKT * PARM.DM[PARM.JJK - 1] * 1000.0;
            if (PARM.UK2[PARM.JJK - 1] < PARM.UK1[PARM.JJK - 1])
                PARM.UK2[PARM.JJK - 1] = PARM.UK1[PARM.JJK - 1];
            PARM.UPK = Math.Min(4000.0 * PARM.BK[2, PARM.JJK - 1] * PARM.DDM[PARM.JJK - 1], PARM.UK2[PARM.JJK - 1] - PARM.UK1[PARM.JJK - 1]);
        }
    }
}