using System;

namespace Epic
{
	public class PESTF
	{
        private static MODPARAM PARM = MODPARAM.Instance;

		public PESTF ()
		{
			// EPIC0810
			// Translated by Paul Cain
			// THIS SUBPROGRAM CALCULATES THE PEST FACTOR BASED ON THE MINIMUN
			// PEST FACTOR (PST(JJK)) FOR CROP JJK AND THE SUM OF DAILY PEST
			// DAMAGE(PSTS)
			
			// The fortran file uses global variables, refer to MODPARAM.cs for
			// a list of all global variables
			
			//Translator's Note: For now, I kept all of the array indices as 
			//	the same numbers they were in their original fortran files  
			//	even though C# use base 0 arrays and fortran use base 1 
			//	arrays. I did this to avoid confusion until we have a better
			// 	understanding of how the program works. 
			
			// USE PARM
            if (PARM.PSTS > 0.0)
            {
                double X1 = PARM.PSTX * PARM.PSTS / PARM.IPST;
                PARM.PSTF[PARM.JJK] = 1.0 - (1.0 - PARM.PST[PARM.JJK]) * X1 / (X1 + Math.Pow(Math.E, PARM.SCRP[9, 1] - PARM.SCRP[9, 2] * X1));
			}
			else{
                PARM.PSTF[PARM.JJK] = 1.0;
			}
		}
	}
}

