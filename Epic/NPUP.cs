using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program calculates the daily P demand for optimal plant
     * growth.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public class NPUP
    {
        public NPUP()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            double CPT;
            if (PARM.NUPC == 0)
            {
	            CPT = PARM.BP[1, PARM.JJK - 1] + PARM.BP[0, PARM.JJK - 1] * Math.Exp(-PARM.BP[3, PARM.JJK - 1] * PARM.HUI[PARM.JJK - 1]);
	        }else{
                CPT = (PARM.BP[3, PARM.JJK - 1] - PARM.BP[2, PARM.JJK - 1]) * (1.0 - PARM.HUI[PARM.JJK - 1] / (PARM.HUI[PARM.JJK - 1] + Math.Exp(PARM.BP[0, PARM.JJK - 1] - PARM.BP[1, PARM.JJK - 1] * PARM.HUI[PARM.JJK - 1]))) + PARM.BP[2, PARM.JJK - 1];
            }
	        PARM.UP2[PARM.JJK - 1] = CPT * PARM.DM[PARM.JJK - 1] * 1000.0;
            PARM.UPP = Math.Min(4000.0 * PARM.BP[2, PARM.JJK - 1] * PARM.DDM[PARM.JJK - 1], PARM.UP2[PARM.JJK - 1] - PARM.UP1[PARM.JJK - 1]);
            PARM.UPP = Math.Max(0.0, PARM.UPP - 1);


        }
    }
}