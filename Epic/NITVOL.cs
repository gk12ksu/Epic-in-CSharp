using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program simulates the transformation from NH3 to NO3, and
     * the volitilzation of NH3 using modified methods of reddy and of
     * the ceres model.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public class NITVOL
    {
        public NITVOL(ref double Z5)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
//!     AD1=WNO3(PARM.ISL)+WNH3(PARM.ISL)

            double X1 = .41 * ((PARM.STMP[PARM.ISL - 1] - 5.0) / 10.0);
            if (X1 <= 0.0)
                return;
            double AKAV, FPH;
            if (PARM.ISL == PARM.LD1)
            {
                double FAF = .335 + .16 * Math.Log(PARM.U10 + .2);
                AKAV = X1 * FAF;
            }else{
                double FCEC = Math.Max(.3, 1.0 - .038 * PARM.CEC[PARM.ISL - 1]);
                double FZ = 1.0 - Z5 / (Z5 + Math.Exp(PARM.SCRP[11, 0] - PARM.SCRP[11, 1] * Z5));
                AKAV = X1 * FCEC * FZ;
            }
            if (PARM.PH[PARM.ISL - 1] > 7.0)
            {
                if (PARM.PH[PARM.ISL - 1] > 7.4)
                {
                    FPH = 5.367 - .599 * PARM.PH[PARM.ISL - 1];
                }else{
                    FPH = 1.0;
                }
            }else{
                FPH = .307 * PARM.PH[PARM.ISL - 1] - 1.269;
            }
            double AKAN = X1 * PARM.SUT * FPH;
            AKAV = AKAV * PARM.SUT;
            double XX = AKAV + AKAN;
            if (XX < 1.0E-5) 
                return;

            double F = Math.Min(PARM.PRMT[63], 1.0 - Math.Exp(-XX));
            X1 = F * PARM.WNH3[PARM.ISL - 1];
            double X2 = 1.0 - Z5 / (Z5 + Math.Exp(5.0 - .05 * Z5));
            double AVOL = X1 * PARM.PRMT[56] * X2;
            double RNIT = X1 - AVOL;
            PARM.WNH3[PARM.ISL - 1] = PARM.WNH3[PARM.ISL - 1] - AVOL - RNIT;
            PARM.SVOL = PARM.SVOL + AVOL;
            PARM.WNO3[PARM.ISL - 1] = PARM.WNO3[PARM.ISL - 1] + RNIT;
            PARM.SNIT = PARM.SNIT + RNIT;
            double XZ = PARM.WNO3[PARM.ISL - 1] + PARM.WNH3[PARM.ISL - 1]; // This line does nothing?
/*
!     AD2=XZ+AVOL
!     DF=AD2-AD1
!     IF(ABS(DF)>.001)WRITE(KW(1),1)IY,MO,KDA,AD1,AD2,DF
!   1 FORMAT(1X,'NITVOL',3I4,3E16.6)     
 */
        }
    }
}