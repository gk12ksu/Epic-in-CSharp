using System;

namespace Epic
{
    public class PSTEV
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public PSTEV()
        {
            // EPIC0810
            // Translated by Paul Cain
            // THIS SUBPROGRAM ESTIMATES UPWARD NO3 MOVEMENT CAUSED BY SOIL EVAPO

            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            //Translator's Note: For now, I kept all of the array indices as 
            //	the same numbers they were in their original fortran files  
            //	even though C# use base 0 arrays and fortran use base 1 
            //	arrays. I did this to avoid confusion until we have a better
            // 	understanding of how the program works. 

            // USE PARM
            if (PARM.NEV == 1)
            {
                return;
            }
            double SUM = 0.0;
            for (int J = PARM.NEV; J >= 2; J--)
            {
                PARM.ISL = PARM.LID[J];
                if (PARM.WNO3[PARM.ISL] < 0.001)
                {
                    continue;
                }
                SUM = SUM + PARM.SEV[PARM.ISL];
                if (SUM <= 0.0)
                {
                    continue;
                }
                double XX = SUM * PARM.WNO3[PARM.ISL] / (PARM.ST[PARM.ISL] + SUM);
                PARM.SSFN = PARM.SSFN + XX;
                PARM.WNO3[PARM.ISL] = PARM.WNO3[PARM.ISL] - XX;
                SUM = 0.0;
            }
            PARM.WNO3[PARM.LD1] = PARM.WNO3[PARM.LD1] + PARM.SSFN;
        }
    }
}

