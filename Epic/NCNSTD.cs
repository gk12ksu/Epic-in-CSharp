using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program removes C and N from standing residue and adds it
     * to the top soil layer as a result of a tillage operation.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/8/2012
     */

    /* ADDITIONAL CHANGE
     * 7/30/2012    Modified by Paul Cain, changing the class name from 
     *              NCCONC to NCNSTD to prevent build errors
     */
    public class NCNSTD
    {
        public NCNSTD(ref double X1, ref double X2, double LCD)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            int JSL;
            if (LCD > 0)
            {
                JSL = PARM.ISL;
            }
            else
            {
                JSL = PARM.LD1;
            }

/* Commented out code in original source
!     XZ=WNO3(JSL)+WNH3(JSL)
!     AD1=WLSN(JSL)+WLMN(JSL)+WBMN(JSL)+WHSN(JSL)+WHPN(JSL)+XZ+X2
 */

            double RLN = 1000.0 * PARM.STDL / (PARM.STDN[PARM.JJK - 1] + Math.Pow(10, -5));
            double RLR = Math.Min(.8, PARM.STDL / (PARM.STD[PARM.JJK - 1] + Math.Pow(10, -5)));
            double X7 = 1000.0 * X1;
            double C7 = .42 * X7;
            PARM.SMS[6, JSL - 1] = PARM.SMS[6, JSL - 1] + C7;
            PARM.SMM[72, PARM.MO - 1] = PARM.SMM[72, PARM.MO - 1] + C7;
            PARM.VAR[72] = PARM.VAR[72] + C7;
            PARM.RCN = C7 / (X2 + Math.Pow(10, -5));
            double X8 = .85 - .018 * RLN;

            if (X8 < .01)
            {
                X8 = .01;
            }
            else
            {
                if (X8 < .7) X8 = .7;
            }

            double XX = X7 * X8;
            PARM.WLM[JSL - 1] = PARM.WLM[JSL - 1] + XX;
            double XZ = X7 - XX;
            PARM.WLS[JSL - 1] = PARM.WLS[JSL - 1] + XZ;
            PARM.WLSL[JSL - 1] = PARM.WLSL[JSL - 1] + XZ * RLR;
            double X6 = X2;
	        PARM.SMM[85, PARM.MO - 1] = PARM.SMM[85, PARM.MO - 1] + X6;
            double XY = .42 * XZ;
            PARM.WLSC[JSL - 1] = PARM.WLSC[JSL - 1] + XY;
            PARM.WLSLC[JSL - 1] = PARM.WLSLC[JSL - 1] + XY * RLR;
            PARM.WLSLNC[JSL - 1] = PARM.WLSC[JSL - 1] - PARM.WLSLC[JSL - 1];
            double X3 = Math.Min(X6, XY / 150.0);
	        PARM.WLSN[JSL - 1] = PARM.WLSN[JSL - 1] + X3;
            PARM.WLMC[JSL - 1] = PARM.WLMC[JSL - 1] + .42 * XX;
            PARM.WLMN[JSL - 1] = PARM.WLMN[JSL - 1] + X6 - X3;
            PARM.RSD[JSL - 1] = .001 * (PARM.WLS[JSL - 1] + PARM.WLM[JSL - 1]);

/* Commented out code in original source
!     XZ=WNO3(JSL)+WNH3(JSL)
!     AD2=WLSN(JSL)+WLMN(JSL)+WBMN(JSL)+WHSN(JSL)+WHPN(JSL)+XZ
!     DF=AD2-AD1
!     IF(ABS(DF)>.001)WRITE(KW(1),1)IY,MO,KDA,AD1,AD2,DF
!   1 FORMAT(1X,'NCNSTD',3I4,3E16.6)     
*/

        }
    }
}