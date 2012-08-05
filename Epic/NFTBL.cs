using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program reads fertilizer table to determine parameters of
     * input fertilizer
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/10/2012
     */
    public class NFTBL
    {
        public NFTBL(ref int L)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            double[] XTP = new double[11];

            if (PARM.NDF > 0)
            {
                for (L = 1; L < PARM.NDF; L++)
                {
                    if (PARM.KDF[L - 1] == PARM.JX[6])
                        return;
                }
            }
            PARM.NDF = PARM.NDF + 1;
            PARM.KDF[PARM.NDF - 1] = PARM.JX[6];
            PARM.KDF1[PARM.JX[6] - 1] = PARM.NDF;
/*
!     READ FERTILIZER TABLE
!  1  FTNM = FERTILIZER NAME
!  2  FN   = MINERAL N FRACTION
!  3  FP   = MINERAL P FRACTION
!  4  FK   = MINERAL K FRACTION
!  5  FNO  = ORGANIC N FRACTION
!  6  FPO  = ORGANIC P FRACTION
!  7  FNH3 = AMMONIA N FRACTION(FNH3/FN)
!  8  FOC  = ORGANIC C FRACTION
!  9  FSLT = SALT FRACTION
! 10  FCST = COST OF FERTILIZER($/KG)
! 11  FCEM = C EMITTED/UNIT OF FERTILIZER(KG/KG)
*/
            int I = -1;
            /* Reads file KR(9)
            while (I != PARM.JX[6])
            {  
                String info = file.ReadLine();
                String[] stats = info.Split(new char[] { ' ' });
                if (stats.Length < 11)
                {
                    if (PARM.IBAT == 0)
                    {
                        Console.WriteLine("FERT NO = " + PARM.JX[6] + " NOT IN FERT LIST FILE");
                    }
                    else
                    {
                        Console.WriteLine("!!!!! " + PARM.ASTN + " FERT NO = " + PARM.JX[6] + " NOT IN FERT LIST FILE");
                    }
                    Environment.Exit(-1);
                }
                else
                {
                    I = Int16.Parse(stats[0]);
                    PARM.FTNM[PARM.NDF - 1] = stats[1];
                    for (int K = 2; K < 11; K++)
                    {
                        XTP[K - 1] = double.Parse(stats[K - 1]);
                    }
                }
            }
             */
            PARM.FN[PARM.NDF - 1] = XTP[1];
            PARM.FP[PARM.NDF - 1] = XTP[2];
            PARM.FK[PARM.NDF - 1] = XTP[3];
            PARM.FNO[PARM.NDF - 1] = XTP[4];
            PARM.FPO[PARM.NDF - 1] = XTP[5];
            PARM.FNH3[PARM.NDF - 1] = XTP[6];
            PARM.FOC[PARM.NDF - 1] = XTP[7];
            PARM.FSLT[PARM.NDF - 1] = XTP[8];
            PARM.FCST[PARM.NDF - 1] = XTP[9];
            PARM.FCEM[PARM.NDF - 1] = XTP[10];
            //REWIND file file KR(9)
          
        }
    }
}