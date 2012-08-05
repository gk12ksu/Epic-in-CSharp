using System;

namespace Epic
{
    public class WRWD
    {
        public WRWD(ref double IEX)
        {
            //EPICv0810
            //Translated by Emily Jordan
            //No description for what this file does was availabe.

            //The fortran files uses global variables, and needs to be fixed
            //for c# style coding.

            //PARAM file
            Epic.MODPARAM PARM  =  Epic.MODPARAM.Instance;
            if(IEX == 0)
            {
                double RN = 1.0 - Epic.AUNIF(PARM.IDG[0]);
                if(RN > PARM.PRWM + .001)
                {
                    PARM.RFV = 0;
                    PARM.LW = 1;
                    return;
                }
            }
              
            double V4 = Epic.AUNIF(PARM.IDG[2]);
            if(PARM.ICDP == 0)
            {
                double R6 = PARM.RFSK / 6.0;
                double ZZ = Epic.ADSTN(PARM.V3, V4);
                PARM.RFV = Epic.WRAIN(R6, ZZ, PARM.RFSD, PARM.RFSK, PARM.RFVM) * PCF[PARM.NWI - 1, PARM.MO - 1];
                PARM.V3 = V4;
            }
            else PARM.RFV = PARM.RFVM * Math.Pow((-Math.Log(V4)), PARM.EXPK);

            PARM.LW = 2;
            return;




        }
    }
}