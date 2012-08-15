using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This function calculates gas concentrations in liquid and
     * air phases.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/8/2012
     */
    public class NCCONC
    {
        public NCCONC()
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            for (int J = 1; J < PARM.NBCL; J++)
            {
                //CLO2=CONC GAS IN LIQ PHASE (G/M3 WATER)
                PARM.CLO2[J - 1] = PARM.AO2C[J - 1] / (PARM.AFP[J - 1] * PARM.HKPO[J - 1] + PARM.VWC[J - 1]);   
                //CGO2=CONC GAS IN GAS PHASE (G/M3 AIR)
                PARM.CGO2[J - 1] = PARM.AO2C[J - 1] / (PARM.TPOR[J - 1] + PARM.VWC[J - 1] * (1.0 / PARM.HKPO[J - 1] - 1.0));
                //CLCO2=CONC GAS IN LIQ PHASE
                PARM.CLCO2[J - 1] = PARM.ACO2C[J - 1] / (PARM.AFP[J - 1] * PARM.HKPC[J - 1] + PARM.VWC[J - 1]);   
                //CGCO2=CONC GAS IN GAS PHASE
                PARM.CGCO2[J - 1] = PARM.ACO2C[J - 1] / (PARM.TPOR[J - 1] + PARM.VWC[J - 1] * (1.0 / PARM.HKPC[J - 1] - 1.0));
                //CLN2O=CONC GAS IN LIQ PHASE
	            PARM.CLN2O[J - 1] = PARM.AN2OC[J - 1] / (PARM.AFP[J - 1] * PARM.HKPN[J - 1] + PARM.VWC[J - 1]); 
                //CGN2O=CONC GAS IN GAS PHASE
                PARM.CGN2O[J - 1] = Math.Max(Math.Pow(10, -10), PARM.AN2OC[J - 1] / (PARM.TPOR[J - 1] + PARM.VWC[J - 1] * (1.0 / PARM.HKPN[J - 1] - 1.0)));
            }


        }
    }
}