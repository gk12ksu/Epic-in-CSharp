using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program estimates aluminum saturation using base
     * saturation, organic C, and PH.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public class NLIMA
    {
        public NLIMA(ref double SB, double DSB, ref double C1, ref double PH, ref double ALS, ref double OC, ref double BSA)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            SB = SB - DSB;
            if (SB < .02)
                SB = .02;
            BSA = C1 * SB;
            if (PH <= 5.6)
            {
                ALS = 154.2 - 1.017 * BSA - 3.173 * OC - 14.23 * PH;
                    if (ALS < .01)
                    {
                        ALS = 0.0;
                        return;
                    }else{
                        if (ALS > 95.0) 
                            ALS = 95.0;
                    }
            }else{
                ALS = 0.0;
            } 

        }
    }
}