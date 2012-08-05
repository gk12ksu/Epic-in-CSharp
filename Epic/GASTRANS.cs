using System;

namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program solves the gas transport equation.
     * 
     * This file has had its array indicies shifted for C#
     * Last Modified On 7/6/2012
     */
    public class GASTRANS
    {

        public GASTRANS(ref double[] CONC, ref double[] DPRM, ref double CUP, ref double CLO, ref int NGS, ref double DSURF)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;
            double[] A = new double[100], B = new double[100], C = new double[100], D = new double[100];// CONC = new double[30], DPRM = new double[30]; Believe to be not needed, think its to define the dimension of the passed in vars
            double ALX = 0.0;

            double R = PARM.DTG/(PARM.DZ*PARM.DZ);
            double R1 = ALX*R;
            double R2 = (1.0 - ALX)*R;
            double R3 = 1.0 + 2.0 * (1.0 - ALX) * R;
            double TGO2 = 0.0;
            double TGCO2 = 0.0;
            double TGN2O = 0.0;

            //UPPER BOUNDARY CONDITION
            A[0] = -R2 * DPRM[0];
            B[0] = 0.0;
            C[0] = CONC[0] + R1 * (DPRM[0] * (CONC[1] - CONC[0]) - DSURF * (CONC[0] - CUP)) + R2 * DSURF * CUP;
            D[0] = 1.0 + 0.5 * R3 * (DSURF + DPRM[0]);

            //MAIN COMPUTATIONS
            for (int ID = 2; ID < PARM.IUN; ID++){
                A[ID - 1] = -R2 * DPRM[ID - 1];
                B[ID - 1] = -R2 * DPRM[ID - 2];
                C[ID - 1] = CONC[ID - 1] + R1 * (DPRM[ID - 1] * (CONC[ID] - CONC[ID - 1]) - DPRM[ID - 2] * (CONC[ID - 1]-CONC[ID - 2]));
                D[ID - 1] = 1.0 + 0.5 * R3 * (DPRM[ID - 2] + DPRM[ID - 1]);
            }

            //LOWER BOUNDARY CONDITION
            A[PARM.NBCL - 1] = 0.0;
            B[PARM.NBCL - 1] = -R2 * DPRM[PARM.IUN - 1];
            C[PARM.NBCL - 1] = CONC[PARM.NBCL - 1] + R1 * (DPRM[PARM.NBCL - 1] * (CLO - CONC[PARM.NBCL - 1]) - DPRM[PARM.NBCL - 2] * (CONC[PARM.NBCL - 1] - CONC[PARM.IUN - 1])) + R2 * DPRM[PARM.NBCL - 1] * CLO;
            D[PARM.NBCL - 1] = 1.0 + 0.5 * R3 * (DPRM[PARM.IUN - 1] + DPRM[PARM.NBCL - 1]);

            //SOLVE TRIADIAGONAL SYSTEM
            Epic.TRIDIAG(B, D, A, C, PARM.NBCL);
            for (int I = 1; I < PARM.NBCL; I++)
            {
                CONC[I - 1] = Math.Max(Math.Pow(10, -10), C[I - 1]);
                if (NGS == 1){
                    PARM.CGO2[I - 1] = CONC[I - 1];
                    continue;
                }

                if (NGS == 2){
                    PARM.CGCO2[I - 1] = CONC[I - 1];
                }else{
                    PARM.CGN2O[I - 1] = CONC[I - 1];
                }
            }

            double X1 = (DPRM[0] / PARM.DZ) * (CUP - CONC[0]) * PARM.AFP[0];
            switch (NGS){
                case 1: PARM.GFO2 = X1;
                    break;
                case 2: PARM.GFCO2 = X1;
                    break;
                case 3: PARM.GFN2O = X1;
                    break;
            }

            return;
        }
    }
}