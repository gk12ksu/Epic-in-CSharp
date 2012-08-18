using System;

namespace Epic
{
    public partial class Functions
    {
        private static MODPARAM PARM = MODPARAM.Instance;

        public static void INIFP(int I3, int II, int JJ, int JRT)
        {
            // EPIC0810
            // Translated by Paul Cain
            // THIS SUBPROGRAM ALLOWS INPUT TO OPERATION SCHEDULE FOR IRRIGATION,
            // FERTILIZER, OR PESTICIDE APPLICATION

            // The fortran file uses global variables, refer to MODPARAM.cs for
            // a list of all global variables

            //Translator's Note: For now, I kept all of the array indices as 
            //	the same numbers they were in their original fortran files  
            //	even though C# use base 0 arrays and fortran use base 1 
            //	arrays. I did this to avoid confusion until we have a better
            // 	understanding of how the program works.

            /* ADDITIONAL CHANGE
           * 8/17/2012    Modified by Paul Cain to make it part of the Functions partial class
           */

            int I1 = I3 - 6;

            switch (I1)
            {
                case 1:
                    PARM.PSTE[II, JJ] = PARM.OPV[1];
                    PARM.PSTR[II, JJ] = PARM.OPV[2];
                    new PSTTBL(); //Call the PSTTBL subroutine
                    PARM.LPC[II, JJ] = PARM.KDP1[PARM.JX[7]];
                    PARM.KP = PARM.KP + 1;
                    break;
                case 2:
                    PARM.VIRR[II, JJ] = PARM.OPV[1];
                    if (PARM.OPV[4] > 0.0)
                    {
                        PARM.EFI = PARM.OPV[4];
                    }
                    PARM.KI = PARM.KI + 1;
                    break;
                case 3:
                    PARM.WFA[II, JJ] = PARM.OPV[1];
                    new NFTBL(L); //Call the NFTBL subroutine
                    PARM.LFT[II, JJ] = PARM.KDF1[PARM.JX[7]];
                    PARM.KF = PARM.KF + 1;
                    break;
                case 13:
                    PARM.RSTK[II, JJ] = PARM.OPV[1];
                    break;
                default:
                    if (PARM.OPV[2] < 0.0)
                    {
                        PARM.CN2 = (PARM.OPV[2] * -1);
                    }
                    else if (PARM.OPV[2] > 0.0)
                    {
                        PARM.LUN =  (int)PARM.OPV[2];
                        new HSGCN(); //Call the HSGCN subroutine
                    }
                    PARM.CND[II, JJ] = PARM.CN2;
                    if (Math.Abs(PARM.OPV[3]) > (1 * Math.Pow(10, -5)))
                    {
                        PARM.BIR = PARM.OPV[3];
                    }
                    PARM.TIR[II, JJ] = PARM.BIR;
                    if (PARM.OPV[4] > 0.0)
                    {
                        PARM.EFI = PARM.OPV[4];
                    }
                    PARM.QIR[II, JJ] = PARM.EFI;
                    if (PARM.OPV[8] > 0.0)
                    {
                        PARM.CFMN = PARM.OPV[8];
                    }
                    PARM.CFRT[II, JJ] = PARM.CFMN;
                    if (PARM.OPV[9] > 0.0)
                    {
                        PARM.HWC[II, JJ] = PARM.OPV[9];
                    }
                    JRT = 0;
                    return;
            }
            PARM.TIR[II, JJ] = PARM.BIR;
            PARM.CND[II, JJ] = PARM.CN2;
            PARM.QIR[II, JJ] = PARM.EFI;
            JRT = 1;
        }
    }
}