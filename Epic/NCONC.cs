using System;
namespace Epic
{
    /*
     * EPICv0810
     * Translated by Brian Dye
     * This program computes parameters of an equation describing the
     * N and P relations to biomass accumulation.
     * 
     * This file has had its array indicies shifted for C#
     * This file has had its go to statements removed
     * Last Modified On 7/8/2012
     */
    public class NCONC
    {
        public NCONC(ref double P0, ref double P5, ref double P1, ref double A)
        {
            Epic.MODPARAM PARM = Epic.MODPARAM.Instance;

            A = 5.0;
            double EA = 0.0, EA1 = 0.0;
            bool CONVERGED = false;
            for (int I = 1; I < 10; I++)
            {
                double A5 = A * .5;
                EA = Math.Exp(A);
                EA1 = EA - 1.0;
                double EG = Math.Exp(-A5);
                double P0G = P0 * EG;
                double EG1 = Math.Exp(A5);
                double PEG = P1 * (EA - EG1);
                double P01 = P0 * (1.0 - EG);
                double X1 = PEG - P01;
                double PG5 = .5 * P0G;
                double FU = X1 / EA1 + P0G - P5;
                if (Math.Abs(FU) < Math.Pow(10, -7))
                {
                    CONVERGED = true;
                    break;
                }
                double DFDA = (EA1 * (P1 * (EA - .5 * EG1) - PG5) - EA * X1) / (EA1 * EA1) - PG5;
                A = A - FU / DFDA;
            }

            if (CONVERGED == false)
            {
                //This program writes to file KW(1) in the source
                //file.Write("\n\n          NCONC DID NOT CONVERGE " + A + " " + FU + "\n);
            }

            P5 = (P1 * EA - P0) / EA1;
            P0 = P0 - P5;
            return;

        }
    }
}