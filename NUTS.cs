using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * this program calculates the plant stress factor caused by limited
     * supply of N or P.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/8/2012
     */
    public partial class Functions
    {
        public static void NUTS(ref double U1, ref double U2, ref double UU)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            UU = 200.0 * (U1 / U2 - .5);
            if (UU > 0.0)
            {
                UU = UU / (UU + Math.Exp(PARM.SCRP[7, 0] - PARM.SCRP[7, 1] * UU));
            }else{
                UU = 0.0;
            }

        }
    }
}